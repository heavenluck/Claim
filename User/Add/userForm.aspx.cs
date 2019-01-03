using ClaimProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject.User.Add
{
    public partial class userForm : System.Web.UI.Page
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
                function.GetListLevel(txtLevel, int.Parse(Session["UserPrivilegeId"].ToString()));
                BindData();
            }
        }

        void BindData()
        {
            txtUser.Text = "";
            txtName.Text = "";
            string sql = "";
            //string search = "";
            if (Session["UserPrivilegeId"].ToString() != "0")
            {
                sql = "SELECT * FROM tbl_user where level <> 0 and delete_status = '0' and username <> '" + Session["User"].ToString() + "' AND (username LIKE '%" + TextBox1.Text + "%' OR NAME LIKE '%" + TextBox1.Text + "%' OR LEVEL LIKE '%" + TextBox1.Text + "%' OR user_cpoint LIKE '%" + TextBox1.Text + "%')";
            }
            else
            {
                sql = "SELECT * FROM tbl_user where delete_status = '0' AND username <> '" + Session["User"].ToString() + "' AND (username LIKE '%" + TextBox1.Text + "%' OR NAME LIKE '%" + TextBox1.Text + "%' OR LEVEL LIKE '%" + TextBox1.Text + "%' OR user_cpoint LIKE '%" + TextBox1.Text + "%')";
            }
            MySqlDataAdapter da = function.MySqlSelectDataSet(sql);
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);
            UserGridView.DataSource = ds.Tables[0];
            UserGridView.DataBind();
            lbUserNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
        }

        protected void UserGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList txtEPrivilege = (DropDownList)e.Row.FindControl("txtEPrivilege");
                if ((txtEPrivilege != null))
                {
                    function.GetListLevel(txtEPrivilege, int.Parse(Session["UserPrivilegeId"].ToString()));
                    txtEPrivilege.SelectedIndex = txtEPrivilege.Items.IndexOf(txtEPrivilege.Items.FindByValue((string)DataBinder.Eval(e.Row.DataItem, "level").ToString()));
                }
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((LinkButton)e.Row.Cells[5].Controls[0]).OnClientClick = "return confirm('ต้องการลบข้อมูลใช่หรือไม่');";
                }
                catch { }
            }
        }

        protected void UserGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            UserGridView.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void UserGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            UserGridView.EditIndex = -1;
            BindData();
        }

        protected void UserGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string sqlUser = "";
            if (Session["UserPrivilegeId"].ToString() == "0")
            {
                TextBox txtEUser = (TextBox)UserGridView.Rows[e.RowIndex].FindControl("txtEUser");
                sqlUser = "username='" + txtEUser.Text + "',";
            }
            TextBox txtEName = (TextBox)UserGridView.Rows[e.RowIndex].FindControl("txtEName");
            DropDownList txtEPrivilege = (DropDownList)UserGridView.Rows[e.RowIndex].FindControl("txtEPrivilege");
            string cpoint = "1";
            if (txtEPrivilege.SelectedValue != "2" && txtEPrivilege.SelectedValue != "3")
            {
                cpoint = "0";
            }

            string sql = "UPDATE tbl_user SET " + sqlUser + " name='" + txtEName.Text + "',level='" + txtEPrivilege.SelectedValue + "',user_cpoint = '" + cpoint + "' WHERE id = '" + UserGridView.DataKeys[e.RowIndex].Value + "'";
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
            UserGridView.EditIndex = -1;
            BindData();

        }

        protected void UserGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = "UPDATE tbl_user SET delete_status = '1' WHERE id = '" + UserGridView.DataKeys[e.RowIndex].Value + "'";
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
            UserGridView.EditIndex = -1;
            BindData();
        }

        protected void btnUserAdd_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == txtPass.Text)
            {
                string sql_check = "SELECT * FROM tbl_user WHERE username = '" + txtUser.Text.Trim() + "'";
                string script = "";
                string cpoint = "1";
                if (txtLevel.SelectedValue != "2" && txtLevel.SelectedValue != "3")
                {
                    cpoint = "0";
                }
                MySqlDataReader rs = function.MySqlSelect(sql_check);
                if (!rs.Read())
                {
                    string sql = "INSERT INTO tbl_user (username,password,name,level,user_cpoint,delete_status) VALUES ('" + txtUser.Text.Trim() + "','" + txtPass.Text.Trim() + "','" + txtName.Text + "','" + txtLevel.SelectedValue + "','" + cpoint + "','0')";
                    if (function.MySqlQuery(sql))
                    {
                        script = "บันทึกข้อมูลสำเร็จ";
                    }
                    else
                    {
                        script = "Error : บันทึกข้อมูลล้มเหลว";
                    }
                }
                else
                {
                    script = "Error : มี Username ในระบบอยู่แล้ว";
                }
                rs.Close();
                function.Close();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('" + script + "')", true);
                UserGridView.EditIndex = -1;
                BindData();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('เพิ่มล้มเหลว < br /> -รหัสผ่านไม่ตรงกัน')", true);
            }
        }

        protected void UserGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UserGridView.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
}