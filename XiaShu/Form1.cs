using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XiaShu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            var html = await GetString("https://www.biquxsw.com/126_126270/");

            var matchcs = Regex.Matches(html, "<dd><a href='(?<url>.*?)'>.*?</dd>");

            var list = new List<string>();
            foreach (Match m in matchcs)
            {
                var url = m.Groups["url"].Value;
                list.Add($"https://www.biquxsw.com/126_126270/{url}");
            }

            list = list.Skip(835).ToList();
            var sb = new StringBuilder();
            foreach (var url in list)
            {
                Debug.WriteLine(url);
                html = await GetString(url);
                var tMathc = Regex.Match(html, "<title>(?<t>.*?)_混在大唐的工科宅男_笔趣阁</title>");
                var contentMatch = Regex.Match(html, "<div id=\"content\" name=\"content\">(?<c>[\\w\\W]+?)</div>");
                sb.AppendLine(tMathc.Groups["t"].Value);
                var content = contentMatch.Groups["c"].Value;
                content = content.Replace("\"", "").Replace("&nbsp;", "").Replace("<br />", "").Replace("一秒记住【笔趣阁 www.biquxsw.com】，精彩小说无弹窗免费阅读", "");
                sb.AppendLine(content);
            }
            File.AppendAllText("d:\\2.txt", sb.ToString());
            MessageBox.Show("Ok");
        }


        private async Task<string> GetString(string url)
        {
            var client = new HttpClient();

            var stream = await client.GetStreamAsync(url);
            using (var gstream = new GZipStream(stream, CompressionMode.Decompress))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("gbk")))
                {
                    return reader.ReadToEnd();
                }
            }

        }

        private string GetString1(string url)
        {
            var request = WebRequest.Create(url);
            var resp = request.GetResponse();
            var stream = resp.GetResponseStream();
            using (var gstream = new GZipStream(stream, CompressionMode.Decompress))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("gbk")))
                {
                    return reader.ReadToEnd();
                }
            }

        }

        private void SaveLog(string html)
        {
            using (var s = new StreamWriter("d:\\1.txt"))
            {
                s.WriteLine(html);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var html = await GetString("https://www.52bqg.com/book_91371/https://www.52bqg.com/book_91371/");

            var matchcs = Regex.Matches(html, "<dd><a href=['\"](?<url>.*?)['\"].*?</dd>");

            var list = new List<string>();
            foreach (Match m in matchcs)
            {
                var url = m.Groups["url"].Value;
                list.Add($"https://www.52bqg.com/book_91371/{url}");
            }

            list = list.Skip(list.Count - 5).ToList();
            var sb = new StringBuilder();
            foreach (var url in list)
            {
                Debug.WriteLine(url);
                html = await GetString(url);
                var tMathc = Regex.Match(html, "<title>(?<t>.*?)_都市最强修真学生_笔趣阁</title>");
                var contentMatch = Regex.Match(html, "<div id=\"content\" name=\"content\">(?<c>[\\w\\W]+?)</div>");
                sb.AppendLine(tMathc.Groups["t"].Value);
                var content = contentMatch.Groups["c"].Value;
                content = content.Replace("\"", "").Replace("&nbsp;", "").Replace("<br />", "").Replace("一秒记住【笔趣阁 www.52bqg.com】，精彩小说无弹窗免费阅读", "");
                sb.AppendLine(content);
            }
            File.AppendAllText("d:\\都市最强修真学生.txt", sb.ToString());
            MessageBox.Show("Ok");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var html1 = await GetString("https://www.236n.cn/7_7363/");

            var matchcs = Regex.Matches(html1, "<dd><a href=['\"](?<url>.*?)['\"].*?</dd>");

            var dict = new Dictionary<int, string>();
            var i = 0;
            foreach (Match m in matchcs)
            {
                var url = m.Groups["url"].Value;
                dict.Add(i++, $"https://www.236n.cn/{url}");
            }
            var list = dict.Select(t => new { t.Key, t.Value }).Skip(9).ToList();



            Dictionary<int, string> st = new Dictionary<int, string>();

           list.AsParallel().ForAll((s) =>
           {
               var d = s;
               var url = s.Value;
               var html = GetString1(url);
               var tMathc = Regex.Match(html, "<title>(?<t>.*?)_都市全能奶爸_笔趣阁</title>");
               var contentMatch = Regex.Match(html, "<div id=\"content\">(?<c>[\\w\\W]+?)</div>");
               var sb = new StringBuilder();
               sb.AppendLine(tMathc.Groups["t"].Value);
               var content = contentMatch.Groups["c"].Value;
               content = content.Replace("\"", "").Replace("&nbsp;", "").Replace("<br />", "").Replace("顶点手机站 m.236n.cn", "");
               sb.AppendLine(content);
               st.Add(d.Key, sb.ToString());
           });


            var con = new StringBuilder();
            foreach (var t in st.AsEnumerable().OrderBy(t => t.Key))
            {
                con.AppendLine(t.Value);
            }
            File.AppendAllText("d:\\都市全能奶爸.txt", con.ToString());
            MessageBox.Show("Ok");
        }
    }
}
