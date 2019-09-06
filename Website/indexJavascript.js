

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
            function(myJson) 
            {
                console.log(JSON.stringify(myJson));
            }
        );
    console.log("done fetching");


    console.log(building);
    console.log(roomNumber);
    console.log(room);
    console.log(day);
}