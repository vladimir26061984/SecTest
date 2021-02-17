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
        bool SuperUser = false;

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
            button1.Enabled = (textBox1.Text.Length > 0) && (textBox2.Text.Length > 0) && (textBox3.Text.Length > 0) && (textBox4.Text.Length > 0) && (textBox5.Text.Length > 0);
            if (button1.Enabled && SuperUser)
                button1.BackColor = Color.FromArgb(150, Color.Gold);
            else if (button1.Enabled && !SuperUser)
                button1.BackColor = Color.Transparent;
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
            
            FrmTest.Call(path, comboBox1.SelectedItem.ToString(), textBox1.Text + " " + textBox2.Text + " " + textBox3.Text, textBox4.Text, textBox5.Text, maxQCount, dateTimePicker1.Value.ToString(), SuperUser);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!LoadData())
                Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics grx = label9.CreateGraphics();
            SizeF SSS1 = grx.MeasureString(label9.Text.Substring(0, 1),label9.Font);
            SizeF SSS2 = grx.MeasureString(label9.Text.Substring(1, 2), label9.Font);
            if (e.Location.X > SSS1.Width && e.Location.X < SSS2.Height)
            {
                SuperUser = !SuperUser;
                if (button1.Enabled && SuperUser)
                    button1.BackColor = Color.FromArgb(150, Color.Gold);
                else if (button1.Enabled && !SuperUser)
                    button1.BackColor = Color.Transparent;
            }
        }
    }
}
