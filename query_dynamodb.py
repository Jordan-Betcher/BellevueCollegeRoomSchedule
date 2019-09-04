import boto3
import json
import calendar
import datetime
from datetime import datetime, timedelta, date
from boto3.dynamodb.conditions import Key, Attr


dynamodb = boto3.resource('dynamodb')

def query_dynamodb(room, timestamp):
    time = datetime.strptime(timestamp, '%Y-%m-%dT%H:%M:%SZ')
    time = time - timedelta(hours=7, minutes=00) #convert currentTime to pacific time
    day = calendar.day_name[time.weekday()]
    timeNumber = get_time_number(time)
    return temp1(room, day, timeNumber)

def get_time_number(time):
    hour = time.hour
    minute = time.minute
    return hour * 100 + minute

def convert_hour_to_pacific_time(hour):
    difference = 7 #given time - bellevue college time
    if(hour > difference):
        return hour - difference
    else:
        return hour + 24 - difference

def temp1(room, day, timeNumber):
    table = dynamodb.Table('Room')
    room = room.lower()
    day = day.lower()
    
    #https://sysadmins.co.za/interfacing-amazon-dynamodb-with-python-using-boto3/
    currentClass = table.scan(
        FilterExpression=Attr('room').eq(room) & Attr('day').eq(day) & Attr('startTime').lt(timeNumber) & Attr('endTime').gt(timeNumber)
    )
    
    currentClassEndTime = 0
    currentClassCode = ""
    for item in currentClass['Items']:
        currentClassEndTime = item["endTime"]
        currentClassCode = str(item["class"])
        break

    nextClasses = table.scan(
        FilterExpression=Attr('room').eq(room) & Attr('day').eq(day) & Attr('startTime').gt(timeNumber)
    )
    
    maxTime = 2359
    nextClassStartTime = maxTime
    nextClassCode = ""
    for item in nextClasses['Items']:
        classStartTime = item["startTime"]
        if(classStartTime < nextClassStartTime):
            nextClassStartTime = classStartTime
            nextClassCode = item["class"]
    
    message = ""
    
    if(currentClassEndTime == 0):
        message += " There is currently no class in room {}. ".format(room)
        if(nextClassStartTime == maxTime):
            message += " There is no classes scheduled for the rest of today, {}. ".format(day)
        else:
            message += " The room is open for {}, the next class will start at {}. ".format(get_human_readable_difference(nextClassStartTime, timeNumber), get_human_readable_time(nextClassStartTime))
    else:
        message += "The {} class in room {} will be finished in {} at {}. ".format(currentClassCode, room, get_human_readable_difference(currentClassEndTime, timeNumber), get_human_readable_time(currentClassEndTime))
        if(nextClassStartTime > currentClassEndTime):
            if(nextClassStartTime == maxTime):
                message += " there are no classes scheduled afterwards. "
            else:
                message += " after that you will have {} before the next class at {}. ".format(get_human_readable_difference(nextClassStartTime, currentClassEndTime), get_human_readable_time(nextClassStartTime))

    return message 

#time is military time ranging from 0000 to 2359, where hhmm (the first two digits are hours and the last two are minutes
def get_human_readable_time(time):
    hours = str(int(str(time)[:2]) % 12)
    minutes = str(time)[2:]
    if(minutes == "0"):
        return " {} ".format(hours)
    else:
        return " {} {} ".format(hours, minutes)

def get_human_readable_difference(endTime, currentTime):
    endTime = str(endTime)
    currentTime = str(currentTime)
    
    if len(endTime) < 4:
        endTime = "0" + endTime
    if len(currentTime) < 4:
        currentTime = "0" + currentTime
    
    hours = int(endTime[:2]) - int(str(currentTime)[:2])
    minutes = int(endTime[2:]) - int(currentTime[2:])
    if(minutes < 0):
        minutes += 60
        hours -= 1
    
    hours = str(hours)
    minutes = str(minutes)
    
    if(minutes == "0"):
        return " {} hours ".format(hours)
    else:
        if(hours == "0"):
            return " {} minutes ".format(minutes)
        else:
            return " {} hours and {} minutes ".format(hours, minutes)
   