using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public class BasePage :System.Web.UI.Page
{
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        ApplyGridViewStyle(this.Controls); // One place, all GridViews
       
    }

    private void ApplyGridViewStyle(ControlCollection controls)
    {
        for (int i = 0; i < controls.Count; i++)
        {
            Control control = controls[i];
            if (control is Button)
            {
                Button btn = (Button)control;
                if (btn.Text.ToLower() == "click")
                {
                    btn.Text = "Search";
                }
            }

            if (control is GridView)
            {
                GridView gv = (GridView)control;
              
                gv.CssClass = "table table-striped table-bordered table-hover custom-gridview";
                // Optional: remove old styles to prevent conflicts
              
                gv.HeaderStyle.CssClass = "table-dark";              
                gv.RowStyle.Reset();
                gv.FooterStyle.CssClass = "custom-footer-style";
                gv.AlternatingRowStyle.Reset();
                gv.PagerStyle.CssClass = "custom-footer-style";
                gv.SelectedRowStyle.Reset();
                DisableHeaderBackcolorRow(gv);
            }
          if(control is TextBox)
            {
                TextBox txt = (TextBox)control;
                txt.BorderColor = System.Drawing.Color.Empty;
                txt.CssClass = "form-control";

               
            }
            else if (control.HasControls())
            {
                ApplyGridViewStyle(control.Controls);
            }
        }
    }
    private void DisableHeaderBackcolorRow(GridView gv)
    {
        if (gv.Controls.Count > 0 && gv.Controls[0] is Table)
        {
            Table table = (Table)gv.Controls[0];
            if (table.Rows.Count > 0)
            {
                GridViewRow maybeCustomRow =(GridViewRow) table.Rows[0];
                if (maybeCustomRow.RowType == DataControlRowType.Header &&
                    maybeCustomRow.Cells.Count == 1 && maybeCustomRow.Cells[0].ColumnSpan > 2 &&
                    maybeCustomRow.Cells[0].Text.Length>0)
                {
                    table.Rows.RemoveAt(0);
                }
            }
        }

    }


   
}
