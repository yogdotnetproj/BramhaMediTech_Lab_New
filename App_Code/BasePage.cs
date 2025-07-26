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
            }
            else if (control.HasControls())
            {
                ApplyGridViewStyle(control.Controls);
            }
        }
    }
}
