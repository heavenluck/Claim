<%@ Page Title="งานอุบัติเหตุ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DefaultClaim.aspx.cs" Inherits="ClaimProject.Claim.DefaultClaim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="boxUserSystem">
            <div class="card card-stats">
                <div class="card-header card-header-danger card-header-icon">
                    <div class="card-icon">
                        <i class="fas fa-car-crash"></i>
                    </div>
                    <h4 class="card-category">
                        <a class="nav-link" href="/Claim/claimForm?add=true">แจ้งเกิดอุบัติเหตุ</a>
                    </h4>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="Div2">
            <div class="card card-stats">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="fas fa-stream"></i>
                    </div>
                    <h4 class="card-category">
                        <a class="nav-link" href="/Claim/claimForm">รายการแจ้งอุบัติเหตุ</a>
                    </h4>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-6 col-sm-6" runat="server" id="Div1">
            <div class="card card-stats">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="fas fa-file-alt"></i>
                    </div>
                    <h4 class="card-category">
                        <a class="nav-link" href="/ReportView/">รายงานอุบัติเหตุ</a>
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
                        <a class="nav-link" href="/Claim/ClaimDevice">รายงานอุปกรณ์ค้างซ่อม</a>
                    </h4>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
