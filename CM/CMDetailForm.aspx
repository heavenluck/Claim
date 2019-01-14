<%@ Page Title="รายการ CM" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CMDetailForm.aspx.cs" Inherits="ClaimProject.CM.CMDetailForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-row">
        <div class="col-md-3">
            <asp:LinkButton ID="btnAddCM" CssClass="btn btn-success" runat="server">แจ้งซ่อมอุปกรณ์</asp:LinkButton>
        </div>
    </div>
    <br />
    <div>
        <div class="row">
            <div class="col-md text-center" style="background-color: darkgrey;"><b>รายการอุปกรณ์ทั่วไป</b></div>
        </div>
        <asp:GridView ID="CMGridView" runat="server">
            <Columns>
                <asp:TemplateField HeaderText="Ref.">
                    <ItemTemplate>
                        <asp:Label ID="lbCM" runat="server" Text=''></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ช่องทาง/ตำแหน่ง">
                    <ItemTemplate>
                        <asp:Label ID="lbCM" runat="server" Text=''></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="อุปกรณ์">
                    <ItemTemplate>
                        <asp:Label ID="lbCM" runat="server" Text=''></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="อาการที่ชำรุด">
                    <ItemTemplate>
                        <asp:Label ID="lbCM" runat="server" Text=''></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="วันที่/เวลา แจ้งซ่อม">
                    <ItemTemplate>
                        <asp:Label ID="lbCM" runat="server" Text=''></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="วันที่/เวลา เข้าแก้ไข">
                    <ItemTemplate>
                        <asp:Label ID="lbCM" runat="server" Text=''></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="แผนเข้า CM">
                    <ItemTemplate>
                        <asp:Label ID="lbCM" runat="server" Text=''></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="หมายเหตุ">
                    <ItemTemplate>
                        <asp:Label ID="lbCM" runat="server" Text=''></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
