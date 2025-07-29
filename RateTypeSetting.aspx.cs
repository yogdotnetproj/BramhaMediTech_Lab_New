using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;

public partial class RateTypeSetting :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    FindPatient_Bal_C sn = new FindPatient_Bal_C();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("RateTypeSetting.aspx");
                    }
                }
                if (Session["Branchid"] != null)
                {
                    ddlratetype1.DataSource = (ArrayList)RateFill(Convert.ToInt32(Session["Branchid"]));
                    ddlratetype1.DataTextField = "Name";
                    ddlratetype1.DataValueField = "DoctorCode";
                    ddlratetype1.DataBind();

                    ddlratetype2.DataSource = (ArrayList)RateFill(Convert.ToInt32(Session["Branchid"]));
                    ddlratetype2.DataTextField = "Name";
                    ddlratetype2.DataValueField = "DoctorCode";
                    ddlratetype2.DataBind();
                }
                BindRate();
            }
            catch (SqlException)
            {
                throw;
            }


        }
    }

    public void BindRate()
    {
        if (Session["Branchid"] != null)
        {
            ddlratetype1.DataSource = (ArrayList)RateFill(Convert.ToInt32(Session["Branchid"]));
            ddlratetype1.DataTextField = "Name";
            ddlratetype1.DataValueField = "DoctorCode";
            ddlratetype1.DataBind();

            ddlratetype2.DataSource = (ArrayList)RateFill(Convert.ToInt32(Session["Branchid"]));
            ddlratetype2.DataTextField = "Name";
            ddlratetype2.DataValueField = "DoctorCode";
            ddlratetype2.DataBind();
        }
    }
    public ICollection RateFill(int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        sc = new SqlCommand("SELECT RatID,RateName from  RatT where branchid=" + branchid + "", conn);

        SqlDataReader dr = null;

        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    DrMT_Bal_C dnt = new DrMT_Bal_C();
                    dnt.DoctorCode = dr["RatID"].ToString();
                    dnt.Name = dr["RateName"].ToString();
                    al.Add(dnt);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                if (dr != null) dr.Close();

                conn.Close(); conn.Dispose();

            }
            catch (SqlException)
            {
                throw;
            }

        }

        return al;
    }

    public ICollection RateFillForCollection(int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        sc = new SqlCommand("SELECT RatID,RateName from  RatT where branchid=" + branchid + " and RateFlag='C'", conn);
        SqlDataReader dr = null;

        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    DrMT_Bal_C dnt = new DrMT_Bal_C();
                    dnt.DoctorCode = dr["RatID"].ToString();
                    dnt.Name = dr["RateName"].ToString();
                    al.Add(dnt);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        finally
        {
            try
            {
                if (dr != null) dr.Close();

                conn.Close(); conn.Dispose();

            }
            catch (SqlException)
            {
                throw;
            }

        }

        return al;
    }

    public ICollection RateFillForCompliment(int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        sc = new SqlCommand("SELECT RatID,RateName from  RatT where branchid=" + branchid + " and RateFlag='R'", conn);

        SqlDataReader dr = null;

        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    DrMT_Bal_C dnt = new DrMT_Bal_C();
                    dnt.DoctorCode = dr["RatID"].ToString();
                    dnt.Name = dr["RateName"].ToString();
                    al.Add(dnt);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        finally
        {
            try
            {
                if (dr != null) dr.Close();

                conn.Close(); conn.Dispose();

            }
            catch (SqlException)
            {
                throw;
            }

        }

        return al;
    }

    protected void ddlratetype1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlratetype2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        sn.P_source = ddlratetype1.SelectedValue;
        sn.P_target = ddlratetype2.SelectedValue;

        if (rbtnRateList.Items[0].Selected == true)
        {
            sn.P_RateTypeflag = 1;
        }
        else if (rbtnRateList.Items[1].Selected == true)
        {
            sn.P_RateTypeflag = 2;
        }
        else if (rbtnRateList.Items[2].Selected == true)
        {
            sn.P_RateTypeflag = 3;
        }
        else if (rbtnRateList.Items[3].Selected == true)
        {
            sn.P_RateTypeflag = 4;
        }

        if (ddloprator.SelectedItem.Text.Trim() == "+")
        {
            sn.P_Opertor = 1;
        }
        else if ((ddloprator.SelectedItem.Text.Trim() == "-"))
        {
            sn.P_Opertor = 2;
        }
        else if ((ddloprator.SelectedItem.Text.Trim() == "*"))
        {
            sn.P_Opertor = 3;
        }
        else if ((ddloprator.SelectedItem.Text.Trim() == "/"))
        {
            sn.P_Opertor = 4;

        }
        else if ((ddloprator.SelectedItem.Text.Trim() == "%"))
        {
            sn.P_Opertor = 5;
        }
        else if ((ddloprator.SelectedItem.Text.Trim() == "="))
        {
            sn.P_Opertor = 6;

        }
        sn.P_addvalue = Convert.ToInt32(txt_value.Text);
        sn.P_Username = Session["username"].ToString();
        if (CheckBox1.Checked == true)
        {
            sn.Deletetargetid(sn.P_RateTypeflag);
            sn.selectRateSetting(Convert.ToInt32(Session["Branchid"]));
            lblerror.Visible = false;
            CheckBox1.Visible = false;
            CheckBox1.Checked = false;
        }
        else
        {
            if (sn.cheaktargetidexist(ddlratetype2.SelectedValue, Convert.ToInt32(Session["Branchid"])) == true || sn.cheaktargetidexist1(ddlratetype2.SelectedValue, Convert.ToInt32(Session["Branchid"])) == true)
            {
                CheckBox1.Visible = true;
                lblerror.Visible = true;
                return;
            }
            else
            {
                sn.selectRateSetting(Convert.ToInt32(Session["Branchid"]));
                lblerror.Visible = false;
                CheckBox1.Visible = false;
                CheckBox1.Checked = false;
            }
        }
    }

    protected void rbtnRateList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // trlabel.Visible = true;
            // trcontrol.Visible = true;
            btnSubmit.Visible = true;

            lblerror.Visible = false;
            CheckBox1.Visible = false;
            CheckBox1.Checked = false;

            if (rbtnRateList.Items[0].Selected == true)
            {
                if (Session["Branchid"] != null)
                {
                    ddlratetype1.DataSource = (ArrayList)RateFillForCollection(Convert.ToInt32(Session["Branchid"]));
                    ddlratetype1.DataTextField = "Name";
                    ddlratetype1.DataValueField = "DoctorCode";
                    ddlratetype1.DataBind();


                    ddlratetype2.DataSource = (ArrayList)RateFillForCollection(Convert.ToInt32(Session["Branchid"]));
                    ddlratetype2.DataTextField = "Name";
                    ddlratetype2.DataValueField = "DoctorCode";
                    ddlratetype2.DataBind();


                }
            }
            if (rbtnRateList.Items[1].Selected == true)
            {
                if (Session["Branchid"] != null)
                {
                    ddlratetype1.DataSource = (ArrayList)RateFillForCollection(Convert.ToInt32(Session["Branchid"]));
                    ddlratetype1.DataTextField = "Name";
                    ddlratetype1.DataValueField = "DoctorCode";
                    ddlratetype1.DataBind();


                    ddlratetype2.DataSource = (ArrayList)RateFillForCompliment(Convert.ToInt32(Session["Branchid"]));
                    ddlratetype2.DataTextField = "Name";
                    ddlratetype2.DataValueField = "DoctorCode";
                    ddlratetype2.DataBind();


                }
            }
            if (rbtnRateList.Items[2].Selected == true)
            {
                if (Session["Branchid"] != null)
                {
                    ddlratetype1.DataSource = (ArrayList)RateFillForCompliment(Convert.ToInt32(Session["Branchid"]));
                    ddlratetype1.DataTextField = "Name";
                    ddlratetype1.DataValueField = "DoctorCode";
                    ddlratetype1.DataBind();


                    ddlratetype2.DataSource = (ArrayList)RateFillForCompliment(Convert.ToInt32(Session["Branchid"]));
                    ddlratetype2.DataTextField = "Name";
                    ddlratetype2.DataValueField = "DoctorCode";
                    ddlratetype2.DataBind();


                }
            }
            if (rbtnRateList.Items[3].Selected == true)
            {
                if (Session["Branchid"] != null)
                {
                    ddlratetype1.DataSource = (ArrayList)RateFillForCompliment(Convert.ToInt32(Session["Branchid"]));
                    ddlratetype1.DataTextField = "Name";
                    ddlratetype1.DataValueField = "DoctorCode";
                    ddlratetype1.DataBind();

                    ddlratetype2.DataSource = (ArrayList)RateFillForCollection(Convert.ToInt32(Session["Branchid"]));
                    ddlratetype2.DataTextField = "Name";
                    ddlratetype2.DataValueField = "DoctorCode";
                    ddlratetype2.DataBind();


                }
            }
        }
        catch (Exception exc)
        {
            if (exc.Message.Equals("Exception aborted."))
            {
                return;
            }
            else
            {
                Response.Cookies["error"].Value = exc.Message;
                Server.Transfer("~/ErrorMessage.aspx");
            }
        }
    }
    public void checkexistpageright(string PageName)
    {

        string MenuSQL = "";
        DataTable MenuDt = new DataTable();
        MenuSQL = String.Format(@"SELECT        Roleright.Rightid, Roleright.Usertypeid, Roleright.FormId, Roleright.FormName, Roleright.Branchid, usr.ROLENAME, " +
              "  TBL_SubMenuMaster.SubMenuNavigateURL, TBL_MenuMaster.MenuName, TBL_MenuMaster.MenuID,   TBL_SubMenuMaster.SubMenuName, TBL_MenuMaster.Icon, " +
              "  TBL_SubMenuMaster.SubMenuID   " +
              "  FROM            Roleright INNER JOIN   usr ON Roleright.Usertypeid = usr.ROLLID AND Roleright.Branchid = usr.branchid INNER JOIN   " +
              "  TBL_SubMenuMaster ON Roleright.FormId = TBL_SubMenuMaster.SubMenuID INNER JOIN   TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID INNER JOIN  " +
              "  CTuser ON Roleright.Usertypeid = CTuser.Rollid  where (CTuser.USERNAME = '" + Convert.ToString(Session["username"]) + "') AND (CTuser.password = '" + Convert.ToString(Session["password"]) + "') and  TBL_SubMenuMaster.Isvisable=1  and TBL_SubMenuMaster.SubMenuNavigateURL='" + PageName + "'  " +
                               " order by MenuID  ");



        string connectionString1 = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString1);

        SqlCommand cmd = new SqlCommand(MenuSQL, con);

        SqlDataAdapter Adp = new SqlDataAdapter(cmd);

        Adp.Fill(MenuDt);
        if (MenuDt.Rows.Count == 0)
        {
            Response.Redirect("Login.aspx", false);
        }
        con.Close();
        con.Dispose();

    }

}