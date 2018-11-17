<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ClaimProject._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label1" runat="server" Text="ปีงบประมาณ : "></asp:Label>
                </div>
                <div class="col-md-1">
                    <asp:DropDownList ID="txtYear" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="txtYear_SelectedIndexChanged">
                        <asp:ListItem>2562</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="boxUserSystem">
                    <div class="card card-stats">
                        <div class="card-header card-header-danger card-header-icon">
                            <div class="card-icon">
                                <i class="fa fa-bell-o"></i>
                            </div>
                            <h4 class="card-category">แจ้งอุบัติเหตุ</h4>
                            <h4 class="card-title">
                                <asp:Label ID="lbAlert" runat="server" Text="Label"></asp:Label>
                            </h4>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <i class="fa fa-th-list"></i>&nbsp
                        <asp:LinkButton ID="btnDetailAlert" runat="server" OnClick="btnDetailAlert_Click">รายละเอียด</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="Div1">
                    <div class="card card-stats">
                        <div class="card-header card-header-warning card-header-icon">
                            <div class="card-icon">
                                <i class="fa fa-clipboard"></i>
                            </div>
                            <h4 class="card-category">ขอใบเสนอราคา</h4>
                            <h4 class="card-title">
                                <asp:Label ID="lbQuote" runat="server" Text="Label"></asp:Label>
                            </h4>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <i class="fa fa-th-list"></i>&nbsp
                        <asp:LinkButton ID="btnDetailQute" runat="server" OnClick="btnDetailQute_Click">รายละเอียด</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="Div2">
                    <div class="card card-stats">
                        <div class="card-header card-header-secondary card-header-icon">
                            <div class="card-icon">
                                <i class="fa fa-send-o"></i>
                            </div>
                            <h4 class="card-category">ส่งเรื่องเข้ากองฯ</h4>
                            <h4 class="card-title">
                                <asp:Label ID="lbSend" runat="server" Text="Label"></asp:Label>
                            </h4>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <i class="fa fa-th-list"></i>&nbsp
                        <asp:LinkButton ID="btnSendto" runat="server" OnClick="btnSendto_Click">รายละเอียด</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="Div4">
                    <div class="card card-stats">
                        <div class="card-header card-header-info card-header-icon">
                            <div class="card-icon">
                                <i class="fa fa-wrench"></i>
                            </div>
                            <h4 class="card-category">อยู่ระหว่างการซ่อม</h4>
                            <h4 class="card-title">
                                <asp:Label ID="lbRepair" runat="server" Text="Label"></asp:Label>
                            </h4>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <i class="fa fa-th-list"></i>&nbsp
                        <asp:LinkButton ID="btnWait" runat="server" OnClick="btnWait_Click">รายละเอียด</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="Div5">
                    <div class="card card-stats">
                        <div class="card-header card-header-success card-header-icon">
                            <div class="card-icon">
                                <i class="fa fa-thumbs-o-up"></i>
                            </div>
                            <h4 class="card-category">ส่งงาน/เสร็จสิ้น</h4>
                            <h4 class="card-title">
                                <asp:Label ID="lbSuccess" runat="server" Text="Label"></asp:Label>
                            </h4>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <i class="fa fa-th-list"></i>&nbsp
                        <asp:LinkButton ID="btnSuccessJob" runat="server" OnClick="btnSuccessJob_Click">รายละเอียด</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
