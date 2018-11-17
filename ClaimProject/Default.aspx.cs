using ClaimProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject
{
    public partial class _Default : Page
    {
        ClaimFunction function = new ClaimFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                if (Session["UserCpoint"].ToString() != "0")
                {
                    Response.Redirect("/Claim/claimForm");
                    //TestGittttxxxx
                    //แก้้้้
                }

                string date = DateTime.Now.ToString("dd-MM") + "-" + (DateTime.Now.Year + 543);

                if (!this.IsPostBack)
                {
                    function.getListItem(txtYear, "SELECT claim_budget_year FROM tbl_claim c GROUP BY claim_budget_year", "claim_budget_year", "claim_budget_year");
                    txtYear.SelectedValue = function.getBudgetYear(date);
                }
                //Response.Redirect("/Claim/claimForm");
                getBind(txtYear.SelectedValue);
            }
        }

        private void getStatusAmount(Label label,int status,string year)
        {
            string sql = "SELECT COUNT(*) AS count_num FROM tbl_claim c JOIN tbl_cpoint ON claim_cpoint = cpoint_id JOIN tbl_status ON status_id = claim_status LEFT JOIN tbl_user ON username = claim_user_start_claim JOIN tbl_status_detail sd ON sd.detail_claim_id = c.claim_id AND sd.detail_status_id = c.claim_status WHERE claim_delete = '0' AND c.claim_status = '" + status+ "' AND c.claim_budget_year = '" + year+"'";
            MySqlDataReader rs = function.MySqlSelect(sql);
            if (rs.Read())
            {
                label.Text = rs.GetString("count_num") +" รายการ";
            }
            else
            {
                label.Text = "0 รายการ";
            }
            rs.Close();
            function.Close();
            function.conn.Close();
        }

        private void getBind(string year)
        {
            getStatusAmount(lbAlert, 1, year);
            getStatusAmount(lbQuote, 2, year);
            getStatusAmount(lbSend, 3, year);
            getStatusAmount(lbRepair, 4, year);
            getStatusAmount(lbSuccess, 5, year);
        }

        protected void txtYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            getBind(txtYear.SelectedItem.Text);
        }

        protected void btnDetailAlert_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Techno/TechnoFormView?s=1&y="+txtYear.SelectedValue);
        }

        protected void btnDetailQute_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Techno/TechnoFormView?s=2&y=" + txtYear.SelectedValue);
        }

        protected void btnSendto_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Techno/TechnoFormView?s=3&y=" + txtYear.SelectedValue);
        }

        protected void btnWait_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Techno/TechnoFormView?s=4&y=" + txtYear.SelectedValue);
        }

        protected void btnSuccessJob_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Techno/TechnoFormView?s=5&y=" + txtYear.SelectedValue);
        }
    }
}