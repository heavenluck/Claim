using ClaimProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.Web;

namespace ClaimProject
{
    public partial class Login : System.Web.UI.Page
    {
        ClaimFunction function = new ClaimFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string sql = "SELECT * FROM tbl_cpoint ORDER BY cpoint_id";
                function.getListItem(txtCpoint, sql, "cpoint_name", "cpoint_id");
            }
        }

        private void MsgBox(string message)
        {
            msgBox.Text = "<div class='alert alert-danger' style='font-size:large;'><strong>ผิดพลาด! </strong><br/>" + message + "</div>";
        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            string mess = "";
            if (txtUser.Text.Trim() == "")
            {
                mess += "- กรุณาป้อน Username<br/>";
            }

            if (txtPass.Text.Trim() == "")
            {
                mess += "- กรุณาป้อน Password<br/>";
            }

            if (mess == "")
            {
                string sql = "SELECT * FROM tbl_user WHERE username ='" + txtUser.Text.Trim() + "' AND PASSWORD = '" + txtPass.Text.Trim() + "'";
                MySqlDataReader rs = function.MySqlSelect(sql);
                if (rs.Read())
                {
                    if (!rs.IsDBNull(0))
                    {
                        string cpoint = "";
                        if (rs.GetString("user_cpoint") == "0")
                        {
                            cpoint = "0";
                        }
                        else
                        {
                            cpoint = txtCpoint.SelectedValue;
                        }
                        // Storee Session
                        Session.Add("User", txtUser.Text);
                        Session.Add("UserName", rs.GetString("name"));
                        Session.Add("UserPrivilegeId", rs.GetString("level"));
                        Session.Add("UserPrivilege", function.GetLevel(int.Parse(rs.GetString("level"))));
                        Session.Add("UserCpoint", cpoint);
                        Session.Timeout = 60 * 24;

                        //Response.Charset = "UTF-8";
                        HttpCookie newCookie = new HttpCookie("ClaimLogin");
                        newCookie["User"] = txtUser.Text;
                        newCookie["UserName"] = rs.GetString("name");
                        newCookie["UserPrivilegeId"] = rs.GetString("level");
                        newCookie["UserPrivilege"] = function.GetLevel(int.Parse(rs.GetString("level")));
                        newCookie["UserCpoint"] = cpoint;
                        newCookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(newCookie);
                        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('dd')</script>");
                        Response.Redirect("/");
                    }
                    else
                    {
                        mess += "- Username หรือ Password ไม่ถูกต้อง";
                    }
                }
                else
                {
                    mess += "- Username หรือ Password ไม่ถูกต้อง";
                }
                rs.Close();
                function.Close();
            }

            if (mess != "")
            {
                MsgBox(mess);
            }
            else
            {
                msgBox.Text = "";
            }

        }

        protected void linkDownload_Click(object sender, EventArgs e)
        {
            DownLoad("/Claim/Upload/chrome_installer.exe");
        }

        public void DownLoad(string FName)
        {
            try
            {
                string strURL = FName;
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"chrome_installer.exe\"");
                byte[] data = req.DownloadData(Server.MapPath(strURL));
                response.BinaryWrite(data);
                response.End();
            }
            catch
            {

            }
        }
    }
}