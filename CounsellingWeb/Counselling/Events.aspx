<%@ Page Title="View Profile" Language="C#" MasterPageFile="~/Counselling/Counselling.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="CounsellingWeb.Events" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
       <style type="text/css">
        .txt {
            text-align: right;
        }

        .lbl {
            color: Red;
            font-weight: bolder;
        }
           </style>




    <div class="pcoded-inner-content">
        <!-- Main-body start -->
        <div class="main-body">
            <div class="page-wrapper">
                <!-- Page-body start -->
                <div class="page-body">

                    <div class="row">
                        <div class="col-sm-12">
                            <!-- Hover table card start -->
                            <div class="card">
                                <div class="card-header">
                                    <h5>My Events</h5>
                                </div>
                                <hr />


                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label txt"><span class="RequiredField">* </span>Event Name</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" Placeholder="User Name" CssClass="lbl" ErrorMessage="<b>Event Name  is required</b>"
                                            ControlToValidate="txtEventName" Display="Dynamic" ValidationGroup="SubmitUserInfo"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label txt">Description</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label txt"><span class="RequiredField">* </span>Upload Image / Logo</label>
                                    <div class="col-sm-5">
                                        <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvImage" runat="server" CssClass="lbl" ErrorMessage="<b>Upload Image / Logo is required</b>"
                                            ControlToValidate="fuImage" Display="Dynamic" ValidationGroup="SubmitUserInfo"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator
                                            ID="FileUpLoadValidator" runat="server"
                                            ErrorMessage="Upload Jpegs and Gifs only."
                                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF)$"
                                            ControlToValidate="fuImage">  
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label"></label>
                                    <div class="col-sm-5">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Post Event" ValidationGroup="SubmitUserInfo" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                                <%--    <br />
                    <h4>College Details</h4>
                    <hr />
                    <div class="mb-2 row">
                        <label for="drpCollege" class="col-sm-3 txt"><span class="RequiredField">* </span>College/Institution</label>
                        <div class="col-sm-5">
                            <asp:DropDownList ID="drpCollege" runat="server" CssClass="form-control drp" AutoPostBack="true" OnSelectedIndexChanged="drpCollege_SelectedIndexChanged">
                                <asp:ListItem Text="Your College/Institution" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" CssClass="lbl" ErrorMessage="Select College in the list" InitialValue="-1"
                                ControlToValidate="drpCollege" Display="Dynamic" ValidationGroup="SubmitUserInfo"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="mb-2 row">
                        <label for="txtDesignation" class="col-sm-3 txt"><span class="RequiredField">* </span>Designation</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" CssClass="lbl" ErrorMessage="<b>Designation required</b>"
                                ControlToValidate="txtDesignation" Display="Dynamic" ValidationGroup="SubmitUserInfo"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="mb-2 row">
                        <label for="txtConvenorRole" class="col-sm-3 txt"><span class="RequiredField">* </span>Convenor Role</label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="drpConvenorRole" runat="server" CssClass="form-control drp" AutoPostBack="true" OnSelectedIndexChanged="drpConvenorRole_SelectedIndexChanged">
                                <asp:ListItem Text="Your Convenor Role" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvConvenorRole" runat="server" CssClass="lbl" ErrorMessage="<b>Convenor Role required</b>" InitialValue="-1"
                                ControlToValidate="drpConvenorRole" Display="Dynamic" ValidationGroup="SubmitUserInfo"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                                --%>
                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>
    
</asp:Content>
