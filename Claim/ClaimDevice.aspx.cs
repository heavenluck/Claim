using ClaimProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject.Claim
{
    public partial class ClaimDevice : System.Web.UI.Page
    {
        ClaimFunction function = new ClaimFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("/");
            }

            if (!this.IsPostBack)
            {
                
                function.getListItem(txtSearchStatus, "SELECT * FROM tbl_status ORDER by status_id", "status_name", "status_id");
                txtSearchStatus.Items.Insert(0, new ListItem("ทั้งหมด", "0"));
                string sql = "";
                if (function.CheckLevel("Department", Session["UserPrivilegeId"].ToString()))
                {
                    sql = "SELECT * FROM tbl_cpoint ORDER BY cpoint_id";
                    function.getListItem(txtSearchCpoint, sql, "cpoint_name", "cpoint_id");
                    txtSearchCpoint.Items.Insert(0, new ListItem("ทั้งหมด", ""));
                }
                else
                {
                    sql = "SELECT * FROM tbl_cpoint WHERE cpoint_id = '" + Session["UserCpoint"].ToString() + "'";
                    function.getListItem(txtSearchCpoint, sql, "cpoint_name", "cpoint_id");
                    BindData(txtSearchCpoint.SelectedValue, txtPoint.Text.Trim(), 0);
                }
            }
        }

        void BindData(string cpoint, string point, int except)
        {
            string sql = "";
            string conCpoint = "";
            if (cpoint != "")
            {
                conCpoint = "AND c.claim_cpoint = '" + cpoint + "' AND c.claim_point Like '%" + point + "%' ";
                if (except > 0)
                {
                    if (CheckDeviceNotDamaged.Checked)
                    {
                        conCpoint += "AND c.claim_status <> '" + except + "'";
                    }
                    else
                    {
                        conCpoint += "AND c.claim_status = '" + except + "'";
                    }
                }
            }
            try
            {
                sql = "SELECT * FROM tbl_claim c JOIN `tbl_claim_com` cc ON cc.`claim_id` = c.`claim_id` JOIN tbl_device_damaged dd ON dd.`claim_id` = c.`claim_id` AND dd.`device_damaged_delete` <> 1 JOIN `tbl_device` d ON d.`device_id` = dd.`device_id` JOIN `tbl_status` s ON s.`status_id` = c.`claim_status` JOIN `tbl_cpoint` cp ON c.`claim_cpoint` = cp.`cpoint_id` WHERE c.`claim_status` <> 5 AND c.`claim_status` <> 6 AND c.`claim_delete` <> 1 " + conCpoint + " ORDER BY c.claim_cpoint,c.claim_point,cc.claim_detail_cb_claim,STR_TO_DATE(c.claim_start_date,'%d-%m-%Y') ASC";
                MySqlDataAdapter da = function.MySqlSelectDataSet(sql);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds);
                ClaimGridView.DataSource = ds.Tables[0];
                ClaimGridView.DataBind();

                //lbCMNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            catch { }
        }

        protected void ClaimGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbClaimSDate = (Label)(e.Row.FindControl("lbClaimSDate"));
            if (lbClaimSDate != null)
            {
                lbClaimSDate.Text = function.ConvertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "claim_start_date"));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindData(txtSearchCpoint.SelectedValue, txtPoint.Text.Trim(), int.Parse(txtSearchStatus.SelectedValue));
            }
            catch { }
        }
    }
}