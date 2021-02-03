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
    public partial class FrmTest : Form
    {
        public DataTable TTT;
        public List<Question> ListQ = new List<Question>();
        public int indexCurentQ = 1;
        public int maxCountValue;
        protected List<int> listIndexes = new List<int>();
        protected int countOk = 0;
        protected string curDT;

        public FrmTest()
        {
            InitializeComponent();
        }

        public void ShowQ()
        {
            buttonNext.BackColor = SystemColors.ButtonFace;
            Question curQ = ListQ[listIndexes[indexCurentQ - 1]];
            labQ.Text = curQ.Text;
            panAnswer.Controls.Clear();
            labelQNum.Text = indexCurentQ.ToString();
            foreach (string SSS in curQ.Answer)
            {
                RadioButton newRB = new RadioButton();
                newRB.Font = labQ.Font;
                newRB.AutoSize = true;
                newRB.Text = SSS;
                panAnswer.Controls.Add(newRB);
            }
        }

        public static void Call(string testFileName, string tableName, string FIO, int maxCount, string dt)
        {
            int i = 1, x;
            Random Rand = new Random();
            using (FrmTest Inst = new FrmTest())
            {
                Inst.labelCurEmp.Text = "Сдает тест: " + FIO;
                Inst.curDT = dt;
                Inst.maxCountValue = maxCount;
                Inst.TTT = new DataTable(tableName);
                Inst.TTT.ReadXmlSchema(testFileName);
                Inst.TTT.ReadXml(testFileName);
                foreach (DataRow RRR in Inst.TTT.Rows)
                {
                    Question NewQ = new Question();
                    NewQ.Num = int.Parse(RRR["Num"].ToString());
                    NewQ.Text = RRR["Text"].ToString();
                    NewQ.setAnswer(RRR["Answer"].ToString());
                    NewQ.ValidAnswer = int.Parse(RRR["Valid"].ToString());
                    Inst.ListQ.Add(NewQ);
                }
                if (Inst.maxCountValue > Inst.TTT.Rows.Count) Inst.maxCountValue = Inst.TTT.Rows.Count;
                for (i = 0; i < Inst.maxCountValue; i++)
                {
                    do
                    {
                        x = Rand.Next(1, Inst.TTT.Rows.Count);
                    } while (Inst.listIndexes.Contains(x));
                    Inst.listIndexes.Add(x);
                }
                Inst.ShowDialog();
            }
            
        }

        private void FrmTest_Shown(object sender, EventArgs e)
        {
            ShowQ();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (indexCurentQ == maxCountValue)
            {
                string filename = TTT.TableName + " " + labelCurEmp.Text.Replace("Сдает тест: ","") + " " + curDT.Replace(".","-").Replace(":","-");
                string pathRes = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "result", filename + ".txt");
                MessageBox.Show("Конец теста! Правильных ответов: " + countOk.ToString() + " из " + maxCountValue.ToString() + " вопросов.");
                using (StreamWriter writetext = new StreamWriter(pathRes))
                {
                    writetext.WriteLine("       Дата теста " + curDT);
                    writetext.WriteLine("Тест сдавал: " + labelCurEmp.Text.Replace("Сдает тест: ", ""));
                    for (int i = 0; i < listIndexes.Count; i++)
                    {
                        Question curQ = ListQ[listIndexes[i]];
                        writetext.WriteLine((i + 1).ToString() + ". " + curQ.Text) ;
                        foreach (string SSS in curQ.Answer)
                        {
                            writetext.WriteLine("    " + SSS.Trim());
                        }
                        if (curQ.isOk)
                            writetext.WriteLine("ответил верно: " + curQ.Answer[curQ.ValidAnswer - 1]);
                        else
                            writetext.WriteLine("ответил неверно: " + curQ.Answer[curQ.ValidAnswer - 1]);
                    }
                    writetext.WriteLine("Правильных ответов: " + countOk.ToString() + " из " + maxCountValue.ToString() + " вопросов.");
                }
                DialogResult = DialogResult.OK;
            }
            else
            {
                int i = 1;
                Question curQ = ListQ[listIndexes[indexCurentQ - 1]];
                //curQ.isOk = panAnswer.Controls
                foreach (RadioButton RB in panAnswer.Controls)
                {
                    if (RB.Checked && i == curQ.ValidAnswer)
                    {
                        curQ.isOk = true;
                        countOk++;
                        buttonNext.BackColor = Color.Gold;
                        break;
                    }
                    i++;
                }
                
                indexCurentQ++;
                ShowQ();
            }
        }
    }
}
