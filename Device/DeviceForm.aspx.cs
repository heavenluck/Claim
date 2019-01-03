using ClaimProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject.Device
{
    public partial class DeviceForm : System.Web.UI.Page
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
                sql = "SELECT * FROM tbl_device WHERE davice_delete <> 1 AND device_name Like '%"+key+"%' ORDER BY device_name ASC";
                MySqlDataAdapter da = function.MySqlSelectDataSet(sql);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds);
                DeviceGridView.DataSource = ds.Tables[0];
                DeviceGridView.DataBind();
                lbDeviceNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            catch { }
        }
        protected void DeviceGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((LinkButton)e.Row.Cells[2].Controls[0]).OnClientClick = "return confirm('ต้องการลบข้อมูลใช่หรือไม่');";
                }
                catch { }
            }
        }

        protected void DeviceGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DeviceGridView.EditIndex = e.NewEditIndex;
            BindData(txtSearch.Text);
        }

        protected void DeviceGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DeviceGridView.EditIndex = -1;
            BindData(txtSearch.Text);
        }

        protected void DeviceGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEDevice = (TextBox)DeviceGridView.Rows[e.RowIndex].FindControl("txtEDevice");

            string sql = "UPDATE tbl_device SET device_name='" + txtEDevice.Text + "' WHERE device_id = '" + DeviceGridView.DataKeys[e.RowIndex].Value + "'";
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
            DeviceGridView.EditIndex = -1;
            BindData(txtSearch.Text);
            txtSearch.Text = "";
        }

        protected void DeviceGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = "UPDATE tbl_device SET davice_delete='1' WHERE device_id = '" + DeviceGridView.DataKeys[e.RowIndex].Value + "'";
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
            DeviceGridView.EditIndex = -1;
            BindData(txtSearch.Text);
            txtSearch.Text = "";
        }

        protected void btnDeviceAdd_Click(object sender, EventArgs e)
        {
            if (txtDeviceName.Text != "")
            {
                string sql = "INSERT INTO tbl_device (device_name,davice_delete) VALUES ('" + txtDeviceName.Text.Trim() + "','0')";
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
                DeviceGridView.EditIndex = -1;
                BindData(txtDeviceName.Text);
                txtDeviceName.Text = "";
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('เพิ่มล้มเหลว < br /> -กรุณาใส่ชื่ออุปกรณ์')", true);
            }
            ClearData();
        }

        void ClearData()
        {
            txtDeviceName.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(txtSearch.Text);
        }
    }
}