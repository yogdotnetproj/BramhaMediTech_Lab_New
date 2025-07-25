using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public class DateTimeConvesion
{
	public DateTimeConvesion()
	{		
	}
    public static DateTime getDateFromString(string dateString)
    {
        try
        {
            if (dateString != "")
            {
                string[] dateTimeArr = dateString.Split('/');
                string month = "";
                switch (dateTimeArr[1])
                {
                    case "01":
                        month = "JAN";
                        break;
                    case "02":
                        month = "FEB";
                        break;
                    case "03":
                        month = "MAR";
                        break;
                    case "04":
                        month = "APR";
                        break;
                    case "05":
                        month = "MAY";
                        break;
                    case "06":
                        month = "JUN";
                        break;
                    case "07":
                        month = "JUL";
                        break;
                    case "08":
                        month = "AUG";
                        break;
                    case "09":
                        month = "SEP";
                        break;
                    case "10":
                        month = "OCT";
                        break;
                    case "11":
                        month = "NOV";
                        break;
                    case "12":
                        month = "DEC";
                        break;
                }

                DateTime dt = Convert.ToDateTime(dateTimeArr[0] + "/" + month + "/" + dateTimeArr[2]);
                return dt;
            }
            else
            {
                return Date.getMinDate();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
