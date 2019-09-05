

function search()
{
    event.preventDefault();

    let room = document.forms["searchForm"]["room"].value.toLowerCase();
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
    let api = `https://67gi99ndv2.execute-api.us-west-2.amazonaws.com/Publish/room`
    + `?BuildingLetter=${building}`
    + `&RoomNumber=${roomNumber}`
    + `&Day=${day}`;

    console.log("fetching");
    fetch(api)
        .then(function(res)
        {
            console.log(res);
        });
    console.log("done fetching");

    console.log(api);

    console.log(building);
    console.log(roomNumber);
    console.log(room);
    console.log(day);
}