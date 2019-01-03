using ClaimProject.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace ClaimProject.ReportView
{
    public partial class Default : System.Web.UI.Page
    {

        ClaimFunction function = new ClaimFunction();
        public string claim_id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.IsPostBack)
            {
                //GridViewAllbyBudget
                rbtBudget.Checked = true;
                rbtBudget_CheckedChanged(null, null);
                string getCpointName = "SELECT * FROM tbl_cpoint Order by cpoint_id ASC";
                function.getListItem(txtStation, getCpointName, "cpoint_name", "cpoint_id");
                //เพิ่มไอเทมให้เลือกในดรอบดาว  (ตำแหน่ง , สร้างใหม่(ชื่ออะไร,values))
                txtStation.Items.Insert(0, new ListItem("ทั้งหมด", ""));
                function.getListItem(txtBudgetYear, "SELECT claim_budget_year FROM tbl_claim  GROUP BY claim_budget_year DESC", "claim_budget_year", "claim_budget_year");

            }
            

        }
        public void bindAllClaim(string sss, string yyy ,string begin ,string end)
        {

            MySqlDataAdapter AllClaim = function.MySqlSelectDataSet(sss);
            DataTable dt = new DataTable();

            AllClaim.Fill(dt);
            GridViewAllbyBudget.DataSource = dt;
            GridViewAllbyBudget.DataBind();

            if(begin == "" && end == "")
            {
                GridViewAllbyBudget.Visible = true;
                lbTable1.Text = "ตารางแสดงข้อมูลอุบัติเหตุทั้งหมดของปีงบประมาณ " + yyy;
                lbTable1.Visible = true;
            }
            else
            {
                string since = function.ConvertDateShortThai(begin);
                string last = function.ConvertDateShortThai(end);
                GridViewAllbyBudget.Visible = true;
                lbTable1.Text = "ตารางแสดงข้อมูลอุบัติเหตุทั้งหมด ( " +since+ " - "+last+" )";
                lbTable1.Visible = true;
            }
            

        }

        public void bindDetail(string ssss, string yyyy, int numx,string begin,string end)
        {
            MySqlDataAdapter AllClaim = function.MySqlSelectDataSet(ssss);
            DataTable dt = new DataTable();

            if(numx == 1 && begin == "" && end == "")
            {
                AllClaim.Fill(dt);
                GridViewthing.DataSource = dt;
                GridViewthing.DataBind();
                GridViewthing.Visible = true;
                lbTable2.Text = "ตารางแสดงข้อมูลจำนวนอุบัติเหตุของช่องทางแต่ละด่านฯ ปีงบประมาณ " + yyyy;
                lbTable2.Visible = true;
            }
            else if (numx != 1 && begin == "" && end == "")
            {
                AllClaim.Fill(dt);
                GridViewEn2.DataSource = dt;
                GridViewEn2.DataBind();
                GridViewEn2.Visible = true;
                lbTable2.Text = "ตารางแสดงข้อมูลความถี่การชำรุดของอุปกรณ์แต่ละช่องทาง ปีงบประมาณ " + yyyy;
                lbTable2.Visible = true;
            }
            else
            {
                string since = function.ConvertDateShortThai(begin);
                string last = function.ConvertDateShortThai(end);
                AllClaim.Fill(dt);
                GridViewEn2.DataSource = dt;
                GridViewEn2.DataBind();
                GridViewEn2.Visible = true;
                lbTable2.Text = "ตารางแสดงข้อมูลความถี่การชำรุดของอุปกรณ์แต่ละช่องทาง ("+since+" - "+last+") " ;
                lbTable2.Visible = true;
            }
                    
        }

        public void bindDetail2(string ssss,int num)
        {
            MySqlDataAdapter AllClaim = function.MySqlSelectDataSet(ssss);
            DataTable dt = new DataTable();

            if(num == 1)
            {
                AllClaim.Fill(dt);
                GridViewEx.DataSource = dt;
                GridViewEx.DataBind();
                GridViewEx.Visible = true;
            }
            else
            {
                AllClaim.Fill(dt);
                GridViewEx2.DataSource = dt;
                GridViewEx2.DataBind();
                GridViewEx2.Visible = true;
            }        
           
        }

        protected void GridViewAllbyBudget_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private bool ChanelCheck(string point)
        {
            return true;
        }

        protected void btnResult_Click(object sender, EventArgs e)
        {
            if(txtStation.SelectedValue == "")
            {
                GridViewEn2.Visible = false;
                GridViewEx2.Visible = false;
                GridViewthing.Visible = true;
                GridViewEx.Visible = true;
            }
            else
            {
                GridViewEn2.Visible = true;
                GridViewEx2.Visible = true;
                GridViewthing.Visible = false;
                GridViewEx.Visible = false;
            }

            string Budget = txtBudgetYear.Text;
            string StartD = txtStartDate.Text;
            string EndD = txtEndDate.Text;
            string beginx = "";
            string endx = "";
            //เตรียมคำสั่งไว้สำหรับคิวรี่
            string sql_query = "";
            string sql_query_out = "";
            string whereAreU = "";
            if(rbtBudget.Checked == true)
            {
                whereAreU = " claim_budget_year='"+Budget+"' ";
            }
            else if(rbtDuration.Checked == true)
            {
                whereAreU = " STR_TO_DATE(claim_start_date,'%d-%m-%Y') " +
                                " BETWEEN STR_TO_DATE('"+ StartD +"','%d-%m-%Y')  " +
                                " AND STR_TO_DATE('"+EndD+"','%d-%m-%Y') ";
                beginx = StartD;
                endx = EndD;
            }

            //เอาตารางที่มีข้อมูลชื่อด่าน
            string sql = "SELECT * FROM tbl_cpoint Order by cpoint_id ASC"; 
            MySqlDataReader rs = function.MySqlSelect(sql);

            if (txtStation.SelectedValue == "") //กรณีเลือกทุกด่าน
            {
                //เตรียมอิ้นในเท่ากับศูนย์เพื่อให้วนที่ตัวแรก
                int i = 0;
                //วนลูปทำคำสั่งโดยการเปลี่ยนตัวแปรตำสั่งตามเงื่อนไขดังต่อไปนี้
                while (rs.Read()) //อ่านข้อมูลตารางที่มีชื่อข้อมูลตาราง
                {
                    //วนรอบแรกจะต้องทำเงื่อนไขแรกเพราะ i เริ่มต้นที่ค่า 0
                    if (i == 0)
                    {
                        sql_query += ""; // += หมายถึงการต่อstring
                        sql_query_out += "";
                    }
                    else
                    {
                        sql_query += " UNION ";
                        sql_query_out += " UNION ";
                    }
                    sql_query += "SELECT cpoint_name AS 'ด่านฯ' "
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN01' OR tbl_claim_com.claim_detail_cb_claim = 'I01' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN01'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN02' OR tbl_claim_com.claim_detail_cb_claim = 'I02' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN02'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN03' OR tbl_claim_com.claim_detail_cb_claim = 'I03' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN03'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN04' OR tbl_claim_com.claim_detail_cb_claim = 'I04' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN04'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN05' OR tbl_claim_com.claim_detail_cb_claim = 'I05' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN05'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN06' OR tbl_claim_com.claim_detail_cb_claim = 'I06' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN06'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN07' OR tbl_claim_com.claim_detail_cb_claim = 'I07' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN07'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN08' OR tbl_claim_com.claim_detail_cb_claim = 'I08' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN08'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN09' OR tbl_claim_com.claim_detail_cb_claim = 'I09' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN09'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN10' OR tbl_claim_com.claim_detail_cb_claim = 'I10' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN10'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN11' OR tbl_claim_com.claim_detail_cb_claim = 'I11' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN11'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN12' OR tbl_claim_com.claim_detail_cb_claim = 'I12' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN12'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN13' OR tbl_claim_com.claim_detail_cb_claim = 'I13' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN13'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN14' OR tbl_claim_com.claim_detail_cb_claim = 'I14' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN14'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN15' OR tbl_claim_com.claim_detail_cb_claim = 'I15' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN15'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN16' OR tbl_claim_com.claim_detail_cb_claim = 'I16' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN16'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN17' OR tbl_claim_com.claim_detail_cb_claim = 'I17' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN17'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN18' OR tbl_claim_com.claim_detail_cb_claim = 'I18' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN18'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN19' OR tbl_claim_com.claim_detail_cb_claim = 'I19' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN19'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN20' OR tbl_claim_com.claim_detail_cb_claim = 'I20' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN20'"
                     + ", ( SELECT (EN01+EN02+EN03+EN04+EN05+EN06+EN07+EN08+EN09+EN10+EN11+EN12+EN13+EN14+EN15+EN16+EN17+EN18+EN19+EN20) AS total FROM (SELECT cpoint_name AS StationName  "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN01' OR tbl_claim_com.claim_detail_cb_claim = 'I01' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN01' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN02' OR tbl_claim_com.claim_detail_cb_claim = 'I02' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN02' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN03' OR tbl_claim_com.claim_detail_cb_claim = 'I03' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN03' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN04' OR tbl_claim_com.claim_detail_cb_claim = 'I04' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN04' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN05' OR tbl_claim_com.claim_detail_cb_claim = 'I05' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN05' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN06' OR tbl_claim_com.claim_detail_cb_claim = 'I06' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN06' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN07' OR tbl_claim_com.claim_detail_cb_claim = 'I07' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN07' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN08' OR tbl_claim_com.claim_detail_cb_claim = 'I08' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN08' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN09' OR tbl_claim_com.claim_detail_cb_claim = 'I09' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN09' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN10' OR tbl_claim_com.claim_detail_cb_claim = 'I10' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN10' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN11' OR tbl_claim_com.claim_detail_cb_claim = 'I11' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN11' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN12' OR tbl_claim_com.claim_detail_cb_claim = 'I12' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN12' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN13' OR tbl_claim_com.claim_detail_cb_claim = 'I13' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN13' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN14' OR tbl_claim_com.claim_detail_cb_claim = 'I14' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN14' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN15' OR tbl_claim_com.claim_detail_cb_claim = 'I15' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN15' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN16' OR tbl_claim_com.claim_detail_cb_claim = 'I16' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN16' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN17' OR tbl_claim_com.claim_detail_cb_claim = 'I17' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN17' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN18' OR tbl_claim_com.claim_detail_cb_claim = 'I18' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN18' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN19' OR tbl_claim_com.claim_detail_cb_claim = 'I19' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN19' "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN20' OR tbl_claim_com.claim_detail_cb_claim = 'I20' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN20' "


                                + " FROM tbl_cpoint "
                                + " LEFT JOIN tbl_claim ON claim_cpoint = cpoint_id "
                                + " LEFT JOIN tbl_claim_com ON tbl_claim.claim_id = tbl_claim_com.claim_id "
                                + " WHERE "+whereAreU+" AND cpoint_id = '" + rs.GetString("cpoint_id") + "' AND claim_delete = '0' "
                                + " GROUP BY cpoint_name) Table_Claim ) AS 'รวม'"


                    + " FROM tbl_cpoint"
                    + " LEFT JOIN tbl_claim ON claim_cpoint=cpoint_id"
                    + " LEFT JOIN tbl_claim_com ON tbl_claim.claim_id=tbl_claim_com.claim_id"
                    + " WHERE "+whereAreU+" AND cpoint_id='" + rs.GetString("cpoint_id") + "' AND claim_delete = '0' "
                    + " GROUP BY cpoint_name";


                    sql_query_out += " SELECT "
                    + "cpoint_name As 'ด่านฯ'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX01' OR tbl_claim_com.claim_detail_cb_claim = 'O01' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX01'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX02' OR tbl_claim_com.claim_detail_cb_claim = 'O02' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX02'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX03' OR tbl_claim_com.claim_detail_cb_claim = 'O03' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX03'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX04' OR tbl_claim_com.claim_detail_cb_claim = 'O04' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX04'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX05' OR tbl_claim_com.claim_detail_cb_claim = 'O05' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX05'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX06' OR tbl_claim_com.claim_detail_cb_claim = 'O06' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX06'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX07' OR tbl_claim_com.claim_detail_cb_claim = 'O07' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX07'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX08' OR tbl_claim_com.claim_detail_cb_claim = 'O08' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX08'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX09' OR tbl_claim_com.claim_detail_cb_claim = 'O09' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX09'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX10' OR tbl_claim_com.claim_detail_cb_claim = 'O10' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX10'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX11' OR tbl_claim_com.claim_detail_cb_claim = 'O11' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX11'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX12' OR tbl_claim_com.claim_detail_cb_claim = 'O12' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX12'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX13' OR tbl_claim_com.claim_detail_cb_claim = 'O13' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX13'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX14' OR tbl_claim_com.claim_detail_cb_claim = 'O14' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX14'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX15' OR tbl_claim_com.claim_detail_cb_claim = 'O15' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX15'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX16' OR tbl_claim_com.claim_detail_cb_claim = 'O16' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX16'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX17' OR tbl_claim_com.claim_detail_cb_claim = 'O17' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX17'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX18' OR tbl_claim_com.claim_detail_cb_claim = 'O18' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX18'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX19' OR tbl_claim_com.claim_detail_cb_claim = 'O19' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX19'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX20' OR tbl_claim_com.claim_detail_cb_claim = 'O20' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX20'"
                     + ", (SELECT (EX01+EX02+EX03+EX04+EX05+EX06+EX07+EX08+EX09+EX10+EX11+EX12+EX13+EX14+EX15+EX16+EX17+EX18+EX19+EX20) AS Total  FROM "
                            + " (SELECT cpoint_name AS StationName "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX01' OR tbl_claim_com.claim_detail_cb_claim = 'O01' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX01'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX02' OR tbl_claim_com.claim_detail_cb_claim = 'O02' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX02'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX03' OR tbl_claim_com.claim_detail_cb_claim = 'O03' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX03'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX04' OR tbl_claim_com.claim_detail_cb_claim = 'O04' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX04'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX05' OR tbl_claim_com.claim_detail_cb_claim = 'O05' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX05'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX06' OR tbl_claim_com.claim_detail_cb_claim = 'O06' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX06'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX07' OR tbl_claim_com.claim_detail_cb_claim = 'O07' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX07'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX08' OR tbl_claim_com.claim_detail_cb_claim = 'O08' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX08'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX09' OR tbl_claim_com.claim_detail_cb_claim = 'O09' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX09'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX10' OR tbl_claim_com.claim_detail_cb_claim = 'O10' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX10'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX11' OR tbl_claim_com.claim_detail_cb_claim = 'O11' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX11'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX12' OR tbl_claim_com.claim_detail_cb_claim = 'O12' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX12'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX13' OR tbl_claim_com.claim_detail_cb_claim = 'O13' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX13'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX14' OR tbl_claim_com.claim_detail_cb_claim = 'O14' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX14'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX15' OR tbl_claim_com.claim_detail_cb_claim = 'O15' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX15'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX16' OR tbl_claim_com.claim_detail_cb_claim = 'O16' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX16'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX17' OR tbl_claim_com.claim_detail_cb_claim = 'O17' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX17'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX18' OR tbl_claim_com.claim_detail_cb_claim = 'O18' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX18'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX19' OR tbl_claim_com.claim_detail_cb_claim = 'O19' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX19'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX20' OR tbl_claim_com.claim_detail_cb_claim = 'O20' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX20'"


                                + " FROM tbl_cpoint "
                                + " LEFT JOIN tbl_claim ON claim_cpoint = cpoint_id "
                                + " LEFT JOIN tbl_claim_com ON tbl_claim.claim_id = tbl_claim_com.claim_id "
                                + " WHERE "+whereAreU+" AND cpoint_id = '" + rs.GetString("cpoint_id") + "' AND claim_delete = '0' "
                                + " GROUP BY cpoint_name) Table_Claim) AS 'รวม' "

                    + " FROM tbl_cpoint"
                    + " LEFT JOIN tbl_claim ON claim_cpoint=cpoint_id"
                    + " LEFT JOIN tbl_claim_com ON tbl_claim.claim_id=tbl_claim_com.claim_id"
                    + " WHERE "+whereAreU+" AND cpoint_id='" + rs.GetString("cpoint_id") + "' AND claim_delete = '0' "
                    + " GROUP BY cpoint_name";


                    //rs.GetString("cpoint_id") เพราะwhile นี้ทำทีละเรคคอด

                    i++;
                }
                rs.Close();
                function.Close();
                string overall = "SELECT cpoint_name AS 'ด่านฯ' ,COUNT(claim_detail_cb_claim) AS Total "
                                    +" FROM tbl_cpoint "
                                    +" LEFT JOIN tbl_claim ON claim_cpoint = cpoint_id "
                                    +" LEFT JOIN tbl_claim_com ON tbl_claim.claim_id = tbl_claim_com.claim_id "
                                    +" WHERE "+whereAreU+" AND claim_delete = '0' GROUP BY cpoint_id   ";

                bindAllClaim(overall, Budget,beginx ,endx);
                bindDetail(sql_query, Budget,1,beginx,endx);
                bindDetail2(sql_query_out,1);
                
            }
            else if (txtStation.SelectedValue != "") //กรณีเลือกรายด่าน!!!
            {

                
                int i = 0;
                //วนลูปทำคำสั่งโดยการเปลี่ยนตัวแปรตำสั่งตามเงื่อนไขดังต่อไปนี้
                while (rs.Read()) //อ่านข้อมูลตารางที่มีชื่อข้อมูลตาราง
                {
                    //วนรอบแรกจะต้องทำเงื่อนไขแรกเพราะ i เริ่มต้นที่ค่า 0
                    if (i == 0)
                    {
                        sql_query += ""; // += หมายถึงการต่อstring
                        sql_query_out += "";
                    }
                    else
                    {
                        sql_query += " UNION ";
                        sql_query_out += " UNION ";
                    }
                     sql_query += "SELECT tbl_device.device_name As 'ชื่ออุปกรณ์' "

                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN01' OR tbl_claim_com.claim_detail_cb_claim = 'I01' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN01'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN02' OR tbl_claim_com.claim_detail_cb_claim = 'I02' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN02'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN03' OR tbl_claim_com.claim_detail_cb_claim = 'I03' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN03'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN04' OR tbl_claim_com.claim_detail_cb_claim = 'I04' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN04'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN05' OR tbl_claim_com.claim_detail_cb_claim = 'I05' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN05'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN06' OR tbl_claim_com.claim_detail_cb_claim = 'I06' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN06'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN07' OR tbl_claim_com.claim_detail_cb_claim = 'I07' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN07'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN08' OR tbl_claim_com.claim_detail_cb_claim = 'I08' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN08'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN09' OR tbl_claim_com.claim_detail_cb_claim = 'I09' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN09'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN10' OR tbl_claim_com.claim_detail_cb_claim = 'I10' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN10'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN11' OR tbl_claim_com.claim_detail_cb_claim = 'I11' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN11'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN12' OR tbl_claim_com.claim_detail_cb_claim = 'I12' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN12'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN13' OR tbl_claim_com.claim_detail_cb_claim = 'I13' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN13'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN14' OR tbl_claim_com.claim_detail_cb_claim = 'I14' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN14'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN15' OR tbl_claim_com.claim_detail_cb_claim = 'I15' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN15'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN16' OR tbl_claim_com.claim_detail_cb_claim = 'I16' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN16'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN17' OR tbl_claim_com.claim_detail_cb_claim = 'I17' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN17'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN18' OR tbl_claim_com.claim_detail_cb_claim = 'I18' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN18'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN19' OR tbl_claim_com.claim_detail_cb_claim = 'I19' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN19'"
                    + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN20' OR tbl_claim_com.claim_detail_cb_claim = 'I20' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN20'"
                    + ", (SELECT(EN01 + EN02 + EN03 + EN04 + EN05 + EN06 + EN07 + EN08 + EN09 + EN10 + EN11 + EN12 + EN13 + EN14 + EN15 + EN16 + EN17 + EN18 + EN19 + EN20) AS Total  FROM  "
                                               + " (SELECT tbl_device.device_name AS StationName "
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN01' OR tbl_claim_com.claim_detail_cb_claim = 'I01' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN01'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN02' OR tbl_claim_com.claim_detail_cb_claim = 'I02' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN02'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN03' OR tbl_claim_com.claim_detail_cb_claim = 'I03' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN03'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN04' OR tbl_claim_com.claim_detail_cb_claim = 'I04' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN04'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN05' OR tbl_claim_com.claim_detail_cb_claim = 'I05' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN05'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN06' OR tbl_claim_com.claim_detail_cb_claim = 'I06' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN06'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN07' OR tbl_claim_com.claim_detail_cb_claim = 'I07' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN07'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN08' OR tbl_claim_com.claim_detail_cb_claim = 'I08' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN08'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN09' OR tbl_claim_com.claim_detail_cb_claim = 'I09' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN09'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN10' OR tbl_claim_com.claim_detail_cb_claim = 'I10' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN10'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN11' OR tbl_claim_com.claim_detail_cb_claim = 'I11' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN11'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN12' OR tbl_claim_com.claim_detail_cb_claim = 'I12' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN12'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN13' OR tbl_claim_com.claim_detail_cb_claim = 'I13' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN13'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN14' OR tbl_claim_com.claim_detail_cb_claim = 'I14' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN14'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN15' OR tbl_claim_com.claim_detail_cb_claim = 'I15' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN15'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN16' OR tbl_claim_com.claim_detail_cb_claim = 'I16' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN16'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN17' OR tbl_claim_com.claim_detail_cb_claim = 'I17' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN17'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN18' OR tbl_claim_com.claim_detail_cb_claim = 'I18' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN18'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN19' OR tbl_claim_com.claim_detail_cb_claim = 'I19' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN19'"
                                             + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EN20' OR tbl_claim_com.claim_detail_cb_claim = 'I20' THEN tbl_claim_com.claim_detail_cb_claim END) 'EN20'"

                                             + " FROM tbl_claim "
                                             + " JOIN tbl_claim_com ON tbl_claim_com.claim_id = tbl_claim.claim_id "
                                             + " JOIN tbl_device_damaged ON tbl_claim_com.claim_id = tbl_device_damaged.claim_id "
                                             + " JOIN tbl_device ON tbl_device_damaged.device_id = tbl_device.device_id "
                                             + " WHERE "+whereAreU+" AND tbl_claim.claim_delete = '0' "
                                             + " AND tbl_claim.claim_cpoint = '" + txtStation.SelectedValue + "') Table_Cliam) AS 'รวม' "

                             + " FROM tbl_claim "
                            + " JOIN tbl_claim_com ON tbl_claim_com.claim_id = tbl_claim.claim_id "
                            + " JOIN tbl_device_damaged ON tbl_claim_com.claim_id = tbl_device_damaged.claim_id "
                            + " JOIN tbl_device ON tbl_device_damaged.device_id = tbl_device.device_id "
                            + " WHERE "+whereAreU+" AND tbl_claim.claim_delete = '0' AND (SUBSTR(claim_detail_cb_claim,1,2)='EN' OR SUBSTR(claim_detail_cb_claim,1,1)='I') "
                            + " AND tbl_claim.claim_cpoint = '" + txtStation.SelectedValue + "' ";


                    sql_query_out += "SELECT tbl_device.device_name As 'ชื่ออุปกรณ์' "

                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX01' OR tbl_claim_com.claim_detail_cb_claim = 'O01' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX01'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX02' OR tbl_claim_com.claim_detail_cb_claim = 'O02' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX02'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX03' OR tbl_claim_com.claim_detail_cb_claim = 'O03' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX03'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX04' OR tbl_claim_com.claim_detail_cb_claim = 'O04' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX04'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX05' OR tbl_claim_com.claim_detail_cb_claim = 'O05' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX05'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX06' OR tbl_claim_com.claim_detail_cb_claim = 'O06' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX06'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX07' OR tbl_claim_com.claim_detail_cb_claim = 'O07' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX07'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX08' OR tbl_claim_com.claim_detail_cb_claim = 'O08' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX08'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX09' OR tbl_claim_com.claim_detail_cb_claim = 'O09' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX09'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX10' OR tbl_claim_com.claim_detail_cb_claim = 'O10' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX10'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX11' OR tbl_claim_com.claim_detail_cb_claim = 'O11' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX11'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX12' OR tbl_claim_com.claim_detail_cb_claim = 'O12' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX12'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX13' OR tbl_claim_com.claim_detail_cb_claim = 'O13' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX13'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX14' OR tbl_claim_com.claim_detail_cb_claim = 'O14' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX14'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX15' OR tbl_claim_com.claim_detail_cb_claim = 'O15' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX15'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX16' OR tbl_claim_com.claim_detail_cb_claim = 'O16' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX16'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX17' OR tbl_claim_com.claim_detail_cb_claim = 'O17' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX17'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX18' OR tbl_claim_com.claim_detail_cb_claim = 'O18' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX18'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX19' OR tbl_claim_com.claim_detail_cb_claim = 'O19' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX19'"
                     + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX20' OR tbl_claim_com.claim_detail_cb_claim = 'O20' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX20'"
                     + ", (SELECT (EX01+EX02+EX03+EX04+EX05+EX06+EX07+EX08+EX09+EX10+EX11+EX12+EX13+EX14+EX15+EX16+EX17+EX18+EX19+EX20) AS Total  FROM "
                            + " (SELECT tbl_device.device_name AS StationName "
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX01' OR tbl_claim_com.claim_detail_cb_claim = 'O01' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX01'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX02' OR tbl_claim_com.claim_detail_cb_claim = 'O02' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX02'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX03' OR tbl_claim_com.claim_detail_cb_claim = 'O03' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX03'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX04' OR tbl_claim_com.claim_detail_cb_claim = 'O04' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX04'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX05' OR tbl_claim_com.claim_detail_cb_claim = 'O05' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX05'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX06' OR tbl_claim_com.claim_detail_cb_claim = 'O06' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX06'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX07' OR tbl_claim_com.claim_detail_cb_claim = 'O07' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX07'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX08' OR tbl_claim_com.claim_detail_cb_claim = 'O08' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX08'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX09' OR tbl_claim_com.claim_detail_cb_claim = 'O09' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX09'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX10' OR tbl_claim_com.claim_detail_cb_claim = 'O10' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX10'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX11' OR tbl_claim_com.claim_detail_cb_claim = 'O11' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX11'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX12' OR tbl_claim_com.claim_detail_cb_claim = 'O12' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX12'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX13' OR tbl_claim_com.claim_detail_cb_claim = 'O13' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX13'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX14' OR tbl_claim_com.claim_detail_cb_claim = 'O14' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX14'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX15' OR tbl_claim_com.claim_detail_cb_claim = 'O15' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX15'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX16' OR tbl_claim_com.claim_detail_cb_claim = 'O16' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX16'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX17' OR tbl_claim_com.claim_detail_cb_claim = 'O17' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX17'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX18' OR tbl_claim_com.claim_detail_cb_claim = 'O18' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX18'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX19' OR tbl_claim_com.claim_detail_cb_claim = 'O19' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX19'"
                            + ", COUNT(CASE WHEN tbl_claim_com.claim_detail_cb_claim = 'EX20' OR tbl_claim_com.claim_detail_cb_claim = 'O20' THEN tbl_claim_com.claim_detail_cb_claim END) 'EX20'"


                                + " FROM tbl_claim "
                                             + " JOIN tbl_claim_com ON tbl_claim_com.claim_id = tbl_claim.claim_id "
                                             + " JOIN tbl_device_damaged ON tbl_claim_com.claim_id = tbl_device_damaged.claim_id "
                                             + " JOIN tbl_device ON tbl_device_damaged.device_id = tbl_device.device_id "
                                             + " WHERE "+whereAreU+" AND tbl_claim.claim_delete = '0' "
                                             + " AND tbl_claim.claim_cpoint = '" + txtStation.SelectedValue + "') Table_Cliam) AS 'รวม' "

                             + " FROM tbl_claim "
                            + " JOIN tbl_claim_com ON tbl_claim_com.claim_id = tbl_claim.claim_id "
                            + " JOIN tbl_device_damaged ON tbl_claim_com.claim_id = tbl_device_damaged.claim_id "
                            + " JOIN tbl_device ON tbl_device_damaged.device_id = tbl_device.device_id "
                            + " WHERE "+whereAreU+" AND tbl_claim.claim_delete = '0' AND (SUBSTR(claim_detail_cb_claim,1,2)='EX' OR SUBSTR(claim_detail_cb_claim,1,1)='O') "
                            + " AND tbl_claim.claim_cpoint = '" + txtStation.SelectedValue + "' ";

                    //rs.GetString("cpoint_id") เพราะwhile นี้ทำทีละเรคคอด

                    i++;
                }
                rs.Close();
                function.Close();
                string overall = "SELECT cpoint_name AS 'ด่านฯ' ,COUNT(claim_detail_cb_claim) AS Total "
                                    + " FROM tbl_cpoint "
                                    + " LEFT JOIN tbl_claim ON claim_cpoint = cpoint_id "
                                    + " LEFT JOIN tbl_claim_com ON tbl_claim.claim_id = tbl_claim_com.claim_id "
                                    + " WHERE "+whereAreU+" AND cpoint_id='"+ txtStation.SelectedValue + "' AND claim_delete = '0'  ";

                bindAllClaim(overall, Budget, beginx, endx);
                bindDetail(sql_query, Budget,2,beginx,endx);
                bindDetail2(sql_query_out,2); 


            }
            

        }

        protected void GridViewthing_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridViewEx_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void rbtBudget_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtBudget.Checked == true)
            {
                lbBudget.Visible = true;
                lbStartDate.Visible = false;
                lbEndDate.Visible = false;
                txtStartDate.Visible = false;
                txtEndDate.Visible = false;
                txtBudgetYear.Visible = true;
                btnResult.Visible = true;
            }
            else if (rbtDuration.Checked == true)
            {
                lbStartDate.Visible = true;
                lbEndDate.Visible = true;
                lbBudget.Visible = false;
                txtBudgetYear.Visible = false;
                txtStartDate.Visible = true;
                txtEndDate.Visible = true;
                btnResult.Visible = true;
            }
        }

        protected void rbtDuration_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtBudget.Checked == true)
            {
                lbBudget.Visible = true;
                lbStartDate.Visible = false;
                lbEndDate.Visible = false;
                txtStartDate.Visible = false;
                txtEndDate.Visible = false;
                txtBudgetYear.Visible = true;
                btnResult.Visible = true;
                
            }
            else if (rbtDuration.Checked == true)
            {
                lbStartDate.Visible = true;
                lbEndDate.Visible = true;
                lbBudget.Visible = false;
                txtBudgetYear.Visible = false;
                txtStartDate.Visible = true;
                txtEndDate.Visible = true;
                btnResult.Visible = true;
            }
        }

        protected void GridViewAllbyBudget_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Width = new Unit("140px");
            e.Row.Cells[1].Width = new Unit("240px");
        }

        protected void GridViewthing_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Width = new Unit("100px");
            e.Row.Cells[21].Font.Bold = true;

        }

        protected void GridViewEx_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Width = new Unit("100px");
            e.Row.Cells[21].Font.Bold = true;
        }

        protected void GridViewEn2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Width = new Unit("140px");
            e.Row.Cells[21].Font.Bold = true;

        }

        protected void GridViewEx2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Width = new Unit("140px");

            e.Row.Cells[21].Font.Bold = true;
            
        }
    }
}