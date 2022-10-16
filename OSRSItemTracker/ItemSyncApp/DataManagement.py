# This file is to manage data between RuneLite's real-time prices and API
import json
from json import JSONEncoder
import ApiConnection as ApiConfig
import ApiClasses
import datetime
import ItemSets


sleep_updateLatest = 120     # Updates every 120 seconds
sleep_oneHourVolume = 3300  # Updates every 55 minutes
sleep_oneDayVolume = 86400  # Updates every 24 hours


def senditemstodatabase():
    items = ApiConfig.fetchrequest("mapping")
    for x in items:
        item_examine = x['examine'] if 'examine' in x else ""
        item_lowalch = x['lowalch'] if 'lowalch' in x else 0
        item_limit = x['limit'] if 'limit' in x else 0
        item_value = x['value'] if 'value' in x else 0
        item_highalch = x['highalch'] if 'highalch' in x else 0
        item_icon = x['icon'] if 'icon' in x else ""
        item_name = x['name'] if 'name' in x else ""
        item = ApiClasses.Item(item_examine, x['id'], str(x['members']), item_lowalch, item_limit, item_value,
                               item_highalch, item_icon, item_name)
        jsonitem = json.dumps(item.__dict__)
        result = ApiConfig.posttoapi("item", jsonitem)
        if result.status_code == 200:
            print("Added item: " + str(item.itemId))
        elif result.status_code == 400:
            print("Error uploading item: " + str(item.itemId))
        else:
            print("Unknown error for item: " + str(item.itemId))
            # time.sleep(1)


# itemId, high, highTime, low, lowTime
def updatelatestprices():
    latestprices = ApiConfig.fetchrequest("latest")
    itemlist = latestprices['data']
    for x in itemlist:
        item_id = x
        # High model
        buy_pricevalue = 0 if itemlist[x]['high'] is None else itemlist[x]['high']
        buy_pricetime = 0 if itemlist[x]['highTime'] is None else itemlist[x]['highTime']
        # Low Model
        sell_pricevalue = 0 if itemlist[x]['low'] is None else itemlist[x]['low']
        sell_pricetime = 0 if itemlist[x]['lowTime'] is None else itemlist[x]['lowTime']
        # API requests
        price_model = ApiClasses.PricePostModel(x, buy_pricevalue, buy_pricetime, sell_pricevalue, sell_pricetime)
        price_jsonpostmodel = json.dumps(price_model.__dict__)
        price_result = ApiConfig.posttoapi("price/", price_jsonpostmodel)
        if price_result.status_code == 200:
            print("Latest buy price added for item: " + str(x))
        elif price_result.status_code == 400:
            print("Error uploading buy price: " + str(x))
            print(price_result.text)




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


# Utils
class PriceEncoder(JSONEncoder):
    def default(self, o):
        return o.__dict__


def toJSON(self):
    return json.dumps(self, default=lambda o: o.__dict__, sort_keys=True, indent=4)
