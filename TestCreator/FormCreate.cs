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
            ans.RemoveAll(ss => ss.Equals(string.Empty));
            if (textBox4.Text.Length > 0)
            for (int i = 1; i <= ans.Count; i++)
            {
                if (ans[i - 1].Contains(textBox4.Text))
                {
                    ans[i - 1] = ans[i - 1].Replace(textBox4.Text, "");
                    Curent.ValidAnswer = i;
                }
            }
            if (Curent.ValidAnswer == 0)
            {
                if (textBox4.Text.Length == 0)
                {
                    if (MessageBox.Show("Не определено значение строки [Признак верного ответа]. Этой строкой маркируется правильный ответ" + Environment.NewLine +
                        "Если нажать 'Да', то будет предложена строка по-умолчанию. 'Нет' - вы укажите строку самостоятельно.", "Внимание! Неполные данные", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        textBox4.Text = "(+)";
                    }
                }
                else
                    MessageBox.Show("Для текущего блока не определен правильный ответ. Убедитесь, что значение строки [Признак верного ответа] находится в конце одного из вариатов ответа.", "Ошибка ввода", MessageBoxButtons.OK);
                return;
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
                textBox1.Text = tablename;
                ttt = new DataTable(tablename);
                ttt.ReadXmlSchema(ofd.FileName);
                ttt.ReadXml(ofd.FileName);
                LIST.Clear();
                foreach (DataRow RRR in ttt.Rows)
                {
                    
                    Curent.Num = int.Parse(RRR["Num"].ToString());
                    Curent.Text = RRR["Text"].ToString();
                    Curent.setAnswer(RRR["Answer"].ToString());
                    Curent.ValidAnswer = int.Parse(RRR["Valid"].ToString());
                    LIST.Add(Curent);
                    NextOb();
                }

            }
        }

     

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            int index = textBox3.SelectionStart;
            if (e.KeyCode == Keys.F3)
            {
                string sss = textBox3.Text;
                int len = sss.Length;
                int iEnd = sss.IndexOf("\r\n", index);
                if (iEnd == -1)
                {
                    sss += " " + textBox4.Text;
                }
                else
                {
                    sss =  sss.Insert(iEnd, " " + textBox4.Text);
                }
                textBox3.Text = sss;
            }
        }
    }
}

