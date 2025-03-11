import discord
from discord.ext import commands, tasks
import yt_dlp
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
#holds the source to a preloaded song for each guild
preloaded_song = {}
#holds the name of the current playing media for each guild
currently_playing = {}

#create a bot/client with command prefix of '!'
client = commands.Bot(command_prefix = '!', intents=intents)

async def getAudio(url):
    global YDL_OPTIONS
    with yt_dlp.YoutubeDL(YDL_OPTIONS) as ydl:
        info = ydl.extract_info(url, download = False)
        return info["url"]

async def loadSource(song):
    global FFMPEG_OPTIONS
    return discord.FFmpegOpusAudio(song, **FFMPEG_OPTIONS)

async def playSource(vc, source):
    try:
        vc.play(source)
    except:
        print("Song Playback Error")

async def playAudio(vc, url):
    try:
        song = await getAudio(url)
        source = await loadSource(song)
        vc.play(source)
    except:
        print("Song Playback Error")


async def preloadNext(url):
    song = await getAudio(url)
    source = await loadSource(song)
    return source

#function for getting the titles from the queue of urls
def printQueue(queue):
    returnString = ""
    for i, url in enumerate(queue):
        r = requests.get("https://noembed.com/embed?dataType=json&url=" + url)
        data = r.json()
        returnString += str(i + 1) + ". " + str(data['title']) + "\n"
        if i >= 9:
            returnString += "And " + str(len(queue) - 10) + " more.\n"
            break
    return returnString

def get_audio_name(url):
    r = requests.get("https://noembed.com/embed?dataType=json&url=" + url)
    data = r.json()
    return str(data['title'])
   

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
            await ctx.send("Added " + str(len(urls)) + " to queue.\nMedia in queue (" + str(len(request_queue[ctx.guild.id])) +"):\n" + printQueue(request_queue[ctx.guild.id]))
            return
        else:
            url = urls.pop(0)
            vc  = ctx.voice_client
            print("playing directly")
            currently_playing[ctx.guild.id] = get_audio_name(url)
            await playAudio(vc, url)
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
            await ctx.send("Media added to queue.\nMedia in queue (" + str(len(request_queue[ctx.guild.id])) + "):\n" + printQueue(request_queue[ctx.guild.id]))
            return
    #downloads content using youtube-dl, plays media using ffmpeg
    vc  = ctx.voice_client
    print("playing directly")
    currently_playing[ctx.guild.id] = get_audio_name(url)
    await playAudio(vc, url)


#background task that plays the next song if there is one
@tasks.loop(seconds=1.0)
async def playNext(bot):
    vc_list = bot.voice_clients
    for vc in vc_list:
        if vc.is_playing() and vc.guild.id in request_queue and not vc.guild.id in preloaded_song:
            if len(request_queue[vc.guild.id]) > 0:
                print("Preloading next song")
                preloaded_song[vc.guild.id] = await preloadNext(request_queue[vc.guild.id][0])
        if not vc.is_playing() and vc.guild.id in request_queue and not vc.is_paused():
            if vc.guild.id in preloaded_song:
                if len(request_queue[vc.guild.id]) > 0:
                    currently_playing[vc.guild.id] = get_audio_name(request_queue[vc.guild.id].pop(0))
                source = preloaded_song.pop(vc.guild.id)
                print("playing from preloaded source")
                await playSource(vc, source)
            elif len(request_queue[vc.guild.id]) > 0:
                url = request_queue[vc.guild.id].pop(0)
                print("playing directly")
                currently_playing[vc.guild.id] = get_audio_name(url)
                await playAudio(vc, url)
        elif not vc.is_playing() and not vc.is_paused() and vc.guild.id in currently_playing:
            currently_playing.pop(vc.guild.id)

#clears queue and currently playing media
@client.command()
async def clear(ctx):
    if ctx.guild.id in request_queue:
        request_queue[ctx.guild.id] = []
        ctx.voice_client.stop()
        await ctx.send("Queue cleared!")
    else:
        await ctx.send("No queue found!")

#prints items in queue
@client.command()
async def queue(ctx):
    if ctx.guild.id in request_queue:
        if len(request_queue[ctx.guild.id]) > 0:
            await ctx.send("Media in queue (" + str(len(request_queue[ctx.guild.id])) + "):\n" + printQueue(request_queue[ctx.guild.id]))
        else:
            await ctx.send("No media in queue!")

@client.command()
async def now(ctx):
    if ctx.guild.id in currently_playing:
        await ctx.send("Currently playing:\n**" + currently_playing[ctx.guild.id] + "**")
    else:
        await ctx.send("Nothing is currently playing!")

@client.command()
async def pause(ctx):
    ctx.voice_client.pause()
    await ctx.send("Paused")

@client.command()
async def resume(ctx):
    ctx.voice_client.resume()
    await ctx.send("resuming")

@client.command()
async def skip(ctx, idx = 0):
    if idx == 0:
        ctx.voice_client.stop()
    elif ctx.guild.id in request_queue:
        if idx >= 1 and idx <= len(request_queue[ctx.guild.id]):
            request_queue[ctx.guild.id].pop(idx - 1)
        else:
            await ctx.send("Invalid Position")
            return
    else:
        await ctx.send("Queue not found")
        return
        
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
