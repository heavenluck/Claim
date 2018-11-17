using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject.Report
{
    public partial class reportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["ReportTitle"] != null)
                {
                    Title = Session["ReportTitle"].ToString();
                    resultReportLeave.ReportSource = Session["Report"];
                    resultReportLeave.Visible = true;
                }
                else
                {
                    Response.Redirect("/");
                }
            }
        }
    }
}