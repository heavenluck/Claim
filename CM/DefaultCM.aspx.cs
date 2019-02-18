using ClaimProject.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject.CM
{
    public partial class DefaultCM : System.Web.UI.Page
    {
        ClaimFunction function = new ClaimFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("/");
            }
            if (function.CheckLevel("Department", Session["UserPrivilegeId"].ToString()))
            {
                Div6.Visible = true;
            }
            else
            {
                Div6.Visible = false;
            }
        }
    }
}