using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "Data");
            string[] sss = System.IO.Directory.GetFiles(path,"*.xml");
            if (sss.Length > 0)
            {
                for (int i = 0; i < sss.Length; i++)
                {
                    comboBox1.Items.Add(System.IO.Path.GetFileNameWithoutExtension(sss[i]).Replace(".xml",""));
                }
                
                res = true;
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Не найдено файлов с тестами.", "Программа будет закрыта", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

           

            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "Data", comboBox1.SelectedItem.ToString() + ".xml") ;
            int maxQCount = 10;
            try
            {
                using (StreamReader readtext = new StreamReader(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "Data", "SecurityTest.conf")))
                {
                    string sText = "";
                    do
                    {
                        sText = readtext.ReadLine();
                        if (sText.IndexOf(comboBox1.SelectedItem.ToString()) > 0)
                        {
                            maxQCount = int.Parse(sText.Replace("[" + comboBox1.SelectedItem.ToString() + "]", ""));
                            break;
                        }
                    } while (sText != null);
                }
            }
            catch (Exception exc)
            { 
            
            }
            FrmTest.Call(path, comboBox1.SelectedItem.ToString(), textBox1.Text + " " + textBox2.Text + " " + textBox3.Text, 4, dateTimePicker1.Value.ToString());
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

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!LoadData())
                Close();
        }

     
    }
}
