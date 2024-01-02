import requests

startPage = "https://en.wikipedia.org/wiki/Stanford_University"
endPage = "https://en.wikipedia.org/wiki/Archive"
r = requests.get(startPage)
keepRunning = True
visitedPages = []
def visitPage(page, depth, maxDepth, path):
    global keepRunning
    if(keepRunning == True):
        thisPageLinks = []
        content = page.text
        print(("\t" * depth) + page.url)
        while(content.find("<p>") != -1):
            index = content.find("<p>")
            endIndex = content.find("</p>")
            p = content[index + 2:endIndex]
            while(p.find("<a href=") != -1):
                p = p[p.find("<a href=") + 9:]
                if(p[0:5] == "/wiki"):
                    link = p[:p.find('"')]
                    if(link not in visitedPages):
                        visitedPages.append(link)
                        thisPageLinks.append(link)
            content = content[endIndex + 3:]
    
        for page in thisPageLinks:           
            nextPage = "https://en.wikipedia.org" + page
            if(nextPage == endPage):
                keepRunning = False
                print(path + " " + page)
                break
            try:
                nextDepth = depth + 1
                if(depth < maxDepth):
                    visitPage(requests.get(nextPage), nextDepth, maxDepth, path + " " + page)
            except:
                print("Page Error. skipping")
        

maxDepth = 2
visitPage(r, 0, 2, '')
if(keepRunning == True):
    print("Did not find path")
print("visited " + str(len(visitedPages)) + " Pages")

