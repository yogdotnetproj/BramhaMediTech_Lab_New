using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public class TextModify
{
	public TextModify()
	{
		//
		// TODO: Add constructor logic here
		//
	}
        public static string splitText(string txt,int indexToSplit)
    {
        if (txt.Length > indexToSplit)
        {
            int no = txt.Length / indexToSplit;
            for (int i = 1; i <= no; i++)
            {
                txt = txt.Insert((i * indexToSplit), " ");
            }
        }
        return txt;
    }
}
