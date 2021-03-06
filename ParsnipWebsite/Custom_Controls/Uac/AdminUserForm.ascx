﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminUserForm.ascx.cs" Inherits="ParsnipWebsite.Custom_Controls.Uac.AdminUserForm" %>
<!-- Special version of Bootstrap that only affects content wrapped in .bootstrap-iso -->
<link rel="stylesheet" href="https://formden.com/static/cdn/bootstrap-iso.css" /> 

<!--Font Awesome (added because you use icons in your prepend/append)-->
<link rel="stylesheet" href="https://formden.com/static/cdn/font-awesome/4.4.0/css/font-awesome.min.css" />

<!-- Inline CSS based on choices in "Settings" tab -->
<style>.bootstrap-iso .formden_header h2, .bootstrap-iso .formden_header p, .bootstrap-iso form{font-family: Arial, Helvetica, sans-serif; color: black}.bootstrap-iso form button, .bootstrap-iso form button:hover{color: white !important;} .asteriskField{color: red;}</style>
        <div class="form-group" style="margin-top:15px">
            <label>Last Logged In</label>
            <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control" ID="TextBox_LastLoggedIn" MaxLength="50" />
        </div>

        <div class="form-group">
            <label>Username</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="username" MaxLength="50" />
        </div>
        
        <div class="form-group">
            <label>Email</label>
            <asp:TextBox runat="server" TextMode="email" CssClass="form-control" ID="email"  MaxLength="254"/>
        </div>
 
        <div class="form-group">
            <label>Password</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="password" MaxLength="50" />
        </div>
            
        <div class="form-group">
            <label>Forename</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="forename" MaxLength ="50" />
        </div>
           
        <div class="form-group">
            <label>Surname</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="surname" MaxLength ="50" />
        </div>
  
        <div class="form-group">
            <label>Gender</label>
            <select runat="server" class="form-control" id="gender">
                <option>Male</option>
                <option>Female</option>
                <option>Other</option>
            </select>
        </div>

        <div class="form-group">
            <label>Date Of Birth</label>
            <div class="bootstrap-iso">
                <div class="form-group ">
                    <div class="input-group">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input runat="server" class="form-control login" id="dobInput" name="date" placeholder="DD/MM/YYYY" type="text"/>
                    </div>
                </div>        
            </div>
        </div>

        <div class="form-group">
            <label>Address Line 1</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="address1" MaxLength ="50" />
        </div>
            
        <div class="form-group">
            <label>Address Line 2</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="address2" MaxLength ="50" />
        </div>
           
        <div class="form-group">
            <label>Address Line 3</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="address3" MaxLength ="50" />
        </div>
            
        <div class="form-group">
            <label>Post Code</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="postCode" MaxLength ="16" />
        </div>
            
        <div class="form-group">
            <label>Mobile Phone</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="mobilePhone" MaxLength ="32" />
        </div>
            
        <div class="form-group">
            <label>Home Phone</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="homePhone" MaxLength ="32" />
        </div>
           
        <div class="form-group">
            <label>Work Phone</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="workPhone"  MaxLength="32"/>
        </div>
            
        <div class="form-group">
            <label>Account Type</label>
            <select runat="server" class="form-control" id="accountType">
                <option>user</option>
                <option>member</option>
                <option>media</option>
                <option>admin</option>
            </select>
        </div>
            
        <div class="form-group">
            <label>Account Status</label>
            <select runat="server" class="form-control" id="accountStatus">
                <option>active</option>
                <option>suspended</option>
                <option>inactive</option>
            </select>
        </div>
         
        <div class="form-group">
            <label for="exampleFormControlFile1">Profile Picture</label>
            <input type="file" class="form-control-file" id="profilePicture">
        </div>

        <div class="form-group">
            <label class="form-check-label">Date Created</label>
            <input runat="server" class="form-control" type="text" placeholder="17/04/1999" id="dateTimeCreated" disabled>
        </div>


            
        <br />
    <!-- Extra JavaScript/CSS added manually in "Settings" tab -->
<!-- Include jQuery -->
<script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.min.js"></script>

<!-- Include Date Range Picker -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>

<script>
	$(document).ready(function(){
		var date_input=$('input[name="date"]'); //our date input has the name "date"
		var container=$('.bootstrap-iso form').length>0 ? $('.bootstrap-iso form').parent() : "body";
		$("[id$=dobInput]").datepicker({
			format: 'dd/mm/yyyy',
			container: container,
			todayHighlight: true,
			autoclose: true,
		})
    })

    var dobInput = document.getElementById("dobInput");
    /*
    alert("dobInput = " + dobInput);
    alert("dobInput = " + dobInput.nodeValue);
    alert("dobInput = " + dobInput.textContent);
    alert("dobInput = " + dobInput.innerHTML);
    alert("dobInput = " + dobInput.innerText);
    */

    function BenUpdate() {
        var dobInput = document.getElementById('dobInput').value;
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

