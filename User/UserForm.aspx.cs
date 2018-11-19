using ClaimProject.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject.User
{
    public partial class UserForm : System.Web.UI.Page
    {
        ClaimFunction function = new ClaimFunction();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirmPass_Click(object sender, EventArgs e)
        {
            string script = "";
            if (txtNewPass.Text.Trim() == txtConfirmNewPass.Text.Trim()&& txtNewPass.Text.Trim() != "" && txtConfirmNewPass.Text.Trim() != "")
            {
                string sql = "UPDATE tbl_user SET password = '"+txtNewPass.Text.Trim()+ "' WHERE username='"+ Session["User"].ToString() + "'";
                if (function.MySqlQuery(sql))
                {
                    txtNewPass.Text = "";
                    txtConfirmNewPass.Text = "";
                    script = "เปลี่ยนรหัสผ่านสำเร็จสำเร็จ<br/>";
                }
                else
                {
                    script = "เปลี่ยนรหัสผ่านสำเร็จล้มเหลว<br/>";
                }
            }
            else
            {
                script = "ใส่ข้อมูลไม่ครบถ้วน หรือ รหัสผ่านใหม่ไม่ตรงกัน";
            }
            function.Close();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('" + script + "')", true);
        }
    }
}