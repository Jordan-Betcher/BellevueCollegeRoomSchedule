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