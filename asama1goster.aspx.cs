using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yazlabYam1
{
    public partial class asama1goster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = Session["icerik"].ToString();
           // Label1.Visible = false;
            string aranankelime2 = Session["aranan"].ToString();
            Label2.Text = "sonuc : " + Regex.Matches(Label1.Text.ToString().ToLower(), aranankelime2.ToLower()).Count;
        }
    }
}