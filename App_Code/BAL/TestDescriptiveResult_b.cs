using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public class TestDescriptiveResult_b
{
	 public TestDescriptiveResult_b()
    {
        this.TextDId = 0;
        this.PatRegID = "";
        this.FID = "";
        this.MTCode = "";
        this.STCODE = "";
        this.TextDesc = "";
        this.Signatureid = 0;        
    }
    public TestDescriptiveResult_b(string PatRegID, string FID, string MTCode, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT * FROM radmst  WHERE PatRegID=@PatRegID and FID=@FID and MTCode=@MTCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {

                this.TextDId = Convert.ToInt32(sdr["ID"]);

                this.PatRegID = sdr["PatRegID"].ToString();

                if (!string.IsNullOrEmpty(sdr["FID"].ToString()))
                    this.FID = Convert.ToString(sdr["FID"]);
                else
                    this.FID = "";

                if (!string.IsNullOrEmpty(sdr["MTCode"].ToString()))
                    this.MTCode = Convert.ToString(sdr["MTCode"]);
                else
                    this.MTCode = "";

                if (!string.IsNullOrEmpty(sdr["STCODE"].ToString()))
                    this.STCODE = Convert.ToString(sdr["STCODE"]);
                else
                    this.STCODE = "";

                if (!string.IsNullOrEmpty(sdr["TextDesc"].ToString()))
                    this.TextDesc = Convert.ToString(sdr["TextDesc"]);
                else
                    this.TextDesc = "";

                this.Signatureid = Convert.ToInt32(sdr["Signatureid"]);

                if (!string.IsNullOrEmpty(sdr["ResultTemplate"].ToString()))
                    this.P_ResultTemplate = Convert.ToString(sdr["ResultTemplate"]);
                else
                    this.P_ResultTemplate = "";

                //if (!string.IsNullOrEmpty(sdr["Image1"].ToString()))
                //    this.Image1 = Convert.ToByte(sdr["Image1"]);
                //else
                //    this.Image01 = "";
            }
            else
            {
                throw new TestDescriptiveResult_bException("No Record Fetched.");
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
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }

        }
    }

 
    internal class TestDescriptiveResult_bException : Exception
    {
        public TestDescriptiveResult_bException(string msg) : base(msg) { }
    }

    public bool Insert(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();     

        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO radmst " +
        "(PatRegID,FID,MTCode,STCODE,TextDesc,Signatureid,branchid,PID,ResultTemplate) " +
        "VALUES (@PatRegID,@FID,@MTCode,@STCODE,@TextDesc,@Signatureid,@branchid,@PID,@ResultTemplate)", conn);
     
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;
        sc.Parameters.Add(new SqlParameter("@Signatureid", SqlDbType.Int)).Value = this.Signatureid;        
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = this.MTCode;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = this.STCODE;
        sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.Text)).Value = this.TextDesc;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = this.P_ResultTemplate;
        sc.Parameters.AddWithValue("@PID", this.PID);
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
        }
        return true;
    }

    public bool Delete(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            " Delete from radmst" +
            " Where PatRegID=@PatRegID and FID=@FID and MTCode=@MTCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = this.PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = this.MTCode;

        try
        {
            conn.Open();

            sc.ExecuteNonQuery();

        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
        }
        return true;
    }

    public TestDescriptiveResult_b(string PatRegID, string FID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Select * from radmst" +
           " WHERE PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "", conn);
       
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = (FID);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();

            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {
                this.TextDId = Convert.ToInt32(sdr["ID"].ToString());
                this.PatRegID = sdr["PatRegID"].ToString();
                this.FID = sdr["FID"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.STCODE = sdr["STCODE"].ToString();
                this.TextDesc = sdr["TextDesc"].ToString();
                this.Signatureid = Convert.ToInt32(sdr["Signatureid"].ToString());
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
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }

    }

    public bool DeletePat(string PatRegID, string FID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            " Delete from radmst" +
            " Where PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;

        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { }
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
        }
        return true;
    }

    public bool update(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
        "Update radmst " +
        "Set TextDesc=@TextDesc,Signatureid=@Signatureid,ResultTemplate=@ResultTemplate" +
        " Where PatRegID=@PatRegID and FID=@FID and MTCode=@MTCode and branchid=" + branchid + "", conn);
        SqlDataReader sdr = null;

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = this.MTCode;
        sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.Text)).Value = this.TextDesc;
        sc.Parameters.Add(new SqlParameter("@Signatureid", SqlDbType.Int)).Value = this.Signatureid;
        sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = this.ResultTemplate;
        try
        {
            conn.Open();

            sc.ExecuteNonQuery();


        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
        }
        return true;
    }
    public DataTable getDefaultResults(string STCODE, int branchid, string SDCode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {
            SqlCommand sc = new SqlCommand();
            if (SDCode != "")
            {
                sc = new SqlCommand(" SELECT * from dfrmst where SdCode='" + SDCode + "' and branchid=" + branchid + " and isactive=1 order by name ", conn);
            }
            else
            {
                sc = new SqlCommand(" SELECT * from dfrmst where STCODE=@STCODE and branchid=" + branchid + " and isactive=1 order by name ", conn);
            }
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;
            conn.Open();
            DataTable dt = new DataTable();
            dt.Load(sc.ExecuteReader());

            
            return dt;

        }
        catch (SqlException ex)
        {
            conn.Close(); conn.Dispose();
            return null;
        }
        
    }


   

 
    public DataTable getDefaultResults_search2(string STCODE, int branchid,string NAme)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {


            SqlCommand sc = new SqlCommand(" SELECT * from dfrmst where STCODE=@STCODE and Name like'" + NAme + "%'  and branchid=" + branchid + " order by name ", conn);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 5)).Value = STCODE;
            conn.Open();
            DataTable dt = new DataTable();
            dt.Load(sc.ExecuteReader());


            return dt;

        }
        catch (SqlException ex)
        {
            conn.Close(); conn.Dispose();
            return null;
        }

    }

    public DataTable getDefaultResults_search(string SDCode, int branchid, string NAme)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {


            SqlCommand sc = new SqlCommand(" SELECT * from   dfrmst INNER JOIN " +
               " MainTest ON dfrmst.STCODE = MainTest.MTCode  where MainTest.SDCode=@SDCode and Name like'" + NAme + "%'  and branchid=" + branchid + " order by name ", conn);
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = SDCode;
            conn.Open();
            DataTable dt = new DataTable();
            dt.Load(sc.ExecuteReader());


            return dt;

        }
        catch (SqlException ex)
        {
            conn.Close(); conn.Dispose();
            return null;
        }

    }


    public void TestDescriptiveResult_bCyro(string PatRegID, string FID, string MTCode, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT * FROM radmst_Cyto  WHERE PatRegID=@PatRegID and FID=@FID and MTCode=@MTCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {

                this.TCytoId = Convert.ToInt32(sdr["TCytoId"]);

                this.PatRegID = sdr["PatRegID"].ToString();

                if (!string.IsNullOrEmpty(sdr["FID"].ToString()))
                    this.FID = Convert.ToString(sdr["FID"]);
                else
                    this.FID = "";

                if (!string.IsNullOrEmpty(sdr["MTCode"].ToString()))
                    this.MTCode = Convert.ToString(sdr["MTCode"]);
                else
                    this.MTCode = "";

                if (!string.IsNullOrEmpty(sdr["STCODE"].ToString()))
                    this.STCODE = Convert.ToString(sdr["STCODE"]);
                else
                    this.STCODE = "";

                if (!string.IsNullOrEmpty(sdr["TextDesc"].ToString()))
                    this.TextDesc = Convert.ToString(sdr["TextDesc"]);
                else
                    this.TextDesc = "";

                this.Signatureid = Convert.ToInt32(sdr["Signatureid"]);

                if (!string.IsNullOrEmpty(sdr["ResultTemplate"].ToString()))
                    this.P_ResultTemplate = Convert.ToString(sdr["ResultTemplate"]);
                else
                    this.P_ResultTemplate = "";
            }
            else
            {
                throw new TestDescriptiveResult_bException("No Record Fetched.");
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
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }

        }
    }

    public bool Insert_Cyto(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO radmst_Cyto " +
        "(PatRegID,FID,MTCode,STCODE,TextDesc,Signatureid,branchid,PID,ResultTemplate,Image1,Image2,Image3,Image4,Image5,Image6) " +
        "VALUES (@PatRegID,@FID,@MTCode,@STCODE,@TextDesc,@Signatureid,@branchid,@PID,@ResultTemplate,@Image1,@Image2,@Image3,@Image4,@Image5,@Image6)", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;
        sc.Parameters.Add(new SqlParameter("@Signatureid", SqlDbType.Int)).Value = this.Signatureid;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = this.MTCode;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = this.STCODE;
        sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.Text)).Value = this.TextDesc;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = this.P_ResultTemplate;
        sc.Parameters.AddWithValue("@Image1", this.Image1);
        sc.Parameters.AddWithValue("@Image2", this.Image2);
        sc.Parameters.AddWithValue("@Image3", this.Image3);
        sc.Parameters.AddWithValue("@Image4", this.Image4);
        sc.Parameters.AddWithValue("@Image5", this.Image5);
        sc.Parameters.AddWithValue("@Image6", this.Image6);
        sc.Parameters.AddWithValue("@PID", this.PID);
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
        }
        return true;
    }


    public DataTable getDefaultResults_search_Cyto(string STCODE, int branchid, string NAme)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {


            SqlCommand sc = new SqlCommand(" SELECT * from dfrmst_Cyto where  Name like'" + NAme + "%'  and branchid=" + branchid + " order by name ", conn);//STCODE=@STCODE and
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;
            conn.Open();
            DataTable dt = new DataTable();
            dt.Load(sc.ExecuteReader());


            return dt;

        }
        catch (SqlException ex)
        {
            conn.Close(); conn.Dispose();
            return null;
        }

    }

    public DataTable getDefaultResults_search_Histo(string STCODE, int branchid, string NAme)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {


            SqlCommand sc = new SqlCommand(" SELECT * from dfrmst_Histo where  Name like'" + NAme + "%'  and branchid=" + branchid + " order by name ", conn);//STCODE=@STCODE and
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;
            conn.Open();
            DataTable dt = new DataTable();
            dt.Load(sc.ExecuteReader());


            return dt;

        }
        catch (SqlException ex)
        {
            conn.Close(); conn.Dispose();
            return null;
        }

    }

    public void TestDescriptiveResult_bHisto(string PatRegID, string FID, string MTCode, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT * FROM radmst_Histo  WHERE PatRegID=@PatRegID and FID=@FID and MTCode=@MTCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {

                this.TCytoId = Convert.ToInt32(sdr["THistoId"]);

                this.PatRegID = sdr["PatRegID"].ToString();

                if (!string.IsNullOrEmpty(sdr["FID"].ToString()))
                    this.FID = Convert.ToString(sdr["FID"]);
                else
                    this.FID = "";

                if (!string.IsNullOrEmpty(sdr["MTCode"].ToString()))
                    this.MTCode = Convert.ToString(sdr["MTCode"]);
                else
                    this.MTCode = "";

                if (!string.IsNullOrEmpty(sdr["STCODE"].ToString()))
                    this.STCODE = Convert.ToString(sdr["STCODE"]);
                else
                    this.STCODE = "";

                if (!string.IsNullOrEmpty(sdr["TextDesc"].ToString()))
                    this.TextDesc = Convert.ToString(sdr["TextDesc"]);
                else
                    this.TextDesc = "";

                this.Signatureid = Convert.ToInt32(sdr["Signatureid"]);

                if (!string.IsNullOrEmpty(sdr["ResultTemplate"].ToString()))
                    this.P_ResultTemplate = Convert.ToString(sdr["ResultTemplate"]);
                else
                    this.P_ResultTemplate = "";
            }
            else
            {
                throw new TestDescriptiveResult_bException("No Record Fetched.");
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
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }

        }
    }
    public bool update_Histo(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
        "Update radmst_Histo " +
        "Set TextDesc=@TextDesc,Signatureid=@Signatureid,ResultTemplate=@ResultTemplate" +
        " Where PatRegID=@PatRegID and FID=@FID and MTCode=@MTCode and branchid=" + branchid + "", conn);
        SqlDataReader sdr = null;

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = this.MTCode;
        sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.Text)).Value = this.TextDesc;
        sc.Parameters.Add(new SqlParameter("@Signatureid", SqlDbType.Int)).Value = this.Signatureid;
        sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = this.P_ResultTemplate;
        try
        {
            conn.Open();

            sc.ExecuteNonQuery();


        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
        }
        return true;
    }
    public bool update_Cyto(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
        "Update radmst_Cyto " +
        "Set TextDesc=@TextDesc,Signatureid=@Signatureid,ResultTemplate=@ResultTemplate" +
        " Where PatRegID=@PatRegID and FID=@FID and MTCode=@MTCode and branchid=" + branchid + "", conn);
        SqlDataReader sdr = null;

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = this.MTCode;
        sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.Text)).Value = this.TextDesc;
        sc.Parameters.Add(new SqlParameter("@Signatureid", SqlDbType.Int)).Value = this.Signatureid;
        sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = this.P_ResultTemplate;
        try
        {
            conn.Open();

            sc.ExecuteNonQuery();


        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
        }
        return true;
    }

    public DataTable Get_DescResult()
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {


            SqlCommand sc = new SqlCommand(" SELECT * from VW_desfiledata_org  ", conn);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;
            conn.Open();
            DataTable dt = new DataTable();
            dt.Load(sc.ExecuteReader());


            return dt;

        }
        catch (SqlException ex)
        {
            conn.Close(); conn.Dispose();
            return null;
        }

    }
    public DataTable getMaindepartment(string MTCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {


            SqlCommand sc = new SqlCommand(" SELECT SDCode from MainTest where MTCode=@STCODE and branchid=" + branchid + " order by SDCode ", conn);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = MTCode;
            conn.Open();
            DataTable dt = new DataTable();
            dt.Load(sc.ExecuteReader());


            return dt;

        }
        catch (SqlException ex)
        {
            conn.Close(); conn.Dispose();
            return null;
        }

    }


    #region Properties
    private byte[] image1;
    public byte[] Image1
    {
        get { return image1; }
        set { image1 = value; }
    }
    private byte[] image2;
    public byte[] Image2
    {
        get { return image2; }
        set { image2 = value; }
    }
    private byte[] image3;
    public byte[] Image3
    {
        get { return image3; }
        set { image3 = value; }
    }
    private byte[] image4;
    public byte[] Image4
    {
        get { return image4; }
        set { image4 = value; }
    }
    private byte[] image5;
    public byte[] Image5
    {
        get { return image5; }
        set { image5 = value; }
    }
    private byte[] image6;
    public byte[] Image6
    {
        get { return image6; }
        set { image6 = value; }
    }
    private int textDId;
    public int TextDId
    {
        get { return textDId; }
        set { textDId = value; }
    }
    private int tCytoId;
    public int TCytoId
    {
        get { return tCytoId; }
        set { tCytoId = value; }
    }
    private int tHistoId;
    public int THistoId
    {
        get { return tHistoId; }
        set { tHistoId = value; }
    }
    private string _PatRegID;
    public string PatRegID
    {
        get { return _PatRegID; }
        set { _PatRegID = value; }
    }

    private int signatureid;
    public int Signatureid
    {
        get { return signatureid; }
        set { signatureid = value; }
    }

    private string fID;
    public string FID
    {
        get { return fID; }
        set { fID = value; }
    }

    private string mTCode;
    public string MTCode
    {
        get { return mTCode; }
        set { mTCode = value; }
    }

    private string sTCODE;
    public string STCODE
    {
        get { return sTCODE; }
        set { sTCODE = value; }
    }

    private string _Image01;
    public string Image01
    {
        get { return _Image01; }
        set { _Image01 = value; }
    }

    private string textDesc;
    public string TextDesc
    {
        get { return textDesc; }
        set { textDesc = value; }
    }

   
    private bool flag;
    public bool P_flag
    {
        get { return flag; }
        set { flag = value; }
    }

    private int _PID;
    public int PID
    {
        get { return _PID; }
        set { _PID = value; }
    }
    private string ResultTemplate;
    public string P_ResultTemplate
    {
        get { return ResultTemplate; }
        set { ResultTemplate = value; }
    }

    #endregion
}