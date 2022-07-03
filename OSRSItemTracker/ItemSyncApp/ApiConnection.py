import requests


# OSRS url
osrsurl = "https://prices.runescape.wiki/api/v1/osrs/"
useragent = "Item Set Price Tracker - TFunk#7503"


# Routes:
# latest
# mapping
# 1h
# 24h


def fetchrequest(route):
    url = osrsurl + route
    response = requests.get(url, headers={'User-Agent': useragent}).json()
    return response


# API connections
apiurl = "http://localhost:5214/"
headers = {
    'Content-type':'application/json',
    'Accept':'application/json'
}

def fetchfromapi(route):
    url = apiurl + route
    response = requests.get(url, headers={'User-Agent': useragent}, verify=False).json()
    return response


def posttoapi(route, requestbody):
    url = apiurl + route
    response = requests.post(url, data=requestbody, verify=False, headers=headers)
    return response
