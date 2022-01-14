<%@ Page Title="User Approval" Language="C#" MasterPageFile="~/Counselling/Counselling.Master" AutoEventWireup="true" CodeBehind="UserApproval.aspx.cs" Inherits="CounsellingWeb.UserApproval" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

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
                                    <h5>User Approvals</h5>
                                </div>
                                <div class="card-block table-border-style">
                                    <div class="table-responsive">


                                        <asp:GridView ID="grdSystemUserDetails" runat="server" CellPadding="0" CellSpacing="0" CssClass="table table-hover"
                                            DataKeyNames="UserID" GridLines="None" AutoGenerateColumns="false" OnRowCommand="grdSystemUserDetails_RowCommand">
                                            <HeaderStyle />
                                            <EmptyDataTemplate>
                                                <label class="lbl">No User record found in our system !</label>
                                            </EmptyDataTemplate>
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:BoundField DataField="Name" HeaderText="Name">
                                                    <HeaderStyle />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Email" HeaderText="Email">
                                                    <HeaderStyle />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="College" HeaderText="College">
                                                    <HeaderStyle />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Designation" HeaderText="Designation">
                                                    <HeaderStyle />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Convenor_Role" HeaderText="Convenor Role">
                                                    <HeaderStyle />
                                                </asp:BoundField>
                                                  <asp:BoundField DataField="IsApproval" HeaderText="Approval">
                                                    <HeaderStyle />
                                                </asp:BoundField>
                                                       <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="150px">
                                                <ItemTemplate>
                                                    <ul class="list-inline mb-0">
                                                        <li class="list-inline-item">
                                                            <asp:LinkButton ID="btnApprove" CssClass="px-2 text-primary " runat="server" Text="Approve" CommandName="Approve"
                                                                CommandArgument='<%# Eval("UserID") %>' CausesValidation="false" ToolTip="Approve"><i class="ti-desktop"></i></asp:LinkButton>
                                                        </li>
                                                        <li class="list-inline-item">
                                                            <asp:LinkButton ID="lnkReject" runat="server" CssClass="px-2 text-danger" Text="Reject" CommandName="Reject"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' CausesValidation="false" ToolTip="Reject"><i class="ti-alert"></i>
                                                            </asp:LinkButton>
                                                            <asp:HiddenField ID="hdnUserID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "UserID")%>'  />

                                                        </li>
                                                    </ul>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                            <!-- Hover table card end -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <asp:Label ID="lblmsg" runat="server"></asp:Label>

</asp:Content>
