using ClaimProject.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace ClaimProject.ReportView
{
    public partial class Default : System.Web.UI.Page
    {
        ClaimFunction function = new ClaimFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            bindAllClaim();
        }

        public void bindAllClaim()
        {
            string ss = "SELECT cpoint_name,COUNT(claim_id),claim_budget_year AS amount FROM tbl_cpoint LEFT JOIN tbl_claim ON claim_cpoint = cpoint_id WHERE claim_budget_year = '2561' GROUP BY claim_cpoint";

            MySqlDataAdapter AllClaim = function.MySqlSelectDataSet(ss);
            DataGrid dt = new DataGrid();
            
            
            GridViewTest.DataSource = dt;
            GridViewTest.DataBind();
            
        }
    }
}