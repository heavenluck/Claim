using ClaimProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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
            BindData("");
        }
        void BindData(string key)
        {
            string sql = "";
            try
            {
                sql = "SELECT * FROM tbl_cm_detail cm JOIN tbl_device d ON cm.cm_detail_driver_id = d.device_id JOIN tbl_cm_status cms ON cm.cm_detail_status_id = cms.cm_status_id ORDER BY cm_detail_id DESC";
                MySqlDataAdapter da = function.MySqlSelectDataSet(sql);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds);
                CMGridView.DataSource = ds.Tables[0];
                CMGridView.DataBind();
                //lbCMNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            catch { }
        }
    }
}