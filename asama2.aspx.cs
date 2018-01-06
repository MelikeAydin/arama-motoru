using NUglify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yazlabYam1
{
    public partial class About : Page
    {
        public TextBox[] tb = new TextBox[10]; // url txtbox
        public TextBox[] tb1 = new TextBox[10]; // kelime
        string degerlerUrl;
        string degerlerKelime;
        protected void Page_Load(object sender, EventArgs e)
        {
            ekle();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button2.Visible = true;
        }
        public void ekle()
        {

            if (TextBox1.Text != "")
            {
                int sayi = int.Parse(TextBox1.Text);
                int uste_uzaklık = 130;
                //Panel1.Controls.Clear();
                for (int i = 0; i < sayi; i++)
                {
                    tb[i] = new TextBox();
                    tb[i].ID = "tb_" + i.ToString();
                    tb[i].Style["Position"] = "Absolute";
                    tb[i].Style["Top"] = uste_uzaklık.ToString() + "px";
                    tb[i].Style["left"] = "180px";
                    Panel1.Controls.Add(tb[i]);
                    Literal l = new Literal();
                    l.Text = "<br/>";
                    Panel1.Controls.Add(l);
                    uste_uzaklık += 30;

                }
            }

            if (TextBox2.Text != "")
            {
                int sayi = int.Parse(TextBox2.Text);
                int uste_uzaklık = 130;
                for (int i = 0; i < sayi; i++)
                {
                    tb1[i] = new TextBox();
                    tb1[i].ID = "tb1_" + i.ToString();
                    tb1[i].Style["Position"] = "Absolute";
                    tb1[i].Style["Top"] = uste_uzaklık.ToString() + "px";
                    tb1[i].Style["left"] = "494px";
                    Panel1.Controls.Add(tb1[i]);
                    Literal l1= new Literal();
                    l1.Text = "<br/>";
                    Panel1.Controls.Add(l1);
                    uste_uzaklık += 30;

                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int urlSayi = int.Parse(TextBox1.Text);
            int kelimeSayi = int.Parse(TextBox2.Text);

            for (int i = 0; i < urlSayi; i++)
            {
                degerlerUrl += tb[i].Text + " ";
            }
            string[] urlKumesi = degerlerUrl.Split(' ');

            for (int i = 0; i < kelimeSayi; i++)
            {
                degerlerKelime += tb1[i].Text + " ";
            }
            string[] kelimeKumesi = degerlerKelime.Split(' ');
            icerikAl(urlKumesi, kelimeKumesi);
        }
        public void icerikAl(string[] urlKumesi, string[] kelimeKumesi)
        {
            string[] urlIcerik = new string[urlKumesi.Length];
            for (int i = 0; i < urlKumesi.Length - 1; i++)
            {
                if (urlKumesi[i] != null)
                {
                    WebRequest req = HttpWebRequest.Create(urlKumesi[i].ToString());
                    WebResponse res;
                    res = req.GetResponse();
                    StreamReader data = new StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
                    string icerik = data.ReadToEnd().ToString();
                    var result = Uglify.HtmlToText(icerik);
                    string icerik2 = result.Code;
                    urlIcerik[i] = icerik2;
                    //Label4.Text += urlIcerik[i] + "<br/>"+ "<br/>"+ "<br/>"; 
                }
            }
            kelimeAra(urlIcerik, kelimeKumesi, urlKumesi);
        }
        public void kelimeAra(string[] urlIcerik, string[] kelimeKumesi, string[] urlKumesi)
        {

            double[] sonuclar = new double[(urlIcerik.Length - 1) * (kelimeKumesi.Length - 1)];
            //int[] sonuclar = new int[(urlIcerik.Length-1) * (kelimeKumesi.Length-1)];
            for (int i = 0; i <= urlIcerik.Length - 1; i++)
            {
                if (urlIcerik[i] != null) { 
                for (int j = 0; j <= kelimeKumesi.Length -1; j++)
                {
                    sonuclar[i] = Regex.Matches(urlIcerik[i].ToString().ToLower(), kelimeKumesi[j].ToString().ToLower()).Count;

                }
                }
            }
            skorhesapla(sonuclar, kelimeKumesi.Length , urlIcerik.Length , urlKumesi);
        }


        public void skorhesapla(double[] skordizi, int skorkelimesayisi, int skorurlsayisi, string[] urlKumesi)
        {
            double[] ytoplamdizi = new double[skorurlsayisi];
            double[] yckıkartmadizi = new double[skorurlsayisi];
            double[] ysayılar = new double[skorurlsayisi];

            double adeger = 0;
            double cdeger = 0;
            double ddeger = 0;
            double ycdeger = 0;
            double ycdeger2 = 0;
            for (int i = 0; i <= skorurlsayisi-1; i++)
            {
                for (int k = 0; k <= skorkelimesayisi-1; k++)
                {
                    adeger += skordizi[k];
                }
                ddeger = adeger / skorkelimesayisi;
                ytoplamdizi[i] = ddeger;
                ddeger = 0;
            }

            for (int j = 0; j < skorurlsayisi-1; j++)
            {
                for (int i = 0; i <= skorkelimesayisi-1; i++)
                {
                    cdeger = ytoplamdizi[j] - skordizi[i];
                    ycdeger += Math.Pow(cdeger, 2);
                    cdeger = 0;
                }
                ycdeger2 = ycdeger / (skorkelimesayisi - 1);
                yckıkartmadizi[j] = 0;
                yckıkartmadizi[j] = Math.Sqrt(ycdeger2);
                ycdeger2 = 0;
             
                Label4.Text += "Skor : " + yckıkartmadizi[j]+"---"+urlKumesi[j].ToString() + "<br/>";
            }
        }
    }
}