<%@ Page Title="Setting/Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="settingPage.aspx.cs" Inherits="ClaimProject.settingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header card-header-warning">
            <h3 class="card-title">เมนู</h3>
        </div>
        <div class="card-body table-responsive">
            <div class="row text-center">
                <div class="col-md-2">
                    <a class="nav-link" href="/User/Add/userForm">
                        <i class="fa fa-address-book-o" style="font-size: 22px;"></i>
                        <p>เพิ่มผู้ใช้งาน</p>
                    </a>
                </div>
                <div class="col-md-2">
                    <a class="nav-link" href="/Device/DeviceForm">
                        <i class="fa fa-cubes" style="font-size: 22px;"></i>
                        <p>รายการอุปกรณ์</p>
                    </a>
                </div>
                <div class="col-md-2">
                    <a class="nav-link" href="/Device/GroupDeviceForm">
                        <i class="fa fa-gears" style="font-size: 22px;"></i>
                        <p>กลุ่มอุปกรณ์</p>
                    </a>
                </div>
                <div class="col-md-2">
                    <a class="nav-link" href="/Company/CompanyForm">
                        <i class="fa fa-handshake-o" style="font-size: 22px;"></i>
                        <p>รายการบริษัท</p>
                    </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
