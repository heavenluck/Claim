<%@ Page Title="งานอุบัติเหตุ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="claimForm.aspx.cs" Inherits="ClaimProject.Claim.claimForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/jquery-ui-1.11.4.custom.css" rel="stylesheet" />
    <div class="row">
        <div class="col-md">
            <asp:LinkButton ID="btnAddClaim" runat="server" CssClass="btn btn-info btn-sm fa" Font-Size="Medium" OnClick="btnAddClaim_Click">&#xf0a2; แจ้งอุบัติเหตุ</asp:LinkButton>
        </div>
    </div>
    <div class="card" style="z-index: 0">
        <div class="card-header card-header-warning">
            <h3 class="card-title">รายการอุบัติเหตุ</h3>
        </div>
        <div class="card-body table-responsive table-sm">
            <div class="row">
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label1" runat="server" Text="ปีงบประมาณ : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="txtSearchYear" runat="server" CssClass="form-control">
                        <asp:ListItem>2562</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label2" runat="server" Text="เลขที่ออกหนังสือเจ้าหน้าที่คอม : "></asp:Label>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="txtSearchComNumber" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1 text-right">
                    <asp:Label ID="Label3" runat="server" Text="ชื่อเรื่อง : "></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSearchComTitle" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label6" runat="server" Text="ด่านฯ : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="txtSearchCpoint" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-1 text-right">
                    <asp:Label ID="Label4" runat="server" Text="วันที่เกิดเหตุ : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSearchDate" runat="server" CssClass="form-control" ToolTip="รูปแบบ เช่น 01-11-2562"></asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <asp:Label ID="Label5" runat="server" Text="สถานะ : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="txtSearchStatus" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md text-center">
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-dark fa" Font-Size="Medium" OnClick="btnSearch_Click">&#xf002; ค้นหา</asp:LinkButton>
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
                PagerSettings-Mode="NumericFirstLast" HeaderStyle-Font-Bold="true"
                OnRowEditing="ClaimGridView_RowEditing"
                OnRowCancelingEdit="ClaimGridView_RowCancelingEdit"
                OnRowUpdating="ClaimGridView_RowUpdating"
                OnRowDeleting="ClaimGridView_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="ด่านฯ">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbCpoint" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cpoint_name") %>' CssClass="links-horizontal" OnCommand="lbNoteCom_Command"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="เลขที่บันทึกข้อความจากเจ้าหน้าที่คอม">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbNoteCom" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.claim_cpoint_note") %>' CssClass="links-horizontal" OnCommand="lbNoteCom_Command"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ชื่อเรื่อง">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbEquipment" runat="server" Text='<%# new ClaimProject.Config.ClaimFunction().ShortText(DataBinder.Eval(Container, "DataItem.claim_equipment").ToString()) %>' OnCommand="lbNoteCom_Command" CssClass="links-horizontal"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="วันที่เกิดเหตุ">
                        <ItemTemplate>
                            <asp:Label ID="lbStartDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="นับเวลา">
                        <ItemTemplate>
                            <asp:Label ID="lbDay" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="สถานะ" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lbStatus" Font-Size="16px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.status_name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="txtStatusEdit" runat="server" CssClass="form-control"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="แจ้งโดย">
                        <ItemTemplate>
                            <asp:Label ID="lbUser" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="พิมพ์">
                        <ItemTemplate>
                            <asp:LinkButton ID="printReport1" runat="server" CssClass="btn btn-sm btn-outline-info" Font-Size="15px" ToolTip="รายงานเบื้องต้น" OnCommand="printReport1_Command"><i class="fa">&#xf02f;</i></asp:LinkButton>
                            <asp:LinkButton ID="printReport2" runat="server" CssClass="btn btn-sm btn-outline-success" Font-Size="15px" ToolTip="รายงานตัวเต็ม" OnCommand="printReport2_Command"><i class="fa">&#xf02f;</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
            <asp:Label ID="lbClaimNull" runat="server" Text=""></asp:Label>
        </div>
    </div>

    <!-- -------------------------------------------------------------------- -->
    <div class="modal fade" id="NoteModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">พิมพ์บันทึกข้อความปะหน้างานอุบัติเหตุ
                        <asp:Label ID="claimID" runat="server" Text=""></asp:Label></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="font-size: medium;">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">เรื่อง</label>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control form-control-sm" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">เลขที่เอกสาร</label>
                                <asp:TextBox ID="txtDocNum" runat="server" CssClass="form-control form-control-sm" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">เรียน เช่น ผจท. ผ่าน หจ.จท.1 </label>
                                <asp:TextBox ID="txtNoteTo" runat="server" CssClass="form-control form-control-sm" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">ลงวันที่</label>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datepicker form-control-sm" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md">
                            1. บันทึกข้อความ
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo1" runat="server" CssClass="form-control text-center form-control-sm" ToolTip="ถ้าไม่มีให้ใส่ 0" />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            2. สำเนาบันทึกการเปรียบเที่ยบปรับ
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo2" runat="server" CssClass="form-control text-center form-control-sm"  ToolTip="ถ้าไม่มีให้ใส่ 0" />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            3. สำเนาใบเสร็จค่าปรับ
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo3" runat="server" CssClass="form-control text-center form-control-sm"  ToolTip="ถ้าไม่มีให้ใส่ 0" />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            4. บันทึกข้อมูลการเกิดอุบัติเหตุถเบื้องต้นสำหรับการแจ้งความ
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo4" runat="server" CssClass="form-control text-center form-control-sm"  ToolTip="ถ้าไม่มีให้ใส่ 0" />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            5. รายงานอุบัติเหตุบนทางหลวง (ส.3-02) จำนวน 
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo5" runat="server" CssClass="form-control text-center form-control-sm" ToolTip="ถ้าไม่มีให้ใส่ 0"  />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            6. สำเนารายงานประจำวันเกี่ยวกับคดี
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo6" runat="server" CssClass="form-control text-center form-control-sm"  ToolTip="ถ้าไม่มีให้ใส่ 0" />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            7. ข้อมูลเบื้องต้นจากการสอบปากคำผู้เกี่ยวข้อง สป.11 จำนวน
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo7" runat="server" CssClass="form-control text-center form-control-sm"  ToolTip="ถ้าไม่มีให้ใส่ 0" />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            8. หนังสือยอมความรับผิด
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo8" runat="server" CssClass="form-control text-center form-control-sm"  ToolTip="ถ้าไม่มีให้ใส่ 0" />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            9. สำเนาบัตรประจำตัวประชาชน
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo9" runat="server" CssClass="form-control text-center form-control-sm"  ToolTip="ถ้าไม่มีให้ใส่ 0" />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            10. สำเนาใบอนุญาตขับรถ
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo10" runat="server" CssClass="form-control text-center form-control-sm"  ToolTip="ถ้าไม่มีให้ใส่ 0" />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            11. สำเนาใบรับรองความเสียหายต่อทรัพย์สิน (ใบเคลมประกัน)
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo11" runat="server" CssClass="form-control text-center form-control-sm"  ToolTip="ถ้าไม่มีให้ใส่ 0" />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            12. บันทึกข้อความรายงานของ พ.ควบคุมระบบ และรองผจด.ประจำผลัด
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo12" runat="server" CssClass="form-control text-center form-control-sm"  ToolTip="ถ้าไม่มีให้ใส่ 0" />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            13. รูปภาพประกอบ
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtNo13" runat="server" CssClass="form-control text-center form-control-sm"  ToolTip="ถ้าไม่มีให้ใส่ 0" />
                        </div>
                        <div class="col-md-1 text-left">
                            ฉบับ
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md text-center">
                            <asp:LinkButton ID="btnSavePrint" runat="server" Font-Size="20px" CssClass="btn btn-success btn-sm" OnClick="btnSavePrint_Click">พิมพ์</asp:LinkButton>
                        </div>
                    </div>
                </div>
        </div>
    </div>

    <!-- -------------------------------------------------------------------- -->

    <script src="/Scripts/jquery-ui-1.11.4.custom.js"></script>
    <script src="/Scripts/moment.min.js"></script>
    <script src="/Scripts/ClaimProjectScript.js"></script>
    <script type="text/javascript">   
        $(function () {
            <% if (claim_id != "")
        {%>
            $("#NoteModal").modal('show');
            <%}
        else
        {%>
            $("#NoteModal").modal('hide');
            <%}%>
            $(".datepicker").datepicker($.datepicker.regional["th"]);
            //$(".datepicker").datepicker("setDate", new Date());
        });

        function ClickNote() {
            $("#NoteModal").modal('show');

        }
    </script>

</asp:Content>
