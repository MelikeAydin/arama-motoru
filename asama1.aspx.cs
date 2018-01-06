using NUglify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yazlabYam1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
          
            WebRequest req = HttpWebRequest.Create(TextBox1.Text);
            WebResponse res;

            res = req.GetResponse();
            StreamReader data = new StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
            string icerik = data.ReadToEnd().ToString();
            var result = Uglify.HtmlToText(icerik);
            string icerik2 = result.Code;

            string arananKelime = TextBox2.Text;
         
            Label3.Text = icerik2 + "<br/>" + "sonuc : " + Regex.Matches(icerik2.ToString().ToLower(), arananKelime.ToLower()).Count;

        }
    }
}
