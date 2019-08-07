// Set the date we're counting down to
var manchesterToKrakowDepartureDate = new Date("Aug 14, 2019 19:05:00").getTime();
var manchesterToKrakowArrivalDate = new Date("Aug 14, 2019 22:45:00").getTime();
var returnCountdownStartDate = new Date("Aug 20, 2019 23:10:00").getTime();
var krakowToManchesterDepartureDate = new Date("Aug 21, 2019 23:10:00").getTime();
var krakowToManchesterArrivalDate = new Date("Aug 22, 2019 00:55:00").getTime();
var now = new Date().getTime();

if (manchesterToKrakowDepartureDate > now)
{
    countdown(manchesterToKrakowDepartureDate, "Departing to Krakow in ");
}
else if (manchesterToKrakowArrivalDate > now)
{
    countdown(manchesterToKrakowArrivalDate, "Arriving in Krakow in ");
}
else if (returnCountdownStartDate > now)
{
    document.getElementById("countdownToKrakow").innerHTML = "Flight <i>should</i> have arrived in Krakow!";
    document.getElementById("countdownInfo").innerHTML = "(Countdown will resume 24 hours before departure)"
}
else if (krakowToManchesterDepartureDate > now)
{
    countdown(krakowToManchesterDepartureDate, "Departing to Manchester in ");
}

else {
    countdown(krakowToManchesterArrivalDate, "Arriving to Manchester in ");
}

function countdown(date, message)
{
    var countDownDate = date;
    var countDownMessage = message;

    // Update the count down every 1 second
    var x = setInterval(function ()
    {
        // Get today's date and time
        var now = new Date().getTime();

        // Find the distance between now and the count down date
        var distance = countDownDate - now;

        // Time calculations for days, hours, minutes and seconds
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);


        // Output the result in an element with id="demo"
        document.getElementById("countdownToKrakow").innerHTML = countDownMessage + days + " days " + hours + " hours "
            + minutes + " minutes " + seconds + " seconds!!!";

        // If the count down is over, write some text 
        if (distance < 0)
        {
            clearInterval(x);
            //document.getElementById("countdownToKrakow").innerHTML = "Flight <i>should</i> have departed";
            document.getElementById("flightDetailsContainer").style.display = "none";
        }
    }, 1000);
}