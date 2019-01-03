<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportView.aspx.cs" Inherits="ClaimProject.Report.reportView" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/crystalreportviewers13/js/crviewer/crv.js"></script>
    <style>
        @font-face {
            font-family: 'THSarabun';
            src: url('/fonts/THSarabun.ttf') format('truetype');
        }

        @font-face {
            font-family: 'THSarabunIT๙';
            src: url('/fonts/THSarabunIT๙.ttf') format('truetype');
        }
    </style>
</head>
<body style="font-family: 'THSarabun,THSarabunIT๙'">
    <form id="iframe" runat="server">
        <div id="dvReport">
            <asp:Button runat="server" ID="btnPrint" Text="พิมพ์" OnClick="btnPrint_Click" />
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            <CR:CrystalReportViewer ID="resultReportLeave" runat="server"
                EnableParameterPrompt="False" 
                ToolPanelView="None" GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False" PrintMode="ActiveX" />
        </div>
    </form>
    <script type="text/javascript">
        function Print() {
            var dvReport = document.getElementById("dvReport");
            var frame1 = dvReport.getElementsByTagName("iframe")[0];
            if (navigator.appName.indexOf("Internet Explorer") != -1 || navigator.appVersion.indexOf("Trident") != -1) {
                frame1.name = frame1.id;
                window.frames[frame1.id].focus();
                window.frames[frame1.id].print();
            }
            else {
                var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                frameDoc.print();
            }
        }

    </script>
</body>
</html>
