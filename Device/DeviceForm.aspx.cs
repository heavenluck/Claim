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
                function.getListItem(txtGroup, "SELECT * FROM tbl_drive_group WHERE driver_group_delete <> 1", "drive_group_name", "drive_group_id");
                txtGroup.Items.Insert(0, new ListItem("เลือก", ""));
            }
        }

        void BindData(string key)
        {
            string sql = "";
            try
            {
                sql = "SELECT * FROM tbl_device d JOIN tbl_drive_group g ON d.davice_group = g.drive_group_id WHERE davice_delete <> 1 AND device_name LIKE '%" + key + "%' ORDER BY device_name ASC";
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
                DropDownList txtEDeviceGroup = (DropDownList)e.Row.FindControl("txtEDeviceGroup");
                if ((txtEDeviceGroup != null))
                {
                    function.getListItem(txtEDeviceGroup, "SELECT * FROM tbl_drive_group WHERE driver_group_delete <> 1", "drive_group_name", "drive_group_id");
                    txtEDeviceGroup.SelectedIndex = txtEDeviceGroup.Items.IndexOf(txtEDeviceGroup.Items.FindByValue((string)DataBinder.Eval(e.Row.DataItem, "drive_group_id").ToString()));
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((LinkButton)e.Row.Cells[4].Controls[0]).OnClientClick = "return confirm('ต้องการลบข้อมูลใช่หรือไม่');";
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
            DropDownList txtEDeviceGroup = (DropDownList)DeviceGridView.Rows[e.RowIndex].FindControl("txtEDeviceGroup");
            TextBox txtEDeviceSchedule = (TextBox)DeviceGridView.Rows[e.RowIndex].FindControl("txtEDeviceSchedule");

            string sql = "UPDATE tbl_device SET device_name='" + txtEDevice.Text + "',davice_group = '" + txtEDeviceGroup.SelectedValue + "',device_schedule_hour='" + txtEDeviceSchedule.Text.Trim() + "' WHERE device_id = '" + DeviceGridView.DataKeys[e.RowIndex].Value + "'";
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
            if (txtDeviceName.Text != "" && txtGroup.SelectedValue != "" && txtSchedule.Text != "")
            {
                string sql = "INSERT INTO tbl_device (device_name,davice_delete,davice_group,device_schedule_hour) VALUES ('" + txtDeviceName.Text.Trim() + "','0','" + txtGroup.SelectedValue + "','" + txtSchedule.Text.Trim() + "')";
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('เพิ่มล้มเหลว < br /> -กรุณาใส่ข้อมูลให้ครบถ้วน')", true);
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