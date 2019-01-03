using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
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
                /*Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();*/
                //report.PrintOptions.PrinterName = "Microsoft Print to PDF";
                //report.PrintOptions.PrinterName = GetDefaultPrinter();
                report.PrintOptions.PrinterName = DropDownList1.SelectedValue;
                report.PrintToPrinter(0, true, 0, 0);
            }
            catch { }
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