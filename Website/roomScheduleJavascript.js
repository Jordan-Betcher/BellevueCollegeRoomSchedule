function search()
{
    event.preventDefault();

    let room = document.forms["searchForm"]["room"].value.toUpperCase();
    let day = document.forms["searchForm"]["day"].value;

    let divError = document.getElementById("divError");
    divError.innerText = "";

    if(room.length != 4)
    {
        divError.innerText = "Error: Room not required length. Should be 4 characters long i.e. L219";
        return;
    }

    let building = room[0];
    let roomNumber = room.substring(1, 4);

    //https://67gi99ndv2.execute-api.us-west-2.amazonaws.com/Publish/room?BuildingLetter=L&RoomNumber=219&Day=Monday
    let api = "";
    api += `https://67gi99ndv2.execute-api.us-west-2.amazonaws.com/Publish/room`;
    api += `?BuildingLetter=${building}`;
    api += `&RoomNumber=${roomNumber}`;
    api += `&Day=${day}`;


    console.log("fetching");
    console.log(api);
    fetch(api)
        .then
        (
            function(response) 
            {
                return response.json();
            }
        )
        .then
        (
            function(json)
            {
                createClasstimeTable(json);
            }
        )
    console.log("done fetching");

    changeURL(room, day);
}

function changeURL(room, day)
{
    let url = document.URL;
    let addOns = `room=${room}&day=${day}`;

    console.log("changine url function");
    console.log(url.search("room"));

    if(url.search(addOns) != -1)
    {
        //skip
        return;
    }
    else if(url.search("room=") != -1)
    {
        //have a different room than is there
        url = url.split("?")[0];
        window.history.pushState("", "", url + "?" + addOns);
    }
    else
    {
        window.history.pushState("", "", url + "?" + addOns);
    }
}

const increments = 30;
const minTime = 700;
const maxTime = 2400;

function createClasstimeTable(classTimeJSON)
{
    console.log("Creating Table");
    console.log(classTimeJSON);
    let classTimes = classTimeJSON.sort
    (
        function(a, b){return a["startTime"]-b["startTime"]}
    )

    let calendar = document.getElementById("calendar");

    let newCalendar = "";
    newCalendar = `<div class="quarterDay">`;

    let time = minTime;
    while ( time < maxTime) 
    {
        let classTime = classTimes.find(function(element) { 
            return time >= element["startTime"] && time <= element["endTime"]; 
          });
        
        
        if(classTime == undefined)
        {
            newCalendar += 
            `
            <div class="row">
                <span class="hour">${time}</span>
                <span class="hourLine">
                    <span class="classCode"></span>
                </span>
            </div>
            `
        }
        else
        {
            let classCode = classTime["classCode"];
            newCalendar += 
            `
            <div class="row">
                <span class="hour">${time}</span>
                <span class="hourLine">
                    <span class="classCode">${classCode}</span>
                </span>
            </div>
            `
        }

        time += increments;
        if(time % 100 >= 60)
        {
            time -= time % 100;
            time += 100;
        }
    }

    newCalendar += `</div>`;
    calendar.innerHTML += newCalendar;
}

function main()
{
    console.log("main");
    let url = document.URL;
    if(url.search("room=") != -1)
    {
        console.log("room found");
        let roomParam = url.split("?")[1].split("&")[0];
        let dayParam = url.split("?")[1].split("&")[1];
        
        if(roomParam == undefined)
        {
            return;
        }
        
        let room = roomParam.split("=")[1];
        document.forms["searchForm"]["room"].value = room;

        if(dayParam == undefined)
        {
            return;
        }

        let day = dayParam.split("=")[1];
        document.forms["searchForm"]["day"].value = day;

        if(room != undefined && day != undefined)
        {
            search();
        }
    }
}

document.addEventListener('DOMContentLoaded', (event) => {
    main();
})