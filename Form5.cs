using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_C_sharp
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f = new Form1();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string html = GetHtmlCode();
            string text_anterior = textBox1.Text;
            List<string> urls = GetUrls(html);
            var dist = 5;
            int i = 0, coloana = 5;


            foreach (var item in urls)
            {
                var pb = new PictureBox();
                SaveImage(item, "image" + i, ImageFormat.Png);

                if (coloana + 130 > panel1.Width)
                {
                    dist += 140;
                    coloana = 0;
                }
                pb.Load(item);
                pb.Size = new Size(130, 130);
                pb.Location = new Point(coloana, dist);
                pb.SizeMode = PictureBoxSizeMode.Normal;
                panel1.Controls.Add(pb);
                coloana += 140; i++;

                SaveImage(item, "image" + i, ImageFormat.Png);
            }

        }
        public void SaveImage(string imagelocation, string filename, ImageFormat format)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(imagelocation);
            Bitmap bitmap; bitmap = new Bitmap(stream);

            if (bitmap != null)
            {
                bitmap.Save(filename, format);
            }

            stream.Flush();
            stream.Close();
            client.Dispose();
        }
        private List<string> GetUrls(string html)
        {
            var urls = new List<string>();

            int adr = html.IndexOf("src=\"https", StringComparison.Ordinal);

            while (adr >= 0)
            {
                adr = html.IndexOf("\"", adr + 4, StringComparison.Ordinal);
                adr++;
                int ndx2 = html.IndexOf("\"", adr, StringComparison.Ordinal);
                string url = html.Substring(adr, ndx2 - adr);
                urls.Add(url);
                adr = html.IndexOf("src=\"https", ndx2, StringComparison.Ordinal);
            }
            return urls;
        }
        string GetHtmlCode()
        {
            if (textBox1.Text != "")
            {
                string url = "https://www.google.com/search?q=" + textBox1.Text + "&tbm=isch";
                string date = "";

                var request = (HttpWebRequest)WebRequest.Create(url);
                var rasp = (HttpWebResponse)request.GetResponse();

                using (Stream dataStream = rasp.GetResponseStream())
                {
                    if (dataStream == null)
                        return "";
                    using (var str = new StreamReader(dataStream))
                    {
                        date = str.ReadToEnd();
                    }
                }
                return date;
            }
            else
            {
                return "";
            }
        }
    }
}
