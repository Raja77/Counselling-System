<%@ Page Title="System User" Language="C#" MasterPageFile="~/NewSite.Master" AutoEventWireup="true" CodeBehind="LogInUser.aspx.cs" Inherits="CounsellingWeb.LogInUser" %>

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

     <h2>Use a local account to log in.</h2>

              <fieldset>
                    <legend>Log in Form</legend>
                    <ol>
                        <li>
                            <asp:Label ID="Label1" runat="server" AssociatedControlID="txtLogIn">User name</asp:Label>

            <asp:TextBox ID="txtLogIn" runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLogIn" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                        </li>
                        <li>
                            <asp:Label ID="Label2" runat="server" AssociatedControlID="txtPassword">Password</asp:Label>
           <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                        </li>
                        </ol>
                        <asp:Button ID="btnLogIn" runat="server" OnClick="btnLogIn_Click" Text="Log In" />
                  </fieldset >





    <asp:Label ID="lblmsg" runat="server"></asp:Label>

</asp:Content>
