using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestCreator
{
    public partial class FormCreate : Form
    {
        List<SecurityTest.Question> LIST = new List<SecurityTest.Question>();
        SecurityTest.Question Curent;

        DataTable ttt;
        SaveFileDialog sfd = new SaveFileDialog();
        OpenFileDialog ofd = new OpenFileDialog();

        private void NextOb()
        {
            Curent = new SecurityTest.Question();
            Curent.Num = LIST.Count + 1;
            label6.Text = Curent.Num.ToString();
        }

        public FormCreate()
        {
            InitializeComponent();
            sfd.InitialDirectory = Application.ExecutablePath;
            sfd.Title = "Сохранение файла с тестами";
            sfd.Filter = "Файлы вопросов (*.xml)|*.xml|Все файлы (*.*)|*.*";

            ofd.InitialDirectory = Application.ExecutablePath;
            ofd.Title = "Загрузка файла с тестами для продолжения заполнения";
            ofd.Filter = "Файлы вопросов (*.xml)|*.xml|Все файлы (*.*)|*.*";

            NextOb();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            sfd.FileName = textBox1.Text;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ttt = new DataTable(textBox1.Text);
                DataColumn ccc = new DataColumn("Num", typeof(int));
                ttt.Columns.Add(ccc);
                ccc = new DataColumn("Text", typeof(string));
                ttt.Columns.Add(ccc);
                //ccc = new DataColumn("Answer", typeof(List<string>));
                ccc = new DataColumn("Answer", typeof(string));
                ttt.Columns.Add(ccc);
                ccc = new DataColumn("Valid", typeof(int));
                ttt.Columns.Add(ccc);

                foreach (SecurityTest.Question ob in LIST)
                {
                    DataRow RRR = ttt.NewRow();
                    RRR["Num"] = ob.Num;
                    RRR["Text"] = ob.Text;
                    RRR["Answer"] = ob.getAnswer();
                    RRR["Valid"] = ob.ValidAnswer;
                    ttt.Rows.Add(RRR);
                }

                ttt.WriteXml(sfd.FileName,true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Curent.Text = textBox2.Text;
            //string[] ans = textBox3.Text.Split('\n').ToList();
            List<string> ans = textBox3.Text.Split('\n').ToList();
            if (textBox4.Text.Length > 0)
            for (int i = 1; i <= ans.Count; i++)
            {
                if (ans[i - 1].Contains(textBox4.Text))
                {
                    ans[i - 1] = ans[i - 1].Replace(textBox4.Text, "");
                    Curent.ValidAnswer = i;
                }
            }
            Curent.Answer = ans;
            LIST.Add(Curent);
            NextOb();
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string tablename = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                ttt = new DataTable(tablename);
                ttt.ReadXmlSchema(ofd.FileName);
                ttt.ReadXml(ofd.FileName);
                LIST.Clear();
                foreach (DataRow RRR in ttt.Rows)
                {
                    NextOb();
                    Curent.Num = int.Parse(RRR["Num"].ToString());
                    Curent.Text = RRR["Text"].ToString();
                    Curent.setAnswer(RRR["Answer"].ToString());
                    Curent.ValidAnswer = int.Parse(RRR["Valid"].ToString());
                    LIST.Add(Curent);
                }

            }
        }
    }
}

