<%@ Page Title="เปลี่ยนรหัสผ่าน" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserForm.aspx.cs" Inherits="ClaimProject.User.UserForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Scripts/bootbox.js"></script>
    <div class="row">
    </div>
    <div class="row">
        <table class="table">
            <tbody>
                <tr>
                    <td style="text-align: right">Username : </td>
                    <td style="text-align: left"><%=Session["User"].ToString() %></td>
                </tr>
                <tr>
                    <td style="text-align: right">ชื่อ-สกุล : </td>
                    <td style="text-align: left"><%=Session["UserName"].ToString() %></td>
                </tr>
                <tr>
                    <td style="text-align: right">สิทธิ์การใช้งาน : </td>
                    <td style="text-align: left"><%=Session["UserPrivilege"].ToString() %></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="align-left">
                        <asp:Button ID="btnChangPass" runat="server" Text="เปลี่ยนรหัสผ่าน" CssClass="btn btn-success" data-toggle="modal" data-target="#ModalChangPass" OnClientClick="return false;" /></td>
                </tr>
            </tbody>
        </table>
    </div>
    <!-- -------------------------------------------------------------------------- -->
    <div class="modal fade" id="ModalChangPass" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">เปลี่ยนรหัสผ่าน</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md">
                            <label class="col-form-label">รหัสผ่านใหม่ </label>
                            <asp:TextBox ID="txtNewPass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <label class="col-form-label">ยืนยันรหัสผ่านใหม่ </label>
                            <asp:TextBox ID="txtConfirmNewPass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">ยกเลิก</button>
                    <asp:Button ID="btnConfirmPass" runat="server" Text="ยืนยัน" CssClass="btn btn-warning" OnClick="btnConfirmPass_Click" />
                </div>
            </div>
        </div>
</asp:Content>
