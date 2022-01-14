<%@ Page Title="DashBoard Activities" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CounsellingWeb.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <style>
        .txt {
            text-align: right;
            margin-top: 5px;
        }

        .lbl {
            color: Red;
            font-weight: bolder;
        }

        .mb-4 {
            margin-bottom: 15px;
        }
    </style>

 <h3>Welcome to the DashBoard</h3>
    <hr />





    <asp:Label ID="lblmsg" runat="server"></asp:Label>

</asp:Content>
