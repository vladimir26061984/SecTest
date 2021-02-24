﻿using System;
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
        /// <summary>
        /// признак суперрежима
        /// </summary>
        bool SuperUser = false;
        object datetime;

        public static List<int> CriteriaList;

        public Form1()
        {
            CriteriaList = new List<int>();
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            

            //textBox1.Text = "Бублий";
            //textBox2.Text = "Тимур";
            //textBox3.Text = "Заурович";
            //textBox4.Text = "Отряд \"Грушовая\"";
            //textBox5.Text = "Специалист";

            //UpdateStart();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        void UpdateStart()
        {
            button1.Enabled = (textBox1.Text.Length > 0) && (textBox2.Text.Length > 0) && (textBox3.Text.Length > 0) && (textBox4.Text.Length > 0) && (textBox5.Text.Length > 0);
            if (button1.Enabled && SuperUser)
                button1.BackColor = Color.FromArgb(150, Color.Gold);
            else if (button1.Enabled && !SuperUser)
                button1.BackColor = Color.Transparent;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateStart();
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
            CriteriaList.Clear();
            int OkValue = maxQCount;
            string strParse = "";
            string Error = "";
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
                            strParse = sText.Replace("[" + comboBox1.SelectedItem.ToString() + "]", "");
                            string[] strParseArr = strParse.Split(";".ToArray());
                            if (int.TryParse(strParseArr[0], out maxQCount))
                                OkValue = maxQCount;
                            else
                                Error = "Ошибка при получениеи количества вопросов в тесте";
                            if (int.TryParse(strParseArr[1], out OkValue))
                                CriteriaList.Add(OkValue);
                            else
                            {
                                if (Error.Length > 0)
                                    Error += Environment.NewLine + "Ошибка при получении количества правильных ответов в тесте";
                                else
                                    Error = "Ошибка при получении количества правильных ответов в тесте";
                                CriteriaList.Add(maxQCount);
                            }
                            break;
                        }
                    } while (sText != null);
                }
            }
            catch (Exception exc)
            { 
            
            }
            
            
            if (Error.Length > 0)
                if (MessageBox.Show(Error + Environment.NewLine + "Если нажмете 'Да', то программа продолжиться со стандартными настройками", "Ошибка при чтении файла настройки для выбранной дисциплины", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }

            var strArr = dateTimePicker1.Value.Date.ToString().Split();
            var strDT_test = strArr[0] + " " + DateTime.Now.ToLongTimeString();
            FrmTest.Call(path, comboBox1.SelectedItem.ToString(), textBox1.Text + " " + textBox2.Text + " " + textBox3.Text, textBox4.Text, textBox5.Text, maxQCount, strDT_test, SuperUser);
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
