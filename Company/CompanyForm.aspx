<%@ Page Title="รายการบริษัท" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyForm.aspx.cs" Inherits="ClaimProject.Company.CompanyForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-row">
        <div class="col-md-3">
            ชื่อบริษัท
            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <br />
            <asp:Button ID="btnCompanyAdd" runat="server" Text="&#xf067; เพิ่ม" Font-Size="Medium" CssClass="btn btn-success btn-sm align-items-end fa" OnClick="btnCompanyAdd_Click" />
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
                        <h3 class="card-title">รายการบริษัท</h3>
                    </div>
                    <div class="card-body table-responsive">
                        <asp:GridView ID="CompanyGridView" runat="server"
                            DataKeyNames="company_id"
                            GridLines="None"
                            OnRowDataBound="CompanyGridView_RowDataBound"
                            AutoGenerateColumns="False"
                            CssClass="table table-hover table-sm"
                            OnRowEditing="CompanyGridView_RowEditing"
                            OnRowCancelingEdit="CompanyGridView_RowCancelingEdit"
                            OnRowUpdating="CompanyGridView_RowUpdating"
                            OnRowDeleting="CompanyGridView_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="ชื่อบริษัท">
                                    <ItemTemplate>
                                        <asp:Label ID="lbECompany" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.company_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtECompany" size="20" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.company_name") %>' CssClass="form-control"></asp:TextBox>
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
