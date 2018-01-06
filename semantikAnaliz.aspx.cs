using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Word;
using WordApplication = Microsoft.Office.Interop.Word.Application;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using NUglify;

namespace yazlabYam1
{
    public partial class semantikAnaliz : System.Web.UI.Page
    {
        Microsoft.Office.Interop.Word.Application appWord;
        protected void Page_Load(object sender, EventArgs e)
        {
            ekle();
            
        }
        //private static string urlPattern = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        //private static string tagPattern = @"<a\b[^>]*(.*?)</a>";
        //private static string emailPattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";


        //List<string> deneme = new List<string>();

        //public static List<string> getInnerUrls(string url)
        //{
        //    List<string> innerUrls = new List<string>();
        //    WebRequest request = WebRequest.Create(url);
        //    StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream());
        //    string htmlCode = reader.ReadToEnd();

        //    List<string> links = getMatches(htmlCode);
        //    foreach (string link in links)
        //    {
        //        if (!Regex.IsMatch(link, urlPattern) && !Regex.IsMatch(link, emailPattern))
        //        {
        //            string absoluteUrlPath = getAblosuteUrl(getDomainName(url), link);
        //            innerUrls.Add(absoluteUrlPath);
        //        }
        //        else
        //        {
        //            innerUrls.Add(link);
        //        }
        //    }
        //    return innerUrls;
        //}

        //private static List<string> getMatches(string source)
        //{
        //    var matchesList = new List<string>();
        //    MatchCollection matches = Regex.Matches(source, tagPattern);
        //    foreach (Match match in matches)
        //    {
        //        string val = match.Value.Trim();
        //        if (val.Contains("href=\""))
        //        {
        //            string link = getSubstring(val, "href=\"", "\"");
        //            matchesList.Add(link);
        //        }
        //    }

        //    return matchesList;
        //}
        //private static string getSubstring(string source, string start, string end)
        //{
        //    int startIndex = source.IndexOf(start) + start.Length;
        //    int length = source.IndexOf(end, startIndex) - startIndex;
        //    return source.Substring(startIndex, length);
        //}
        //private static string getAblosuteUrl(string domainName, string path)
        //{  
            
        //        string absoluteUrl = "";
        //        if (domainName[domainName.Length - 1] == '/')
        //        {
        //            absoluteUrl += domainName;
        //        }
        //        else
        //        {
        //            absoluteUrl += domainName + "/";
        //        }

        //        if (path.Contains("../"))
        //        {
        //            string temp = domainName.Substring(0, domainName.LastIndexOf("/", 6));
        //            temp = temp.Substring(0, temp.LastIndexOf("/", 5));
        //            absoluteUrl = temp + path.Substring(3);
        //            return absoluteUrl;
        //        }
        //        if (path.Contains("./"))
        //        {
        //            string temp = domainName.Substring(0, domainName.LastIndexOf("/", 7));
        //            absoluteUrl = temp + path.Substring(4);
        //            return absoluteUrl;
        //        }
           
        //        if (path[0] == '/')
        //        {
        //            absoluteUrl += path.Substring(1);
        //            return absoluteUrl;
               
        //        }
        //        absoluteUrl += path;
            
           

        //    return absoluteUrl;
        //}
        //private static string getDomainName(string url)
        //{
        //    int length = url.IndexOf("/", 8);
        //    string domainName = url.Substring(0, length);
        //    return domainName;
        //}





