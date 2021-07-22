function myFunction(id, expandedIndicatorId)
{
    var x = document.getElementById(id);
    var expandedIndicator = document.getElementById(expandedIndicatorId);
    if (x.className.indexOf("w3-hide") == -1)
    {
        x.className += " w3-hide";
        expandedIndicator.className = "fa fa-plus";
    }
    else
    {
        x.className = x.className.replace(" w3-hide", "");
        expandedIndicator.className = expandedIndicator.className.replace("fa-plus", "fa-minus");
    }
}
