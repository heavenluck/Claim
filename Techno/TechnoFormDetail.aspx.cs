using ClaimProject.Config;
using CrystalDecisions.CrystalReports.Engine;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ClaimProject.Techno
{
    public partial class TechnoFormDetail : System.Web.UI.Page
    {
        ClaimFunction function = new ClaimFunction();
        public string Quotations_id = "";
        int Quotations = 7;
        int SendTo = 15;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                if (Session["UserCpoint"].ToString() != "0")
                {
                    Response.Redirect("/Claim/claimForm");
                }

                if (!this.IsPostBack)
                {
                    if (txtDateOrder.Text == "")
                    {
                        txtDateOrder.Text = DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year + 543);
                    }
                    if (txtDateSendOrder.Text == "")
                    {
                        txtDateSendOrder.Text = DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year + 543);
                        txtDateSendOrder_TextChanged(null, null);
                    }
                    PageLoadData();
                    string sql = "SELECT * FROM tbl_company ORDER BY company_name";
                    function.getListItem(txtCompany, sql, "company_name", "company_id");
                    //lbTitle.Text = Session["codePK"].ToString();
                    sql = "SELECT * FROM tbl_quotations q JOIN tbl_company c ON q.quotations_company_id = c.company_id WHERE q.quotations_claim_id = '" + Session["codePK"].ToString() + "' AND quotations_delete = '0'";
                    function.getListItem(txtCompanyOrder, sql, "company_name", "company_id");

                }

                if (int.Parse(Session["status_id"].ToString()) >= 3)
                {
                    if (int.Parse(Session["status_id"].ToString()) != 3)
                    {
                        btnSaveNoteTo.Visible = false;
                    }

                    string[] readText = File.ReadAllLines(HostingEnvironment.MapPath("/Config/") + "ListDocTechno.txt");
                    int num = 1;
                    foreach (string s in readText)
                    {
                        if (num != 7)
                        {
                            AddControls(num, num + ". " + s + " จำนวน", Panel1);
                        }
                        else
                        {
                            AddControls(num, num + ". " + s + " " + function.GetSelectValue("tbl_claim_com", "claim_id='" + Session["codePK"].ToString() + "'", "claim_detail_insurer") + " จำนวน", Panel1);
                        }
                        num++;
                    }

                    string sql_doc = "SELECT * FROM tbl_quotations q JOIN tbl_company c ON c.company_id = q.quotations_company_id WHERE q.quotations_claim_id = '" + Session["codePK"].ToString() + "'";
                    MySqlDataReader rs = function.MySqlSelect(sql_doc);
                    while (rs.Read())
                    {
                        AddControls(num, num + ". ใบประเมินราคาค่าเสียหาย ของ " + rs.GetString("company_name") + " จำนวน", Panel1);
                        num++;
                    }
                    rs.Close();
                    function.Close();
                    if (!this.IsPostBack)
                    {
                        getDataStatus3();
                    }

                }

                if (int.Parse(Session["status_id"].ToString()) >= 4)
                {
                    getDataStatus4();
                }

                if (int.Parse(Session["status_id"].ToString()) >= 5)
                {
                    getDataStatus5();
                }
            }
        }

        void PageLoadData()
        {

            string sql = "SELECT * FROM tbl_claim c JOIN tbl_claim_com cc ON c.claim_id = cc.claim_id JOIN tbl_cpoint cp ON cp.cpoint_id = c.claim_cpoint JOIN tbl_status s ON s.status_id = c.claim_status WHERE c.claim_id = '" + Session["codePK"].ToString() + "'";
            MySqlDataReader rs = function.MySqlSelect(sql);
            if (rs.Read())
            {
                EnableBtn(rs.GetString("status_id"));
                Session["status_id"] = rs.GetString("status_id");
                lbTitle.Text = rs.GetString("claim_equipment");
                lbTitleStatus.CssClass = "badge badge-" + rs.GetString("status_alert");
                lbTitleStatus.Text = rs.GetString("status_name");
                lbCpoint.Text = rs.GetString("cpoint_name");
                lbCb.Text = rs.GetString("claim_detail_cb_claim");
                if (rs.GetString("claim_detail_point").ToLower() != "tsb" && rs.GetString("claim_detail_point").ToLower() != "") { lbCpoint.Text += " " + rs.GetString("claim_detail_point"); }
                lbDate.Text = function.ConvertDatelongThai(rs.GetString("claim_start_date"));
                lbDate.Text += " เวลา " + rs.GetString("claim_detail_time") + " น.";
                lbAround.Text = rs.GetString("claim_detail_around");
                lbAlert.Text = rs.GetString("claim_detail_user_alear") + " ตำแหน่ง" + rs.GetString("claim_detail_pos_user_alear");
                lbDetail.Text = rs.GetString("claim_detail_accident");
                lbCar.Text = rs.GetString("claim_detail_car");
                lbLP.Text = rs.GetString("claim_detail_license_plate");
                if (rs.GetString("claim_detail_lp2") != "") { lbLP.Text += " ทะเบียนส่วนพ่วง " + rs.GetString("claim_detail_lp2"); }
                lbDriver.Text = rs.GetString("claim_detail_driver");
                lbIDCard.Text = rs.GetString("claim_detail_idcard");
                lbAddress.Text = rs.GetString("claim_detail_address");
                lbTel.Text = rs.GetString("claim_detail_tel");
                lbInsure.Text = rs.GetString("claim_detail_insurer");
                lbClaimNum.Text = rs.GetString("claim_detail_clemence");
                lbPolicy.Text = rs.GetString("claim_detail_policyholders");
                lbInform.Text = rs.GetString("claim_detail_inform");
                lbEmp.Text = rs.GetString("claim_detail_supervisor") + " ตำแหน่ง " + rs.GetString("claim_detail_supervisor_pos") + "<br/>";
            }
            else
            {
                Response.Redirect("/Techno/TechnoFormView");
            }
            rs.Close();
            function.Close();

            sql = "SELECT * FROM tbl_claim_com_working WHERE detail_com_id = '" + Session["codePK"].ToString() + "'";
            rs = function.MySqlSelect(sql);
            int i = 1;
            lbEmpCom.Text = "";
            while (rs.Read())
            {
                lbEmpCom.Text += i + ". " + rs.GetString("com_working_name") + " ตำแหน่ง " + rs.GetString("com_working_pos") + "<br/>";
                i++;
            }
            rs.Close();
            function.Close();

            sql = "SELECT* FROM tbl_device_damaged dd JOIN tbl_device d ON d.device_id = dd.device_id WHERE dd.claim_id = '" + Session["codePK"].ToString() + "'";
            rs = function.MySqlSelect(sql);
            lbDevice.Text = "";
            i = 1;
            while (rs.Read())
            {
                lbDevice.Text += i + ". " + rs.GetString("device_name") + " / " + rs.GetString("device_damaged") + "<br/>";
                i++;
            }
            rs.Close();
            function.Close();
            BindConpaney();
        }

        void EnableBtn(string status)
        {
            switch (status)
            {
                case "1":
                    btns1.Visible = true;
                    btns2.Visible = false;
                    btns3.Visible = false;
                    btns4.Visible = false;
                    Div1.Visible = false;
                    Div2.Visible = false;
                    Div3.Visible = false;
                    Div4.Visible = false;
                    break;
                case "2":
                    btns1.Visible = true;
                    btns2.Visible = true;
                    btns3.Visible = false;
                    btns4.Visible = false;
                    Div1.Visible = true;
                    Div2.Visible = false;
                    Div3.Visible = false;
                    Div4.Visible = false;
                    break;
                case "3":
                    btns1.Visible = false;
                    btns2.Visible = false;
                    btns3.Visible = true;
                    btns4.Visible = false;
                    Div1.Visible = true;
                    Div2.Visible = true;
                    Div3.Visible = false;
                    Div4.Visible = false;
                    break;
                case "4":
                    btns1.Visible = false;
                    btns2.Visible = false;
                    btns3.Visible = false;
                    btns4.Visible = true;
                    Div1.Visible = true;
                    Div2.Visible = true;
                    Div3.Visible = true;
                    Div4.Visible = false;
                    break;
                case "5":
                    btns1.Visible = false;
                    btns2.Visible = false;
                    btns3.Visible = false;
                    btns4.Visible = false;
                    Div1.Visible = true;
                    Div2.Visible = true;
                    Div3.Visible = true;
                    Div4.Visible = true;
                    break;
            }
        }

        protected void btns1_Click(object sender, EventArgs e)
        {
            BindConpaney();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#QuotationsModel').modal();", true);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#QuotationsModel').modal('show');</script>", false);
        }


        protected void QuotationsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            QuotationsGridView.EditIndex = e.NewEditIndex;
            BindConpaney();
        }

        protected void QuotationsGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            QuotationsGridView.EditIndex = -1;
            BindConpaney();
        }

        protected void QuotationsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtENote = (TextBox)QuotationsGridView.Rows[e.RowIndex].FindControl("txtENote");
            string sql = "UPDATE tbl_quotations SET quotations_note_number = '" + txtENote.Text + "' WHERE quotations_id = '" + QuotationsGridView.DataKeys[e.RowIndex].Value + "'";
            //string script = "";
            if (function.MySqlQuery(sql))
            {

            }
            QuotationsGridView.EditIndex = -1;
            Response.Redirect("/Techno/TechnoFormDetail");
        }

        protected void QuotationsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = "DELETE FROM tbl_quotations  WHERE quotations_id = '" + QuotationsGridView.DataKeys[e.RowIndex].Value + "'";
            //string script = "";
            if (function.MySqlQuery(sql))
            {
                //script = "บันทึกสำเร็จ";
            }
            BindConpaney();
        }

        protected void btnSaveQuotations_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO tbl_quotations (quotations_claim_id,quotations_company_id,quotations_company_price,quotations_note_number,quotations_delete,quotations_date_send,quotations_date_recive,quotations_doc_img,quotations_order,quotations_order_img) VALUES ('" + Session["codePK"].ToString() + "','" + txtCompany.SelectedValue + "','0','" + txtNoteNumber.Text + "','0','" + function.ConvertDateTimeEB(DateTime.Now.ToString("dd-MM-yyyy")).ToString("dd-MM-yyyy") + "','0','0','0','0')";
            //string script = "";
            if (function.MySqlQuery(sql))
            {
                //script = "บันทึกสำเร็จ";
                sql = "SELECT * FROM tbl_status_detail WHERE detail_claim_id='" + Session["codePK"].ToString() + "' AND detail_status_id= '2'";
                MySqlDataReader rs = function.MySqlSelect(sql);
                if (!rs.Read())
                {
                    rs.Close();
                    function.Close();
                    sql = "INSERT INTO tbl_status_detail (detail_status_id,detail_claim_id,detail_date_start,detail_date_end) VALUES ('2','" + Session["codePK"].ToString() + "','" + function.ConvertDateTimeEB(DateTime.Now.ToString("dd-MM-yyyy")).ToString("dd-MM-yyyy") + "','" + function.ConvertDateTime(function.ConvertDateTimeEB(DateTime.Now.ToString("dd-MM-yyyy")).ToString("dd-MM-yyyy"), Quotations) + "')";
                    if (function.MySqlQuery(sql))
                    {
                        sql = "UPDATE tbl_claim SET claim_status = '2' WHERE claim_id = '" + Session["codePK"].ToString() + "'";
                        function.MySqlQuery(sql);
                    }
                }
                rs.Close();
                function.Close();

                txtNoteNumber.Text = "";
                txtCompany.SelectedIndex = 0;
                Response.Redirect("/Techno/TechnoFormDetail");
            }
            BindConpaney();
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('" + script + "')", true);

        }

        void BindConpaney()
        {
            string sql = "SELECT * FROM tbl_quotations q JOIN tbl_company c ON q.quotations_company_id = c.company_id WHERE quotations_claim_id = '" + Session["codePK"].ToString() + "'";
            MySqlDataAdapter da = function.MySqlSelectDataSet(sql);
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);
            QuotationsGridView.DataSource = ds.Tables[0];
            QuotationsGridView.DataBind();

            QuotaGridView.DataSource = ds.Tables[0];
            QuotaGridView.DataBind();
        }

        protected void btns2_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO tbl_status_detail (detail_status_id,detail_claim_id,detail_date_start,detail_date_end) VALUES ('3','" + Session["codePK"].ToString() + "','" + function.ConvertDateTimeEB(DateTime.Now.ToString("dd-MM-yyyy")).ToString("dd-MM-yyyy") + "','" + function.ConvertDateTime(function.ConvertDateTimeEB(DateTime.Now.ToString("dd-MM-yyyy")).ToString("dd-MM-yyyy"), SendTo) + "')";
            if (function.MySqlQuery(sql))
            {
                sql = "UPDATE tbl_claim SET claim_status = '3' WHERE claim_id = '" + Session["codePK"].ToString() + "'";
                function.MySqlQuery(sql);
            }
            Response.Redirect("/Techno/TechnoFormDetail");
        }

        protected void btns3_Click(object sender, EventArgs e)
        {
            getPrice();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#WaitQuotationsModel').modal();", true);
        }

        protected void btns4_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#SuccessQuotationsModel').modal();", true);

            /*string sql = "INSERT INTO tbl_status_detail (detail_status_id,detail_claim_id,detail_date_start,detail_date_end) VALUES ('5','" + Session["codePK"].ToString() + "','" + function.ConvertDateTimeEB(DateTime.Now.ToString("dd-MM-yyyy")).ToString("dd-MM-yyyy") + "','" + function.ConvertDateTime(function.ConvertDateTimeEB(DateTime.Now.ToString("dd-MM-yyyy")).ToString("dd-MM-yyyy"), 0) + "')";
            if (function.MySqlQuery(sql))
            {
                sql = "UPDATE tbl_claim SET claim_status = '5' WHERE claim_id = '" + Session["codePK"].ToString() + "'";
                function.MySqlQuery(sql);
            }
            Response.Redirect("/Techno/TechnoFormDetail");*/
        }

        protected void QuotaGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbDateSend = (Label)e.Row.FindControl("lbDateSend");
            if (lbDateSend != null)
            {
                lbDateSend.Text = function.ConvertDatelongThai(lbDateSend.Text);
            }

            Label lbPrice = (Label)e.Row.FindControl("lbPrice");
            if (lbPrice != null)
            {
                if (lbPrice.Text != "" && lbPrice.Text != "0")
                {
                    lbPrice.Text = double.Parse(lbPrice.Text).ToString("#,##0.00") + " บาท";
                }
                else
                {
                    lbPrice.Text = "";
                }
            }

            Label lbDateRecive = (Label)e.Row.FindControl("lbDateRecive");
            if (lbDateRecive != null)
            {
                lbDateRecive.Text = function.ConvertDatelongThai(lbDateRecive.Text);
            }

            LinkButton btnPrint = (LinkButton)e.Row.FindControl("btnPrint");
            if (btnPrint != null)
            {
                btnPrint.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "quotations_id").ToString();
            }

            LinkButton btnPrint2 = (LinkButton)e.Row.FindControl("btnPrint2");
            if (btnPrint2 != null)
            {
                btnPrint2.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "quotations_id").ToString();
            }

            LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
            if (btnEdit != null)
            {
                if (Session["status_id"].ToString() != "2")
                {
                    btnEdit.Visible = false;
                }
                btnEdit.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "quotations_id").ToString();
            }

            Image DocClaim = (Image)e.Row.FindControl("DocClaim");
            if (DocClaim != null)
            {
                DocClaim.ImageUrl = DataBinder.Eval(e.Row.DataItem, "quotations_doc_img").ToString();
                if (DocClaim.ImageUrl == "")
                {
                    DocClaim.Visible = false;
                }
            }

            LinkButton btnDocDownload = (LinkButton)e.Row.FindControl("btnDocDownload");
            if (btnDocDownload != null)
            {
                if (DocClaim.ImageUrl != "")
                {
                    btnDocDownload.CommandName = (string)DataBinder.Eval(e.Row.DataItem, "quotations_doc_img").ToString();
                }
                else
                {
                    btnDocDownload.Visible = false;
                }
            }
        }

        protected void QuotationsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbPrice = (Label)e.Row.FindControl("lbPrice");
            if (lbPrice != null)
            {
                lbPrice.Text = double.Parse(lbPrice.Text).ToString("#,##0.00") + " บาท";
            }
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            Session["Quota_id"] = e.CommandName;
            lbCompany.Text = function.GetSelectValue("tbl_quotations q JOIN tbl_company c ON q.quotations_company_id = c.company_id", "quotations_id='" + e.CommandName + "'", "company_name");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#ReciveQuotationsModel').modal();", true);
        }

        protected void btnPrint_Command(object sender, CommandEventArgs e)
        {
            PrintNote(0, e.CommandName);
        }

        protected void btnPrint2_Command(object sender, CommandEventArgs e)
        {
            PrintNote(1, e.CommandName);
        }

        void PrintNote(int doc, string key)
        {
            string sql = "SELECT * FROM tbl_device_damaged dd JOIN tbl_device d ON dd.device_id=d.device_id WHERE dd.claim_id = '" + Session["codePK"].ToString() + "'";
            string Device = "";

            int i = 1;
            MySqlDataReader rs = function.MySqlSelect(sql);
            while (rs.Read())
            {
                if (i == 1)
                {
                    Device = i + ". " + rs.GetString("device_name");
                }
                else
                {
                    Device += "\r\n                      " + i + ". " + rs.GetString("device_name") + "";
                }
                i++;
            }
            rs.Close();
            function.Close();

            string date = function.ConvertDatelongThai(function.GetSelectValue("tbl_quotations", "quotations_id='" + key + "'", "quotations_date_send"));
            string note_num = "คค.060005/ฝจ./" + function.GetSelectValue("tbl_quotations", "quotations_id='" + key + "'", "quotations_note_number") + "/" + date.Split(' ')[2];
            string title_name = "ขอความอนุเคราะห์เสนอราคาอุปกรณ์";
            string send_to = "ผู้จัดการ " + function.GetSelectValue("tbl_quotations JOIN tbl_company ON company_id=quotations_company_id", "quotations_id='" + key + "'", "company_name"); ;
            string cpoint = function.GetSelectValue("tbl_claim JOIN tbl_cpoint ON claim_cpoint = cpoint_id", "claim_id='" + Session["codePK"].ToString() + "'", "cpoint_name");

            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath("/Techno/Quotations.rpt"));

            rpt.SetParameterValue("part_img", Server.MapPath("/Claim/300px-Thai_government_Garuda_emblem_(Version_2).jpg"));
            rpt.SetParameterValue("device", Device);
            rpt.SetParameterValue("note_num", note_num.Trim());
            rpt.SetParameterValue("date", date);
            rpt.SetParameterValue("title_name", title_name);
            rpt.SetParameterValue("send_to", send_to);
            rpt.SetParameterValue("cpoint", cpoint);
            rpt.SetParameterValue("user", ReplaceName(Session["UserName"].ToString().Split(' ')[0]));

            if (doc == 1)
            {
                rpt.SetParameterValue("copy_title", "สำเนา");
                rpt.SetParameterValue("name", "ลงชื่อ          เผชิญ หุนตระนี");
                rpt.SetParameterValue("copy", "2.) สำเนาเรียน");
                rpt.SetParameterValue("copy1", "หจ.จท.1, หจ.จท.2, หจ.จท.3, งานเทคโน");
                rpt.SetParameterValue("copy_detail", "- เพื่อโปรดทราบ");
                rpt.SetParameterValue("name_copy", "(นายเผชิญ หุนตระนี)\r\n                                            ผจท.");
            }
            else
            {
                rpt.SetParameterValue("copy_title", "");
                rpt.SetParameterValue("name", "");
                rpt.SetParameterValue("copy", "");
                rpt.SetParameterValue("copy1", "");
                rpt.SetParameterValue("copy_detail", "");
                rpt.SetParameterValue("name_copy", "");
            }

            Session["Report"] = rpt;
            Session["ReportTitle"] = "บันทึกข้อความ";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Report/reportView','_newtab');", true);
        }

        protected void btnSaveRecive_Click(object sender, EventArgs e)
        {
            String NewFileDocName = "";
            if (fileDoc.HasFile)
            {
                string typeFile = fileDoc.FileName.Split('.')[fileDoc.FileName.Split('.').Length - 1];
                if (typeFile == "jpg" || typeFile == "jpeg" || typeFile == "png")
                {
                    if (txtDateRecive.Text.Trim() != "")
                    {
                        NewFileDocName = Session["CodePK"].ToString() + "_Recive" + Quotations_id + new Random().Next(1000, 9999);
                        NewFileDocName = "/Techno/Upload/" + function.getMd5Hash(NewFileDocName) + "." + typeFile;
                        fileDoc.SaveAs(Server.MapPath(NewFileDocName.ToString()));

                        try { double.Parse(txtPrice.Text); }
                        catch { txtPrice.Text = "0.00"; }

                        string sql_text = "quotations_date_recive = '" + txtDateRecive.Text.Trim() + "',quotations_company_price='" + double.Parse(txtPrice.Text) + "',quotations_doc_img='" + NewFileDocName + "'";
                        string condition = "quotations_id = '" + Session["Quota_id"].ToString() + "'";
                        string sql_update = "UPDATE tbl_quotations SET " + sql_text + " WHERE " + condition;
                        function.MySqlQuery(sql_update);
                        Response.Redirect("/Techno/TechnoFormDetail");
                    }
                    else
                    {
                        //AlertPop("Error : แนบรูปภาพล้มเหลว ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น", "error");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('กรุณาใส่วันที่')", true);
                    }
                }
                else
                {
                    //AlertPop("Error : แนบรูปภาพล้มเหลว ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น", "error");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : แนบรูปภาพล้มเหลว ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น')", true);
                }
            }
            else
            {
                //AlertPop("Error : แนบรูปภาพล้มเหลวไม่พบไฟล์", "error");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : แนบรูปภาพล้มเหลวไม่พบไฟล์')", true);
            }
        }

        protected void btnDocDownload_Command(object sender, CommandEventArgs e)
        {
            DownLoad(e.CommandName.ToString());
        }

        public void DownLoad(string FName)
        {
            try
            {
                string strURL = FName;
                string[] typeFile = FName.Split('/');
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + function.getMd5Hash(Session["CodePK"].ToString() + DateTime.Now) + "." + typeFile[typeFile.Length - 1].Split('.')[1] + "\"");
                byte[] data = req.DownloadData(Server.MapPath(strURL));
                response.BinaryWrite(data);
                response.End();
            }
            catch
            {

            }
        }

        private void AddControls(int controlNumber, string text, Panel Div)
        {
            Label newLabel = new Label();
            Label newLabelEnd = new Label();
            TextBox newTextbox = new TextBox();

            // textbox needs a unique id to maintain state information
            newTextbox.ID = "txtDoc_" + controlNumber;
            newTextbox.CssClass = "form-control text-center";


            newLabel.Text = text;
            newLabelEnd.Text = " ฉบับ";

            // add the label and textbox to the panel, then add the panel to the form
            Div.Controls.Add(new LiteralControl("<il class='row'>"));
            Div.Controls.Add(newLabel);
            Div.Controls.Add(newTextbox);
            Div.Controls.Add(newLabelEnd);
            Div.Controls.Add(new LiteralControl("</il>"));
        }

        protected void btnSaveNoteTo_Click(object sender, EventArgs e)
        {

            string text = "claim_doc_id,claim_doc_type,claim_doc_num,claim_doc_title,claim_doc_to,claim_doc_date";
            string value = "'" + Session["CodePK"].ToString() + "','1','" + txtNoteNumTo.Text.Trim() + "','" + txtNoteTitleTo.Text.Trim() + "','" + txtNoteSendTo.Text.Trim() + "','" + txtDateNoteto.Text.Trim() + "'";

            string text_updat = "claim_doc_num = '" + txtNoteNumTo.Text.Trim() + "',claim_doc_title='" + txtNoteTitleTo.Text.Trim() + "',claim_doc_to='" + txtNoteSendTo.Text.Trim() + "',claim_doc_date='" + txtDateNoteto.Text.Trim() + "'";
            //string message = "";
            int i = 1;
            foreach (TextBox textBox in Panel1.Controls.OfType<TextBox>())
            {
                //message += "claim_doc_no" + i + "='" + textBox.Text.Trim() + "',";
                text += ",claim_doc_no" + i;
                value += ",'" + textBox.Text.Trim() + "'";
                text_updat += ",claim_doc_no" + i + "='" + textBox.Text.Trim() + "'";
                i++;
            }

            if (function.GetSelectValue("tbl_claim_doc", "claim_doc_id = '" + Session["CodePK"].ToString() + "' AND claim_doc_type = '1'", "claim_doc_id") == "")
            {
                string sql = "INSERT INTO tbl_claim_doc (" + text + ") VALUES (" + value + ")";
                if (function.MySqlQuery(sql))
                {
                    sql = "UPDATE tbl_status_detail SET detail_date_start ='" + txtDateNoteto.Text + "',detail_date_end ='" + function.ConvertDateTime(txtDateNoteto.Text, SendTo) + "' WHERE detail_status_id='3' AND detail_claim_id='" + Session["CodePK"].ToString() + "'";
                    function.MySqlQuery(sql);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('บันทึกสำเร็จ')", true);
                }
            }
            else
            {
                string sql_update = "UPDATE tbl_claim_doc SET " + text_updat + " WHERE claim_doc_id = '" + Session["CodePK"].ToString() + "' AND claim_doc_type = '1'";
                if (function.MySqlQuery(sql_update))
                {
                    sql_update = "UPDATE tbl_status_detail SET detail_date_start ='" + txtDateNoteto.Text + "',detail_date_end ='" + function.ConvertDateTime(txtDateNoteto.Text, SendTo) + "' WHERE detail_status_id='3' AND detail_claim_id='" + Session["CodePK"].ToString() + "'";
                    function.MySqlQuery(sql_update);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('บันทึกสำเร็จ')", true);
                }
            }

        }

        void PrintReport(int doc)
        {
            string cpoint_title = "ฝ่ายบริหารการจัดเก็บเงินค่าธรรมเนียม กองทางหลวงพิเศษระหว่างเมือง โทร 0 2360 7871";
            string num_title = "กท./ฝจ./";
            string date_thai = "";
            string note_title = "";
            string txt_to = "";
            string note_text_to = "";
            string note_text = "";
            string list_dev = "";
            string list_doc = "";
            string name = "(นายเผชิญ  หุนตระนี)\r\n                                                                                          ผจท.";
            string copy_title = "2.) สำเนาเรียน";
            string copy_tiele1 = "";
            string name_copy = "";
            string user = ReplaceName(Session["UserName"].ToString().Split(' ')[0]);


            num_title += txtNoteNumTo.Text + "/" + txtDateNoteto.Text.Split('-')[2];
            date_thai = function.ConvertDatelongThai(txtDateNoteto.Text.Trim());
            note_title = txtNoteTitleTo.Text.Trim();
            txt_to = txtNoteSendTo.Text.Trim();


            string sql = "SELECT * FROM tbl_claim c JOIN tbl_claim_com cc ON c.claim_id = cc.claim_id JOIN tbl_claim_doc cd ON c.claim_id = cd.claim_doc_id AND claim_doc_type = '0' JOIN tbl_cpoint cp ON c.claim_cpoint = cp.cpoint_id WHERE c.claim_id = '" + Session["CodePK"].ToString() + "'";
            MySqlDataReader rs = function.MySqlSelect(sql);
            if (rs.Read())
            {
                note_text_to = "อ้างถึง บันทึกข้อความ ด่านฯ " + rs.GetString("cpoint_name") + " ที่ " + rs.GetString("claim_doc_num") + " ลงวันที่ " + function.ConvertDatelongThai(rs.GetString("claim_doc_date")) + " เรื่อง " + rs.GetString("claim_doc_title");
                note_text = "ฝ่ายบริหารการจัดเก็บค่าธรรมเนียม ได้รับรายงานว่า เมื่อวันที่ " + function.ConvertDatelongThai(rs.GetString("claim_start_date")) + " เวลาประมาณ " + rs.GetString("claim_detail_time") + " น.";
                note_text += " ได้เกิดอุบัติเหตุ" + rs.GetString("claim_detail_car") ;
                if (rs.GetString("claim_detail_license_plate")==""|| rs.GetString("claim_detail_license_plate")=="-"|| rs.GetString("claim_detail_license_plate")=="ไม่ทราบ")
                {
                    note_text += " ไม่ทราบหมายเลขทะเบียน ";
                }
                else
                {
                    note_text += " หมายเลขทะเบียน " + rs.GetString("claim_detail_license_plate");
                    if (rs.GetString("claim_detail_lp2") != "" && rs.GetString("claim_detail_lp2") != "-") { note_text += " ส่วนพ่วงหมายเลขทะเบียน " + rs.GetString("claim_detail_lp2"); }
                    note_text += " จังหวัด" + rs.GetString("claim_detail_province") + " เมื่อถึงด่านฯ " + rs.GetString("cpoint_name") + " เข้าช่องทางที่ " + rs.GetString("claim_detail_cb_claim");
                    note_text += " " + rs.GetString("claim_detail_direction") + " (มุ่งหน้า" + rs.GetString("claim_detail_direction_in") + ") รถได้เฉี่ยวชนอุปกรณ์ ทำให้ทรัพย์สินของทางราชการได้รับความเสียหาย ผู้ขับขี่คือ " + rs.GetString("claim_detail_driver") + " ที่อยู่ " + rs.GetString("claim_detail_address");
                    if (rs.GetString("claim_detail_insurer") != "" && rs.GetString("claim_detail_insurer") != "-")
                    {
                        note_text += " รถคันดังกล่าวได้ทำประกันไว้กับ " + rs.GetString("claim_detail_insurer") + " หมายเลขเคลมเลขที่ " + rs.GetString("claim_detail_clemence") + " หมายเลขกรมธรรม์ " + rs.GetString("claim_detail_policyholders");
                    }
                    else
                    {
                        note_text += " รถคันดังกล่าวไม่ได้ทำประกันไว้";
                    }
                }
                note_text += " ทั้งนี้ด่านฯ " + rs.GetString("cpoint_name") + " ได้ดำเนินการแจ้งความร้องทุกไว้ที่ " + rs.GetString("claim_detail_inform") + " ไว้เป็นหลักฐานเรียบร้อยแล้ว";
                copy_tiele1 = "หจ.จท." + rs.GetString("cpoint_sup") + ", ผจด." + rs.GetString("cpoint_name") + "\r\n                   - เพื่อทราบติดตามผลการดำเนินงานต่อไป";
                name_copy = "(นายเผชิญ  หุนตระนี)\r\n                                 ผจท.";
            }
            rs.Close();
            function.Close();

            list_dev += "ความเสียหายของทรัพย์สินของทางราชการ เบื้องต้นสรุปได้ดังนี้";
            sql = "SELECT * FROM tbl_device_damaged d JOIN tbl_device dd ON d.device_id = dd.device_id WHERE claim_id ='" + Session["CodePK"].ToString() + "'";
            int i = 1;
            rs = function.MySqlSelect(sql);
            while (rs.Read())
            {

                list_dev += "\r\n                                 " + i + ". " + rs.GetString("device_name") + " " + rs.GetString("device_damaged");
                i++;
            }
            rs.Close();
            function.Close();

            i = 1;
            list_doc = "เอกสารประกอบการพิจารณาแนบ ดังนี้";
            string[] readText = File.ReadAllLines(HostingEnvironment.MapPath("/Config/") + "ListDocTechno" + ".txt");
            foreach (string s in readText)
            {
                //list.Items.Add(new ListItem(s.Trim(), s.Trim()));
                list_doc += "\r\n                                 " + i + ". " + s + " จำนวน " + function.GetSelectValue("tbl_claim_doc", "claim_doc_id = '" + Session["CodePK"].ToString() + "' AND claim_doc_type = '1'", "claim_doc_no" + i) + " ฉบับ";
                i++;
            }

            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath("/Techno/reportOfficialBooks.rpt"));

            rpt.SetParameterValue("part_img", Server.MapPath("/Claim/300px-Thai_government_Garuda_emblem_(Version_2).jpg"));

            rpt.SetParameterValue("cpoint_title", cpoint_title);
            rpt.SetParameterValue("num_title", num_title);
            rpt.SetParameterValue("date_thai", date_thai);
            rpt.SetParameterValue("note_title", note_title);
            rpt.SetParameterValue("txt_to", txt_to);
            rpt.SetParameterValue("note_text_to", note_text_to);
            rpt.SetParameterValue("note_text", note_text);
            rpt.SetParameterValue("list_dev", list_dev);
            rpt.SetParameterValue("list_doc", list_doc);

            if (doc != 0)
            {
                name = "(ลงนาม)       เผชิญ  หุนตระนี\r\n                                                                               " + name;

                rpt.SetParameterValue("name", name);
                rpt.SetParameterValue("copy_title", copy_title);
                rpt.SetParameterValue("copy_tiele1", copy_tiele1);
                rpt.SetParameterValue("name_copy", name_copy);
                rpt.SetParameterValue("user", user);
            }
            else
            {
                name = "\r\n                                                                               " + name;
                rpt.SetParameterValue("name", name);
                rpt.SetParameterValue("copy_title", "");
                rpt.SetParameterValue("copy_tiele1", "");
                rpt.SetParameterValue("name_copy", "");
                rpt.SetParameterValue("user", "");
            }

            Session["Report"] = rpt;
            Session["ReportTitle"] = "บันทึกข้อความ";
            //Response.Redirect("/Report/reportView", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Report/reportView','_newtab');", true);

        }

        void getDataStatus3()
        {
            string sql = "SELECT * FROM tbl_claim_doc WHERE claim_doc_id = '" + Session["CodePK"].ToString() + "' AND claim_doc_type = '1'";
            MySqlDataReader rs = function.MySqlSelect(sql);
            if (rs.Read())
            {
                txtNoteNumTo.Text = rs.GetString("claim_doc_num");
                txtNoteTitleTo.Text = rs.GetString("claim_doc_title");
                txtNoteSendTo.Text = rs.GetString("claim_doc_to");
                txtDateNoteto.Text = rs.GetString("claim_doc_date");
                int i = 1;
                if (txtNoteSendTo.Text.Trim() != "")
                {
                    foreach (TextBox textBox in Panel1.Controls.OfType<TextBox>())
                    {
                        //message += "claim_doc_no" + i + "='" + textBox.Text.Trim() + "',";
                        textBox.Text = rs.GetString("claim_doc_no" + i);
                        i++;
                    }
                }
            }
            rs.Close();
            function.Close();
        }

        protected void btnNoteToCpoy_Click(object sender, EventArgs e)
        {
            PrintReport(1);
        }

        protected void btnNoteTo_Click(object sender, EventArgs e)
        {
            PrintReport(0);
        }

        protected void btnSaveOrder_Click(object sender, EventArgs e)
        {
            String NewFileDocName = "";
            if (FileOrder.HasFile)
            {
                string typeFile = FileOrder.FileName.Split('.')[FileOrder.FileName.Split('.').Length - 1];
                if (typeFile == "jpg" || typeFile == "jpeg" || typeFile == "png")
                {
                    NewFileDocName = Session["CodePK"].ToString() + "_Order" + Quotations_id + new Random().Next(1000, 9999);
                    NewFileDocName = "/Techno/Upload/Order/" + function.getMd5Hash(NewFileDocName) + "." + typeFile;
                    FileOrder.SaveAs(Server.MapPath(NewFileDocName.ToString()));

                    string sql = "INSERT INTO tbl_status_detail (detail_status_id,detail_claim_id,detail_date_start,detail_date_end) VALUES ('4','" + Session["codePK"].ToString() + "','" + txtDateOrder.Text.Trim() + "','" + function.ConvertDateTime(txtDateOrder.Text.Trim(), int.Parse(txtSendOrder.Text)) + "')";
                    if (function.MySqlQuery(sql))
                    {
                        sql = "UPDATE tbl_claim SET claim_status = '4' WHERE claim_id = '" + Session["codePK"].ToString() + "'";
                        function.MySqlQuery(sql);

                        sql = "UPDATE tbl_quotations SET quotations_order='1', quotations_order_img='" + NewFileDocName + "' WHERE quotations_claim_id = '" + Session["codePK"].ToString() + "' AND quotations_company_id = '" + txtCompanyOrder.SelectedValue + "'";
                        function.MySqlQuery(sql);
                    }

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('บันทึกสำเร็จ')", true);
                    Response.Redirect("/Techno/TechnoFormDetail");
                }
                else
                {
                    //AlertPop("Error : แนบรูปภาพล้มเหลว ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น", "error");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : แนบรูปภาพล้มเหลว ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น')", true);
                }
            }
            else
            {
                //AlertPop("Error : แนบรูปภาพล้มเหลวไม่พบไฟล์", "error");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : แนบรูปภาพล้มเหลวไม่พบไฟล์')", true);
            }


        }

        protected void txtCompanyOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            getPrice();
        }

        void getPrice()
        {
            txtPriceOrder.Enabled = false;
            txtPriceOrder.Text = double.Parse(function.GetSelectValue("tbl_company c JOIN tbl_quotations q ON q.quotations_company_id = c.company_id", "q.quotations_claim_id = '" + Session["CodePK"].ToString() + "' AND quotations_company_id ='" + txtCompanyOrder.SelectedValue + "'", "quotations_company_price")).ToString("#,##0.00");
        }

        void getDataStatus4()
        {
            string sql = "SELECT * FROM tbl_company c JOIN tbl_quotations q ON q.quotations_company_id = c.company_id WHERE q.quotations_claim_id = '" + Session["CodePK"].ToString() + "' AND q.quotations_order = '1'";
            MySqlDataReader rs = function.MySqlSelect(sql);
            if (rs.Read())
            {
                lbCompanyOrder.Text = rs.GetString("company_name");
                lbPriceOrder.Text = double.Parse(rs.GetString("quotations_company_price")).ToString("#,##0.00") + " บาท";
                ImageOrder.ImageUrl = rs.GetString("quotations_order_img"); ;
            }
            rs.Close();
            function.Close();

            lbDateOrderStart.Text = function.ConvertDatelongThai(function.GetSelectValue("tbl_status_detail", "detail_claim_id = '" + Session["CodePK"].ToString() + "' AND detail_status_id = '4'", "detail_date_start"));
            lbDateOrderEnd.Text = function.ConvertDatelongThai(function.GetSelectValue("tbl_status_detail", "detail_claim_id = '" + Session["CodePK"].ToString() + "' AND detail_status_id = '4'", "detail_date_end"));
        }

        protected void btnDownloadOrder_Click(object sender, EventArgs e)
        {
            DownLoad(ImageOrder.ImageUrl);
        }

        protected void txtDateSendOrder_TextChanged(object sender, EventArgs e)
        {
            if (txtDateSendOrder.Text != "")
            {
                string dateLine = function.GetSelectValue("tbl_status_detail", "detail_claim_id = '" + Session["CodePK"].ToString() + "' AND detail_status_id = '4'", "detail_date_end");
                if (dateLine != "")
                {
                    if (function.ConvertDateTime(txtDateSendOrder.Text) <= function.ConvertDateTime(dateLine))
                    {
                        DivFine.Visible = false;
                    }
                    else
                    {
                        DivFine.Visible = true;
                    }
                }
            }
        }

        protected void btnSaveSendDoc_Click(object sender, EventArgs e)
        {
            String NewFileDocName = "";
            if (FileUploadSendDoc.HasFile)
            {
                string typeFile = FileUploadSendDoc.FileName.Split('.')[FileUploadSendDoc.FileName.Split('.').Length - 1];
                if (typeFile == "jpg" || typeFile == "jpeg" || typeFile == "png")
                {
                    NewFileDocName = Session["CodePK"].ToString() + "_Order_Send" + Quotations_id + new Random().Next(1000, 9999);
                    NewFileDocName = "/Techno/Upload/Order/" + function.getMd5Hash(NewFileDocName) + "." + typeFile;
                    FileUploadSendDoc.SaveAs(Server.MapPath(NewFileDocName.ToString()));

                    string sql = "INSERT INTO tbl_status_detail (detail_status_id,detail_claim_id,detail_date_start,detail_date_end) VALUES ('5','" + Session["codePK"].ToString() + "','" + txtDateSendOrder.Text.Trim() + "','" + txtDateSendOrder.Text.Trim() + "')";
                    if (function.MySqlQuery(sql))
                    {
                        sql = "UPDATE tbl_claim SET claim_status = '5' WHERE claim_id = '" + Session["codePK"].ToString() + "'";
                        function.MySqlQuery(sql);

                        string fine = "0";
                        if (txtFine.Visible)
                        {
                            fine = txtFine.Text.Trim();
                        }

                        sql = "UPDATE tbl_quotations SET quotations_fine='" + double.Parse(fine) + "', quotations_doc_img_send='" + NewFileDocName + "' WHERE quotations_claim_id = '" + Session["codePK"].ToString() + "' AND quotations_order = '1'";
                        function.MySqlQuery(sql);
                    }

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('บันทึกสำเร็จ')", true);
                    Response.Redirect("/Techno/TechnoFormDetail");
                }
                else
                {
                    //AlertPop("Error : แนบรูปภาพล้มเหลว ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น", "error");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : แนบรูปภาพล้มเหลว ไฟล์เอกสารต้องเป็น *.jpg *.jpge *.png เท่านั้น')", true);
                }
            }
            else
            {
                //AlertPop("Error : แนบรูปภาพล้มเหลวไม่พบไฟล์", "error");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Error : แนบรูปภาพล้มเหลวไม่พบไฟล์')", true);
            }
        }

        protected void btnDownloadOrderSend_Click(object sender, EventArgs e)
        {
            DownLoad(ImageOrderSend.ImageUrl);
        }

        void getDataStatus5()
        {

            lbDateSendOrder.Text = function.ConvertDatelongThai(function.GetSelectValue("tbl_status_detail", "detail_claim_id = '" + Session["CodePK"].ToString() + "' AND detail_status_id = '5'", "detail_date_start"));
            string sql = "SELECT * FROM tbl_company c JOIN tbl_quotations q ON q.quotations_company_id = c.company_id WHERE q.quotations_claim_id = '" + Session["CodePK"].ToString() + "' AND q.quotations_order = '1'";
            MySqlDataReader rs = function.MySqlSelect(sql);
            if (rs.Read())
            {
                lbFineOrder.Text = double.Parse(rs.GetString("quotations_fine")).ToString("#,##0.00") + " บาท";
                ImageOrderSend.ImageUrl = rs.GetString("quotations_doc_img_send");
                if (lbFineOrder.Text == "0.00 บาท")
                {
                    lbFineOrder.Text = "ไม่มีค่าปรับ";
                }
            }
            rs.Close();
            function.Close();
        }

        string ReplaceName(string name)
        {
            string name_replace = "";
            name_replace = name.Replace("นาย", "");
            name_replace = name_replace.Replace("นาง", "");
            name_replace = name_replace.Replace("นางสาว", "");
            name_replace = name_replace.Replace("ว่าที่ร้อยตรี", "");
            name_replace = name_replace.Replace("ว่าที่ร้อยตรีหญิง", "");
            name_replace = name_replace.Replace("ว่าที่ร.ต.", "");
            name_replace = name_replace.Replace("ว่าที่ร.ต.หญิง", "");
            name_replace = name_replace.Replace("ว่าที่ ร.ต.", "");
            name_replace = name_replace.Replace("ว่าที่ ร.ต.หญิง", "");
            return "CRS : "+name_replace;
        }

        protected void txtDateNoteto_TextChanged(object sender, EventArgs e)
        {
            if (function.GetSelectValue("tbl_claim_doc", "claim_doc_id = '" + Session["CodePK"].ToString() + "' AND claim_doc_type = '1'", "claim_doc_id") == "")
            {
                string sql = "INSERT INTO tbl_claim_doc (claim_doc_id,claim_doc_date,claim_doc_type,claim_doc_num,claim_doc_title,claim_doc_to) VALUES ('" + Session["CodePK"].ToString() + "','" + txtDateNoteto.Text.Trim()+ "','1',' ',' ',' ')";
                if (function.MySqlQuery(sql))
                {
                    sql = "UPDATE tbl_status_detail SET detail_date_start ='" + txtDateNoteto.Text + "',detail_date_end ='" + function.ConvertDateTime(txtDateNoteto.Text, SendTo) + "' WHERE detail_status_id='3' AND detail_claim_id='" + Session["CodePK"].ToString() + "'";
                    function.MySqlQuery(sql);
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('บันทึกสำเร็จ')", true);
                }
            }
            else
            {
                string sql_update = "UPDATE tbl_claim_doc SET claim_doc_date = '"+ txtDateNoteto.Text.Trim() + "'  WHERE claim_doc_id = '" + Session["CodePK"].ToString() + "' AND claim_doc_type = '1'";
                if (function.MySqlQuery(sql_update))
                {
                    sql_update = "UPDATE tbl_status_detail SET detail_date_start ='" + txtDateNoteto.Text + "',detail_date_end ='" + function.ConvertDateTime(txtDateNoteto.Text, SendTo) + "' WHERE detail_status_id='3' AND detail_claim_id='" + Session["CodePK"].ToString() + "'";
                    function.MySqlQuery(sql_update);
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('บันทึกสำเร็จ')", true);
                }
            }
        }
    }
}