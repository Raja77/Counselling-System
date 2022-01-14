<%@ Page Title="Student General Elective Selection" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ElectiveSubjectSelections.aspx.cs" Inherits="CounsellingWeb.ElectiveSubjectSelections" EnableViewState="true" %>

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
    </style>
    <hr />
    <br>
    <div class="mb-4 row">
        <label for="txtRollNo" class="col-sm-3 txt"><span class="RequiredField">* </span>Please enter your Roll No.</label>
        <div class="col-sm-3">
            <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvRollNo" runat="server" CssClass="lbl" ErrorMessage="<b>Enter Roll No</b>"
                ControlToValidate="txtRollNo" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revRollNo" ControlToValidate="txtRollNo" CssClass="lbl" runat="server" Display="Dynamic"
                ErrorMessage="<b>Only Numbers allowed</b>" ValidationExpression="\d+"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-3">
            <asp:Button ID="btnCheckDetails" runat="server" Text="Check your details" ToolTip="Click here to get your details"
                OnClick="btnCheckDetails_Click" CssClass="btn"></asp:Button>
        </div>
    </div>
    <br />
    <br />
    <div>

        <asp:GridView ID="grdStudentsDetail" CellPadding="0" CellSpacing="0" CssClass="table"
            DataKeyNames="classrollno" GridLines="None" runat="server" AutoGenerateColumns="false">
            <HeaderStyle />
            <EmptyDataTemplate>
                <label class="lbl">No such Roll No.found in our system !</label>
            </EmptyDataTemplate>
            <AlternatingRowStyle CssClass="alt" />
            <Columns>
                <asp:BoundField DataField="classrollno" HeaderText="Roll No">
                    <HeaderStyle />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="headerWidth">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "name")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="email" HeaderText="Email">
                    <HeaderStyle />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Marks obtained in 12th" HeaderStyle-CssClass="headerWidth">
                    <ItemTemplate>
                        <asp:Label ID="lblMarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "obtmarks")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Programme" HeaderStyle-CssClass="headerWidth">
                    <ItemTemplate>
                        <asp:Label ID="lblCourseapplied" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Courseapplied")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="dvNOPref" runat="server">
        <asp:Label ID="lblError" runat="server" CssClass="lbl" Font-Size="20"></asp:Label>
    </div>

    <div id="dvPref" runat="server" visible="false">
        <br />
        <h4>Set your Preferences</h4>
        <hr />
        <div class="mb-4 row">
            <label for="drppref1" class="col-sm-3 txt"><span class="RequiredField">* </span>Select your 1st Preference</label>
            <div class="col-sm-8">
                <asp:DropDownList ID="drppref1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drppref1_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvdrppref1" runat="server" CssClass="lbl" ErrorMessage="Select Ist Preference" InitialValue="-1"
                    ControlToValidate="drppref1" Display="Dynamic" ValidationGroup="SubmitPref"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="mb-4 row">
            <label for="drppref2" class="col-sm-3 txt"><span class="RequiredField">* </span>Select your 2nd Preference</label>
            <div class="col-sm-5">
                <asp:DropDownList ID="drppref2" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drppref2_SelectedIndexChanged" Enabled="false">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvdrppref2" runat="server" CssClass="lbl" ErrorMessage="Select 2nd Preference" InitialValue="-1"
                    ControlToValidate="drppref2" Display="Dynamic" ValidationGroup="SubmitPref"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />

        <div class="mb-4 row">
            <label for="drppref3" class="col-sm-3 txt"><span class="RequiredField">* </span>Select your 3rd Preference</label>
            <div class="col-sm-5">
                <asp:DropDownList ID="drppref3" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drppref3_SelectedIndexChanged" Enabled="false" Width="700px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvdrppref3" runat="server" CssClass="lbl" ErrorMessage="Select 3rd Preference" InitialValue="-1"
                    ControlToValidate="drppref3" Display="Dynamic" ValidationGroup="SubmitPref"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="mb-4 row">
            <label for="drppref4" class="col-sm-3 txt"><span class="RequiredField">* </span>Select your 4th Preference</label>
            <div class="col-sm-5">
                <asp:DropDownList ID="drppref4" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drppref4_SelectedIndexChanged" Enabled="false" Width="700px">
                    <%--           <asp:ListItem Text="Select 4th Preference" Value="-1"></asp:ListItem>--%>
                    <%--     <asp:ListItem Text="Biomolecules and Cell Biology" Value="Bio-Technology"></asp:ListItem>
            <asp:ListItem Text="Biomolecules" Value="Bio-Chemistry"></asp:ListItem>
            <asp:ListItem Text="Animal Diversity" Value="Zoology"></asp:ListItem>
            <asp:ListItem Text="Biodiversity (Microbes, Algae, Fungi and Archegoniate)" Value="Botany"></asp:ListItem>
            <asp:ListItem Text="Fundamental Concepts of Chemistry" Value="Chemistry"></asp:ListItem>--%>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvdrppref4" runat="server" CssClass="lbl" ErrorMessage="Select 4th Preference" InitialValue="-1"
                    ControlToValidate="drppref4" Display="Dynamic" ValidationGroup="SubmitPref"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <br />
        <div class="mb-4 row">
            <label for="drppref3" class="col-sm-3 txt"></label>
            <div class="col-sm-3">

                <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Submit your Preferences" ValidationGroup="SubmitPref" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn" Text="Print your Preferences" Visible="false" OnClientClick="window.print();"/>
            </div>
            <div class="col-sm-3">
                <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear your Preferences" CausesValidation="false" OnClick="btnClear_Click" />
            </div>
        </div>
        <br />
        <br />
        <div class="mb-4 row">
            <label for="drppref3" class="col-sm-3 txt"></label>
            <div class="col-sm-8">
                <asp:Label ID="lblmsg" runat="server" Font-Size="20"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
