<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TechnoFormView.aspx.cs" Inherits="ClaimProject.Techno.TechnoFormView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="card" style="z-index: 0">
                <div class="card-header card-header-warning">
                    <h3 class="card-title">รายการ<%= new ClaimProject.Config.ClaimFunction().GetSelectValue("tbl_status","status_id = '"+status+"'","status_name") %></h3>
                </div>
                <div class="card-body table-responsive table-sm">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group bmd-form-group">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-dark btn-sm fa" Font-Size="Medium" OnClick="btnSearch_Click">&#xf002; ค้นหา</asp:LinkButton>
                        </div>
                    </div>
                    <hr />
                    <asp:GridView ID="ClaimGridView" runat="server"
                        DataKeyNames="claim_id"
                        GridLines="None"
                        OnRowDataBound="ClaimGridView_RowDataBound"
                        AutoGenerateColumns="False"
                        CssClass="table table-hover table-sm"
                        Font-Size="19px"
                        AllowSorting="true"
                        AllowPaging="true"
                        PageSize="50"
                        OnPageIndexChanging="ClaimGridView_PageIndexChanging"
                        PagerSettings-Mode="NumericFirstLast" HeaderStyle-Font-Bold="true">
                        <Columns>
                            <asp:TemplateField HeaderText="ด่านฯ">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbCpoint" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cpoint_name") %>' CssClass="links-horizontal" OnCommand="btnChangeStatus_Command"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ชื่อเรื่อง">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEquipment" runat="server" Text='<%# new ClaimProject.Config.ClaimFunction().ShortText(DataBinder.Eval(Container, "DataItem.claim_equipment").ToString()) %>' CssClass="links-horizontal" OnCommand="btnChangeStatus_Command"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันที่เกิดอุบัติเหตุ">
                                <ItemTemplate>
                                    <asp:Label ID="_lbDateStart" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันที่ทำรายการ">
                                <ItemTemplate>
                                    <asp:Label ID="lbStartDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ผ่านมาแล้ว">
                                <ItemTemplate>
                                    <asp:Label ID="lbDay" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ครบกำหนด">
                                <ItemTemplate>
                                    <asp:Label ID="lbCountdown" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สถานะ" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lbStatus" Font-Size="16px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.status_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                    </asp:GridView>
                    <asp:Label ID="lbClaimNull" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
