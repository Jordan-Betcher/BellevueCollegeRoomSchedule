

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

    console.log(building);
    console.log(roomNumber);
    console.log(room);
    console.log(day);
}