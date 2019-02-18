<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClaimDevice.aspx.cs" Inherits="ClaimProject.Claim.ClaimDevice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card" style="z-index: 0">
        <div class="card-header card-header-warning">
            <h3 class="card-title">รายการอุปกรณ์ค้างซ่อม</h3>
        </div>
        <div class="card-body table-responsive table-sm">
            <div class="row">
                <div class="col-md-1 text-right">
                    <asp:Label ID="Label6" runat="server" Text="ด่านฯ : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="txtSearchCpoint" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-1 text-right">
                    <asp:Label ID="Label1" runat="server" Text="Annex :"></asp:Label>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="txtPoint" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label5" runat="server" Text="สถานะ : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="txtSearchStatus" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:CheckBox ID="CheckDeviceNotDamaged" runat="server" />
                    <label>ยกเว้น</label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 text-right">
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-dark fa" Font-Size="Medium" OnClick="btnSearch_Click">&#xf002; ค้นหา</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div id="Div1" runat="server">
        <div class="card" style="z-index: 0">
            <div class="card-header card-header-warning">
                <h3 class="card-title">รายการอุบัติเหตุ</h3>
            </div>
            <div class="card-body table-responsive table-sm">
                <asp:GridView ID="ClaimGridView" runat="server"
                    AutoGenerateColumns="False" CssClass="col table table-striped table-hover"
                    HeaderStyle-CssClass="text-center" HeaderStyle-BackColor="ActiveBorder" RowStyle-CssClass="text-center"
                    OnRowDataBound="ClaimGridView_RowDataBound" Font-Size="19px">
                    <Columns>
                        <asp:TemplateField HeaderText="ลำดับ">
                            <ItemTemplate>
                                <asp:Label ID="lbClaimNumrow" Text='<%#  Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ด่านฯ">
                            <ItemTemplate>
                                <asp:Label ID="lbClaimCpoint" Text='<%# DataBinder.Eval(Container, "DataItem.cpoint_name")+" "+DataBinder.Eval(Container, "DataItem.claim_point") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ช่องทาง">
                            <ItemTemplate>
                                <asp:Label ID="lbClaimChannel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.claim_detail_cb_claim") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="อุปกรณ์">
                            <ItemTemplate>
                                <asp:Label ID="lbClaimDeviceName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.device_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="อาการที่ชำรุด">
                            <ItemTemplate>
                                <asp:Label ID="lbClaimProblem" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.device_damaged") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="วันที่เกิดเหตุ">
                            <ItemTemplate>
                                <asp:Label ID="lbClaimSDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.claim_start_date")+" "+DataBinder.Eval(Container, "DataItem.claim_detail_time")+" น." %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="เวลาเกิดเหตุ">
                            <ItemTemplate>
                                <asp:Label ID="lbClaimSTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.claim_detail_time")+" น." %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="สถานะ">
                            <ItemTemplate>
                                <asp:Label ID="lbClaimStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.status_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
