<%@ Page Title="งาน CM" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DefaultCM.aspx.cs" Inherits="ClaimProject.CM.DefaultCM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-4 col-md-6 col-sm-6" runat="server" id="boxUserSystem">
            <div class="card card-stats">
                <div class="card-header card-header-danger card-header-icon">
                    <div class="card-icon">
                        <i class="fas fa-wrench"></i>
                    </div>
                    <h4 class="card-category">
                        <a class="nav-link" href="/CM/CMDetailForm">แจ้งซ่อมอุปกรณ์ CM</a>
                    </h4>
                </div>
            </div>
        </div>
        <div class="col-lg-4 col-md-6 col-sm-6" runat="server" id="Div4">
            <div class="card card-stats">
                <div class="card-header card-header-success card-header-icon">
                    <div class="card-icon">
                        <i class="fas fa-tools"></i>
                    </div>
                    <h4 class="card-category">
                        <a class="nav-link" href="/CM/CMEditForm">บริษัทเข้าซ่อม CM</a>
                    </h4>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-4 col-md-6 col-sm-6" runat="server" id="Div6">
            <div class="card card-stats">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="fas fa-eye"></i>
                    </div>
                    <h4 class="card-category">
                        <a class="nav-link" href="/CM/CMSurveyForm">ตรวจสอบผลการ CM</a>
                    </h4>
                </div>
            </div>
        </div>
        <div class="col-lg-4 col-md-6 col-sm-6" runat="server" id="Div1">
            <div class="card card-stats">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="fas fa-file-alt"></i>
                    </div>
                    <h4 class="card-category">
                        <a class="nav-link" href="#">รายงานแจ้งซ่อม CM</a>
                    </h4>
                </div>
            </div>
        </div>
    </div>
    <hr />
    <!--
    <div class="row">
        <div class="col-lg-4 col-md-6 col-sm-6" runat="server" id="Div2">
            <div class="card card-stats">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="fas fa-magic"></i>
                    </div>
                    <h4 class="card-category">
                        <a class="nav-link" href="#">กำหนดการ PM</a>
                    </h4>
                </div>
            </div>
        </div>
        <div class="col-lg-4 col-md-6 col-sm-6" runat="server" id="Div5">
            <div class="card card-stats">
                <div class="card-header card-header-success card-header-icon">
                    <div class="card-icon">
                        <i class="fas fa-cogs"></i>
                    </div>
                    <h4 class="card-category">
                        <a class="nav-link" href="#">บริษัทเข้า PM</a>
                    </h4>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-4 col-md-6 col-sm-6" runat="server" id="Div3">
            <div class="card card-stats">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="fas fa-file-alt"></i>
                    </div>
                    <h4 class="card-category">
                        <a class="nav-link" href="#">รายงานการ PM</a>
                    </h4>
                </div>
            </div>
        </div>
    </div>-->
</asp:Content>
