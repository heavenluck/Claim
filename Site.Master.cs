using ClaimProject.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject
{
    public partial class SiteMaster : MasterPage
    {
        ClaimFunction function = new ClaimFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("/Login");
            }
            else
            {
                lbUser.Text = "ยินดีต้อนรับ : " + Session["UserName"].ToString() + " : " + Session["UserPrivilege"] + " " + function.GetSelectValue("tbl_cpoint", "cpoint_id='" + Session["UserCpoint"].ToString() + "'", "cpoint_name");
                if (Session["UserPrivilegeId"].ToString() == "0" || Session["UserPrivilegeId"].ToString() == "1")
                {
                    nav3.Visible = true;
                    nav4.Visible = true;
                    nav5.Visible = true;
                    nav6.Visible = true;
                }
                else
                {
                    nav3.Visible = false;
                    nav4.Visible = false;
                    nav5.Visible = false;
                    nav6.Visible = false;
                }

                if (Session["UserCpoint"].ToString() != "0")
                {
                    nav0.Visible = false;
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Session.Contents.RemoveAll();
            Session.RemoveAll();
            Response.Redirect("/");
        }

    }
}