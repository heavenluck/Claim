using ClaimProject.Config;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ClaimProject.Report
{
    public partial class reportView : System.Web.UI.Page
    {
        //ReportDocument cryRpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["ReportTitle"] != null)
                {
                    Title = Session["ReportTitle"].ToString();
                    resultReportLeave.ReportSource = Session["Report"];
                    this.DropDownList1.DataSource = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
                    this.DropDownList1.DataBind();
                    this.DropDownList1.SelectedIndex = 0;
                }
                else
                {
                    Response.Redirect("/");
                }
            }
            //resultReportLeave.Visible = true;
            //resultReportLeave.HasPrintButton = false;
            //resultReportLeave.HasExportButton = false;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ReportDocument report = Session["Report"] as ReportDocument;
                //MemoryStream oStream = (MemoryStream)report.ExportToStream(ExportFormatType.PortableDocFormat);
                /*Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();*/
                //report.PrintOptions.PrinterName = "Microsoft Print to PDF";
                report.PrintOptions.PrinterName = GetDefaultPrinter();

                //report.PrintOptions.PrinterName = DropDownList1.SelectedValue;
                //report.PrintToPrinter(0, true, 0, 0);

                //Control ctrl = (Control)resultReportLeave;
                //PrintHelper.PrintWebControl(resultReportLeave);

                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = "C:\\SampleReport.pdf";
                CrExportOptions = report.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                report.Export();
            }
            catch (Exception ex)
            {

            }
        }

        protected void CriaPDF()
        {

            ReportDocument repDoc = Session["Report"] as ReportDocument;

            System.IO.Stream s = repDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "applicattion/octect-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=บันทึกข้อความ.pdf");
            Response.AddHeader("Content-Length", s.Length.ToString());
            Response.BinaryWrite(((System.IO.MemoryStream)s).ToArray());
            Response.End();
        }
        string GetDefaultPrinter()
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }
            return string.Empty;
        }

    }
}