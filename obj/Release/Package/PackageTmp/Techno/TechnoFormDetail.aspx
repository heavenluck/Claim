<%@ Page Title="รายละเอียดอุบัติเหตุ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TechnoFormDetail.aspx.cs" Inherits="ClaimProject.Techno.TechnoFormDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header card-header-warning">
            <div class="col-md-2">
                <h3 class="card-title">รายละเอียดอุบัติเหตุ</h3>
            </div>
        </div>
        <div class="card-body table-responsive">

            <div class="row">
                <div class="col-md-2 text-right"></div>
                <div class="col-md">
                    <h3>
                        <asp:Label ID="lbTitleStatus" runat="server" Text="Label"></asp:Label></h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">เรื่อง : </div>
                <div class="col-md">
                    <asp:Label ID="lbTitle" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">ด่านฯ : </div>
                <div class="col-md-2">
                    <asp:Label ID="lbCpoint" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="col-md-1 text-right">วันที่ : </div>
                <div class="col-md-3">
                    <asp:Label ID="lbDate" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lbAround" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">ได้รับแจ้งจาก : </div>
                <div class="col-md">
                    <asp:Label ID="lbAlert" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">เกิดอุบัติเหตุตู้ : </div>
                <div class="col-md">
                    <asp:Label ID="lbCb" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">รายละเอียด : </div>
                <div class="col-md">
                    <asp:Label ID="lbDetail" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2 text-right">คู่กรณี : </div>
                <div class="col-md-3">
                    <asp:Label ID="lbCar" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="col-md-2 text-right">หมายเลขทะเบียน : </div>
                <div class="col-md">
                    <asp:Label ID="lbLP" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">ชื่อผู้ขับขี่ : </div>
                <div class="col-md-3">
                    <asp:Label ID="lbDriver" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="col-md-2 text-right">หมายเลขบัตรประชาชน : </div>
                <div class="col-md">
                    <asp:Label ID="lbIDCard" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">ที่อยู่ : </div>
                <div class="col-md">
                    <asp:Label ID="lbAddress" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">เบอร์โทรศัพท์ : </div>
                <div class="col-md-3">
                    <asp:Label ID="lbTel" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2 text-right">ทำประกันไว้กับ : </div>
                <div class="col-md">
                    <asp:Label ID="lbInsure" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">หมายเลขเคลม : </div>
                <div class="col-md-3">
                    <asp:Label ID="lbClaimNum" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="col-md-2 text-right">หมายเลขกรมธรรม์ : </div>
                <div class="col-md">
                    <asp:Label ID="lbPolicy" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 text-right">แจ้งความไว้ที่ : </div>
                <div class="col-md-3">
                    <asp:Label ID="lbInform" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-3 text-right">รายการอุปกรณ์ที่ได้รับความเสียหาย : </div>
                <div class="col-md">
                    <asp:Label ID="lbDevice" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-3 text-right">ผู้ควบคุม : </div>
                <div class="col-md">
                    <asp:Label ID="lbEmp" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 text-right">เจ้าหน้าที่คอม : </div>
                <div class="col-md">
                    <asp:Label ID="lbEmpCom" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <br />
            <hr />
            <div runat="server" id="Div2">
                <div class="row">
                    <div class="col-md">
                        <h3>ออกหนังสือส่งกองทางหลวงพิเศษระหว่างเมือง</h3>
                    </div>
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-3 text-right">เลขที่หนังสือ : </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtNoteNumTo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3 text-right">วันที่ : </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtDateNoteto" runat="server" AutoPostBack="true" CssClass="form-control datepicker" OnTextChanged="txtDateNoteto_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row">
                    <div class="col-md-3 text-right">เรื่อง : </div>
                    <div class="col-md">
                        <asp:TextBox ID="txtNoteTitleTo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 text-right">เรียน : </div>
                    <div class="col-md">
                        <asp:TextBox ID="txtNoteSendTo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md">
                        <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md text-center">
                        <asp:Button ID="btnSaveNoteTo" CssClass="btn btn-info btn-sm" Font-Size="20px" runat="server" Text="บันทึก" OnClick="btnSaveNoteTo_Click" />
                        <asp:Button ID="btnNoteTo" CssClass="btn btn-info btn-sm" Font-Size="20px" runat="server" Text="พิมพ์ตัวจริง" OnClick="btnNoteTo_Click" />
                        <asp:Button ID="btnNoteToCpoy" CssClass="btn btn-info btn-sm" Font-Size="20px" runat="server" Text="พิมพ์สำเนา" OnClick="btnNoteToCpoy_Click" />
                    </div>
                </div>
                <hr />
            </div>
           <div runat="server" id="Div1">
                <div class="row">
                    <div class="col-md-3">
                        <h3>รายการใบเสนอราคา</h3>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md">
                        <asp:GridView ID="QuotaGridView" runat="server"
                            DataKeyNames="quotations_id"
                            GridLines="None"
                            OnRowDataBound="QuotaGridView_RowDataBound"
                            AutoGenerateColumns="False"
                            CssClass="table table-hover table-sm"
                            HeaderStyle-Font-Bold="true">
                            <Columns>
                                <asp:TemplateField HeaderText="ชื่อบริษัท">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCompany" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.company_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เลขที่หนังสือ">
                                    <ItemTemplate>
                                        <asp:Label ID="lbNote" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.quotations_note_number") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่ออกหนังสือ">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDateSend" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.quotations_date_send") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="พิมพ์หนังสือ">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPrint" CssClass="text-info fa" runat="server" Font-Size="Larger" OnCommand="btnPrint_Command" ToolTip="ตัวจริง">&#xf02f;</asp:LinkButton>
                                        <asp:LinkButton ID="btnPrint2" CssClass="text-dark fa" runat="server" Font-Size="Larger" OnCommand="btnPrint2_Command" ToolTip="สำเนา">&#xf02f;</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ลงรับใบเสนอราคา" ControlStyle-CssClass="text-center text-warning fa">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" Font-Size="Larger" OnCommand="btnEdit_Command">&#xf044;</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ราคาที่บริษัทเสนอ">
                                    <ItemTemplate>
                                        <asp:Label ID="lbPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.quotations_company_price") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่ได้รับใบเสนอราคา">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDateRecive" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.quotations_date_recive") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เอกสารแนบ">
                                    <ItemTemplate>
                                        <asp:Image ID="DocClaim" runat="server" Width="100px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDocDownload" runat="server" Font-Size="Larger" CssClass="fa" OnCommand="btnDocDownload_Command">&#xf0ed;</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
               <hr />
            </div>
            <div runat="server" id="Div3">
                <div class="row">
                    <div class="col-md">
                        <h3>ข้อมูลใบสั่งจ้าง</h3>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 text-right">
                        บริษัทที่ได้งาน : 
                    </div>
                    <div class="col-md">
                        <asp:Label ID="lbCompanyOrder" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 text-right">
                        ราคาจ้าง : 
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lbPriceOrder" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="col-md-1 text-right">
                        วันที่สั่งจ้าง : 
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lbDateOrderStart" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 text-right">
                        กำหนดแล้วเสร็จภายใน : 
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lbDateOrderEnd" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 text-right">
                        ใบสั่งจ้าง : 
                    </div>
                    <div class="col-md-2">
                        <asp:Image ID="ImageOrder" runat="server" Width="300px" />
                        <asp:LinkButton ID="btnDownloadOrder" CssClass="btn btn-outline-info btn-sm" Font-Size="15px" runat="server" OnClick="btnDownloadOrder_Click">ดาวน์โหลด</asp:LinkButton>
                    </div>
                </div>
                <hr />
            </div>
            <div runat="server" id="Div4">
                <div class="row">
                    <div class="col-md">
                        <h3>ข้อมูลใบส่งงาน/เสร็จสิ้น</h3>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 text-right">
                        วันที่ส่งงาน : 
                    </div>
                    <div class="col-md">
                        <asp:Label ID="lbDateSendOrder" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 text-right">
                        ค่าปรับ : 
                    </div>
                    <div class="col-md">
                        <asp:Label ID="lbFineOrder" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 text-right">
                        ใบส่งงาน : 
                    </div>
                    <div class="col-md-2">
                        <asp:Image ID="ImageOrderSend" runat="server" Width="300px" />
                        <asp:LinkButton ID="btnDownloadOrderSend" CssClass="btn btn-outline-info btn-sm" Font-Size="15px" runat="server" OnClick="btnDownloadOrderSend_Click">ดาวน์โหลด</asp:LinkButton>
                    </div>
                </div>
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="row text-center">
                        <div class="col-md"></div>
                        <div class="col-md-2">
                            <asp:Button ID="btns0" runat="server" CssClass="btn btn-danger" OnClick="btns0_Click" Text="ลบข้อมูล" OnClientClick="return CompareConfirm('ยืนยัน คุณต้องการลบข้อมูล ใช่หรือไม่')" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btns1" runat="server" CssClass="btn btn-dark" Text="ส่งเรื่องเข้ากองฯ" OnClick="btns2_Click" OnClientClick="return CompareConfirm('ยืนยันเปลี่ยนสถานะส่งเรื่องเข้ากองฯ ?');" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btns2" runat="server" CssClass="btn btn-warning" OnClick="btns1_Click" Text="ขอใบเสนอราคา" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btns3" runat="server" CssClass="btn btn-primary" Text="อยู่ระหว่างซ่อม" OnClick="btns3_Click" OnClientClick="return CompareConfirm('ยืนยันเปลี่ยนเป็นอยู่ระหว่างซ่อม ?');" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btns4" runat="server" CssClass="btn btn-success" Text="ส่งงาน/เสร็จสิ้น" OnClick="btns4_Click" OnClientClick="return CompareConfirm('ยืนยันเปลี่ยนส่งงาน/เสร็จสิ้น ?');" />
                        </div>
                        <div class="col-md"></div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btns0" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- Start ขอใบเสนอราคา -->
    <div class="modal" id="QuotationsModel">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">ขอใบเสนอราคา</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-3 text-right">ชื่อบริษัท : </div>
                                <div class="col-md">
                                    <asp:DropDownList ID="txtCompany" runat="server" CssClass="form-control custom-control col-md-6"></asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3 text-right">เลขที่หนังสือ : </div>
                                <div class="col-md">
                                    <asp:TextBox ID="txtNoteNumber" runat="server" CssClass="form-control col-md-2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md text-center">
                                    <asp:Button ID="btnSaveQuotations" runat="server" Font-Size="20px" CssClass="btn btn-warning btn-sm" Text="ส่งเสนอราคา" OnClick="btnSaveQuotations_Click" />
                                </div>
                            </div>
                            <hr />
                            <asp:GridView ID="QuotationsGridView" runat="server"
                                DataKeyNames="quotations_id"
                                GridLines="None"
                                AutoGenerateColumns="False"
                                CssClass="table table-hover table-sm"
                                OnRowDataBound="QuotationsGridView_RowDataBound"
                                OnRowEditing="QuotationsGridView_RowEditing"
                                OnRowCancelingEdit="QuotationsGridView_RowCancelingEdit"
                                OnRowUpdating="QuotationsGridView_RowUpdating"
                                OnRowDeleting="QuotationsGridView_RowDeleting"
                                HeaderStyle-Font-Bold="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="ชื่อบริษัท">
                                        <ItemTemplate>
                                            <asp:Label ID="lbCompany" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.company_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขที่หนังสือ">
                                        <ItemTemplate>
                                            <asp:Label ID="lbNote" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.quotations_note_number") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtENote" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.quotations_note_number") %>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" CancelText="ยกเลิก" EditText="&#xf040; แก้ไข" UpdateText="แก้ไข" HeaderText="แก้ไข" ControlStyle-Font-Size="Small" ControlStyle-CssClass="fa" />
                                    <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="fa" ControlStyle-Font-Size="Small" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSaveQuotations" />
                            <asp:PostBackTrigger ControlID="btns2" />
                            <asp:PostBackTrigger ControlID="btns3" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <!-- End ขอใบเสนอราคา -->
    <!-- Start ลงรับใบเสนอราคา -->
    <div class="modal" id="ReciveQuotationsModel">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">ลงรับใบเสนอราคา</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4 text-right">บริษัท : </div>
                        <div class="col-md">
                            <asp:Label ID="lbCompany" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">วันที่รับใบเสนอะราคา : </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDateRecive" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">ราคาที่บริษัทเสนอ : </div>
                        <div class="col-md">
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-right">แนบรูปภาพ : </div>
                        <div class="col-md">
                            <div class="custom-file">
                                <label class="custom-file-label" for="customFile">เลือกไฟล์</label>
                                <asp:FileUpload ID="fileDoc" runat="server" CssClass="custom-file-input" lang="en" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md text-center">
                            <asp:Button ID="btnSaveRecive" runat="server" Font-Size="20px" CssClass="btn btn-warning btn-sm" Text="ส่งเสนอราคา" OnClick="btnSaveRecive_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End ลงรับใบเสนอราคา -->
    <!-- Start อยู่ระหว่างซ่อม -->
    <div class="modal" id="WaitQuotationsModel">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title">ข้อมูลใบสั่งจ้าง</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-3 text-right">บริษัท : </div>
                                <div class="col-md">
                                    <asp:DropDownList ID="txtCompanyOrder" AutoPostBack="true" runat="server" CssClass="form-control custom-control" OnSelectedIndexChanged="txtCompanyOrder_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3 text-right">ราคาจ้าง : </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPriceOrder" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="Label3" runat="server" Text=" บาท"></asp:Label>
                                </div>
                            </div>
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-3 text-right">วันที่จ้าง : </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDateOrder" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4 text-right">กำหนดส่งงานภายใน : </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtSendOrder" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" Text=" วัน"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-3 text-right">แนบรูปภาพ : </div>
                        <div class="col-md-5">
                            <div class="custom-file">
                                <label class="custom-file-label" for="customFile">เลือกไฟล์</label>
                                <asp:FileUpload ID="FileOrder" runat="server" CssClass="custom-file-input" lang="en" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md text-center">
                            <asp:Button ID="btnSaveOrder" runat="server" Font-Size="20px" CssClass="btn btn-warning btn-sm" Text="บันทึก" OnClick="btnSaveOrder_Click" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <!-- End อยู่ระหว่างซ่อม -->
    <!-- Start ส่งงานเสร็จสิ้น -->
    <div class="modal" id="SuccessQuotationsModel">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">ส่งงาน/เสร็จสิ้น</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-4 text-right">วันที่ส่งงาน : </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtDateSendOrder" AutoPostBack="true" runat="server" CssClass="form-control datepicker" OnTextChanged="txtDateSendOrder_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" runat="server" id="DivFine">
                                <div class="col-md-4 text-right">ค่าปรับ : </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtFine" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:Label ID="Label2" runat="server" Text=" บาท"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-4 text-right">แนบรูปภาพ : </div>
                        <div class="col-md">
                            <div class="custom-file">
                                <label class="custom-file-label" for="customFile">เลือกไฟล์</label>
                                <asp:FileUpload ID="FileUploadSendDoc" runat="server" CssClass="custom-file-input" lang="en" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md text-center">
                            <asp:Button ID="btnSaveSendDoc" runat="server" Font-Size="20px" CssClass="btn btn-warning btn-sm" Text="เสร็จสิ้น" OnClick="btnSaveSendDoc_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End ส่งงานเสร็จสิ้น -->
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
    </script>
</asp:Content>
