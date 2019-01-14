using ClaimProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject.Device
{
    public partial class GroupDeviceForm : System.Web.UI.Page
    {
        ClaimFunction function = new ClaimFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserPrivilegeId"] == null) { Response.Redirect("/"); }
            if (Session["UserPrivilegeId"].ToString() != "0" && Session["UserPrivilegeId"].ToString() != "1")
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
            try
            {
                sql = "SELECT * FROM tbl_drive_group WHERE driver_group_delete <> 1 AND drive_group_name Like '%" + key + "%' ORDER BY drive_group_name ASC";
                MySqlDataAdapter da = function.MySqlSelectDataSet(sql);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds);
                GroupGridView.DataSource = ds.Tables[0];
                GroupGridView.DataBind();
                lbGroupNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            catch { }
        }
        protected void GroupGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((LinkButton)e.Row.Cells[3].Controls[0]).OnClientClick = "return confirm('ต้องการลบข้อมูลใช่หรือไม่');";
                }
                catch { }
            }
        }

        protected void GroupGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GroupGridView.EditIndex = e.NewEditIndex;
            BindData(txtSearch.Text);
        }

        protected void GroupGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GroupGridView.EditIndex = -1;
            BindData(txtSearch.Text);
        }

        protected void GroupGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEGroup = (TextBox)GroupGridView.Rows[e.RowIndex].FindControl("txtEGroup");
            TextBox txtEGroupToken = (TextBox)GroupGridView.Rows[e.RowIndex].FindControl("txtEGroupToken");

            string sql = "UPDATE tbl_drive_group SET drive_group_name='" + txtEGroup.Text + "',drive_group_token = '"+ txtEGroupToken.Text.Trim()+ "' WHERE drive_group_id = '" + GroupGridView.DataKeys[e.RowIndex].Value + "'";
            string script = "";
            if (function.MySqlQuery(sql))
            {
                script = "แก้ไขข้อมูลสำเร็จ";
            }
            else
            {
                script = "Error : แก้ไขข้อมูลล้มเหลว";
            }
            function.Close();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('" + script + "')", true);
            GroupGridView.EditIndex = -1;
            BindData(txtSearch.Text);
            txtSearch.Text = "";
        }

        protected void GroupGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = "UPDATE tbl_drive_group SET driver_group_delete='1' WHERE drive_group_id = '" + GroupGridView.DataKeys[e.RowIndex].Value + "'";
            string script = "";
            if (function.MySqlQuery(sql))
            {
                script = "ลบข้อมูลสำเร็จ";
            }
            else
            {
                script = "Error : ลบข้อมูลล้มเหลว";
            }
            function.Close();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('" + script + "')", true);
            GroupGridView.EditIndex = -1;
            BindData(txtSearch.Text);
            txtSearch.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(txtSearch.Text);
        }

        protected void btnGroupAdd_Click(object sender, EventArgs e)
        {
            if (txtGroupName.Text != "" && txtTokenLine.Text != "")
            {
                string sql = "INSERT INTO tbl_drive_group (drive_group_name,drive_group_token,driver_group_delete) VALUES ('" + txtGroupName.Text.Trim() + "','"+ txtTokenLine.Text.Trim() + "','0')";
                string script = "";
                if (function.MySqlQuery(sql))
                {
                    script = "บันทึกข้อมูลสำเร็จ";
                }
                else
                {
                    script = "Error : บันทึกข้อมูลล้มเหลว";
                }
                function.Close();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('" + script + "')", true);
                GroupGridView.EditIndex = -1;
                BindData(txtGroupName.Text);
                txtGroupName.Text = "";
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('เพิ่มล้มเหลว < br /> - กรุณาใส่ข้อมูลให้ครบถ้วน')", true);
            }
            ClearData();
        }

        void ClearData()
        {
            txtGroupName.Text = "";
            txtTokenLine.Text = "";
        }
    }
}