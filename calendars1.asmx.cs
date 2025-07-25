using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace WebApp_1
{
    /// <summary>
    /// Summary description for calendars
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class calendars : System.Web.Services.WebService
    {
        List<cls1> list = new List<cls1>();
        List<businessHrs> Bhrs = new List<businessHrs>();
        List<MainData> lists = new List<MainData>();

        public calendars()
        {
            list.Add(new cls1() { doctorId = 1, EventID = "1", StartDate = "2019-05-01T10:00:00", EndDate = "2019-05-01T10:30:00", EventName = "Mr.Joshi", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Red.ToString(), ClassName = "Canceled" });
            list.Add(new cls1() { doctorId = 1, EventID = "11", StartDate = "2019-05-01T10:30:00", EndDate = "2019-05-01T11:00:00", EventName = "Akshay", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 1, EventID = "12", StartDate = "2019-05-01T11:00:00", EndDate = "2019-05-01T11:30:00", EventName = "Seema", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 1, EventID = "13", StartDate = "2019-05-01T11:30:00", EndDate = "2019-05-01T12:00:00", EventName = "Ajay", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 1, EventID = "2", StartDate = "2019-05-6T12:00:00", EndDate = "2019-05-6T12:30:00", EventName = "Sam", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 1, EventID = "3", StartDate = "2019-05-13T4:00:00", EndDate = "2019-05-13T14:30:00", EventName = "Yuvraj", Url = "www.google.com", ImageType = true, BackgroundColor = Color.Red.ToString(), ClassName = "Canceled" });
            list.Add(new cls1() { doctorId = 1, EventID = "4", StartDate = "2019-05-13T10:00:00", EndDate = "2019-05-13T10:30:00", EventName = "Sachin", Url = "www.google.com", ImageType = true, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 1, EventID = "5", StartDate = "2019-05-13T11:00:00", EndDate = "2019-05-13T11:30:00", EventName = "Alex", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 1, EventID = "211", StartDate = "2019-05-21T10:00", EndDate = "2019-05-21T10:30", EventName = "Raj", Url = "www.google.com", ImageType = false, BackgroundColor = Color.GreenYellow.ToString(), ClassName = "Pending" });
            list.Add(new cls1() { doctorId = 1, EventID = "212", StartDate = "2019-05-21T10:30", EndDate = "2019-05-21T11:00", EventName = "Shyam", Url = "www.google.com", ImageType = false, BackgroundColor = Color.GreenYellow.ToString(), ClassName = "Pending" });
            list.Add(new cls1() { doctorId = 1, EventID = "213", StartDate = "2019-05-21T11:00", EndDate = "2019-05-21T11:30", EventName = "Babloo", Url = "www.google.com", ImageType = false, BackgroundColor = Color.GreenYellow.ToString(), ClassName = "Pending" });
            list.Add(new cls1() { doctorId = 1, EventID = "214", StartDate = "2019-05-21T11:30", EndDate = "2019-05-21T12:00", EventName = "Radha", Url = "www.google.com", ImageType = false, BackgroundColor = Color.LawnGreen.ToString(), ClassName = "Confirmed" });

            list.Add(new cls1() { doctorId = 2, EventID = "1", StartDate = "2019-05-01T10:00:00", EndDate = "2019-05-01T10:30:00", EventName = "Mr.Joshi", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Red.ToString(), ClassName = "Canceled" });
            list.Add(new cls1() { doctorId = 2, EventID = "11", StartDate = "2019-05-01T10:30:00", EndDate = "2019-05-01T11:00:00", EventName = "Akshay", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 2, EventID = "12", StartDate = "2019-05-01T11:00:00", EndDate = "2019-05-01T11:30:00", EventName = "Seema", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 2, EventID = "3", StartDate = "2019-05-13T4:00:00", EndDate = "2019-05-13T14:30:00", EventName = "Yuvraj", Url = "www.google.com", ImageType = true, BackgroundColor = Color.Red.ToString(), ClassName = "Canceled" });
            list.Add(new cls1() { doctorId = 2, EventID = "4", StartDate = "2019-05-13T10:00:00", EndDate = "2019-05-13T10:30:00", EventName = "Sachin", Url = "www.google.com", ImageType = true, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 2, EventID = "5", StartDate = "2019-05-13T11:00:00", EndDate = "2019-05-13T11:30:00", EventName = "Alex", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 2, EventID = "211", StartDate = "2019-05-21T10:00", EndDate = "2019-05-21T10:30", EventName = "Raj", Url = "www.google.com", ImageType = false, BackgroundColor = Color.GreenYellow.ToString(), ClassName = "Pending" });
            list.Add(new cls1() { doctorId = 2, EventID = "212", StartDate = "2019-05-21T10:30", EndDate = "2019-05-21T11:00", EventName = "Shyam", Url = "www.google.com", ImageType = false, BackgroundColor = Color.GreenYellow.ToString(), ClassName = "Pending" });
            list.Add(new cls1() { doctorId = 2, EventID = "213", StartDate = "2019-05-21T11:00", EndDate = "2019-05-21T11:30", EventName = "Babloo", Url = "www.google.com", ImageType = false, BackgroundColor = Color.GreenYellow.ToString(), ClassName = "Pending" });
            list.Add(new cls1() { doctorId = 2, EventID = "214", StartDate = "2019-05-21T11:30", EndDate = "2019-05-21T12:00", EventName = "Radha", Url = "www.google.com", ImageType = false, BackgroundColor = Color.LawnGreen.ToString(), ClassName = "Confirmed" });

            list.Add(new cls1() { doctorId = 3, EventID = "3", StartDate = "2019-05-13T4:00:00", EndDate = "2019-05-13T14:30:00", EventName = "Yuvraj", Url = "www.google.com", ImageType = true, BackgroundColor = Color.Red.ToString(), ClassName = "Canceled" });
            list.Add(new cls1() { doctorId = 3, EventID = "4", StartDate = "2019-05-13T10:00:00", EndDate = "2019-05-13T10:30:00", EventName = "Sachin", Url = "www.google.com", ImageType = true, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 3, EventID = "5", StartDate = "2019-05-13T11:00:00", EndDate = "2019-05-13T11:30:00", EventName = "Alex", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 3, EventID = "211", StartDate = "2019-05-21T10:00", EndDate = "2019-05-21T10:30", EventName = "Raj", Url = "www.google.com", ImageType = false, BackgroundColor = Color.GreenYellow.ToString(), ClassName = "Pending" });
            list.Add(new cls1() { doctorId = 3, EventID = "212", StartDate = "2019-05-21T10:30", EndDate = "2019-05-21T11:00", EventName = "Shyam", Url = "www.google.com", ImageType = false, BackgroundColor = Color.GreenYellow.ToString(), ClassName = "Pending" });
            list.Add(new cls1() { doctorId = 3, EventID = "213", StartDate = "2019-05-21T11:00", EndDate = "2019-05-21T11:30", EventName = "Babloo", Url = "www.google.com", ImageType = false, BackgroundColor = Color.GreenYellow.ToString(), ClassName = "Pending" });
            list.Add(new cls1() { doctorId = 3, EventID = "214", StartDate = "2019-05-21T11:30", EndDate = "2019-05-21T12:00", EventName = "Radha", Url = "www.google.com", ImageType = false, BackgroundColor = Color.LawnGreen.ToString(), ClassName = "Confirmed" });

            list.Add(new cls1() { doctorId = 4, EventID = "1", StartDate = "2019-05-01T10:00:00", EndDate = "2019-05-01T10:30:00", EventName = "Mr.Joshi", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Red.ToString(), ClassName = "Canceled" });
            list.Add(new cls1() { doctorId = 4, EventID = "11", StartDate = "2019-05-01T10:30:00", EndDate = "2019-05-01T11:00:00", EventName = "Akshay", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 4, EventID = "12", StartDate = "2019-05-01T11:00:00", EndDate = "2019-05-01T11:30:00", EventName = "Seema", Url = "www.google.com", ImageType = false, BackgroundColor = Color.Green.ToString(), ClassName = "Completed" });
            list.Add(new cls1() { doctorId = 4, EventID = "3", StartDate = "2019-05-13T4:00:00", EndDate = "2019-05-13T14:30:00", EventName = "Yuvraj", Url = "www.google.com", ImageType = true, BackgroundColor = Color.Red.ToString(), ClassName = "Canceled" });
            list.Add(new cls1() { doctorId = 4, EventID = "212", StartDate = "2019-05-21T10:30", EndDate = "2019-05-21T11:00", EventName = "Shyam", Url = "www.google.com", ImageType = false, BackgroundColor = Color.GreenYellow.ToString(), ClassName = "Pending" });
            list.Add(new cls1() { doctorId = 4, EventID = "213", StartDate = "2019-05-21T11:00", EndDate = "2019-05-21T11:30", EventName = "Babloo", Url = "www.google.com", ImageType = false, BackgroundColor = Color.GreenYellow.ToString(), ClassName = "Pending" });
            list.Add(new cls1() { doctorId = 4, EventID = "214", StartDate = "2019-05-21T11:30", EndDate = "2019-05-21T12:00", EventName = "Radha", Url = "www.google.com", ImageType = false, BackgroundColor = Color.LawnGreen.ToString(), ClassName = "Confirmed" });




            Bhrs.Add(new businessHrs() { doctorId = 1, down = new int[] { 1, 2, 3 }, start = "10:00", end = "16:00" });
            Bhrs.Add(new businessHrs() { doctorId = 2, down = new int[] { 2, 5, 7 }, start = "12:00", end = "16:00" });
            Bhrs.Add(new businessHrs() { doctorId = 3, down = new int[] { 1, 2, 3, 4 }, start = "8:00", end = "16:00" });
            Bhrs.Add(new businessHrs() { doctorId = 4, down = new int[] { 6, 7 }, start = "7:00", end = "19:00" });

        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void GetAppointmentData(string id)
        {
            var listss = from m in list
                         where m.doctorId == Convert.ToInt32(id)
                         select m;

            lists.Add(new MainData()
            {
                hrs = Bhrs.Find(x => x.doctorId == Convert.ToInt32(id)),
                events = listss.ToList(),
            });

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(lists));
        }


        [WebMethod]
        public void GetDocSchedule()
        {
            List<cls1> list = new List<cls1>();

            list.Add(new cls1() { EventID = "1", StartDate = "2019-01-01", EndDate = "2019-01-12", EventName = "title1", Url = "www.google.com", ImageType = false });
            list.Add(new cls1() { EventID = "2", StartDate = "2019-02-16", EndDate = "2019-02-22", EventName = "title2", Url = "www.google.com", ImageType = false });
            list.Add(new cls1() { EventID = "3", StartDate = "2019-05-02", EndDate = "2019-05-22", EventName = "title3", Url = "www.google.com", ImageType = true });
            list.Add(new cls1() { EventID = "4", StartDate = "2019-07-01", EndDate = "2019-07-02", EventName = "title4", Url = "www.google.com", ImageType = true });
            list.Add(new cls1() { EventID = "5", StartDate = "2019-11-11", EndDate = "2019-11-12", EventName = "title5", Url = "www.google.com", ImageType = false });

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(list));
        }

        [WebMethod]
        public void SaveAppointmentEvent(string doctorId, string appDate, string startTime, string endTime, 
            string patientNameF, string patientNameL, string address, string note)
        {
            string res = "Something went wrong.";

            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            using (SqlConnection connection =
                       new SqlConnection(ConString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_InsertAppointment", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@AppDate", SqlDbType.Date).Value = Convert.ToDateTime(appDate);
                    cmd.Parameters.Add("@AppTime", SqlDbType.DateTime).Value = (Convert.ToDateTime(startTime)).ToString("hh:mm:ss tt", CultureInfo.CurrentCulture);
                    cmd.Parameters.Add("@PatientF", SqlDbType.VarChar).Value = patientNameF;
                    cmd.Parameters.Add("@PatientL", SqlDbType.VarChar).Value = patientNameL;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.Int).Value = doctorId;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = 2;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = "May";
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = address;
                    cmd.Parameters.Add("@Note", SqlDbType.VarChar).Value = note;
                    cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = System.DateTime.Now;

                    connection.Open();

                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        res = "Appointment booked successfully.";
                    }
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(res));
        }

        class clsSlot
        {
            public string name { get; set; }
            public string className { get; set; }
        }

        class clsSlot1
        {
            public string docName { get; set; }
            public string docId { get; set; }
            public string slotInterval { get; set; }
        }

        [WebMethod]
        public void GetDocScheduleByDate(string doctorId, string appDate)
        {
            List<clsSlot> list = new List<clsSlot>();

            DataTable dt = ReadDataSlot(doctorId, appDate, null, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        list.Add(new clsSlot() { name = dt.Rows[i]["TimeSlot"].ToString(), className = dt.Rows[i]["Status"].ToString() });
                    }

                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(list));
        }

        class AppDetails
        {
            public string AppDate { get; set; }
            public string AppTime { get; set; }
            public string PatientFirstName { get; set; }
            public string PatientLastName { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedOn { get; set; }
            public string Status { get; set; }
            public string AppId { get; set; }
            public string Address { get; set; }
            public string Note { get; set; }

        }

        [WebMethod]
        public void Get_AppoinmentPatientDetails(string doctorId, string clickedDate)
        {
            List<AppDetails> listApp = new List<AppDetails>();

            DataTable dt = GetAppoinmentPatientDetails(doctorId, clickedDate);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listApp.Add(new AppDetails()
                        {
                            AppId = dt.Rows[i]["AppId"].ToString(),
                            AppDate = dt.Rows[i]["AppDate"].ToString(),
                            AppTime = dt.Rows[i]["AppTime"].ToString(),
                            CreatedBy = dt.Rows[i]["CreatedBy"].ToString(),
                            CreatedOn = dt.Rows[i]["CreatedOn"].ToString(),
                            Status = dt.Rows[i]["Status"].ToString(),
                            PatientFirstName = dt.Rows[i]["PatientFirstName"].ToString(),
                            PatientLastName = dt.Rows[i]["PatientLastName"].ToString(),
                            Address = dt.Rows[i]["Address"].ToString(),
                            Note = dt.Rows[i]["Note"].ToString()
                        });
                    }

                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listApp));
        }


        [WebMethod]
        public void GetDocList()
        {
            List<clsSlot1> listS = new List<clsSlot1>();

            DataTable dt = ReadDataDocList();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listS.Add(new clsSlot1()
                        {
                            docId = dt.Rows[i]["DoctorId"].ToString(),
                            docName = dt.Rows[i]["DoctorName"].ToString(),
                            slotInterval = dt.Rows[i]["SlotInterval"].ToString()
                        });
                    }

                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listS));
        }

        [WebMethod]
        public void GetHistoryList()
        {
            List<List<clsSlot>> lists = new List<List<clsSlot>>();
            List<clsSlot> listP = new List<clsSlot>();
            List<clsSlot> listF = new List<clsSlot>();
            List<clsSlot> listS = new List<clsSlot>();

            DataTable dt = ReadDataHistoryList();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        switch (Convert.ToInt32(dt.Rows[i]["typeId"].ToString()))
                        {
                            case 1:
                                listP.Add(new clsSlot() { name = dt.Rows[i]["catId"].ToString(), className = dt.Rows[i]["catName"].ToString() });
                                break;
                            case 2:
                                listF.Add(new clsSlot() { name = dt.Rows[i]["catId"].ToString(), className = dt.Rows[i]["catName"].ToString() });
                                break;
                            case 3:
                                listS.Add(new clsSlot() { name = dt.Rows[i]["catId"].ToString(), className = dt.Rows[i]["catName"].ToString() });
                                break;
                        }

                    }
                }
            }
            lists.Add(listS);
            lists.Add(listP);
            lists.Add(listF);
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(lists));
        }

        class cls_GeneralEmr
        {
            public string EmrId { get; set; }
            public string Patientregid { get; set; }
            public string opd { get; set; }
            public string ipd { get; set; }
            public string branchid { get; set; }
            public string createdby { get; set; }
            public string createdon { get; set; }
            public string updatedby { get; set; }
            public string updatedon { get; set; }
            public string chiefcomplaints { get; set; }
            public string allergies { get; set; }
            public string medicalhistory { get; set; }
            public string ht { get; set; }
            public string wt { get; set; }
            public string pulse { get; set; }
            public string bp { get; set; }
            public string resp { get; set; }
            public string spo2 { get; set; }
            public string chest { get; set; }
            public string a { get; set; }
            public string b { get; set; }
            public string c { get; set; }
            public string d { get; set; }
            public string e { get; set; }
            public string pasthistory { get; set; }
            public string personalhistory { get; set; }
            public string familyhistory { get; set; }
            public string surgicalhistory { get; set; }
            public string f { get; internal set; }
        }

        [WebMethod]
        public void ReadGeneralEmrDetails(string patientregid, string opd, string ipd, string branchid)
        {
            List<cls_GeneralEmr> listGeneralEmr = new List<cls_GeneralEmr>();

            DataTable dt = new DataTable();

            dt = GetGeneralEmrDetailsEdit(patientregid, opd, ipd, branchid);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listGeneralEmr.Add(new cls_GeneralEmr()
                        {
                            EmrId = dt.Rows[i]["EmrId"].ToString(),
                            Patientregid = dt.Rows[i]["Patientregid"].ToString(),
                            opd = dt.Rows[i]["opd"].ToString(),
                            ipd = dt.Rows[i]["ipd"].ToString(),
                            branchid = dt.Rows[i]["branchid"].ToString(),
                            createdby = dt.Rows[i]["createdby"].ToString(),
                            updatedby = dt.Rows[i]["updatedby"].ToString(),

                            chiefcomplaints = dt.Rows[i]["chiefcomplaints"].ToString(),
                            allergies = dt.Rows[i]["allergies"].ToString(),
                            medicalhistory = dt.Rows[i]["medicalhistory"].ToString(),
                            ht = dt.Rows[i]["ht"].ToString(),
                            wt = dt.Rows[i]["wt"].ToString(),
                            pulse = dt.Rows[i]["pulse"].ToString(),
                            bp = dt.Rows[i]["bp"].ToString(),
                            resp = dt.Rows[i]["resp"].ToString(),
                            spo2 = dt.Rows[i]["spo2"].ToString(),
                            chest = dt.Rows[i]["chest"].ToString(),
                            a = dt.Rows[i]["a"].ToString(),
                            b = dt.Rows[i]["b"].ToString(),
                            c = dt.Rows[i]["c"].ToString(),
                            d = dt.Rows[i]["d"].ToString(),
                            e = dt.Rows[i]["e"].ToString(),
                            //f = dt.Rows[i]["f"].ToString(),
                            pasthistory = dt.Rows[i]["pasthistory"].ToString(),
                            personalhistory = dt.Rows[i]["personalhistory"].ToString(),
                            familyhistory = dt.Rows[i]["familyhistory"].ToString(),
                            surgicalhistory = dt.Rows[i]["surgicalhistory"].ToString(),
                        });

                    }
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listGeneralEmr));
        }

        private DataTable GetGeneralEmrDetailsEdit(string patregId, string opd, string ipd, string branchid)
        {
            DataTable dt = new DataTable();

            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            using (SqlConnection connection =
                       new SqlConnection(ConString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetGeneralEmrDetailsByPatientId", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Patientregid", SqlDbType.VarChar).Value = patregId;
                    cmd.Parameters.Add("@Opd", SqlDbType.VarChar).Value = opd;
                    cmd.Parameters.Add("@Ipd", SqlDbType.VarChar).Value = ipd;
                    cmd.Parameters.Add("@BranchId", SqlDbType.VarChar).Value = branchid;

                    connection.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }

                }
            }
            return dt;
        }

        class clsVitals
        {
            public string CreatedOn { get; set; }
            public string Ht { get; set; }
            public string Wt { get; set; }
            public string Pulse { get; set; }
            public string Bp { get; set; }
            public string Resp { get; set; }
            public string Spo2 { get; set; }
            public string Chest { get; set; }
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public string E { get; set; }
        }

        [WebMethod]
        public void ReadGeneralEmrDetails3(string patientregid, string opd, string ipd, string branchid)
        {
            List<clsVitals> obj = new List<clsVitals>();

            DataTable dt = new DataTable();

            dt = GetGeneralEmrDetailsBind(patientregid, opd, ipd, branchid);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        obj.Add(new clsVitals()
                        {
                            CreatedOn = dt.Rows[i]["createdon"].ToString(),
                            Ht = dt.Rows[i]["Ht"].ToString(),
                            Wt = dt.Rows[i]["wt"].ToString(),
                            Pulse = dt.Rows[i]["pulse"].ToString(),
                            Bp = dt.Rows[i]["bp"].ToString(),
                            Resp = dt.Rows[i]["resp"].ToString(),
                            Spo2 = dt.Rows[i]["spo2"].ToString(),
                            Chest = dt.Rows[i]["chest"].ToString(),
                            A = dt.Rows[i]["a"].ToString(),
                            B = dt.Rows[i]["b"].ToString(),
                            C = dt.Rows[i]["c"].ToString(),
                            D = dt.Rows[i]["d"].ToString(),
                            E = dt.Rows[i]["e"].ToString(),
                        });

                    }
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(obj));
        }

        [WebMethod]
        public void ReadGeneralEmrDetails2(string patientregid, string opd, string ipd, string branchid)
        {
            List<List<clsSlot>> obj = new List<List<clsSlot>>();

            List<cls_GeneralEmr> listGeneralEmr = new List<cls_GeneralEmr>();

            DataTable dt = new DataTable();

            dt = GetGeneralEmrDetailsBind(patientregid, opd, ipd, branchid);

            List<clsSlot> objC = new List<clsSlot>();
            List<clsSlot> objA = new List<clsSlot>();
            List<clsSlot> objM = new List<clsSlot>();

            List<clsSlot> objV = new List<clsSlot>();

            List<clsSlot> objL = new List<clsSlot>();
            List<clsSlot> objF = new List<clsSlot>();
            List<clsSlot> objS = new List<clsSlot>();
            List<clsSlot> objT = new List<clsSlot>();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objC.Add(new clsSlot()
                        {
                            name = dt.Rows[i]["createdon"].ToString(),
                            className = dt.Rows[i]["chiefcomplaints"].ToString()
                        });

                        objA.Add(new clsSlot()
                        {
                            name = dt.Rows[i]["createdon"].ToString(),
                            className = dt.Rows[i]["allergies"].ToString()
                        });

                        objM.Add(new clsSlot()
                        {
                            name = dt.Rows[i]["createdon"].ToString(),
                            className = dt.Rows[i]["medicalhistory"].ToString()
                        });

                        objV.Add(new clsSlot()
                        {
                            name = dt.Rows[i]["createdon"].ToString(),
                            className = dt.Rows[i]["medicalhistory"].ToString()
                        });

                        objL.Add(new clsSlot()
                        {
                            name = dt.Rows[i]["createdon"].ToString(),
                            className = dt.Rows[i]["personalhistory"].ToString()
                        });

                        objF.Add(new clsSlot()
                        {
                            name = dt.Rows[i]["createdon"].ToString(),
                            className = dt.Rows[i]["familyhistory"].ToString()
                        });

                        objS.Add(new clsSlot()
                        {
                            name = dt.Rows[i]["createdon"].ToString(),
                            className = dt.Rows[i]["surgicalhistory"].ToString()
                        });

                        objT.Add(new clsSlot()
                        {
                            name = dt.Rows[i]["createdon"].ToString(),
                            className = dt.Rows[i]["pasthistory"].ToString()
                        });
                    }
                }
            }

            obj.Add(objC);
            obj.Add(objA);
            obj.Add(objM);

            obj.Add(objV);

            obj.Add(objL);
            obj.Add(objF);
            obj.Add(objS);
            obj.Add(objT);

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(obj));
        }

        private DataTable GetGeneralEmrDetailsBind(string patregId, string opd, string ipd, string branchid)
        {
            DataTable dt = new DataTable();

            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            using (SqlConnection connection =
                       new SqlConnection(ConString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetGeneralEmrDetails_2", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Patientregid", SqlDbType.VarChar).Value = patregId;
                    //cmd.Parameters.Add("@Opd", SqlDbType.VarChar).Value = opd;
                    //cmd.Parameters.Add("@Ipd", SqlDbType.VarChar).Value = ipd;
                    //cmd.Parameters.Add("@BranchId", SqlDbType.VarChar).Value = branchid;

                    connection.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }

                }
            }
            return dt;
        }

        private DataTable GetGeneralEmrDetailsBind2(string patregId, string opd, string ipd, string branchid)
        {
            DataTable dt = new DataTable();

            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            using (SqlConnection connection =
                       new SqlConnection(ConString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetGeneralEmrDetails_2", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Patientregid", SqlDbType.VarChar).Value = patregId;
                    //cmd.Parameters.Add("@Opd", SqlDbType.VarChar).Value = opd;
                    //cmd.Parameters.Add("@Ipd", SqlDbType.VarChar).Value = ipd;
                    //cmd.Parameters.Add("@BranchId", SqlDbType.VarChar).Value = branchid;

                    connection.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }

                }
            }
            return dt;
        }

        private DataTable GetAppoinmentPatientDetails(string docId, string clickedDate)
        {
            DataTable dt = new DataTable();

            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            using (SqlConnection connection =
                       new SqlConnection(ConString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetAppointmentPatientDetails", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@doctorId", SqlDbType.VarChar).Value = docId;
                    cmd.Parameters.Add("@clickedDate", SqlDbType.VarChar).Value = string.IsNullOrEmpty(clickedDate) ? null : clickedDate;

                    connection.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }

                }
            }
            return dt;
        }

        [WebMethod]
        public void SaveGeneralEmr(string type,
            string patientregid, string opd, string ipd, string branchid, string createdby, string updatedby,
            string chiefComplaints, string allergy, string medicalHistory, string ht, string wt,
                   string pulse, string bp, string resp, string spo2, string chest, string a, string b,
                   string c, string d, string e, string f, string past, string personal, string family, string surgery
            )
        {
            string res = "";

            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            using (SqlConnection connection =
                       new SqlConnection(ConString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_InsertGeneralEmr", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@type", SqlDbType.Int).Value = Convert.ToInt32(type);
                    cmd.Parameters.Add("@Patientregid", SqlDbType.VarChar).Value = patientregid;
                    cmd.Parameters.Add("@opd", SqlDbType.Int).Value = Convert.ToInt32(opd);
                    cmd.Parameters.Add("@ipd", SqlDbType.Int).Value = Convert.ToInt32(ipd);
                    cmd.Parameters.Add("@branchid", SqlDbType.Int).Value = Convert.ToInt32(branchid);
                    cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = createdby;
                    cmd.Parameters.Add("@updatedby", SqlDbType.VarChar).Value = updatedby;
                    cmd.Parameters.Add("@chiefcomplaints", SqlDbType.VarChar).Value = chiefComplaints;
                    cmd.Parameters.Add("@allergies", SqlDbType.VarChar).Value = allergy;
                    cmd.Parameters.Add("@medicalhistory", SqlDbType.VarChar).Value = medicalHistory;
                    cmd.Parameters.Add("@ht", SqlDbType.Float).Value = Convert.ToInt64(ht);
                    cmd.Parameters.Add("@wt", SqlDbType.Float).Value = Convert.ToInt64(wt);
                    cmd.Parameters.Add("@pulse", SqlDbType.Float).Value = Convert.ToInt64(pulse);
                    cmd.Parameters.Add("@bp", SqlDbType.Int).Value = Convert.ToInt32(bp);
                    cmd.Parameters.Add("@resp", SqlDbType.Int).Value = Convert.ToInt32(resp);
                    cmd.Parameters.Add("@spo2", SqlDbType.Int).Value = Convert.ToInt32(spo2);
                    cmd.Parameters.Add("@chest", SqlDbType.Float).Value = Convert.ToInt64(chest);
                    cmd.Parameters.Add("@a", SqlDbType.Int).Value = Convert.ToInt32(a);
                    cmd.Parameters.Add("@b", SqlDbType.Int).Value = Convert.ToInt32(b);
                    cmd.Parameters.Add("@c", SqlDbType.Int).Value = Convert.ToInt32(c);
                    cmd.Parameters.Add("@d", SqlDbType.Int).Value = Convert.ToInt32(d);
                    cmd.Parameters.Add("@e", SqlDbType.Int).Value = Convert.ToInt32(e);
                    cmd.Parameters.Add("@pasthistory", SqlDbType.VarChar).Value = past;
                    cmd.Parameters.Add("@personalhistory", SqlDbType.VarChar).Value = personal;
                    cmd.Parameters.Add("@familyhistory", SqlDbType.VarChar).Value = family;
                    cmd.Parameters.Add("@surgicalhistory", SqlDbType.VarChar).Value = surgery;

                    connection.Open();

                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        res = "Saved successfully.";
                    }
                    else
                    {
                        res = "Something went wrong";
                    }
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(res));
        }

        private DataTable ReadDataDocList()
        {
            DataTable dt = new DataTable();

            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            using (SqlConnection connection =
                       new SqlConnection(ConString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetDoctorsList", connection))
                {
                    connection.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }

                }
            }

            return dt;
        }

        private DataTable ReadDataHistoryList()
        {
            DataTable dt = new DataTable();

            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            using (SqlConnection connection =
                       new SqlConnection(ConString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetHistoryList", connection))
                {
                    connection.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }

            return dt;
        }

        [WebMethod]
        public void SaveEvent(string doctorId, string title, string start, string end)
        {
            list.Add(new cls1()
            {
                doctorId = Convert.ToInt32(doctorId),
                StartDate = start,
                EndDate = end,
                EventName = title,
                Url = "www.google.com",
                ImageType = false,
                BackgroundColor = Color.GreenYellow.ToString(),
                ClassName = "Pending"
            });
            //EventID = "1"

            var listss = from m in list
                         where m.doctorId == Convert.ToInt32(doctorId)
                         select m;

            lists.Add(new MainData()
            {
                hrs = Bhrs.Find(x => x.doctorId == Convert.ToInt32(doctorId)),
                events = listss.ToList(),
            });

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(lists));
        }

        private static DataTable ReadDataSlot(string doctorId, string findDate, string start, string end)
        {
            DataTable dt = new DataTable();

            //string[] val = findDate.Split('-');

            //string newDate = val[2] + "-" + val[1] + "-" + val[0];

            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            using (SqlConnection connection =
                       new SqlConnection(ConString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetTimeSlotsByDoctorId", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@docId", SqlDbType.VarChar).Value = doctorId;
                    cmd.Parameters.Add("@findDate", SqlDbType.Date).Value = Convert.ToDateTime(findDate);
                    //cmd.Parameters.Add("@dtStart", SqlDbType.VarChar).Value = start;
                    //cmd.Parameters.Add("@dtEnd", SqlDbType.VarChar).Value = end;

                    connection.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public class MainData
        {
            public List<cls1> events { get; set; }
            public businessHrs hrs { get; set; }

        }

        public class cls1
        {
            public int doctorId { get; set; }
            public string EventID { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string EventName { get; set; }
            public string Url { get; set; }
            public bool ImageType { get; set; }
            public string BackgroundColor { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string ClassName { get; set; }
        }

        public class businessHrs
        {
            public int doctorId { get; set; }
            public int[] down { get; set; }
            public string start { get; set; }
            public string end { get; set; }
        }

    }
}
