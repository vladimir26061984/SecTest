using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SecurityTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = (textBox1.Text.Length > 0) && (textBox2.Text.Length > 0) && (textBox3.Text.Length > 0);
        }

        private bool LoadData()
        {
            bool res = false;

            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    string tablename = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
            //    ttt = new DataTable(tablename);
            //    ttt.ReadXmlSchema(ofd.FileName);
            //    ttt.ReadXml(ofd.FileName);
            //    LIST.Clear();
            //    foreach (DataRow RRR in ttt.Rows)
            //    {
            //        NextOb();
            //        Curent.Num = int.Parse(RRR["Num"].ToString());
            //        Curent.Text = RRR["Text"].ToString();
            //        Curent.setAnswer(RRR["Answer"].ToString());
            //        Curent.ValidAnswer = int.Parse(RRR["Valid"].ToString());
            //        LIST.Add(Curent);
            //    }

            //}
        }
    }
}
