using ClaimProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject.Techno
{
    public partial class TechnoFormView : System.Web.UI.Page
    {
        ClaimFunction function = new ClaimFunction();
        public string status = "";
        string year = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                if (Session["UserCpoint"].ToString() != "0")
                {
                    Response.Redirect("/Claim/claimForm");
                }
                //Response.Redirect("/Claim/claimForm");
                if (!string.IsNullOrEmpty(Request.Params["s"]) && !string.IsNullOrEmpty(Request.Params["y"]))
                {
                    status = Request.Params["s"];
                    year = Request.Params["y"];
                }

                if (!this.IsPostBack)
                {
                    BindData("", status, year);
                }
            }
        }

        protected void ClaimGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton lbCpoint = (LinkButton)(e.Row.FindControl("lbCpoint"));
            if (lbCpoint != null)
            {
                lbCpoint.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "claim_id");
            }

            LinkButton lbEquipment = (LinkButton)(e.Row.FindControl("lbEquipment"));
            if (lbEquipment != null)
            {
                lbEquipment.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "claim_id");
                lbEquipment.Text = function.ShortText((string)DataBinder.Eval(e.Row.DataItem, "claim_equipment"));
                lbEquipment.ToolTip = (string)DataBinder.Eval(e.Row.DataItem, "claim_equipment");
            }

            Label _lbDateStart = (Label)(e.Row.FindControl("_lbDateStart"));
            if (_lbDateStart != null)
            {
                _lbDateStart.Text = function.ConvertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "claim_start_date"));
            }

            Label lbStartDate = (Label)(e.Row.FindControl("lbStartDate"));
            if (lbStartDate != null)
            {
                lbStartDate.Text = function.ConvertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "detail_date_start"));
            }

            Label lbDay = (Label)(e.Row.FindControl("lbDay"));
            if (lbDay != null)
            {
                string[] data = DataBinder.Eval(e.Row.DataItem, "detail_date_start").ToString().Split('-');
                DateTime dateStart = DateTime.ParseExact(data[0] + "-" + data[1] + "-" + (int.Parse(data[2]) - 543), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateDifference differnce = new DateDifference(dateStart);

                if (differnce.ToString() == "")
                {
                    lbDay.CssClass = "badge badge-danger";
                    lbDay.Text = "NEW!!";
                }
                else
                {
                    lbDay.Text = differnce.ToString();
                }
            }

            Label lbCountdown = (Label)(e.Row.FindControl("lbCountdown"));
            if (lbDay != null)
            {
                string[] data = DataBinder.Eval(e.Row.DataItem, "detail_date_end").ToString().Split('-');
                DateTime dateStart = DateTime.ParseExact(data[0] + "-" + data[1] + "-" + (int.Parse(data[2]) - 543), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateDifference differnce = new DateDifference(dateStart);
                if (dateStart < DateTime.Now.Date)
                {
                    lbCountdown.Text = "เกินกำหนดมา " + differnce.ToString();
                    lbCountdown.CssClass = "text-danger";
                }
                else
                {
                    if (differnce.ToString() != "")
                    {
                        lbCountdown.Text = "ครบกำหนดอีก " + differnce.ToString();
                        lbCountdown.CssClass = "text-success";
                    }
                    else
                    {
                        lbCountdown.Text = "ครบกำหนดวันนี้ ";
                        lbCountdown.CssClass = "text-warning";
                    }
                }

                if ((string)DataBinder.Eval(e.Row.DataItem, "status_name") == "ส่งงาน/เสร็จสิ้น")
                {
                    lbCountdown.Text = "เสร็จสิ้น";
                    lbCountdown.CssClass = "text-success";
                }
            }

            Label lbStatus = (Label)(e.Row.FindControl("lbStatus"));
            if (lbStatus != null)
            {
                lbStatus.CssClass = "badge badge-" + (string)DataBinder.Eval(e.Row.DataItem, "status_alert");
            }

            LinkButton btnChangeStatus = (LinkButton)(e.Row.FindControl("btnChangeStatus"));
            if (btnChangeStatus != null)
            {
                btnChangeStatus.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "claim_id");
            }
        }

        protected void ClaimGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ClaimGridView.PageIndex = e.NewPageIndex;
            BindData("", status, year);
        }

        void BindData(string txtSearch, string status, string year)
        {
            string sql = "";
            try
            {
                sql = "SELECT * FROM tbl_claim c JOIN tbl_cpoint ON claim_cpoint = cpoint_id JOIN tbl_status ON status_id = claim_status LEFT JOIN tbl_user ON username=claim_user_start_claim JOIN tbl_status_detail sd ON sd.detail_claim_id = c.claim_id AND sd.detail_status_id = c.claim_status WHERE claim_delete = '0' AND c.claim_status = '" + status + "' AND claim_budget_year = '" + year + "' AND (cpoint_name LIKE '%" + txtSearch + "%' OR claim_cpoint_note LIKE '%" + txtSearch + "%' OR claim_equipment LIKE '%" + txtSearch + "%' ) ORDER BY status_id ASC, STR_TO_DATE(claim_cpoint_date, '%d-%m-%Y') ASC";
                Session["sql"] = sql;

                MySqlDataAdapter da = function.MySqlSelectDataSet(sql);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds);
                ClaimGridView.DataSource = ds.Tables[0];
                ClaimGridView.DataBind();
                lbClaimNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(txtSearch.Text.Trim(), status, year);
        }

        protected void lbCpoint_Command(object sender, CommandEventArgs e)
        {
            Session["codePK"] = e.CommandName;
            Session["View"] = true;
            Response.Redirect("/Claim/claimDetailForm");
        }

        protected void btnChangeStatus_Command(object sender, CommandEventArgs e)
        {
            Session["codePK"] = e.CommandName;
            Response.Redirect("/Techno/TechnoFormDetail");
        }
    }
}