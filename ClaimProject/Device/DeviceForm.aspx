<%@ Page Title="รายการอุปกรณ์" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeviceForm.aspx.cs" Inherits="ClaimProject.Device.DeviceForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-row">
        <div class="col-md-3">
            ชื่ออุปกรณ์ 
            <asp:TextBox ID="txtDeviceName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <br />
            <asp:Button ID="btnDeviceAdd" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnDeviceAdd_Click" />
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
                        <h3 class="card-title">รายการอุปกรณ์</h3>
                    </div>
                    <div class="card-body table-responsive">
                        <asp:GridView ID="DeviceGridView" runat="server"
                            DataKeyNames="device_id"
                            GridLines="None"
                            OnRowDataBound="DeviceGridView_RowDataBound"
                            AutoGenerateColumns="False"
                            CssClass="table table-hover table-sm"
                            OnRowEditing="DeviceGridView_RowEditing"
                            OnRowCancelingEdit="DeviceGridView_RowCancelingEdit"
                            OnRowUpdating="DeviceGridView_RowUpdating"
                            OnRowDeleting="DeviceGridView_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="ชื่ออุปกรณ์">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDevice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.device_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEDevice" size="20" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.device_name") %>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" CancelText="ยกเลิก" EditText="&#xf040; แก้ไข" UpdateText="แก้ไข" HeaderText="ปรับปรุง" ControlStyle-Font-Size="Small" ControlStyle-CssClass="btn btn-outline-warning btn-sm fa" />
                                <asp:CommandField ShowDeleteButton="True" HeaderText="ลบ" DeleteText="&#xf014; ลบ" ControlStyle-CssClass="btn btn-outline-danger btn-sm fa" ControlStyle-Font-Size="Small" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="card-footer">
                        <div class="stats">
                            <asp:Label ID="lbDeviceNull" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
