<%@ Page Title="บริษัทเข้าซ่อม CM" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CMEditForm.aspx.cs" Inherits="ClaimProject.CM.CMEditForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                            <asp:TemplateField HeaderText="อัพเดท" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnStatusUpdate" runat="server" OnCommand="btnStatusUpdate_Command" CssClass="fas text-primary">&#xf0aa;</asp:LinkButton>
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
    <div class="modal fade" id="UpdateStatusModal" tabindex="-1" role="dialog" aria-labelledby="UpdateStatusModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">บริษัทเข้าแก้ไข
                        <asp:Label ID="Label1" runat="server" Text="Label1" CssClass="text-dark"></asp:Label></h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="line-height: inherit;">
                    <div class="row" style="height: 30px">
                        <div class="col-md">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">ด่านฯ : </label>
                                <asp:Label ID="Label5" runat="server" Text="Label" CssClass="text-dark"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="height: 30px">
                        <div class="col-md">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">ช่องทาง : </label>
                                <asp:Label ID="Label2" runat="server" Text="Label" CssClass="text-dark"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="height: 30px">
                        <div class="col-md">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">อุปกรณ์ : </label>
                                <asp:Label ID="Label3" runat="server" CssClass="text-dark"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="height: 30px">
                        <div class="col-md">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">อาการ : </label>
                                <asp:Label ID="Label4" runat="server" CssClass="text-dark"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="height: 80px">
                        <div class="col-md">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">วันที่เข้าซ่อม</label>
                                <asp:TextBox ID="txtEDate" runat="server" CssClass="form-control datepicker" />
                            </div>
                        </div>
                        <div class="col-md">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">เวลา</label>
                                <asp:TextBox ID="txtETime" runat="server" CssClass="form-control time" />
                            </div>
                        </div>
                    </div>
                    <div class="row" style="height: 80px">
                        <div class="col-md">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">วิธีแก้ไข</label>
                                <asp:TextBox ID="txtMethod" runat="server" CssClass="form-control time" />
                            </div>
                        </div>
                    </div>
                    <div class="row" style="height: 80px">
                        <div class="col-md">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">หมายเหตุ</label>
                                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control time" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <br />
                            <label class="bmd-label-floating">แนบไฟล์อุปกรณ์ที่เสียหาย</label>
                            <asp:FileUpload ID="fileImg" runat="server" CssClass="custom-file" lang="en" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnUpdateCM" runat="server" CssClass="btn btn-success btn-sm" Font-Size="Medium" OnCommand="btnUpdateCM_Command" OnClientClick="return CompareConfirm('ยืนยันบันทึกข้อมูล ใช่หรือไม่');">บันทึก</asp:LinkButton>
                    <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal" style="font-size: medium">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script src="/Scripts/jquery-ui-1.11.4.custom.js"></script>
    <script src="/Scripts/moment.min.js"></script>
    <script src="/Scripts/ClaimProjectScript.js"></script>
    <script type="text/javascript">
        <%if (cm_id != "")
        {%>
        $('#UpdateStatusModal').modal("show");
        <%}
        else
        {%>
        $('#UpdateStatusModal').modal("hide");
        <%}%>
</script>
    <script type="text/javascript"> 
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

        function checkText() {
            var str1 = "1";
            var str2 = "2";

            if (str1 === str2) {
                // your logic here
                return false;
            } else {
                // your logic here
                if ($('<%=txtMethod.ClientID%>').val === "") {
                    alert("กรุณาใส่วิธีแก้ไข");
                    return false;
                } else {
                    return true;
                }
            }

        }
    </script>
</asp:Content>
