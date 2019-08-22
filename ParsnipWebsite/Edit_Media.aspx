﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit_Media.aspx.cs" Inherits="ParsnipWebsite.Edit_Media" %>
<%@ Register Src="~/Custom_Controls/Menu/Menu.ascx" TagPrefix="menuControls" TagName="Menu" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- iPhone FAVICONS -->
    <link rel="apple-touch-icon" sizes="114×114" href="Resources/Favicons/apple-icon-114×114.png" />
    <link rel="apple-touch-icon" sizes="72×72" href="Resources/Favicons/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" href="Resources/Favicons/apple-icon.png" />
    <!-- BOOTSTRAP START -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/css/bootstrap.min.css" integrity="sha384-Smlep5jCw/wG7hdkwQ/Z5nLIefveQRIY9nfy6xoR1uRYBtpZgI6339F5dgvm/e9B" crossorigin="anonymous" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.2/js/bootstrap.min.js" integrity="sha384-o+RDsa0aLu++PJvFqy8fFScvbHFLtbvScb8AjopnFD+iEQ7wo/CG0xlczd+2O/em" crossorigin="anonymous"></script>
    <!-- BOOTSTRAP END -->

    <script src="../Javascript/UsefulFunctions.js"></script>
    <link id="link_style" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Css/SharedStyle.css" />
    <script src="../Javascript/ApplyStyle.js"></script>

    <script src="Javascript/IntersectionObserver.js"></script>

    <title>Edit Image</title>

    <!-- Special version of Bootstrap that only affects content wrapped in .bootstrap-iso -->
<link rel="stylesheet" href="https://formden.com/static/cdn/bootstrap-iso.css" /> 

<!--Font Awesome (added because you use icons in your prepend/append)-->
<link rel="stylesheet" href="https://formden.com/static/cdn/font-awesome/4.4.0/css/font-awesome.min.css" />

<!-- Inline CSS based on choices in "Settings" tab -->
<style>.bootstrap-iso .formden_header h2, .bootstrap-iso .formden_header p, .bootstrap-iso form{font-family: Arial, Helvetica, sans-serif; color: black}.bootstrap-iso form button, .bootstrap-iso form button:hover{color: white !important;} .asteriskField{color: red;}</style>
</head>
<body class="fade0p5" id="body" style="text-align:center;"  >
    <menuControls:Menu runat="server" ID="Menu" />
    
    <div class="center_form" style="padding-bottom:5%" >
        <form id="form1" runat="server" >
            <!-- Title -->
            <div class="form-group" style="padding-left:5%; padding-right: 5%;" >
                <label style="text-align:left; width:100%">Title</label>
                <asp:TextBox CssClass="form-control" runat="server" ID="InputTitleTwo" />
            </div>

            <!-- Album select -->
            <div runat="server" id="DropDownDiv" style="padding-left:5%; padding-right: 5%;">
                <label>Select an album:</label>
                <asp:DropDownList ID="NewAlbumsDropDown" runat="server" AutoPostBack="False" CssClass="form-control" >
                </asp:DropDownList>
                <br />
            </div>

            <div class="form-group" style="padding-left:5%; padding-right: 5%;">
                <label>Date Captured</label>
                <div class="bootstrap-iso">
                <div class="form-group ">
                    <div class="input-group">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input runat="server" class="form-control login" id="input_date_media_captured" name="date" placeholder="DD/MM/YYYY" type="text"/>
                        </div>
                    </div>        
                </div>
            </div>

            <!-- Image preview -->
            <asp:Image runat="server" ID="ImagePreview" CssClass="image-preview" Width="100%" visible="false" />
            <a runat="server" id="a_play_video" visible="false" >
                <div class="play-button-div">
                    <img runat="server" id="thumbnail" style="width:100%" />
                    <span class="play-button-icon">
                        <img src="Resources\Media\Images\Web_Media\play_button_2.png" />
                    </span>
                </div>
            </a>
            <div runat="server" id="youtube_video_container" class="youtube-container" style="display:inline-block; margin-bottom:0px; padding-bottom:0px" visible="false">
                <div runat="server" id="youtube_video" class="youtube-player" />
            </div>

            <!-- Delete / save buttons -->
            <div style="width:100%; padding-left:5%; padding-right:5%; padding-top:20px; display:block;">
                <asp:Button runat="server" ID="btn_AdminDelete"  CssClass="btn btn-primary float-left" Width="100px" Text="Delete" Visible="false" data-toggle="modal" data-target="#confirmDelete" OnClientClick="return false;" UseSubmitBehavior="false"></asp:Button>
                <asp:Button runat="server" ID="ButtonSave" class="btn btn-primary float-right" Text="Save" Width="100px" OnClick="ButtonSave_Click"></asp:Button>
            </div>

            <!-- Delete modal -->
            <div class="modal fade" id="confirmDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true" style="text-align:left">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLongTitle">Confirm Delete</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            Are you sure that you want to DELETE this photo?
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                            <button id="BtnDeleteImage" class="btn btn-primary" onclick="DeletePhoto(); return false;" >Confirm</button>

                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
      
    <script src="../Javascript/Youtube.js"></script>
    <script>
        //Uses url parameter "id" to delete the image whose Id is 
        //sepcified in the url parameter "id". Fired by delete modal.
        function DeletePhoto()
        {
            var url_string = window.location.href
            var url;
            var redirect = "edit_media?"

            try
            {
                //More efficient but does not work on older browsers
                url = new URL(url_string);
                redirect += "id=" + url.searchParams.get("id") + "&delete=true";
            }
            catch (e)
            {
                //More compatible method
                url = window.location.href;
                redirect += "id=" + url.split('=')[url.split('=').length - 1] + "&delete=true";
            }

            //Use window.location.replace if possible because 
            //you don't want the current url to appear in the 
            //history, or to show - up when using the back button.
            try { window.location.replace(redirect); }
            catch (e) { window.location = redirect; }
        }
    </script>
        <!-- Extra JavaScript/CSS added manually in "Settings" tab -->
<!-- Include jQuery -->
<script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.min.js"></script>

<!-- Include Date Range Picker -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script>
        $(document).ready(function ()
        {
            var date_input = $('input[name="date"]'); //our date input has the name "date"
            var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
            $("[id$=input_date_media_captured]").datepicker({
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
            })
        })

        var dobInput = document.getElementById("input_date_media_captured");
        /*
        alert("dobInput = " + dobInput);
        alert("dobInput = " + dobInput.nodeValue);
        alert("dobInput = " + dobInput.textContent);
        alert("dobInput = " + dobInput.innerHTML);
        alert("dobInput = " + dobInput.innerText);
        */

        function BenUpdate()
        {
            var dobInput = document.getElementById('input_date_media_captured').value;
            document.getElementById('dobCopy1').value = dobInput;
        }

        var dobCopy = document.getElementById("dobCopy");
        dobInput.addEventListener('onchange')
        {
            alert("onchange");
            dobCopy.nodeValue = dobInput.nodeValue;
        }

        dobInput.addEventListener('onclick')
        {
            alert("onchange");
            dobCopy.nodeValue = dobInput.nodeValue;
        }
</script>
</body>
</html>


