using ClaimProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject.CM
{
    public partial class CMSurveyForm : System.Web.UI.Page
    {
        ClaimFunction function = new ClaimFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("/");
            }

            if (!this.IsPostBack)
            {
                BindData("");
            }
        }

        void BindData(string key)
        {
            string sql = "";
            string sqlPlus = "";
            if (Session["UserCpoint"].ToString() != "0")
            {
                sqlPlus = "AND c.cpoint_id = '" + Session["UserCpoint"].ToString() + "'";
            }
            try
            {
                sql = "SELECT * FROM tbl_cm_detail cm JOIN tbl_device d ON cm.cm_detail_driver_id = d.device_id JOIN tbl_cpoint c ON cm.cm_cpoint = c.cpoint_id WHERE cm.cm_detail_status_id='1' " + sqlPlus + " ORDER BY STR_TO_DATE(cm.cm_detail_sdate, '%d-%m-%Y'), cm.cm_detail_stime, cm_detail_status_id ASC";
                MySqlDataAdapter da = function.MySqlSelectDataSet(sql);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds);
                CMGridView.DataSource = ds.Tables[0];
                CMGridView.DataBind();
                if (ds.Tables[0].Rows.Count == 0) { DivCMGridView.Visible = false; } else { DivCMGridView.Visible = true; }
                //lbCMNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            catch (Exception e) { e.ToString(); }
        }

        protected void CMGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbSDate = (Label)(e.Row.FindControl("lbSDate"));
            if (lbSDate != null)
            {
                lbSDate.Text = function.ConvertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "cm_detail_sdate"));
            }

            Label lbStatus = (Label)(e.Row.FindControl("lbStatus"));
            if (lbStatus != null)
            {
                lbStatus.Text = function.GetStatusCM(DataBinder.Eval(e.Row.DataItem, "cm_detail_status_id").ToString());
            }

            Label btnDateEditCM = (Label)(e.Row.FindControl("btnDateEditCM"));
            if (btnDateEditCM != null)
            {
                if (!DataBinder.Eval(e.Row.DataItem, "cm_detail_edate").Equals(DBNull.Value))
                {
                    btnDateEditCM.Text = function.ConvertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "cm_detail_edate"));
                }
            }

            Label btnTimeEditCM = (Label)(e.Row.FindControl("btnTimeEditCM"));
            if (btnTimeEditCM != null)
            {
                if (!DataBinder.Eval(e.Row.DataItem, "cm_detail_etime").Equals(DBNull.Value))
                {
                    btnTimeEditCM.Text = (string)DataBinder.Eval(e.Row.DataItem, "cm_detail_etime");
                    if (btnTimeEditCM.Text != "") { btnTimeEditCM.Text += " น."; }
                }
            }

            LinkButton btnStatusUpdate = (LinkButton)(e.Row.FindControl("btnStatusUpdate"));
            if (btnStatusUpdate != null)
            {
                btnStatusUpdate.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "cm_detail_id").ToString();
            }

            LinkButton btnCancel = (LinkButton)(e.Row.FindControl("btnCancel"));
            if (btnCancel != null)
            {
                btnCancel.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "cm_detail_id").ToString();
            }
        }

        protected void btnStatusUpdate_Command(object sender, CommandEventArgs e)
        {
            string sql = "UPDATE tbl_cm_detail SET cm_detail_status_id = '2' WHERE cm_detail_id = '" + e.CommandName + "'";
            if (function.MySqlQuery(sql))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('อนุมัติข้อมูลสำเร็จ')", true);
                BindData("");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('ล้มเหลวเกิดข้อผิดพลาด')", true);
            }
        }

        protected void btnCancel_Command(object sender, CommandEventArgs e)
        {
            string sql = "UPDATE tbl_cm_detail SET cm_detail_edate='',cm_detail_etime='',cm_detail_method ='',cm_detail_status_id = '0' WHERE cm_detail_id = '" + e.CommandName + "'";
            if (function.MySqlQuery(sql))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('บันทึกข้อมูลสำเร็จ')", true);
                BindData("");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('ล้มเหลวเกิดข้อผิดพลาด')", true);
            }
        }
    }
}