# This file is to manage data between Runelite's real-time prices and API
import json
import ApiConnection as ApiConfig
import ApiClasses
import datetime
import ItemSets


sleep_updateLatest = 120     # Updates every 120 seconds
sleep_oneHourVolume = 3300  # Updates every 55 minutes
sleep_oneDayVolume = 86400  # Updates every 24 hours


def senditemstodatabase():
    items = ApiConfig.fetchrequest("mapping")
    for i in ItemSets.allItemIDs:
        for x in items:
            if x['id'] == i:
                item = ApiClasses.Item(x['examine'], x['id'], str(x['members']), x['lowalch'], x['limit'], x['value'],
                                       x['highalch'], x['icon'], x['name'])
                jsonitem = json.dumps(item.__dict__)
                result = ApiConfig.posttoapi("item", jsonitem)
                if result.status_code == 200:
                    print("Added item: " + str(item.itemId))
                elif result.status_code == 400:
                    print("Error uploading item: " + str(item.itemId))
                # time.sleep(1)


# itemId, high, highTime, low, lowTime
def updatelatestprices():
    latestprices = ApiConfig.fetchrequest("latest")
    itemlist = latestprices['data']
    for i in ItemSets.allItemIDs:
        for y in itemlist:
            if i == int(y):
                high = 0 if itemlist[y]['high'] is None else itemlist[y]['high']
                highTime = 0 if itemlist[y]['highTime'] is None else itemlist[y]['highTime']
                low = 0 if itemlist[y]['low'] is None else itemlist[y]['low']
                lowTime = 0 if itemlist[y]['lowTime'] is None else itemlist[y]['lowTime']
                price = ApiClasses.Price(y, int(high), str(datetime.date.fromtimestamp(highTime)), int(low), str(datetime.date.fromtimestamp(lowTime)))
                jsonprice = json.dumps(price.__dict__)
                result = ApiConfig.posttoapi("price/" + str(y), jsonprice)
                if result.status_code == 200:
                    print("Latest price added: " + str(i))
                elif result.status_code == 400:
                    print("Error uploading price: " + str(i))
                # time.sleep(1)


def updateonehourvolume():
    onehourvolume = ApiConfig.fetchrequest("1h")
    onehourlist = onehourvolume['data']
    for i in ItemSets.allItemIDs:
        for y in onehourlist:
            if i == int(y):
                avgHighPrice = 0 if onehourlist[y]['avgHighPrice'] is None else onehourlist[y]['avgHighPrice']
                highPriceVolume = 0 if onehourlist[y]['highPriceVolume'] is None else onehourlist[y]['highPriceVolume']
                avgLowPrice = 0 if onehourlist[y]['avgLowPrice'] is None else onehourlist[y]['avgLowPrice']
                lowPriceVolume = 0 if onehourlist[y]['lowPriceVolume'] is None else onehourlist[y]['lowPriceVolume']
                price = ApiClasses.Volume(int(y), avgHighPrice, highPriceVolume, avgLowPrice, lowPriceVolume, 60, datetime.datetime.now().strftime("%d/%m/%Y %H:%M:%S"))
                jsonprice = json.dumps(price.__dict__)
                result = ApiConfig.posttoapi("volume/" + str(y), jsonprice)
                if result.status_code == 200:
                    print("One hour volume added for: " + str(i))
                elif result.status_code == 400:
                    print("Error uploading one hour volume: " + str(i))
                # time.sleep(1)


def updateonedayvolume():
    onedayvolume = ApiConfig.fetchrequest("24h")
    onedaylist = onedayvolume['data']
    for i in ItemSets.allItemIDs:
        for y in onedaylist:
            if i == int(y):
                avgHighPrice = 0 if onedaylist[y]['avgHighPrice'] is None else onedaylist[y]['avgHighPrice']
                highPriceVolume = 0 if onedaylist[y]['highPriceVolume'] is None else onedaylist[y]['highPriceVolume']
                avgLowPrice = 0 if onedaylist[y]['avgLowPrice'] is None else onedaylist[y]['avgLowPrice']
                lowPriceVolume = 0 if onedaylist[y]['lowPriceVolume'] is None else onedaylist[y]['lowPriceVolume']
                price = ApiClasses.Volume(int(y), avgHighPrice, highPriceVolume, avgLowPrice, lowPriceVolume, 1440, datetime.datetime.now().strftime("%d/%m/%Y %H:%M:%S"))
                jsonprice = json.dumps(price.__dict__)
                result = ApiConfig.posttoapi("volume/" + str(y), jsonprice)
                if result.status_code == 200:
                    print("One day volume added for: " + str(y))
                elif result.status_code == 400:
                    print("Error uploading 24h volume for: " + str(y))
                # time.sleep(1)

