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
        //public DataTable TTT;

        public DataTable ttt;
        public DataTable vvv;
        public DataSet DS;
        public string TestName;
        public List<Question> ListQ = new List<Question>();
        Question SelectItem;
        public int indexCurentQ = 1;
        public int maxCountValue;
        protected List<int> listIndexes = new List<int>();
        protected int countOk = 0;
        protected string curDT;
        protected bool superUser = false;
        protected string PodName;
        protected string DolName;

        bool FlagRentry = false;
        int ConutInteration = 15;

        DateTime startTime;

        public FrmTest()
        {
            InitializeComponent();
            startTime = DateTime.Now;
        }

        bool isHasPicture
        {
            get {
                bool retVal = false;
                if (SelectItem.PictureQ.Length > 0)
                    retVal = true;
                else
                {
                    foreach (Answers ans in SelectItem.Answer)
                    {
                        retVal = ans.PictureA.Length > 0;
                        if (retVal) break;
                    }
                }

                return retVal;
            }
        }

        public void ShowQ()
        {
            buttonNext.BackColor = Color.LightSteelBlue;
            SelectItem = ListQ[listIndexes[indexCurentQ - 1]];
            labelHint.Visible = isHasPicture;
            if (labelHint.Visible) StartHint();
            labelQNum.Text = indexCurentQ.ToString();
            labQ.Text = SelectItem.Text;
            if (SelectItem.PictureQ.Length > 0)
            {
                pictureBoxQ.Visible = true;
                pictureBoxQ.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxQ.Image = stringToImage(SelectItem.PictureQ);
            }
            else
            {
                pictureBoxQ.Image = null;
                pictureBoxQ.Visible = false;
            }
            tableLayoutPanel1.SuspendLayout();
            ClearLayout();
            int j = 0;
            tableLayoutPanel1.RowCount++;
            foreach (Answers item in SelectItem.Answer)
            {
                CreateAnswer(SelectItem, item, j);
                j++;
            }
            tableLayoutPanel1.ResumeLayout();
        }

        private void ClearLayout()
        {
            for (int i = tableLayoutPanel1.Controls.Count - 1; i >= 0; i--)
            {
                tableLayoutPanel1.Controls.RemoveAt(i);
            }

            tableLayoutPanel1.RowCount = 0;
            tableLayoutPanel1.RowStyles.Clear();
        }

        /// <summary>
        /// необхоодимо сделать общей
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public Image stringToImage(string inputString)
        {
            byte[] imageBytes = Convert.FromBase64String(inputString);
            MemoryStream ms = new MemoryStream(imageBytes);
            Image image = Image.FromStream(ms, true, true);
            return image;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox pic = (sender as PictureBox);
            if (pic.Image != null)
            {
                FormPic fp = new FormPic();
                fp.pictureBox1.Image = pic.Image;
                fp.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                fp.ShowDialog();
            }
        }

        private Panel CreatePanel(Question QOb,int indexAnswer)
        {
            Panel pan = new Panel();
            //pan.Width = pictureBoxQ.Width;
            //pan.Height = pictureBoxQ.Height;
            pan.Width = 160;
            pan.Height = 100;

            PictureBox pbox = new PictureBox();
            pbox.Visible = true;
            pbox.Name = "ib";
            pbox.Width = pan.Width;
            pbox.Height = pan.Height;
            pbox.Cursor = Cursors.Hand;
            if (QOb.Answer[indexAnswer].PictureA.Length > 0)
            {
                pbox.Image = stringToImage(QOb.Answer[indexAnswer].PictureA);
                pbox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            pbox.Click += pictureBox1_Click;
            pbox.Dock = DockStyle.Fill;
            pan.Controls.Add(pbox);
            return pan;
        }

        private void CreateAnswer(SecurityTest.Question SelectItem, SecurityTest.Answers item, int j)
        {
            tableLayoutPanel1.RowCount++;
            Label lll = new Label();
            //TextBox tb = new TextBox();
            Label tb = new Label();
            lll.Text = item.originIndex.ToString();
            lll.Name = "lab" + j.ToString();
            lll.Height = 30;
            lll.Dock = DockStyle.Fill;
            lll.Font = labQ.Font;
            lll.TextAlign = ContentAlignment.MiddleCenter;
            tb.Name = "tb" + j.ToString();
            tb.Text = item.Text;
            //tb.ReadOnly = true;
            //tb.Multiline = true;
            tb.TextAlign = ContentAlignment.MiddleLeft;
            if (SelectItem.Answer[j].PictureA.Length > 0)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
                tableLayoutPanel1.Controls.Add(lll, 0, j);
                Panel PPP = CreatePanel(SelectItem, j);
                PPP.Name = "pan" + j.ToString();
                tableLayoutPanel1.Controls.Add(PPP, 1, j);
                PPP.Dock = DockStyle.Fill;
                tableLayoutPanel1.Controls.Add(tb, 2, j);
                tb.Dock = DockStyle.Fill;
            }
            else
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
                tableLayoutPanel1.Controls.Add(lll, 0, j);
                tableLayoutPanel1.Controls.Add(tb, 1, j);
                tableLayoutPanel1.SetColumnSpan(tb, 2);
                tb.Dock = DockStyle.Fill;
            }
            
            RadioButton rb = new RadioButton();
            rb.Name = "rb" + j.ToString();
            rb.Dock = DockStyle.Fill;
            rb.TextAlign = ContentAlignment.MiddleCenter;
            rb.CheckedChanged += Rb_CheckedChanged;
            rb.Padding = new Padding(5, 0, 0, 0);
            tableLayoutPanel1.Controls.Add(rb, 3, j);
            
        }

        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (sender as RadioButton);
            if (rb.Checked)
            {
                int IndexAnswer = int.Parse(rb.Name.Replace("rb", ""));
                SelectItem.UserAnswer = IndexAnswer + 1;
                buttonNext.Enabled = true;
                buttonNext.BackColor = Color.Khaki;
            }
        }

        

        private void NewRB_MouseLeave(object sender, EventArgs e)
        {
            RadioButton RB = (sender as RadioButton);
            RB.BackColor = Color.Transparent;
            
        }

        private void NewRB_MouseEnter(object sender, EventArgs e)
        {
            RadioButton RB = (sender as RadioButton);
            RB.BackColor = Color.Gold;
        }

        

        /// <summary>
        /// Дубликат из TestCreator/TestCreator
        /// </summary>
        private void CreateDS()
        {
            ttt = new DataTable("Вопросы");
            DataColumn ccc = new DataColumn("Num", typeof(int)); //ид вопроса
            ttt.Columns.Add(ccc);
            ccc = new DataColumn("Text", typeof(string)); //текст вопроса
            ttt.Columns.Add(ccc);
            ccc = new DataColumn("IndexValid", typeof(int)); //правильный вариант
            ttt.Columns.Add(ccc);
            ccc = new DataColumn("PictureQ", typeof(string)); //кортинка вопроса
            ttt.Columns.Add(ccc);

            vvv = new DataTable("Ответы");
            ccc = new DataColumn("Index", typeof(int));//ид ответа
            vvv.Columns.Add(ccc);
            ccc = new DataColumn("Num", typeof(int));//ид вопроса (для связи)
            vvv.Columns.Add(ccc);
            ccc = new DataColumn("Text", typeof(string));//тест ответа
            vvv.Columns.Add(ccc);
            ccc = new DataColumn("PictureA", typeof(string));//кортинка ответа
            vvv.Columns.Add(ccc);


            DS = new DataSet();

            DS.Tables.Add(ttt);
            DS.Tables.Add(vvv);
        }


        public void LoadDS(string fileName)
        {
            CreateDS();
            DS.ReadXml(fileName);
            foreach (DataRow RRR in DS.Tables[0].Rows)
            {
                Question Curent = new Question();
                Curent.Num = int.Parse(RRR["Num"].ToString());
                Curent.Text = RRR["Text"].ToString();
                Curent.PictureQ = RRR["PictureQ"].ToString();
                List<Answers> tmp_lst = new List<Answers>();
                foreach (DataRow RRR_And in DS.Tables[1].Rows)
                {
                    if (Curent.Num == int.Parse(RRR_And["Num"].ToString()))
                    {
                        Answers newOb = new Answers();
                        newOb.PictureA = RRR_And["PictureA"].ToString();
                        newOb.originIndex = int.Parse(RRR_And["Index"].ToString());
                        newOb.Text = RRR_And["Text"].ToString();
                        tmp_lst.Add(newOb);
                    }
                }
                Curent.setAnswer(tmp_lst.ToArray());
                Curent.ValidAnswer = int.Parse(RRR["IndexValid"].ToString());
                ListQ.Add(Curent);
            }
        }
        public static void Call(string testFileName, string tableName, string FIO, string pod, string dol, int maxCount, string dt, bool supUser)
        {
            int i = 1, x;
            Random Rand = new Random();
            using (FrmTest Inst = new FrmTest())
            {
                Inst.labelCurEmp.Text = "Сдает тест: " + FIO;
                Inst.curDT = dt;
                Inst.maxCountValue = maxCount;
                Inst.superUser = supUser;
                Inst.PodName = pod;
                Inst.DolName = dol;
                Inst.TestName = tableName;

                //Inst.TTT = new DataTable(tableName);
                //Inst.TTT.ReadXmlSchema(testFileName);
                //Inst.TTT.ReadXml(testFileName);
                Inst.LoadDS(testFileName);
                if (Inst.maxCountValue > Inst.ttt.Rows.Count) Inst.maxCountValue = Inst.ttt.Rows.Count;
                Inst.Text = "Выполнение теста. Осталось вопросов: " + Inst.maxCountValue;
                for (i = 0; i < Inst.maxCountValue; i++)
                {
                    do
                    {
                        x = Rand.Next(0, Inst.ttt.Rows.Count);
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
            //List<string> userParam = new List<string>();
            //List<Question> list = new List<Question>();
            //string filename = TTT.TableName + " " + labelCurEmp.Text.Replace("Сдает тест: ", "") + " " + curDT.Replace(".", "-").Replace(":", "-");
            //string pathRes = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "result", filename + ".pdf");
            //reportGenerator oB = new reportGenerator(pathRes, userParam, list, "");
            //Close();

            int i = 1;
            Question curQ = ListQ[listIndexes[indexCurentQ - 1]];
            //curQ.isOk = panAnswer.Controls

            if (superUser)
            {
                curQ.UserAnswer = curQ.ValidAnswer;
                countOk++;
            }
            else
            {
                if (curQ.isOk)
                    countOk++;
            }
            
            if (indexCurentQ == maxCountValue)
            {
                string FIO = labelCurEmp.Text.Replace("Сдает тест: ", "");
                string[] FIO_Arr = FIO.Split("".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                //string filename = TTT.TableName + " " + FIO_Arr[0] + " " + FIO_Arr[1][0] + FIO_Arr[2][0] + " " + curDT.Replace(".", "-").Replace(":", "-");
                string filename = TestName + " " + FIO_Arr[0] + " " + FIO_Arr[1][0] + FIO_Arr[2][0] + " " + curDT.Replace(".", "-").Replace(":", "-"); 
                string pathRes = System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "result"), filename + ".pdf");
                List<string> userParam = new List<string>();
                List<Question> list = new List<Question>();
                MessageBox.Show("Тестирование завершено. Правильных ответов: " + countOk.ToString() + " из " + maxCountValue.ToString() + " вопросов.");
                for (i = 0; i < listIndexes.Count; i++)
                {
                    curQ = ListQ[listIndexes[i]];
                    list.Add(curQ);
                }
                userParam.Add(labelCurEmp.Text.Replace("Сдает тест: ", ""));
                userParam.Add(curDT);
                userParam.Add(DolName);
                userParam.Add(PodName);
                //userParam.Add(TTT.TableName);
                userParam.Add(TestName);
                TimeSpan tsp = DateTime.Now - startTime;
                userParam.Add(tsp.Minutes.ToString()+":"+tsp.Seconds.ToString());
                userParam.Add(countOk.ToString());
                userParam.Add((maxCountValue - countOk).ToString());
                //using (StreamWriter writetext = new StreamWriter(pathRes))
                //{
                //    writetext.WriteLine("       Дата теста " + curDT);
                //    writetext.WriteLine("Тест сдавал: " + labelCurEmp.Text.Replace("Сдает тест: ", ""));
                //    for (i = 0; i < listIndexes.Count; i++)
                //    {
                //        curQ = ListQ[listIndexes[i]];
                //        writetext.WriteLine((i + 1).ToString() + ". " + curQ.Text);
                //        foreach (string SSS in curQ.Answer)
                //        {
                //            writetext.WriteLine("    " + SSS.Trim());
                //        }
                //        writetext.WriteLine("Ответ: " + curQ.UserAnswer);
                //        writetext.WriteLine("Верный ответ: " + curQ.ValidAnswer);

                //        if (curQ.isOk)
                //            writetext.WriteLine("Результат: Верно");
                //        else
                //            writetext.WriteLine("Результат: Неверно");
                //    }
                //    writetext.WriteLine("Правильных ответов: " + countOk.ToString() + " из " + maxCountValue.ToString() + " вопросов.");
                //}


                if (DateTime.Now < new DateTime(2021, 11, 1))
                {
                    reportGenerator oB = new reportGenerator(pathRes, userParam, list, "");
                }
                else
                {
                    MessageBox.Show("Необходимо обновить лицензию программы. Обратитесь к Разработчику: vladimir.chemir@yandex.ru.", "Ошибка построения отчета", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                DialogResult = DialogResult.OK;
            }
            else
            {
                indexCurentQ++;
                ShowQ();
            }

            Text = "Выполнение теста. Осталось вопросов: " + (maxCountValue - (indexCurentQ - 1));
            buttonNext.Enabled = false;

        }

        private void FrmTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (indexCurentQ < maxCountValue)
            {
                if (MessageBox.Show("Тестирование не завершено. Вы подтверждаете завершение тестирования?", "Подтвердите операцию", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                    e.Cancel = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FlagRentry = !FlagRentry;
            if (FlagRentry)
                //labelHint.ForeColor = Color.Orange;
                labelHint.ForeColor = Color.Red;
            else
                labelHint.ForeColor = Color.Chocolate;
            ConutInteration--;
            if (ConutInteration == 0) timer1.Enabled = false;
        }

        void StartHint()
        {
            ConutInteration = 15;
            FlagRentry = false;
            timer1.Enabled = true;
        }
    }
}
