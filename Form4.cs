using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_C_sharp
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public static string date = "";
        public class universitati
        {
            public string[] web_pages { get; set; }

            public string name { get; set; }
            public string country { get; set; }
            public string alpha_two_code { get; set; }
        }

        public class unis
        {
            public List<universitati> list { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var http = new HttpClient();
            var link = new Uri("http://universities.hipolabs.com/search?name=middle");
            var rez = http.GetAsync(link).Result;
            var json = rez.Content.ReadAsStringAsync().Result;
            Console.WriteLine(json);
            var datejson = JsonConvert.DeserializeObject<List<universitati>>(json);

            var dist = 5; var inaltime = 5;

            if (rez != null)
            {
                chart1.Titles.Add("Numarul facultatilor din fiecare tara: ");
                string header = "|        Name:         |              Country                |            Alpha Two Code             |                      Webpage                     |";
                var csv = new StringBuilder();
                foreach (var item in datejson)
                {
                    var lbl1 = new Label();
                    var lbl2 = new Label();
                    var lbl3 = new Label();
                    var lbl4 = new Label();

                    lbl1.Click += (s, p) =>
                    {
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            string web = item.web_pages[0];
                            web = web.Replace("&", "^&");
                            Process.Start(new ProcessStartInfo("cmd", $"/c start {web}") { CreateNoWindow = true });
                        }
                    };
                    lbl2.Click += (s, p) =>
                    {
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            string web = item.web_pages[0];
                            web = web.Replace("&", "^&");
                            Process.Start(new ProcessStartInfo("cmd", $"/c start {web}") { CreateNoWindow = true });
                        }
                    };
                    lbl3.Click += (s, p) =>
                    {
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            string web = item.web_pages[0];
                            web = web.Replace("&", "^&");
                            Process.Start(new ProcessStartInfo("cmd", $"/c start {web}") { CreateNoWindow = true });
                        }
                    };


                    panel1.Controls.Add(lbl1);
                    panel1.Controls.Add(lbl2);
                    panel1.Controls.Add(lbl3);
                    panel1.Controls.Add(lbl4);

                    lbl1.AutoSize = true;
                    lbl1.BackColor = Color.AntiqueWhite;
                    lbl1.Text = item.name.ToString();
                    lbl1.Location = new Point(dist + 5, inaltime + 10);
                    string name = lbl1.Text;

                    lbl2.AutoSize = true;
                    lbl2.BackColor = Color.WhiteSmoke;
                    lbl2.Text = item.country.ToString();
                    lbl2.Location = new Point(dist + 350, inaltime + 10);
                    string country = lbl2.Text;


                    lbl3.AutoSize = true;
                    lbl3.BackColor = Color.FloralWhite;
                    lbl3.Text = item.alpha_two_code.ToString();
                    lbl3.Location = new Point(dist + 600, inaltime + 10);
                    string alpha_two_code = lbl3.Text;


                    lbl4.AutoSize = true;
                    lbl4.BackColor = Color.White;
                    lbl4.ForeColor = Color.White;
                    lbl4.Text = "Site-ul universitatii este: " + item.web_pages[0].ToString();
                    lbl4.Location = new Point(dist + 5, inaltime + 5);
                    string web_pages = lbl4.Text;


                    panel1.BackColor = Color.White;
                    inaltime += 35;
                    string linie = string.Format("{0}\n,{1},{2},{3},{4}\n", header, name, country, alpha_two_code, web_pages);
                    csv.AppendLine(linie);
                    File.WriteAllText(@"C:\Users\Victor\Desktop\datele.csv", csv.ToString());


                }

            }
            else
            {
                string alerta = "Nu exista date!";
                string linie = string.Format("{0}}\n", alerta);
            }

            string[] arrytoate = new string[datejson.Count];
            string[] arryscurt = new string[datejson.Count];

            for (int i = 0; i < datejson.Count; i++)
            {
                arrytoate[i] = datejson[i].country;
            }

            arryscurt = arrytoate.Distinct().ToArray();

            for (int i = 0; i < arryscurt.Length; i++)
            {
                int count = arrytoate.Count(s => s == arryscurt[i]);
                chart1.Series[0].Points.AddXY(arryscurt[i].ToString(), count);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