        public TextBox[] tb = new TextBox[10]; // url txtbox
        public TextBox[] tb1 = new TextBox[10]; // kelime
        string degerlerUrl;
        string degerlerKelime;
       

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
                    Literal l1 = new Literal();
                    l1.Text = "<br/>";
                    Panel1.Controls.Add(l1);
                    uste_uzaklık += 30;

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button2.Visible = true;
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
                }
            }
            kelimeAra(urlIcerik, kelimeKumesi, urlKumesi);
            semantik(kelimeKumesi);
        }
        public void semantik(string[] kelimeKumesi)
        {
            for (int i = 0; i<kelimeKumesi.Length; i++)
            {
                object objNull = null;
                object objFalse = false;
                object objLanguage = Microsoft.Office.Interop.Word.WdLanguageID.wdTurkish;
                appWord = new Microsoft.Office.Interop.Word.Application();
                List<string> synonym = new List<string>();
                synonym.Clear();
                Microsoft.Office.Interop.Word.SynonymInfo si = appWord.get_SynonymInfo(kelimeKumesi[i], ref (objLanguage));


                // first find out how many meanings were found for word
                int iMeanings = (int)si.MeaningCount;

                if (iMeanings > 0)
                {
                    var strMeanings = si.MeaningList as Array;
                    if (strMeanings != null)
                        foreach (var strMeaning in strMeanings)
                        {
                            // get Synonym List for each meaning... note that
                            // get_SynonymList takes an object ref, thus we
                            // must create objMeaning object
                            var objMeaning = strMeaning;

                            var aSynonyms = si.SynonymList[objMeaning];

                            var strSynonyms = si.SynonymList[objMeaning] as Array;
                            if (strSynonyms != null)
                                foreach (string strSynonym in strSynonyms)
                                {
                                    // loop over each synonym in ArrayList
                                    // and add to lbSynonym ListBox
                                    synonym.Add(strSynonym);
}

                            for (int s = 0; s <synonym.ToArray().Count(); s++)
                            {
                                Label4.Text = synonym[s].ToString();
                            }

                        }
                }
                else
                {
                    // no meanings/synonyms found... set ListBox value to "NONE"
                    synonym.Add("NONE");
                }
                // Clean up COM object

                // quit WINWORD app
                appWord.Quit(ref objFalse, ref objNull, ref objNull);
            }
           
        }
        public void kelimeAra(string[] urlIcerik, string[] kelimeKumesi, string[] urlKumesi)
        {

            double[] sonuclar = new double[(urlIcerik.Length - 1) * (kelimeKumesi.Length - 1)];
            for (int i = 0; i <= urlIcerik.Length - 1; i++)
            {
                if (urlIcerik[i] != null)
                {
                    for (int j = 0; j <= kelimeKumesi.Length - 1; j++)
                    {
                        sonuclar[i] = Regex.Matches(urlIcerik[i].ToString().ToLower(), kelimeKumesi[j].ToString().ToLower()).Count;

                    }
                }
            }
            skorhesapla(sonuclar, kelimeKumesi.Length, urlIcerik.Length, urlKumesi);
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
            for (int i = 0; i <= skorurlsayisi - 1; i++)
            {
                for (int k = 0; k <= skorkelimesayisi - 1; k++)
                {
                    adeger += skordizi[k];
                }
                ddeger = adeger / skorkelimesayisi;
                ytoplamdizi[i] = ddeger;
                ddeger = 0;
            }

            for (int j = 0; j < skorurlsayisi - 1; j++)
            {
                for (int i = 0; i <= skorkelimesayisi - 1; i++)
                {
                    cdeger = ytoplamdizi[j] - skordizi[i];
                    ycdeger += Math.Pow(cdeger, 2);
                    cdeger = 0;
                }
                ycdeger2 = ycdeger / (skorkelimesayisi - 1);
                yckıkartmadizi[j] = 0;
                yckıkartmadizi[j] = Math.Sqrt(ycdeger2);
                ycdeger2 = 0;
                string link2 = urlKumesi[j];
                string link = UrliLinkeCevir(link2);
                //sonuclar2[i] = deger / (kelimeKumesi.Length - 1);



                //for (int s = 0; s < 3; s++)
                //{
                //    // List<string> deneme = new List<string>();
                //    deneme = getInnerUrls(urlKumesi[j]);
                //}

                //Label4.Text += link + "   " + yckıkartmadizi[j] + "   " + deneme[0].ToString() + "    " + deneme[1].ToString() + "    " + deneme[2].ToString() + "<br/>";
            }
        }

        private string UrliLinkeCevir(string link)
        {
            string regex = @"((www\.|(http|https|ftp|news|file)+\:\/\/)[&#95;.a-z0-9-]+\.[a-z0-9\/&#95;:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])";
            Regex r = new Regex(regex, RegexOptions.IgnoreCase);
            return r.Replace(link, "<a href=\"$1\" title=\"Click to open in a new window or tab\" target=\"&#95;blank\">$1</a>").Replace("href=\"www", "href=\"http://www");
        }
    }
}