using ClaimProject.Config;
using CrystalDecisions.CrystalReports.Engine;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClaimProject.Claim
{
    public partial class claimForm : System.Web.UI.Page
    {
        ClaimFunction function = new ClaimFunction();
        public string claim_id = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["add"] == "true")
            {
                btnAddClaim_Click(null, null);
            }

            if (!this.IsPostBack)
            {
                string date = DateTime.Now.ToString("dd-MM") + "-" + (DateTime.Now.Year + 543);
                BindData(date.Split('-')[2]);
                function.getListItem(txtSearchYear, "SELECT claim_budget_year FROM tbl_claim c GROUP BY claim_budget_year ORDER by claim_budget_year DESC", "claim_budget_year", "claim_budget_year");
                function.getListItem(txtSearchStatus, "SELECT * FROM tbl_status ORDER by status_id", "status_name", "status_id");
                txtSearchStatus.Items.Insert(0, new ListItem("ทั้งหมด", ""));
                txtSearchYear.SelectedValue = function.getBudgetYear(date);

                string sql = "";
                if (Session["UserCpoint"] != null)
                {
                    if (Session["UserCpoint"].ToString() == "0")
                    {
                        sql = "SELECT * FROM tbl_cpoint ORDER BY cpoint_id";
                        function.getListItem(txtSearchCpoint, sql, "cpoint_name", "cpoint_id");
                        txtSearchCpoint.Items.Insert(0, new ListItem("ทั้งหมด", ""));
                    }
                    else
                    {
                        sql = "SELECT * FROM tbl_cpoint WHERE cpoint_id = '" + Session["UserCpoint"].ToString() + "'";
                        function.getListItem(txtSearchCpoint, sql, "cpoint_name", "cpoint_id");
                        btnSearch_Click(null,null);
                    }
                }
                else
                {
                    Response.Redirect("/");
                }
            }
            if (Session["UserPrivilegeId"].ToString() == "4")
            {
                btnAddClaim.Visible = false;
            }
        }


        void BindData(string month)
        {
            string sql = "";
            if (Session["User"] != null)
            {
                if (month != "")
                {
                    sql = "SELECT * FROM tbl_claim c JOIN tbl_cpoint ON claim_cpoint = cpoint_id JOIN tbl_status ON status_id = claim_status LEFT JOIN tbl_user ON username=claim_user_start_claim JOIN tbl_status_detail sd ON sd.detail_claim_id = c.claim_id AND sd.detail_status_id = c.claim_status WHERE claim_delete = '0' AND (cpoint_id Like '%" + Session["UserCpoint"].ToString() + "%' AND claim_cpoint_note LIKE '%" + txtSearchComNumber.Text + "%' AND claim_equipment LIKE '%" + txtSearchComTitle.Text + "%' AND claim_budget_year = '" + txtSearchYear.SelectedValue + "' AND claim_start_date LIKE '%" + month + "%' AND claim_status LIKE '%" + txtSearchStatus.SelectedValue + "%' ) ORDER BY status_id ASC, STR_TO_DATE(claim_cpoint_date, '%d-%m-%Y') DESC";
                    Session["sql"] = sql;
                }
                else
                {
                    sql = "SELECT * FROM tbl_claim c JOIN tbl_cpoint ON claim_cpoint = cpoint_id JOIN tbl_status ON status_id = claim_status LEFT JOIN tbl_user ON username=claim_user_start_claim JOIN tbl_status_detail sd ON sd.detail_claim_id = c.claim_id AND sd.detail_status_id = c.claim_status WHERE claim_delete = '0' AND (cpoint_id Like '%" + txtSearchCpoint.SelectedValue + "%' AND claim_cpoint_note LIKE '%" + txtSearchComNumber.Text + "%' AND claim_equipment LIKE '%" + txtSearchComTitle.Text + "%' AND claim_budget_year = '" + txtSearchYear.SelectedValue + "' AND claim_start_date LIKE '%" + txtSearchDate.Text + "%' AND claim_status LIKE '%" + txtSearchStatus.SelectedValue + "%') ORDER BY status_id ASC, STR_TO_DATE(claim_cpoint_date, '%d-%m-%Y') DESC";
                    Session["sql"] = sql;
                }
            }

            try
            {


                MySqlDataAdapter da = function.MySqlSelectDataSet(sql);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds);
                ClaimGridView.DataSource = ds.Tables[0];
                ClaimGridView.DataBind();
                lbClaimNull.Text = "พบข้อมูลจำนวน " + ds.Tables[0].Rows.Count + " แถว";
            }
            catch { }
        }

        protected void ClaimGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            LinkButton lbCpoint = (LinkButton)(e.Row.FindControl("lbCpoint"));
            if (lbCpoint != null)
            {
                lbCpoint.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "claim_id");
            }

            LinkButton lbNoteCom = (LinkButton)(e.Row.FindControl("lbNoteCom"));
            if (lbNoteCom != null)
            {
                lbNoteCom.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "claim_id");
            }


            LinkButton lbEquipment = (LinkButton)(e.Row.FindControl("lbEquipment"));
            if (lbEquipment != null)
            {
                lbEquipment.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "claim_id");
                lbEquipment.ToolTip = (string)DataBinder.Eval(e.Row.DataItem, "claim_equipment"); ;
            }


            Label lbCpointDate = (Label)(e.Row.FindControl("lbCpointDate"));
            if (lbCpointDate != null)
            {
                lbCpointDate.Text = function.ConvertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "claim_cpoint_date"));
            }

            Label lbStartDate = (Label)(e.Row.FindControl("lbStartDate"));
            if (lbStartDate != null)
            {
                lbStartDate.Text = function.ConvertDateShortThai((string)DataBinder.Eval(e.Row.DataItem, "claim_start_date"));
            }

            Label lbDay = (Label)(e.Row.FindControl("lbDay"));
            if (lbDay != null)
            {
                string[] data = DataBinder.Eval(e.Row.DataItem, "claim_start_date").ToString().Split('-');
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

            Label lbStatus = (Label)(e.Row.FindControl("lbStatus"));
            if (lbStatus != null)
            {
                lbStatus.CssClass = "badge badge-" + (string)DataBinder.Eval(e.Row.DataItem, "status_alert");
            }

            LinkButton printReport1 = (LinkButton)(e.Row.FindControl("printReport1"));
            if (printReport1 != null)
            {
                printReport1.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "claim_id");
                if (DataBinder.Eval(e.Row.DataItem, "claim_status").ToString() == "6")
                {
                    printReport1.Visible = false;
                }
                //printReport1.OnClientClick = "document.forms[0].target ='_blank';";
                //printReport1.t
            }

            LinkButton printReport2 = (LinkButton)(e.Row.FindControl("printReport2"));
            if (printReport2 != null)
            {
                printReport2.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "claim_id");
                if (DataBinder.Eval(e.Row.DataItem, "claim_status").ToString() == "6")
                {
                    printReport2.Visible = false;
                }
                //printReport1.t
            }

            //*** Edit ***'
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList txtStatusEdit = (DropDownList)e.Row.FindControl("txtStatusEdit");
                if ((txtStatusEdit != null))
                {
                    string sql_status = "SELECT * FROM tbl_status";
                    function.getListItem(txtStatusEdit, sql_status, "status_name", "status_id");
                    txtStatusEdit.SelectedIndex = txtStatusEdit.Items.IndexOf(txtStatusEdit.Items.FindByValue((string)DataBinder.Eval(e.Row.DataItem, "status_id").ToString()));
                }
            }
        }

        protected void ClaimGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ClaimGridView.EditIndex = e.NewEditIndex;
            BindData("");
        }

        protected void ClaimGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ClaimGridView.EditIndex = -1;
            BindData("");
        }

        protected void ClaimGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            /*if (Session["UserPrivilegeId"].ToString() == "0" || Session["UserPrivilegeId"].ToString() == "1")
            {
                string sql = "UPDATE tbl_claim SET claim_delete = '1' WHERE claim_id = '" + ClaimGridView.DataKeys[e.RowIndex].Value + "'";
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
                BindData("");
            }*/
        }

        protected void ClaimGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ClaimGridView.PageIndex = e.NewPageIndex;
            BindData("");
        }

        protected void btnAddClaim_Click(object sender, EventArgs e)
        {
            string pkCode = "";
            Session["View"] = false;
            string cpoint = Session["UserCpoint"].ToString();
            if (cpoint.Length < 3)
            {
                cpoint = "10" + cpoint;
            }

            while (pkCode == "")
            {
                pkCode = function.GeneratorPK(int.Parse(cpoint));
            }

            if (pkCode != "")
            {
                Session["CodePK"] = pkCode;
                Response.Redirect("/Claim/claimDetailForm");
            }
        }

        protected void lbNoteCom_Command(object sender, CommandEventArgs e)
        {

            Session["codePK"] = e.CommandName;
            Session["View"] = false;
            if (function.GetSelectValue("tbl_claim", "claim_id='" + e.CommandName + "'", "claim_status") != "1" && function.GetSelectValue("tbl_claim", "claim_id='" + e.CommandName + "'", "claim_status") != "6")
            {
                Session["View"] = true;
            }

            if (Session["UserPrivilegeId"].ToString() == "4")
            {
                Session["View"] = true;
            }

            Response.Redirect("/Claim/claimDetailForm");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData("");
        }

        protected void ClaimGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DropDownList txtStatusEdit = (DropDownList)ClaimGridView.Rows[e.RowIndex].FindControl("txtStatusEdit");

            string sql = "UPDATE tbl_claim SET claim_status = '" + txtStatusEdit.SelectedValue + "' WHERE claim_id = '" + ClaimGridView.DataKeys[e.RowIndex].Value + "'";
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
            ClaimGridView.EditIndex = -1;
            BindData("");
        }
        protected bool ShowHide()
        {
            if (Session["UserPrivilegeId"].ToString() == "0" || Session["UserPrivilegeId"].ToString() == "1")
                return true;
            else
                return false;
        }

        void GetReport(string key, int report)
        {
            string startDate = "";
            string around = "";
            string time = "";
            string nameAleat = "";
            string posAleat = "";
            string cpointName = "";
            string cabinet = "";
            string cabinet_claim = "";
            string direction = "";
            string directionIn = "";
            string detail = "";
            string supper = "";
            string supperPos = "";
            string car = "";
            string licensePlate = "";
            string licensePlate2 = "";
            string province = "";
            string comeFrom = "";
            string nameDrive = "";
            string idcard = "";
            string address = "";
            string telDrive = "";
            string insurer = "";
            string clemence = "";
            string policyholders = "";
            string inform = "";
            string point = "";
            string noteTo = "";
            string noteTo1 = "";
            string noteNumber = "";
            string cpointDate = "";
            string title = "";
            string cpoint_manager = "";
            string title2 = "";
            string DateTitle = "";
            //..//
            string sql = "SELECT * FROM tbl_claim c JOIN tbl_claim_com cc ON cc.claim_id=c.claim_id JOIN tbl_cpoint cp ON cp.cpoint_id = c.claim_cpoint WHERE c.claim_id = '" + key + "'";

            MySqlDataReader rs = function.MySqlSelect(sql);
            if (rs.Read())
            {
                startDate = rs.GetString("claim_start_date");
                around = rs.GetString("claim_detail_around");
                time = rs.GetString("claim_detail_time");
                nameAleat = rs.GetString("claim_detail_user_alear");
                posAleat = rs.GetString("claim_detail_pos_user_alear");
                cpointName = rs.GetString("cpoint_name");
                cabinet = rs.GetString("claim_detail_cb");
                cabinet_claim = rs.GetString("claim_detail_cb_claim");
                direction = rs.GetString("claim_detail_direction");
                directionIn = rs.GetString("claim_detail_direction_in");
                detail = rs.GetString("claim_detail_accident");
                supper = rs.GetString("claim_detail_supervisor");
                supperPos = rs.GetString("claim_detail_supervisor_pos");

                car = rs.GetString("claim_detail_car").Replace(",", "").ToUpper();
                licensePlate = rs.GetString("claim_detail_license_plate");
                licensePlate2 = rs.GetString("claim_detail_lp2");
                province = rs.GetString("claim_detail_province");
                comeFrom = rs.GetString("claim_detail_comefrom");
                nameDrive = rs.GetString("claim_detail_driver");
                idcard = rs.GetString("claim_detail_idcard");
                address = rs.GetString("claim_detail_address");
                telDrive = rs.GetString("claim_detail_tel");
                insurer = rs.GetString("claim_detail_insurer");
                clemence = rs.GetString("claim_detail_clemence");
                policyholders = rs.GetString("claim_detail_policyholders");
                inform = rs.GetString("claim_detail_inform");
                point = rs.GetString("claim_detail_point");
                noteTo = rs.GetString("claim_detail_note_to");
                noteNumber = rs.GetString("claim_cpoint_note");
                cpointDate = rs.GetString("claim_cpoint_date");
                title = rs.GetString("claim_equipment");
                cpoint_manager = rs.GetString("cpoint_manager");

            }
            rs.Close();
            function.Close();

            string strNote = "เนื่องด้วยเมื่อวันที่ " + function.ConvertDatelongThai(startDate) + " " + around + " เวลาประมาณ " + time + " น. ได้รับแจ้งจาก" + nameAleat + " " + posAleat + " ปฏิบัติหน้าที่ประจำด่านฯ " + cpointName + (point != "" ? " " + point : "");
            if (cabinet != "") { strNote += " ตู้ " + cabinet; }
            strNote += " " + direction + " ได้แจ้งว่าเกิดอุบัติเหตุ" + detail + " ตู้ " + cabinet_claim + " จึงได้แจ้งรองผู้จัดการด่านฯ ประจำผลัด คือ " + supper + " ให้ทราบ";
            strNote += " หลังจากได้รับแจ้งเหตุเจ้าหน้าที่ควบคุมระบบและรองผู้จัดการด่านฯ ได้ลงไปตรวจสอบที่เกิดเหตุพร้อมบันทึกภาพไว้เป็นหลักฐาน พบคู่กรณีเป็น" + car;

            if (licensePlate == "" || licensePlate == "-" || licensePlate == "ไม่ทราบ")
            {
                strNote += "ไม่ทราบหมายเลขทะเบียน";
            }
            else
            {
                strNote += " หมายเลขทะเบียน " + licensePlate;

                if (licensePlate2 != "" && licensePlate2 != "-") { strNote += " ส่วนพ่วงหมายเลขทะเบียน " + licensePlate2; }
                strNote += " จังหวัด" + province + " ขับรถมาจาก" + comeFrom + "มุ่งหน้า" + directionIn + " โดยมี" + nameDrive + " เลขที่บัตรประจำตัวประชาชน " + idcard + " ที่อยู่ " + address + (telDrive.Trim() != "" && telDrive.Trim() != "-" ? " หมายเลขโทรศัพท์ " + telDrive : "") + " เป็นผู้ขับรถยนต์คันดังกล่าว";
                if (insurer.Trim() == "" || insurer.Trim() == "-")
                {
                    strNote += " ซึ่งรถยนต์คันดังกล่าวไม่ได้ทำประกันไว้";
                }
                else
                {
                    strNote += " ซึ่งรถยนต์คันดังกล่าวได้ทำประกันไว้กับ" + insurer + " หมายเลขเคลมเลขที่ " + clemence + " หมายเลขกรมธรรม์ " + policyholders;
                }
                strNote += " ทั้งนี้ ด่านฯ " + cpointName + (point != "" ? " " + point : "") + " ได้ดำเนินการแจ้งความร้องทุกข์ไว้ที่ " + inform + " ไว้เป็นหลักฐานแล้ว";
            }

            string name = "";
            string com = "";
            string dev = "";
            string listDoc = "";
            string doc_num = "";

            string sql_com = "SELECT * FROM tbl_claim_com_working WHERE detail_com_id ='" + key + "'";
            string sql_dev = "SELECT * FROM tbl_device_damaged d JOIN tbl_device dd ON d.device_id = dd.device_id WHERE claim_id ='" + key + "' AND d.device_damaged_delete = '0'";
            int i = 1;

            if (report == 0)
            {
                rs = function.MySqlSelect(sql_com);
                while (rs.Read())
                {
                    if (i == 1)
                    {
                        name += "(" + rs.GetString("com_working_name") + ")\r\n" + rs.GetString("com_working_pos");
                        com += "ซึ่งมีเจ้าหน้าที่ควบคุมระบบปฏิบัติหน้าที่ประจำผลัด ดังนี้\r\n                      ";
                        com += i + ". " + rs.GetString("com_working_name");
                    }
                    else
                    {
                        com += "\r\n                      " + i + ". " + rs.GetString("com_working_name");
                    }
                    i++;
                }
                rs.Close();
                name += "\r\n\r\n\r\n";
                name += "(" + function.GetSelectValue("tbl_claim_com", "claim_id='" + key + "'", "claim_detail_supervisor") + ")";
                name += "\r\n" + function.GetSelectValue("tbl_claim_com", "claim_id='" + key + "'", "claim_detail_supervisor_pos");

                function.Close();

                i = 1;
                rs = function.MySqlSelect(sql_dev);
                while (rs.Read())
                {
                    if (i == 1)
                    {
                        dev += "จากการลงไปตรวจสอบเบื้องต้นพบทรัพย์สินของทางราชการเสียหาย ดังนี้\r\n                      ";
                        dev += i + ". " + rs.GetString("device_name") + " " + rs.GetString("device_damaged");
                    }
                    else
                    {
                        dev += "\r\n                      " + i + ". " + rs.GetString("device_name") + " " + rs.GetString("device_damaged");
                    }
                    i++;
                }
                rs.Close();
                function.Close();
            }
            else
            {
                i = 1;
                rs = function.MySqlSelect(sql_dev);
                while (rs.Read())
                {
                    if (i == 1)
                    {
                        dev += "ความเสียหายของทรัพย์สินของทางราชการ เบื้องต้นสรุปได้ ดังนี้\r\n                      ";
                        dev += i + ". " + rs.GetString("device_name") + " " + rs.GetString("device_damaged");
                    }
                    else
                    {
                        dev += "\r\n                      " + i + ". " + rs.GetString("device_name") + " " + rs.GetString("device_damaged");
                    }
                    i++;
                }
                rs.Close();
                function.Close();


                string sql_doc = "SELECT * FROM tbl_claim_doc WHERE claim_doc_id = '" + key + "' AND claim_doc_type = '0'";
                rs = function.MySqlSelect(sql_doc);
                if (rs.Read())
                {
                    doc_num = rs.GetString("claim_doc_num");
                    noteTo1 = rs.GetString("claim_doc_to");
                    listDoc += "เอกสารประกอบการพิจารณาแนบ ดังนี้";
                    listDoc += "\r\n                      1. บันทึกข้อความ " + converNum(rs.GetString("claim_doc_no1")) + " ฉบับ";
                    listDoc += "\r\n                      2. สำเนาบันทึกการเปรียบเทียบปรับ จำนวน " + converNum(rs.GetString("claim_doc_no2")) + " ฉบับ";
                    listDoc += "\r\n                      3. สำเนาใบเสร็จค่าปรับ จำนวน " + converNum(rs.GetString("claim_doc_no3")) + " ฉบับ";
                    listDoc += "\r\n                      4. บันทึกข้อมูลการเกิดอุบัติเหตุเบื้องต้นสำหรับการแจ้งความ จำนวน " + converNum(rs.GetString("claim_doc_no4")) + " ฉบับ";
                    listDoc += "\r\n                      5. รายงานอุบัติเหตุบนทางหลวง (ส.3-02) จำนวน " + converNum(rs.GetString("claim_doc_no5")) + " ฉบับ";
                    listDoc += "\r\n                      6. สำเนารายงานประจำวันเกี่ยวกับคดี จำนวน จำนวน " + converNum(rs.GetString("claim_doc_no6")) + " ฉบับ";
                    listDoc += "\r\n                      7. ข้อมูลเบื้องต้นจากการสอบปากคำผู้เกี่ยวข้อง สป.11 จำนวน " + converNum(rs.GetString("claim_doc_no7")) + " ฉบับ";
                    listDoc += "\r\n                      8. หนังสือยอมรับความผิด จำนวน " + converNum(rs.GetString("claim_doc_no8")) + " ฉบับ";
                    listDoc += "\r\n                      9. สำเนาบัตรประจำตัวประชาชน จำนวน " + converNum(rs.GetString("claim_doc_no9")) + " ฉบับ";
                    listDoc += "\r\n                      10. สำเนาใบอนุญาตขับรถ จำนวน " + converNum(rs.GetString("claim_doc_no10")) + " ฉบับ";
                    listDoc += "\r\n                      11. สำเนาใบรับรองความเสียหายต่อทรัพย์สิน (ใบเคลมประกัน) จำนวน " + converNum(rs.GetString("claim_doc_no11")) + " ฉบับ";
                    listDoc += "\r\n                      12. บันทึกข้อความรายงานของ พ.ควบคุมระบบ และรองผจด.ประจำผลัด จำนวน " + converNum(rs.GetString("claim_doc_no12")) + " ฉบับ";
                    listDoc += "\r\n                      13. รูปภาพประกอบ จำนวน " + converNum(rs.GetString("claim_doc_no13")) + " ฉบับ";
                    title2 = rs.GetString("claim_doc_title");
                    DateTitle = rs.GetString("claim_doc_date");
                }
                rs.Close();
                function.Close();
            }

            ReportDocument rpt = new ReportDocument();
            string cpoint_title = "ด่านฯ " + cpointName ;
            if (report == 0)
            {
                cpoint_title += " "+point;
                rpt.Load(Server.MapPath("/Claim/reportCom.rpt"));
                doc_num = noteNumber;
                rpt.SetParameterValue("list_com", com != "" ? com  : "");
                rpt.SetParameterValue("name", name);
                rpt.SetParameterValue("txt_to", noteTo);
                rpt.SetParameterValue("note_title", title);
                rpt.SetParameterValue("date_thai", function.ConvertDatelongThai(cpointDate));
            }
            else
            {
                rpt.Load(Server.MapPath("/Claim/reportOfficialBooks.rpt"));
                rpt.SetParameterValue("list_doc", listDoc != "" ? listDoc : "");
                rpt.SetParameterValue("name", "(" + cpoint_manager + ")\r\nผู้จัดการด่านฯ " + cpointName);
                rpt.SetParameterValue("txt_to", noteTo1);
                rpt.SetParameterValue("note_title", title2);
                rpt.SetParameterValue("date_thai", function.ConvertDatelongThai(DateTitle));
            }
            cpoint_title += " ฝ่ายบริหารการจัดเก็บเงินค่าธรรมเนียม โทร. " + function.GetSelectValue("tbl_cpoint", "cpoint_name='" + cpointName + "'", "cpoint_tel");
            rpt.SetParameterValue("cpoint_title", cpoint_title);
            rpt.SetParameterValue("num_title", doc_num);
            rpt.SetParameterValue("note_text", strNote);

            rpt.SetParameterValue("part_img", Server.MapPath("/Claim/300px-Thai_government_Garuda_emblem_(Version_2).jpg"));

            rpt.SetParameterValue("list_dev", dev);


            Session["Report"] = rpt;
            Session["ReportTitle"] = "บันทึกข้อความ";
            //Response.Redirect("/Report/reportView", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Report/reportView','_newtab');", true);
        }

        protected void printReport1_Command(object sender, CommandEventArgs e)
        {
            GetReport(e.CommandName, 0);
        }

        protected void printReport2_Command(object sender, CommandEventArgs e)
        {
            claim_id = e.CommandName;
            claimID.Text = e.CommandName;
            Session["claim_id"] = e.CommandName;
            clearDate();
            string sql_query = "SELECT * FROM tbl_claim_doc WHERE claim_doc_id = '" + e.CommandName + "' and claim_doc_type = '0'";
            MySqlDataReader rs = function.MySqlSelect(sql_query);
            if (rs.Read())
            {
                txtNoteTo.Text = rs.GetString("claim_doc_to");
                txtDocNum.Text = rs.GetString("claim_doc_num").Split('/')[3];
                txtDate.Text = rs.GetString("claim_doc_date");
                txtTitle.Text = rs.GetString("claim_doc_title");
                txtNo1.Text = rs.GetString("claim_doc_no1");
                txtNo2.Text = rs.GetString("claim_doc_no2");
                txtNo3.Text = rs.GetString("claim_doc_no3");
                txtNo4.Text = rs.GetString("claim_doc_no4");
                txtNo5.Text = rs.GetString("claim_doc_no5");
                txtNo6.Text = rs.GetString("claim_doc_no6");
                txtNo7.Text = rs.GetString("claim_doc_no7");
                txtNo8.Text = rs.GetString("claim_doc_no8");
                txtNo9.Text = rs.GetString("claim_doc_no9");
                txtNo10.Text = rs.GetString("claim_doc_no10");
                txtNo11.Text = rs.GetString("claim_doc_no11");
                txtNo12.Text = rs.GetString("claim_doc_no12");
                txtNo13.Text = rs.GetString("claim_doc_no13");
            }
            rs.Close();
            function.Close();
        }

        protected void btnSavePrint_Click(object sender, EventArgs e)
        {
            if (Session["claim_id"].ToString() != "")
            {
                string note_number = "กท./ฝจ./" + function.GetSelectValue("tbl_claim JOIN tbl_cpoint ON cpoint_id = claim_cpoint", "claim_id='" + Session["claim_id"].ToString() + "'", "cpoint_name") + "/";
                note_number += txtDocNum.Text.Trim() == "" ? "          " : txtDocNum.Text.Trim() + "/" + function.GetSelectValue("tbl_claim", "claim_id='" + Session["claim_id"].ToString() + "'", "claim_cpoint_date").Split('-')[2]; ;
                string note_to = txtNoteTo.Text;
                string[] textValue = new string[16];
                textValue[0] = txtNo1.Text.Trim();
                textValue[1] = txtNo2.Text.Trim();
                textValue[2] = txtNo3.Text.Trim();
                textValue[3] = txtNo4.Text.Trim();
                textValue[4] = txtNo5.Text.Trim();
                textValue[5] = txtNo6.Text.Trim();
                textValue[6] = txtNo7.Text.Trim();
                textValue[7] = txtNo8.Text.Trim();
                textValue[8] = txtNo9.Text.Trim();
                textValue[9] = txtNo10.Text.Trim();
                textValue[10] = txtNo11.Text.Trim();
                textValue[11] = txtNo12.Text.Trim();
                textValue[12] = txtNo13.Text.Trim();

                string sql_query = "SELECT * FROM tbl_claim_doc WHERE claim_doc_id = '" + Session["claim_id"].ToString() + "' and claim_doc_type = '0'";
                MySqlDataReader rs = function.MySqlSelect(sql_query);
                if (!rs.Read())
                {
                    string sql = "INSERT INTO tbl_claim_doc ( claim_doc_id,claim_doc_title,claim_doc_date,claim_doc_num, claim_doc_type, claim_doc_to, claim_doc_no1, claim_doc_no2, claim_doc_no3, claim_doc_no4, claim_doc_no5, claim_doc_no6, claim_doc_no7, claim_doc_no8, claim_doc_no9, claim_doc_no10, claim_doc_no11, claim_doc_no12, claim_doc_no13, claim_doc_no14, claim_doc_no15, claim_doc_no16) VALUES ( '" + Session["claim_id"].ToString() + "','" + txtTitle.Text.Trim() + "','" + txtDate.Text.Trim() + "','" + note_number + "', '0', '" + note_to + "', '" + textValue[0] + "', '" + textValue[1] + "', '" + textValue[2] + "', '" + textValue[3] + "', '" + textValue[4] + "', '" + textValue[5] + "', '" + textValue[6] + "', '" + textValue[7] + "', '" + textValue[8] + "', '" + textValue[9] + "', '" + textValue[10] + "', '" + textValue[11] + "', '" + textValue[12] + "', '0', '0', '0' )";
                    if (function.MySqlQuery(sql))
                    {

                    }
                }
                else
                {
                    string sql = "UPDATE tbl_claim_doc SET claim_doc_title='" + txtTitle.Text.Trim() + "',claim_doc_date='" + txtDate.Text.Trim() + "',claim_doc_to = '" + note_to + "',claim_doc_num='" + note_number + "', claim_doc_no1 = '" + textValue[0] + "', claim_doc_no2 = '" + textValue[1] + "', claim_doc_no3 = '" + textValue[2] + "', claim_doc_no4 = '" + textValue[3] + "', claim_doc_no5 = '" + textValue[4] + "', claim_doc_no6 = '" + textValue[5] + "', claim_doc_no7 = '" + textValue[6] + "', claim_doc_no8 = '" + textValue[7] + "', claim_doc_no9 = '" + textValue[8] + "', claim_doc_no10 = '" + textValue[9] + "', claim_doc_no11 = '" + textValue[10] + "', claim_doc_no12 = '" + textValue[11] + "', claim_doc_no13 = '" + textValue[12] + "' WHERE claim_doc_id = '" + Session["claim_id"].ToString() + "' AND claim_doc_type = '0'";
                    if (function.MySqlQuery(sql))
                    {

                    }
                }

                GetReport(Session["claim_id"].ToString(), 1);
                //clearDate();
            }
        }

        void clearDate()
        {
            txtNoteTo.Text = "";
            txtDocNum.Text = "";
            txtTitle.Text = "";
            txtDate.Text = "";
            txtNo1.Text = "";
            txtNo2.Text = "";
            txtNo3.Text = "";
            txtNo4.Text = "";
            txtNo5.Text = "";
            txtNo6.Text = "";
            txtNo7.Text = "";
            txtNo8.Text = "";
            txtNo9.Text = "";
            txtNo10.Text = "";
            txtNo11.Text = "";
            txtNo12.Text = "";
            txtNo13.Text = "";
        }

        private string converNum(string num)
        {
            if (num == "0")
            {
                num = "-";
            }
            return num;
        }

    }
}