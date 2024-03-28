import discord
from discord.ext import commands, tasks
import youtube_dl
import requests
from pytube import Playlist

#tells Discord API what the bot intends to do
intents = discord.Intents.all()
intents.typing = False
intents.presences = False

#get token from token file
f = open("token.txt", "r")
token = f.read()
f.close()

#configs to download and play audio
FFMPEG_OPTIONS = {'before_options': '-reconnect 1 -reconnect_streamed 1 -reconnect_delay_max 5', 'options': '-vn'}
YDL_OPTIONS = {'format':"bestaudio"}

#initiate an empty dictionary to store a queue for each server or "guild"
request_queue = {}

#create a bot/client with command prefix of '!'
client = commands.Bot(command_prefix = '!', intents=intents)

#function for getting the titles from the queue of urls
def printQueue(queue):
    returnString = ""
    for i, url in enumerate(queue):
        r = requests.get("https://noembed.com/embed?dataType=json&url=" + url)
        data = r.json()
        returnString += str(i + 1) + ". " + str(data['title']) + "\n"
        if i >= 9:
            break
    return returnString
   

#makes the bot join the user is in if the user is in a voice channel
@client.command()
async def join(ctx):
    if ctx.author.voice is None:
        await ctx.send("Not in a voice channel.")
    voice_channel = ctx.author.voice.channel
    if ctx.voice_client is None:
        await voice_channel.connect()
    else:
        await ctx.voice_client.move_to(voice_channel)

#runs join command.
#If there is already media playing, it gets added to queue otherwise it plays
@client.command()
async def play(ctx, url):
    if ctx.author.voice is None:
        await ctx.send("Not in a voice channel.")
    voice_channel = ctx.author.voice.channel
    if ctx.voice_client is None:
        await voice_channel.connect()
    else:
        await ctx.voice_client.move_to(voice_channel)
    #adds media to queue if the bot is already playing
    #the queue uses guild id to create seperate queues for different guilds
    #handles playlists by adding every song from the list to the queue
    if "playlist?" in url: #playlist
        pl = Playlist(url)
        urls = []
        for link in pl:
            urls.append(link)
        if ctx.voice_client.is_playing():
            if ctx.guild.id in request_queue:
                request_queue[ctx.guild.id].extend(urls)
            else:
                request_queue[ctx.guild.id] = urls
            await ctx.send("Added " + str(len(urls)) + " to queue.\nMedia in queue:\n" + printQueue(request_queue[ctx.guild.id]))
            return
        else:
            url = urls.pop(0)
            vc  = ctx.voice_client
            with youtube_dl.YoutubeDL(YDL_OPTIONS) as ydl:
                try:
                    info = ydl.extract_info(url, download = False)
                    song = info["url"]
                    source = await discord.FFmpegOpusAudio.from_probe(song, **FFMPEG_OPTIONS)
                    vc.play(source)
                except:
                    print("error")
            if ctx.guild.id in request_queue:
                request_queue[ctx.guild.id].extend(urls)
            else:
                request_queue[ctx.guild.id] = urls
            await ctx.send("Added " + str(len(urls)) + " to queue.\nMedia in queue:\n" + printQueue(request_queue[ctx.guild.id]))
            return 
    else: #single link
        if ctx.voice_client.is_playing():
            if ctx.guild.id in request_queue:
                request_queue[ctx.guild.id].append(url)
            else:
                request_queue[ctx.guild.id] = [url]
            await ctx.send("Media added to queue.\nMedia in queue:\n" + printQueue(request_queue[ctx.guild.id]))
            return
    #downloads content using youtube-dl, plays media using ffmpeg
    vc  = ctx.voice_client
    with youtube_dl.YoutubeDL(YDL_OPTIONS) as ydl:
        try:
            info = ydl.extract_info(url, download = False)
            song = info["url"]
            source = await discord.FFmpegOpusAudio.from_probe(song, **FFMPEG_OPTIONS)
            vc.play(source)
        except:
            print("error")

#background task that plays the next song if there is one
@tasks.loop(seconds=1.0)
async def playNext(bot):
    vc_list = bot.voice_clients
    for vc in vc_list:
        if not vc.is_playing() and vc.guild.id in request_queue:
            if len(request_queue[vc.guild.id]) > 0:
                url = request_queue[vc.guild.id].pop(0)
                with youtube_dl.YoutubeDL(YDL_OPTIONS) as ydl:
                    try:
                        info = ydl.extract_info(url, download = False)
                        song = info["url"]
                        source = await discord.FFmpegOpusAudio.from_probe(song, **FFMPEG_OPTIONS)
                        vc.play(source)
                    except:
                        print("error")


#prints items in queue
@client.command()
async def queue(ctx):
    if ctx.guild.id in request_queue:
        await ctx.send("Media in queue:\n" + printQueue(request_queue[ctx.guild.id]))

@client.command()
async def pause(ctx):
    ctx.voice_client.pause()
    await ctx.send("Paused")

@client.command()
async def resume(ctx):
    ctx.voice_client.resume()
    await ctx.send("resuming")

@client.command()
async def skip(ctx):
    ctx.voice_client.stop()
    await ctx.send("Song skipped")
    
@client.command()
async def dc(ctx):
    await ctx.voice_client.disconnect()

@client.command()
async def ping(ctx):
    await ctx.send("pong!")

#initiates the playNext routine and any other startup necessary
@client.event
async def on_ready():
    playNext.start(client)
    print('online')

client.run(token)
