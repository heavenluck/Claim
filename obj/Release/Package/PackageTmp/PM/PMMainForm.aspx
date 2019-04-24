<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PMMainForm.aspx.cs" Inherits="ClaimProject.PM.PMMainForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Scripts/bootbox.js"></script>
    <script src="/Scripts/HRSProjectScript.js"></script>
    <div class="card" style="z-index: 0">
        <div class="card-header card-header-warning">
            <h3 class="card-title">แจ้งการบำรุงรักษาอุปกรณ์</h3>
        </div>
        <div class="card-body table-responsive table-sm">
            <div class="row" >
                <div class="col-md-2">
                    <div class="form-group bmd-form-group">
                        <label class="bmd-label-floating">ด่านฯ</label>
                        <asp:DropDownList ID="txtPMCpoint" runat="server" CssClass="form-control custom-select"></asp:DropDownList>
                    </div>
                </div>

            </div>
        </div>



    </div>










    <script src="/Scripts/jquery-ui-1.11.4.custom.js"></script>
    <script src="/Scripts/moment.min.js"></script>
    <script src="/Scripts/ClaimProjectScript.js"></script>

</asp:Content>
