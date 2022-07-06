using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_C_sharp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] listadate = System.IO.File.ReadAllLines(@"C:\Users\Victor\Desktop\C#\Task-uriCompletate\bin\Debug\FisierTask-uri.txt");

            label3.Text = listadate[0] + " " + listadate[1];
            label4.Text = listadate[2];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
