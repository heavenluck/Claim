using ClaimProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject.CM
{
    public partial class CMDetailForm : System.Web.UI.Page
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
                string sql = "";
                if (function.CheckLevel("Department", Session["UserPrivilegeId"].ToString()))
                {
                    sql = "SELECT * FROM tbl_cpoint ORDER BY cpoint_id";
                    function.getListItem(txtCpoint, sql, "cpoint_name", "cpoint_id");
                    function.getListItem(txtCpointSearch, sql, "cpoint_name", "cpoint_id");
                    txtCpointSearch.Items.Insert(0, new ListItem("ทั้งหมด", ""));
                }
                else
                {
                    sql = "SELECT * FROM tbl_cpoint WHERE cpoint_id = '" + Session["UserCpoint"].ToString() + "'";
                    function.getListItem(txtCpoint, sql, "cpoint_name", "cpoint_id");
                    function.getListItem(txtCpointSearch, sql, "cpoint_name", "cpoint_id");
                    //txtCpointSearch.Items.Insert(0, new ListItem("ทั้งหมด", ""));
                }

                string sql_Device = "SELECT * FROM tbl_device ORDER BY device_name";
                function.getListItem(txtDeviceAdd, sql_Device, "device_name", "device_id");
                txtDeviceAdd.Items.Insert(0, new ListItem("", ""));
                txtSTime.Text = DateTime.Now.ToString("HH.mm");
                BindData("");

                if (Request["ref"] != null)
                {
                    txtRef.Value = Request["ref"].ToString();
                    sql = "SELECT * FROM tbl_cm_detail WHERE cm_detail_id = '" + txtRef.Value + "'";
                    MySqlDataReader rs = function.MySqlSelect(sql);
                    if (rs.Read())
                    {
                        txtCpoint.SelectedValue = rs.GetString("cm_cpoint");
                        txtPoint.Text = rs.GetString("cm_point");
                        txtChannel.Text = rs.GetString("cm_detail_channel");
                        txtSDate.Text = rs.GetString("cm_detail_sdate");
                        txtSTime.Text = rs.GetString("cm_detail_stime");
                        txtDeviceAdd.SelectedValue = rs.GetString("cm_detail_driver_id");
                        txtProblem.Text = rs.GetString("cm_detail_problem");
                        txtNote.Text = rs.GetString("cm_detail_note");
                    }
                    rs.Close();
                    function.Close();

                    btnSaveCM.Visible = false;
                    btnEditCM.Visible = true;
                    btnCancelCM.Visible = true;

                    if (function.CheckLevel("Techno", Session["UserPrivilegeId"].ToString()))
                    {
                        btnDeleteCM.Visible = true;
                    }
                    else
                    {
                        btnDeleteCM.Visible = false;
                    }

                }
                else
                {
                    btnSaveCM.Visible = true;
                    btnEditCM.Visible = false;
                    btnCancelCM.Visible = false;
                    btnDeleteCM.Visible = false;
                }
            }



        }
        void BindData(string key)
        {
            string sql = "";
            string sqlPlus = "";
            if (Session["UserCpoint"].ToString() != "0")
            {
                sqlPlus = "WHERE c.cpoint_id = '" + Session["UserCpoint"].ToString() + "'";
            }
            if (key != "")
            {
                sqlPlus = "WHERE c.cpoint_id = '" + key + "'";
            }
            try
            {
                sql = "SELECT * FROM tbl_cm_detail cm JOIN tbl_device d ON cm.cm_detail_driver_id = d.device_id JOIN tbl_cpoint c ON cm.cm_cpoint = c.cpoint_id " + sqlPlus + " ORDER BY cm_cpoint,cm_point,cm_detail_channel,STR_TO_DATE(cm.cm_detail_sdate, '%d-%m-%Y'), cm.cm_detail_stime, cm_detail_status_id ASC";
                MySqlDataAdapter da = function.MySqlSelectDataSet(sql);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds);
                CMGridView.DataSource = ds.Tables[0];
                CMGridView.DataBind();
                if (ds.Tables[0].Rows.Count == 0) { DivCMGridView.Visible = false; } else { DivCMGridView.Visible = true; }
                //lbCMNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            catch { }
        }

        protected void btnSaveCM_Click(object sender, EventArgs e)
        {

            String NewFileDocName = "";
            if (fileImg.HasFile)
            {
                string typeFile = fileImg.FileName.Split('.')[fileImg.FileName.Split('.').Length - 1];
                if (typeFile == "jpg" || typeFile == "jpeg" || typeFile == "png")
                {
                    NewFileDocName = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + fileImg.FileName.Split('.')[0];
                    NewFileDocName = "/CM/Upload/" + function.getMd5Hash(NewFileDocName) + "." + typeFile;
                    fileImg.SaveAs(Server.MapPath(NewFileDocName.ToString()));

                    string sql_insert = "INSERT INTO tbl_cm_detail (cm_detail_driver_id,cm_detail_problem,cm_detail_status_id,cm_detail_channel,cm_detail_sdate,cm_detail_stime,cm_detail_simg,cm_detail_note,cm_cpoint,cm_point,cm_user) VALUES ('" + txtDeviceAdd.SelectedValue + "','" + txtProblem.Text + "','0','" + txtChannel.Text + "','" + txtSDate.Text + "','" + txtSTime.Text + "','" + NewFileDocName + "','" + txtNote.Text + "','" + txtCpoint.SelectedValue + "','" + txtPoint.Text.Trim() + "','" + Session["User"].ToString() + "')";
                    if (function.MySqlQuery(sql_insert))
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('บันทึกข้อมูลสำเร็จ')", true);
                        BindData("");
                        ClearDate();
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('ล้มเหลว เกิดข้อผิดพลาด')", true);
                    }
                }
                else
                {
                    //AlertPop("Error : แนบรูปภาพล้มเหลว ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น", "error");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : แนบรูปภาพล้มเหลว ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น')", true);
                }
            }
            else
            {
                //AlertPop("Error : แนบรูปภาพล้มเหลวไม่พบไฟล์", "error");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : แนบรูปภาพล้มเหลวไม่พบไฟล์')", true);
            }
        }

        private void ClearDate()
        {
            txtCpoint.SelectedIndex = 0;
            txtPoint.Text = "";
            txtDeviceAdd.SelectedIndex = 0;
            txtProblem.Text = "";
            txtChannel.Text = "";
            txtNote.Text = "";
            txtSDate.Text = DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year + 543);
            txtSTime.Text = DateTime.Now.ToString("HH.mm");
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

            LinkButton btnEditCM = (LinkButton)(e.Row.FindControl("btnEditCM"));
            if (btnEditCM != null)
            {
                btnEditCM.CommandName = DataBinder.Eval(e.Row.DataItem, "cm_detail_id").ToString();
                if(DataBinder.Eval(e.Row.DataItem, "cm_detail_status_id").ToString() != "0")
                {
                    btnEditCM.Visible = false;
                }
            }
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("/CM/CMDetailForm?ref=" + e.CommandName);
        }

        protected void btnEditCM_Click(object sender, EventArgs e)
        {
            String NewFileDocName = "";
            if (fileImg.HasFile)
            {
                string typeFile = fileImg.FileName.Split('.')[fileImg.FileName.Split('.').Length - 1];
                if (typeFile == "jpg" || typeFile == "jpeg" || typeFile == "png")
                {
                    NewFileDocName = "S_"+DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + fileImg.FileName.Split('.')[0];
                    NewFileDocName = "/CM/Upload/" + function.getMd5Hash(NewFileDocName) + "." + typeFile;
                    fileImg.SaveAs(Server.MapPath(NewFileDocName.ToString()));
                    UpdateCM(NewFileDocName);
                }
                else
                {
                    //AlertPop("Error : แนบรูปภาพล้มเหลว ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น", "error");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : แนบรูปภาพล้มเหลว ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น')", true);
                }
            }
            else
            {
                UpdateCM("");
                //AlertPop("Error : แนบรูปภาพล้มเหลวไม่พบไฟล์", "error");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : แนบรูปภาพล้มเหลวไม่พบไฟล์')", true);
            }
        }

        private void UpdateCM(string NewFileDocName)
        {
            string img = "";
            if (NewFileDocName != "")
            {
                img = "cm_detail_simg = '" + NewFileDocName + "',";
            }

            string sql_insert = "UPDATE tbl_cm_detail SET cm_detail_driver_id = '" + txtDeviceAdd.SelectedValue + "',cm_detail_problem='" + txtProblem.Text + "',cm_detail_channel='" + txtChannel.Text + "',cm_detail_sdate='" + txtSDate.Text + "',cm_detail_stime='" + txtSTime.Text + "'," + img + "cm_detail_note='" + txtNote.Text + "',cm_cpoint='" + txtCpoint.SelectedValue + "',cm_point='" + txtPoint.Text + "' WHERE cm_detail_id = '" + txtRef.Value + "'";
            if (function.MySqlQuery(sql_insert))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('แก้ไขข้อมูลสำเร็จ')", true);
                BindData("");
                //ClearDate();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('ล้มเหลว เกิดข้อผิดพลาด')", true);
            }
        }

        protected void btnCancelCM_Click(object sender, EventArgs e)
        {
            Response.Redirect("/CM/CMDetailForm");
        }

        protected void btnDeleteCM_Click(object sender, EventArgs e)
        {
            string sql_insert = "DELETE FROM tbl_cm_detail WHERE cm_detail_id = '" + txtRef.Value + "'";
            if (function.MySqlQuery(sql_insert))
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('ลบข้อมูลสำเร็จ')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('ลบข้อมูลสำเร็จ'); window.location='/CM/CMDetailForm';", true);
                BindData("");
                ClearDate();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('ล้มเหลว เกิดข้อผิดพลาด')", true);
            }
        }

        protected void txtCpointSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData(txtCpointSearch.SelectedValue);
        }
    }
}