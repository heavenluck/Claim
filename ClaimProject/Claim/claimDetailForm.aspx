<%@ Page Title="รายการอุบัติเหตุ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="claimDetailForm.aspx.cs" Inherits="ClaimProject.Claim.claimDetailForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Scripts/bootbox.js"></script>
    <script src="/Scripts/HRSProjectScript.js"></script>
    <div class="tab-content">
        <div class="card" style="font-size: 19px; z-index: 0;" runat="server" id="cardBody">
            <div class="card-header card-header-warning">
                <h3 class="card-title">รายการอุบัติเหตุ</h3>
            </div>
            <div class="card-body table-responsive">
                <div runat="server" id="divCom">
                    <h3 class="card-title alert-warning">รายการละเอียดการเกิดอุบัติเหตุ (เจ้าหน้าที่คอม)</h3>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group bmd-form-group">
                                <asp:DropDownList ID="txtCpoint" runat="server" CssClass="form-control custom-select"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">Annex </label>
                                <asp:TextBox ID="txtPoint" runat="server" CssClass="form-control" ToolTip="Annex เช่น 1 หรือ 2 หรือเว้นว่าง" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">เลขที่บันทึกเจ้าหน้าที่คอม 4 หลัก</label>
                                <asp:TextBox ID="txtCpointNote" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">ลงวันที่</label>
                                <asp:TextBox ID="txtCpointDate" runat="server" CssClass="form-control datepicker" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">ชื่อเรื่อง : เช่น อุบัติเหตุรถชนไม้คานกั้นอัตโนมัติ ALB ตู้ EN ๐๑ </label>
                                <asp:TextBox ID="txtEquipment" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <!--</div>
            <div class="row">-->
                        <div class="col-md-2">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">วันที่เกิดเหตุ</label>
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control datepicker" />
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">เวลา</label>
                                <asp:TextBox ID="txtTime" runat="server" CssClass="form-control" ToolTip="เวลา เช่น 10.30 ไม่ต้องใส่ น." />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group bmd-form-group">
                                <asp:DropDownList ID="txtAround" runat="server" CssClass="form-control custom-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">เรียน เช่น ผจท. ผ่าน ผจด.</label>
                                <asp:TextBox ID="txtNoteTo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <!--</div>
            <div class="row">-->
                        <div class="col-md-3">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">ได้รับแจ้งจาก</label>
                                <asp:TextBox ID="txtNameAleat" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group bmd-form-group">
                                <asp:DropDownList ID="txtPosAleat" runat="server" CssClass="form-control custom-select"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <label class="bmd-label-floating">ประจำตู้</label>
                            <asp:DropDownList ID="txtCB" runat="server" CssClass="combobox form-control"></asp:DropDownList>
                        </div>
                        <!--</div>
            <div class="row">-->
                        <div class="col-md-4">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">แจ้งว่าเกิดอุบัติเหตุ...</label>
                                <asp:TextBox ID="txtDetail" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label class="bmd-label-floating">ตู้ที่เกิดอุบัติเหตุ</label>
                            <asp:DropDownList ID="txtCBClaim" runat="server" CssClass="combobox form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">ฝั่ง เช่น ขาเข้าระบบ หรือ ขาออกระบบ</label>
                                <asp:TextBox ID="txtDirection" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">รถคู่กรณีเป็นรถ ยี่ห้อ สี</label>
                                <asp:TextBox ID="txtCar" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">หมายเลขทะเบียน</label>
                                <asp:TextBox ID="txtLicensePlate" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">หมายเลขทะเบียนส่วงพ่วง</label>
                                <asp:TextBox ID="txtLp2" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">จังหวัด</label>
                                <asp:TextBox ID="txtProvince" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">วิ่งมาจาก</label>
                                <asp:TextBox ID="txtComeFrom" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">มุ่งหน้า</label>
                                <asp:TextBox ID="txtDirectionIn" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <!--</div>
            <div class="row">-->
                        <div class="col-md-3">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">ขับขี่โดย</label>
                                <asp:TextBox ID="txtNameDrive" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">เลขที่บัตรประจำตัวประชาชน 13 หลัก</label>
                                <asp:TextBox ID="txtIdcard" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <!--</div>
            <div class="row">-->
                        <div class="col-md-3">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">เบอร์โทรคู่กรณี</label>
                                <asp:TextBox ID="txtTelDrive" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">ที่อยู่คู่กรณี</label>
                                <asp:TextBox ID="txtAddressDriver" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <hr />
                </div>
                <div id="divSup" runat="server">
                    <h3 class="card-title alert-warning">รายการละเอียดอุบัติเหตุ (รองผู้จัดการด่านฯ)</h3>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">บริษัทประกันภัย</label>
                                <asp:TextBox ID="txtInsurer" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">หมายเลขกรมธรรม์</label>
                                <asp:TextBox ID="txtPolicyholders" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">หมายเลขเคลม</label>
                                <asp:TextBox ID="txtClemence" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating">สถานีตรวจที่แจ้งความ</label>
                                <asp:TextBox ID="txtInform" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <hr />
                </div>
                <h3 class="card-title alert-warning">พนักงานที่ปฏิบัติงาน</h3>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group bmd-form-group">
                            <label class="bmd-label-floating">รองผู้จัดการด่านฯ ประจำผลัด</label>
                            <asp:TextBox ID="txtSup" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group bmd-form-group">
                            <asp:DropDownList ID="txtPosSup" runat="server" CssClass="form-control custom-select"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group bmd-form-group">
                            <label class="bmd-label-floating">พนักงานควบคุมระบบที่ปฏิบัติหน้าที่</label>
                            <asp:TextBox ID="txtComName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group bmd-form-group">
                            <asp:DropDownList ID="txtPosCom" runat="server" CssClass="form-control custom-select"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="btnAddCom" runat="server" Text="&#xf234; เพิ่ม พ.ควบคุมระบบที่ปฏิบัติหน้าที่" CssClass="btn btn-success btn-sm fa" OnClick="btnAddCom_Click"></asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5">
                        <asp:GridView ID="ComGridView" runat="server"
                            DataKeyNames="com_working_id"
                            GridLines="None"
                            OnRowDataBound="ComGridView_RowDataBound"
                            AutoGenerateColumns="False"
                            CssClass="table table-hover table-sm"
                            OnRowDeleting="ComGridView_RowDeleting" HeaderStyle-Font-Bold="true" RowStyle-CssClass="table-success">
                            <Columns>
                                <asp:TemplateField HeaderText="ชื่อ-สกุล">
                                    <ItemTemplate>
                                        <asp:Label ID="lbUser" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.com_working_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ตำแหน่ง">
                                    <ItemTemplate>
                                        <asp:Label ID="lbUser" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.com_working_pos") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="fa text-danger" ControlStyle-Font-Size="Small" />
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="LabelCom" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <hr />
                <h3 class="card-title alert-warning">รายการอุปกรณ์ที่ได้รับความเสียหาย</h3>
                <div class="row">
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="bmd-label-floating">อุปกรณ์ที่ได้รับความเสียหาย</label>
                            <asp:DropDownList ID="txtDevice" runat="server" CssClass="combobox form-control custom-select"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form">
                            <br />
                            <label class="bmd-label-floating">ความเสียหาย</label>
                            <asp:TextBox ID="txtDeviceBroken" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-2 text-right">
                        <br />
                        <br />
                        <asp:LinkButton ID="btnAddDeviceBroken" runat="server" Text="&#xf067; เพิ่มอุปกรณ์ที่ได้รับความเสียหาย" Font-Size="Small" CssClass="btn btn-success btn-sm fa" OnClick="btnAddDeviceBroken_Click" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-5">
                        <asp:GridView ID="DeviceGridView" runat="server"
                            DataKeyNames="device_damaged_id"
                            GridLines="None"
                            OnRowDataBound="DeviceGridView_RowDataBound"
                            AutoGenerateColumns="False"
                            CssClass="table table-hover table-sm"
                            OnRowDeleting="DeviceGridView_RowDeleting" HeaderStyle-Font-Bold="true" RowStyle-CssClass="table-danger">
                            <Columns>
                                <asp:TemplateField HeaderText="ชื่ออุปกรณ์ที่ได้รับความเสียหาย">
                                    <ItemTemplate>
                                        <asp:Label ID="lbUser" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.device_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ความเสียหาย">
                                    <ItemTemplate>
                                        <asp:Label ID="lbUser" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.device_damaged") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="fa text-danger" ControlStyle-Font-Size="Small" />
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lbClaimDetailNull" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <hr />
                <div class="row card-title alert-warning">
                    <div class="col-md-3">
                        <h3>แนบรูปภาพประกอบ</h3>
                    </div>
                </div>
                <h5 class="text-danger">เช่น รูปภาพความเสียหาย รูปภาพรถคู่กรณี</h5>
                <div class="row">
                    <div class="col-md-3">
                        <div class="custom-file">
                            <label class="custom-file-label" for="customFile">เลือกไฟล์</label>
                            <asp:FileUpload ID="fileImg" runat="server" CssClass="custom-file-input" lang="en" />
                        </div>
                    </div>
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnAddImg" runat="server" Text="&#xf0c6; แนบ" Font-Size="Small" CssClass="btn btn-success btn-sm fa" OnClick="btnAddImg_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5">
                        <asp:GridView ID="FileGridView" runat="server"
                            DataKeyNames="claim_img_id"
                            GridLines="None"
                            OnRowDataBound="FileGridView_RowDataBound"
                            AutoGenerateColumns="False"
                            CssClass="table table-hover table-sm"
                            OnRowDeleting="FileGridView_RowDeleting" HeaderStyle-Font-Bold="true" RowStyle-CssClass="table-success">
                            <Columns>
                                <asp:TemplateField HeaderText="รูปภาพประกอบ">
                                    <ItemTemplate>
                                        <asp:Image ID="ImgClaim" runat="server" Width="200px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDownload" runat="server" Font-Size="Small" CssClass="fa" OnCommand="btnDownload_Command">&#xf0ed; Download</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="fa text-danger" ControlStyle-Font-Size="Small" />
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="LabelImg" runat="server" Text="Label"></asp:Label>
                        <!--<asp:LinkButton ID="btnReportImg" runat="server" Text="&#xf02f; พิมพ์รูปภาพประกอบ" Font-Size="Large" CssClass="btn btn-dark fa btn-sm" OnClick="btnReportImg_Click" />-->
                    </div>
                </div>

                <hr />

                <div class="row card-title alert-warning">
                    <div class="col-md-5">
                        <h3>แนบรูปภาพเอกสารประกอบ</h3>
                    </div>
                </div>
                <h5 class="text-danger">เช่น สำเนาบัตรประจำตัวประชาชน สำเนาใบขับขี่ สำเนาใบยอมรับความผิด เอกสารที่เกี่ยวข้องอื่นๆ</h5>
                <div class="row">
                    <div class="col-md-3">
                        <div class="custom-file">
                            <label class="custom-file-label" for="customFile">เลือกไฟล์</label>
                            <asp:FileUpload ID="FileUploadDoc" runat="server" CssClass="custom-file-input" lang="en" />

                        </div>
                    </div>
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnUploadDoc" runat="server" Text="&#xf0c6; แนบ" Font-Size="Small" CssClass="btn btn-success btn-sm fa" OnClick="btnUploadDoc_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5">
                        <asp:GridView ID="UploadDocGridView" runat="server"
                            DataKeyNames="claim_img_id"
                            GridLines="None"
                            OnRowDataBound="UploadDocGridView_RowDataBound"
                            AutoGenerateColumns="False"
                            CssClass="table table-hover table-sm"
                            OnRowDeleting="UploadDocGridView_RowDeleting" HeaderStyle-Font-Bold="true" RowStyle-CssClass="table-success">
                            <Columns>
                                <asp:TemplateField HeaderText="รูปภาพประกอบ">
                                    <ItemTemplate>
                                        <asp:Image ID="DocClaim" runat="server" Width="200px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDocDownload" runat="server" Font-Size="Small" CssClass="fa" OnCommand="btnDownload_Command">&#xf0ed; Download</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="fa text-danger" ControlStyle-Font-Size="Small" />
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="LabelDoc" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="stats">
                </div>
            </div>
            <div class="row">
                <div class="col-md text-center">
                    <asp:Button ID="btnSaveReport" runat="server" Text="&#xf0c7; บันทึก" Font-Size="Large" CssClass="btn btn-info fa btn-sm" OnClick="btnSaveReport_Click"></asp:Button>
                    <asp:LinkButton ID="btnDelete" runat="server" Text="&#xf014; ลบข้อมูล" Font-Size="Large" CssClass="btn btn-danger fa btn-sm" OnClick="btnDelete_Click" />
                    <!--<asp:LinkButton ID="btnPrintNoteSup" runat="server" Text="&#xf02f; พิมพ์บันทึกข้อความ (รายงานเบื้องต้น)" Font-Size="Large" CssClass="btn btn-dark fa btn-sm" OnClick="btnPrintNoteSup_Click" />-->
                    <br />
                    &nbsp;
                </div>
            </div>
        </div>
    </div>
    <script src="/Scripts/jquery-ui-1.11.4.custom.js"></script>
    <script src="/Scripts/moment.min.js"></script>
    <script src="/Scripts/ClaimProjectScript.js"></script>
    <script type="text/javascript">   
        $(function () {
            //datepicker
        <% if (alert != "")
        { %>
            demo.showNotification('top', 'center', '<%=icon%>','<%=alertType%>', '<%=alert%>');
        <% } %>

            $(".datepicker").datepicker($.datepicker.regional["th"]);
            if ($(".datepicker").val() == "") {
                $(".datepicker").datepicker("setDate", new Date());
            }

            $(".datepicker").attr('maxlength', '10');
        });

    </script>
    <script type="text/javascript">
        $(function () {
                //
                <%
        if (Session["View"].Equals(true))
        {
                %>
            $('.tab-content input').attr('disabled', 'true');
            $('.tab-content select').attr('disabled', 'true');
            $('.tab-content textarea').attr('disabled', 'true');
            $('.tab-content a').removeAttr('href');
            $('.tab-content a').removeAttr('onclick');
            $('.tab-content a').attr('disabled', 'true');
            $('.tab-content a').hide();
            //$('.combobox').attr('disabled', 'true');
            $('.tab-content input[type=submit]').hide();
            $('.formHead').hide();
                <%
        }
            %>
        });
    </script>
</asp:Content>
