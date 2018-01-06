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
    public partial class asama2goster : System.Web.UI.Page
    {
        //string[] urlIcerik = new string[10];
        int[] kelimesayisi = new int[10];
        protected void Page_Load(object sender, EventArgs e)
        {
            #region atamalar
            string url1 = Session["url1"].ToString();
            string url2 = Session["url2"].ToString();
            string url3 = Session["url3"].ToString();
            string url4 = Session["url4"].ToString();
            string url5 = Session["url5"].ToString();
            string url6 = Session["url6"].ToString();
            string url7 = Session["url7"].ToString();
            string url8 = Session["url8"].ToString();
            string url9 = Session["url9"].ToString();
            string url10 = Session["url10"].ToString();

            string kelime1 = Session["kelime1"].ToString();
            string kelime2 = Session["kelime2"].ToString();
            string kelime3 = Session["kelime3"].ToString();
            string kelime4 = Session["kelime4"].ToString();
            string kelime5 = Session["kelime5"].ToString();
            string kelime6 = Session["kelime6"].ToString();
            string kelime7 = Session["kelime7"].ToString();
            string kelime8 = Session["kelime8"].ToString();
            string kelime9 = Session["kelime9"].ToString();
            string kelime10 = Session["kelime10"].ToString();
            #endregion
            #region dizi atamaları
            string[] urlDizisi = new string[10];
            string[] kelimeDizisi = new string[10];

            urlDizisi[0] = url1;
            urlDizisi[1] = url2;
            urlDizisi[2] = url3;
            urlDizisi[3] = url4;
            urlDizisi[4] = url5;
            urlDizisi[5] = url6;
            urlDizisi[6] = url7;
            urlDizisi[7] = url8;
            urlDizisi[8] = url9;
            urlDizisi[9] = url10;


            kelimeDizisi[0] = kelime1;
            kelimeDizisi[1] = kelime2;
            kelimeDizisi[2] = kelime3;
            kelimeDizisi[3] = kelime4;
            kelimeDizisi[4] = kelime5;
            kelimeDizisi[5] = kelime6;
            kelimeDizisi[6] = kelime7;
            kelimeDizisi[7] = kelime8;
            kelimeDizisi[8] = kelime9;
            kelimeDizisi[9] = kelime10;
            #endregion
            icerikAl(urlDizisi, kelimeDizisi);
        }
        public void icerikAl(string[] urlDizisi, string[] kelimeDizisi)
        {
            string[] urlIcerik = new string[10];
            for (int i = 0; i < urlDizisi.Length; i++)
            {
                if (urlDizisi[i] == "")
                {
                    break;
                }
                WebRequest req = HttpWebRequest.Create(urlDizisi[i]);
                WebResponse res;
                res = req.GetResponse();
                StreamReader data = new StreamReader(res.GetResponseStream(), System.Text.Encoding.GetEncoding("windows-1254"));
                string icerik = data.ReadToEnd().ToString();
                var result = Uglify.HtmlToText(icerik);
                string icerik2 = result.Code;
                urlIcerik[i] = icerik2;

                icerik2 = "";
            }
            kelimeAra0(urlIcerik, kelimeDizisi);
            //int kelimeSayisi = kelimeAra(urlIcerik, kelimeDizisi);
            //return kelimeSayisi;
            //Label1.Text = urlIcerik[1];
        }
        public int kelimeAra(string[] urlIcerik, string[] kelimeDizisi)
        {
            int kelimeSayisi = 0;
            for (int i = 0; i < urlIcerik.Length; i++)
            {
                if (urlIcerik[i] == "")
                {
                    break;
                }
                kelimeSayisi = Regex.Matches(urlIcerik[i].ToString().ToLower(), kelimeDizisi[i].ToLower()).Count;
            }
            return kelimeSayisi;
        }

        public void kelimeAra0(string[] urlIcerik, string[] kelimeDizisi)
        {
            for (int i = 0; i < kelimeDizisi.Length; i++)
            {
                if (kelimeDizisi[i] == "")
                {
                    break;
                }
                int sayac = Regex.Matches(urlIcerik[0].ToString().ToLower(), kelimeDizisi[i].ToLower()).Count;
                kelimesayisi[i] = sayac;
                sayac = 0;
                //foreach(Match match in Regex)
            }
            skorBul(kelimesayisi);



        }
        public void skorBul(int[] skorBuldizisi)
        {

            int sayitut = kelimesayisi.Length;
            switch (sayitut)
            {
                case 10:
                    {
                        double deger1 = Math.Abs(skorBuldizisi[0] - skorBuldizisi[1]);
                        double deger2 = Math.Abs(skorBuldizisi[0] - skorBuldizisi[2]);
                        double deger3 = Math.Abs(skorBuldizisi[1] - skorBuldizisi[2]);
                        double toplam = deger1 + deger2 + deger3;
                        Label2.Text = deger1 + "               " + deger2 + "        " + deger3+ " skor : " + toplam;
                        //double skor = toplam / 20;
                        //Label2.Text = skorBuldizisi[0].ToString() + "               " + skorBuldizisi[1].ToString() + "        " + skorBuldizisi[2].ToString() + " skor : " + skor;

                        break;
                    }

                    

            }
        }
    }
}

        
    