<%@ Page Title="ตรวจสอบผลการ CM" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CMSurveyForm.aspx.cs" Inherits="ClaimProject.CM.CMSurveyForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="DivCMGridView" runat="server" class="col-12">
        <div class="card" style="z-index: 0">
            <div class="card-header card-header-warning">
                <h3 class="card-title">รายการแจ้งซ่อมอุปกรณ์</h3>
            </div>
            <div class="card-body table-responsive table-sm">
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="450px">
                    <asp:GridView ID="CMGridView" runat="server"
                        AutoGenerateColumns="False" CssClass="col table table-striped table-hover"
                        HeaderStyle-CssClass="text-center" HeaderStyle-BackColor="ActiveBorder" RowStyle-CssClass="text-center"
                        OnRowDataBound="CMGridView_RowDataBound" Font-Size="19px" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Ref." ControlStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="lbref" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cm_detail_id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ด่านฯ" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lbCpoint" Text='<%# DataBinder.Eval(Container, "DataItem.cpoint_name")+" "+DataBinder.Eval(Container, "DataItem.cm_point") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ช่องทาง" ControlStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:Label ID="lbChannel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cm_detail_channel") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="อุปกรณ์" ControlStyle-Width="450px">
                                <ItemTemplate>
                                    <asp:Label ID="lbDeviceName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.device_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="อาการที่ชำรุด" ControlStyle-Width="350px">
                                <ItemTemplate>
                                    <asp:Label ID="lbProblem" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cm_detail_problem") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันที่แจ้งซ่อม" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lbSDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เวลาแจ้งซ่อม" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lbSTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cm_detail_stime")+" น." %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันที่แก้ไข" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="btnDateEditCM" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เวลาที่แก้ไข" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="btnTimeEditCM" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สถานะ" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lbStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วิธีแก้ไข" ControlStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lbMethod" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cm_detail_method") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="หมายเหตุ" ControlStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lbNote" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cm_detail_note") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="อนุมัติ" ControlStyle-Width="50px">
                                <ItemTemplate>
                                    <div class="row">
                                        <asp:LinkButton ID="btnStatusUpdate" runat="server" OnCommand="btnStatusUpdate_Command" OnClientClick="return CompareConfirm('ยืนยันข้อมูลถูกต้อง ใช่หรือไม่');" CssClass="fas text-success">&#xf058;</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" OnCommand="btnCancel_Command" OnClientClick="return CompareConfirm('ยืนยันไม่อนุมัติ ใช่หรือไม่');" CssClass="fas text-danger">&#xf057;</asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                        <HeaderStyle BackColor="#507CD1" CssClass="text-center" Font-Bold="True" ForeColor="White"></HeaderStyle>

                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />

                        <RowStyle CssClass="text-center" BackColor="#EFF3FB"></RowStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </div>
    <script src="/Scripts/jquery-ui-1.11.4.custom.js"></script>
    <script src="/Scripts/moment.min.js"></script>
    <script src="/Scripts/ClaimProjectScript.js"></script>
    <script type="text/javascript"> 
        function ClickAdd() {
            $("#addCMModal").modal('show');
            return false;
        }

        function CompareConfirm(msg) {
            var str1 = "1";
            var str2 = "2";

            if (str1 === str2) {
                // your logic here
                return false;
            } else {
                // your logic here
                return confirm(msg);
            }
        }
    </script>
</asp:Content>
