<%@ Page Title="รายการ CM" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CMDetailForm.aspx.cs" Inherits="ClaimProject.CM.CMDetailForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Scripts/bootbox.js"></script>
    <script src="/Scripts/HRSProjectScript.js"></script>
    <div class="card" style="z-index: 0">
        <div class="card-header card-header-warning">
            <h3 class="card-title">แจ้งซ่อมอุปกรณ์</h3>
        </div>
        <div class="card-body table-responsive table-sm">
            <asp:HiddenField ID="txtRef" runat="server" />
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group bmd-form-group">
                        <label class="bmd-label-floating">ด่านฯ</label>
                        <asp:DropDownList ID="txtCpoint" runat="server" CssClass="form-control custom-select"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group bmd-form-group">
                        <label class="bmd-label-floating">Annex</label>
                        <asp:TextBox ID="txtPoint" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group bmd-form-group">
                        <label class="bmd-label-floating">ช่องทาง</label>
                        <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group bmd-form-group">
                        <label class="bmd-label-floating">วันที่แจ้งซ่อม</label>
                        <asp:TextBox ID="txtSDate" runat="server" CssClass="form-control datepicker" />
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group bmd-form-group">
                        <label class="bmd-label-floating">เวลา</label>
                        <asp:TextBox ID="txtSTime" runat="server" CssClass="form-control time" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group bmd-form-group">
                        <label class="bmd-label-floating">อุปกรณ์</label>
                        <asp:DropDownList ID="txtDeviceAdd" runat="server" CssClass="combobox form-control custom-select" />
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group bmd-form-group">
                        <label class="bmd-label-floating">ปัญหา/อาการ</label>
                        <asp:TextBox ID="txtProblem" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <br />
                    <label class="bmd-label-floating">แนบไฟล์อุปกรณ์ที่เสียหาย</label>
                    <asp:FileUpload ID="fileImg" runat="server" CssClass="custom-file" lang="en" />
                </div>
                <div class="col-md-5">
                    <div class="form-group bmd-form-group">
                        <label class="bmd-label-floating">หมายเหตุ</label>
                        <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md text-center">
                    <asp:LinkButton ID="btnSaveCM" runat="server" Font-Size="20px" CssClass="btn btn-success btn-sm" OnClientClick="return CompareConfirm('ยืนยัน แจ้งซ่อมอุปกรณ์ ใช่หรือไม่');" OnClick="btnSaveCM_Click">แจ้งซ่อม</asp:LinkButton>
                    <asp:LinkButton ID="btnEditCM" runat="server" Font-Size="20px" CssClass="btn btn-warning btn-sm" OnClientClick="return CompareConfirm('ยืนยัน แก้ไขแจ้งซ่อมอุปกรณ์ ใช่หรือไม่');" OnClick="btnEditCM_Click">แก้ไขแจ้งซ่อม</asp:LinkButton>
                    <asp:LinkButton ID="btnCancelCM" runat="server" Font-Size="20px" CssClass="btn btn-dark btn-sm" OnClick="btnCancelCM_Click">ยกเลิก</asp:LinkButton>
                    <asp:LinkButton ID="btnDeleteCM" runat="server" Font-Size="20px" CssClass="btn btn-danger btn-sm" OnClientClick="return CompareConfirm('ยืนยัน ลบข้อมูลการแจ้งซ่อมอุปกรณ์ ใช่หรือไม่');" OnClick="btnDeleteCM_Click">ลบข้อมูลการแจ้งซ่อม</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <br />

    <div id="DivCMGridView" runat="server" class="col-12">
        <div class="card" style="z-index: 0">
            <div class="card-header card-header-warning">
                <h3 class="card-title">รายการแจ้งซ่อมอุปกรณ์</h3>
            </div>
            <div class="card-body table-responsive table-sm">
                <div class="row">
                    <div class="col-md">
                        <label class="bmd-label-floating">ด่านฯ : </label>
                        <asp:DropDownList ID="txtCpointSearch" runat="server" CssClass="form-control custom-select" AutoPostBack="true" OnSelectedIndexChanged="txtCpointSearch_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="550px">
                    <asp:GridView ID="CMGridView" runat="server"
                        AutoGenerateColumns="False" CssClass="col table table-striped table-hover"
                        HeaderStyle-CssClass="text-center" HeaderStyle-BackColor="ActiveBorder" RowStyle-CssClass="text-center"
                        OnRowDataBound="CMGridView_RowDataBound" Font-Size="19px" CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="1">
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
                            <asp:TemplateField HeaderText="เวลาแจ้งซ่อม" ControlStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="lbSTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cm_detail_stime")+" น." %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สถานะ" ControlStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:Label ID="lbStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="หมายเหตุ" ControlStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lbNote" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cm_detail_note") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="แก้ไข" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditCM" runat="server" CssClass="fas text-warning" OnCommand="btnEdit_Command">&#xf303;</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" CssClass="text-center" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" CssClass="text-center" />
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
