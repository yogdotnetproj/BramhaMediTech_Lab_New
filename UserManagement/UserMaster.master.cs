using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using DAL;
using System.Text;

public partial class MasterPage : System.Web.UI.MasterPage
{
    clsDbDatabase ObjDBCon = new clsDbDatabase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
        }
        GetMenuITem();
    }
   
    private void GetMenuITem()
    {
        // objDBCon.OpenConnection(Data Source=dip-PC\sqlexpress;Initial Catalog=RealEstate_20;Integrated Security=True
       // string conn = @"Data Source=dip-PC\sqlexpress;Initial Catalog=RealEstate_20;Integrated Security=True";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
      // objDBCon.OpenConnection();
        SqlDataAdapter da=new SqlDataAdapter ("SP_phmnuds",conn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ds.Relations.Add("childRows", ds.Tables[0].Columns["MenuID"], ds.Tables[1].Columns["MenuID"]);
        foreach (DataRow level1dataRow in ds.Tables[0].Rows)
        {
            MenuItem item = new MenuItem();
            item.Text = level1dataRow["MenuName"].ToString();
            item.NavigateUrl = level1dataRow["NavigateURL"].ToString();



            DataRow[] level2DataRows = level1dataRow.GetChildRows("childRows");
            foreach (DataRow level2DataRow in level2DataRows)
            {

                MenuItem ChildItem = new MenuItem();
                ChildItem.Text = level2DataRow["SubMenuName"].ToString();
                ChildItem.NavigateUrl = level2DataRow["SubMenuNavigateURL"].ToString();
                item.ChildItems.Add(ChildItem);
            }
            RealMenu.Items.Add(item);
        }

    }
}
