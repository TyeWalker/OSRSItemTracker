# This folder contains all classes
from json import JSONEncoder


class Item:
    def __init__(self, examine, id, members, lowalch, limit, value, highalch, icon, name):
        self.examineText = examine
        self.itemId = id
        self.IsMembers = members
        self.lowAlchValue = lowalch
        self.Buylimit = limit
        self.value = value
        self.highAlchValue = highalch
        self.iconName = icon
        self.name = name


class Price:
    def __init__(self, itemId, high, highTime, low, lowTime):
        self.itemId = itemId
        self.highPrice = high
        self.highTime = highTime
        self.lowPrice = low
        self.lowTime = lowTime


class Volume:
    def __init__(self, itemId, AverageHighPrice, AverageHighVolume, AverageLowPrice, AverageLowVolume, TimeDuration, TimeOfEntry):
        self.itemId = itemId
        self.AverageHighPrice = AverageHighPrice
        self.AverageHighVolume = AverageHighVolume
        self.AverageLowPrice = AverageLowPrice
        self.AverageLowVolume = AverageLowVolume
        self.TimeDuration = TimeDuration
        self.TimeOfEntry = TimeOfEntry


class JsonEncoderClass(JSONEncoder):
    def default(self, o):
        return o.__dict__
