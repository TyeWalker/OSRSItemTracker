import datetime
import time
import threading
import DataManagement
import urllib3


sleep_latestprice = 120  # Updates every 120 seconds
sleep_oneHourVolume = 3300  # Updates every 55 minutes
sleep_oneDayVolume = 86400  # Updates every 24 hours


def latestpricethread():
    while True:
        for attempt in range(10):
            rightnow = datetime.datetime.now()
            try:
                print("Starting latest price sync: " + str(rightnow))
                DataManagement.updatelatestprices()
                print("Latest prices complete")
                print("Sleep latest prices sync for: " + str(sleep_latestprice) + " seconds")
                time.sleep(sleep_latestprice)
            except:
                print("Failed to update latest price" + str(datetime.datetime.now()))


def onehourvolumethread():
    while True:
        for attempt in range(10):
            rightnow = datetime.datetime.now()
            try:
                print("Starting one hour volume sync: " + str(rightnow))
                DataManagement.updateonehourvolume()
                print("one hour volume complete")
                print("Sleep one hour volume sync for: " + str(sleep_oneHourVolume) + " seconds")
                time.sleep(sleep_oneHourVolume)
            except:
                print("Failed to one hour volume price" + str(datetime.datetime.now()))


def onedayvolumethread():
    while True:
        for attempt in range(10):
            rightnow = datetime.datetime.now()
            try:
                print("Starting one day volume sync: " + str(rightnow))
                DataManagement.updateonedayvolume()
                print("one day volume complete")
                print("Sleep one day volume sync for: " + str(sleep_oneDayVolume) + " seconds")
                time.sleep(sleep_oneDayVolume)
            except:
                print("Failed to one hour volume price" + str(datetime.datetime.now()))


# Threads
thread_latestprice = threading.Thread(target=latestpricethread)
thread_onehourvolume = threading.Thread(target=onehourvolumethread)
thread_onedayvolume = threading.Thread(target=onedayvolumethread)


if __name__ == "__main__":
    # ignore Unverified HTTPS request error because running locally:
    urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)
    # add all items to db
    # uncomment line below for first launch:
    DataManagement.senditemstodatabase()
    # Add Latest Prices:
    thread_latestprice.start()
    # thread above to run every 2 minutes
    # volume 1h:
    thread_onehourvolume.start()
    # thread above to run every 55 minutes
    # volume 24h:
    thread_onedayvolume.start()
    # thread above to run every 1440 minutes
