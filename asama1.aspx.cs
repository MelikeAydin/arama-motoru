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

            //char[] diziTurkce = new char[6];
            //diziTurkce[0] = 'ı';
            //diziTurkce[1] = 'ö';
            //diziTurkce[2] = 'ü';
            //diziTurkce[3] = 'ş';
            //diziTurkce[4] = 'ğ';
            //diziTurkce[5] = 'ç';
            //char[] diziIngilizce = new char[6];
            //diziIngilizce[0] = 'i';
            //diziIngilizce[1] = 'o';
            //diziIngilizce[2] = 'u';
            //diziIngilizce[3] = 's';
            //diziIngilizce[4] = 'g';
            //diziIngilizce[5] = 'c';
            //char[] harfler = arananKelime.ToCharArray();
            //for(int i=0; i<harfler.Length; i++)
            //{
            //    if (harfler[i] == diziTurkce[0])
            //        harfler[i] = diziIngilizce[0];
            //   else if (harfler[i] == diziTurkce[1])
            //        harfler[i] = diziIngilizce[1];
            //    else if (harfler[i] == diziTurkce[2])
            //        harfler[i] = diziIngilizce[2];
            //   else  if (harfler[i] == diziTurkce[3])
            //        harfler[i] = diziIngilizce[3];
            //  else  if (harfler[i] == diziTurkce[4])
            //        harfler[i] = diziIngilizce[4];
            //  else  if (harfler[i] == diziTurkce[5])
            //        harfler[i] = diziIngilizce[5];
            //   else if (harfler[i] == diziIngilizce[0])
            //        harfler[i] = diziTurkce[0];
            //    else if (harfler[i] == diziIngilizce[1])
            //        harfler[i] = diziTurkce[1];
            //    else if (harfler[i] == diziIngilizce[2])
            //        harfler[i] = diziTurkce[2];
            //    else if (harfler[i] == diziIngilizce[3])
            //        harfler[i] = diziTurkce[3];
            //    else if (harfler[i] == diziIngilizce[4])
            //        harfler[i] = diziTurkce[4];
            //    else if (harfler[i] == diziIngilizce[5])
            //        harfler[i] = diziTurkce[5];
            //}
            //string yeni = new string(harfler);
            //arananKelime = yeni;
         
            Label3.Text = icerik2 + "<br/>" + "sonuc : " + Regex.Matches(icerik2.ToString().ToLower(), arananKelime.ToLower()).Count;

        }
    }
}