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

async function main()
{
    let module = await import('/calendar.js');
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