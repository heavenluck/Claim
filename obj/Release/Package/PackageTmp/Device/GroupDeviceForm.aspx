<%@ Page Title="กลุ่มอุปกรณ์" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GroupDeviceForm.aspx.cs" Inherits="ClaimProject.Device.GroupDeviceForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-row">
        <div class="col-md-3">
            ชื่อกลุ่มอุปกรณ์
            <asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3">
            Token Line
            <asp:TextBox ID="txtTokenLine" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <br />
            <asp:Button ID="btnGroupAdd" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnGroupAdd_Click" OnClientClick="return CompareConfirm('ยืนยันเพิ่มกลุ่มอุปกรณ์ ใช่หรือไม่');" />
        </div>
    </div>
    <hr />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-row">
                <div class="col-md-3">
                    ค้นหา 
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <br />
                    <asp:Button ID="btnSearch" runat="server" Text="&#xf002; ค้นหา" Font-Size="Medium" CssClass="btn btn-dark btn-sm align-items-end fa" OnClick="btnSearch_Click" />
                </div>
            </div>
            <div class="form-row">
                <div class="card">
                    <div class="card-header card-header-warning">
                        <h3 class="card-title">รายการกลุ่มอุปกรณ์</h3>
                    </div>
                    <div class="card-body table-responsive">
                        <asp:GridView ID="GroupGridView" runat="server"
                            DataKeyNames="drive_group_id"
                            GridLines="None"
                            OnRowDataBound="GroupGridView_RowDataBound"
                            AutoGenerateColumns="False"
                            CssClass="table table-hover table-sm"
                            OnRowEditing="GroupGridView_RowEditing"
                            OnRowCancelingEdit="GroupGridView_RowCancelingEdit"
                            OnRowUpdating="GroupGridView_RowUpdating"
                            OnRowDeleting="GroupGridView_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="ชื่อกลุ่มอุปกรณ์">
                                    <ItemTemplate>
                                        <asp:Label ID="lbEGroup" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.drive_group_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEGroup" size="20" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.drive_group_name") %>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Token Line">
                                    <ItemTemplate>
                                        <asp:Label ID="lbEGroupToken" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.drive_group_token") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEGroupToken" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.drive_group_token") %>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" CancelText="ยกเลิก" EditText="&#xf040; แก้ไข" UpdateText="แก้ไข" HeaderText="ปรับปรุง" ControlStyle-Font-Size="Small" ControlStyle-CssClass="btn btn-outline-warning btn-sm fa" />
                                <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="btn btn-outline-danger btn-sm fa" ControlStyle-Font-Size="Small" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="card-footer">
                        <div class="stats">
                            <asp:Label ID="lbGroupNull" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
