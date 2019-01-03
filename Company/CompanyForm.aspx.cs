using ClaimProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject.Company
{
    public partial class CompanyForm : System.Web.UI.Page
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
                sql = "SELECT * FROM tbl_company WHERE company_status <> 1 AND company_name Like '%" + key + "%' ORDER BY company_name ASC";
                MySqlDataAdapter da = function.MySqlSelectDataSet(sql);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds);
                CompanyGridView.DataSource = ds.Tables[0];
                CompanyGridView.DataBind();
                lbDeviceNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            catch { }
        }

        protected void CompanyGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void CompanyGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CompanyGridView.EditIndex = e.NewEditIndex;
            BindData(txtSearch.Text);
        }

        protected void CompanyGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CompanyGridView.EditIndex = -1;
            BindData(txtSearch.Text);
        }

        protected void CompanyGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtECompany = (TextBox)CompanyGridView.Rows[e.RowIndex].FindControl("txtECompany");

            string sql = "UPDATE tbl_company SET company_name='" + txtECompany.Text + "' WHERE company_id = '" + CompanyGridView.DataKeys[e.RowIndex].Value + "'";
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
            CompanyGridView.EditIndex = -1;
            BindData(txtSearch.Text);
            txtSearch.Text = "";
        }

        protected void CompanyGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = "UPDATE tbl_company SET company_status='1' WHERE company_id = '" + CompanyGridView.DataKeys[e.RowIndex].Value + "'";
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
            CompanyGridView.EditIndex = -1;
            BindData(txtSearch.Text);
            txtSearch.Text = "";
        }

        protected void btnCompanyAdd_Click(object sender, EventArgs e)
        {
            if (txtCompanyName.Text != "")
            {
                string sql = "INSERT INTO tbl_company (company_name,company_status) VALUES ('" + txtCompanyName.Text.Trim() + "','0')";
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
                CompanyGridView.EditIndex = -1;
                BindData(txtCompanyName.Text);
                txtCompanyName.Text = "";
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('เพิ่มล้มเหลว < br /> -กรุณาใส่ชื่ออุปกรณ์')", true);
            }
            ClearData();
        }

        void ClearData()
        {
            txtCompanyName.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(txtSearch.Text);
        }
    }
}