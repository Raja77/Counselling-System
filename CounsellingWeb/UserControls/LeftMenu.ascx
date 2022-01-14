<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.ascx.cs" Inherits="CounsellingWeb.LeftMenu" %>
   <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="../Content/StyleSheet1.css" rel="stylesheet" />

 <div class="d-flex" id="wrapper">
            <div class="bg-light border-light" id ="sidebar-wrapper">
                <div class="sidebar-heading"><i class="fa fa-home"></i>&nbsp;&nbsp;Home</div>
                <div class="list-group list-group-flush">
                    <a href="#" class="list-group-item list-group-item-action bg-light"><i class="fa-solid fa-gauge-simple"></i>&nbsp;DashBoard</a>
                    <a href="#" class="list-group-item list-group-item-action bg-light"><i class="fa fa-user-unlock"></i>&nbsp;User Approvals</a>
                    <a href="#" class="list-group-item list-group-item-action bg-light"><i class="fa-solid fa-calendar-days"></i>&nbsp;Calendar</a>
                         <a href="#" class="list-group-item list-group-item-action bg-light"><i class="fa-solid fa-calendar-pen"></i>&nbsp;My Events</a>
                    <a href="#" class="list-group-item list-group-item-action bg-light"><i class="fa-solid fa-calendar-pen"></i>&nbsp;Upcoming Events</a>
                    <a href="#" class="list-group-item list-group-item-action bg-light">Invites</a>
                    <a href="#" class="list-group-item list-group-item-action bg-light"><i class="fa-solid fa-mailbox"></i>&nbsp;Inbox</a>
                     <a href="#" class="list-group-item list-group-item-action bg-light"><i class="fa-solid fa-calendar-pen"></i>&nbsp;Activity Tracker (Analytics)</a>
<%--                    <a href="#" class="list-group-item list-group-item-action bg-light"><i class="fa-solid fa-address-card"></i>&nbsp;Profile</a>--%>

                </div>
            </div>
        </div>