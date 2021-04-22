using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestCreator
{
    public partial class FormCreate : Form
    {
        List<SecurityTest.Question> LIST = new List<SecurityTest.Question>();
        SecurityTest.Question Curent;
        public string fileName = "";

        DataTable ttt;
        DataTable vvv;
        DataSet DS;
        
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

            ofd.InitialDirectory = Application.ExecutablePath;
            ofd.Title = "Загрузка изображения";
            ofd.Filter = "Все файлы (*.*)|*.*";

            NextOb();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveDS();
        }

        private void SaveTable()
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
            ttt.WriteXml(fileName, true);
        }

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

        public void SaveDS()
        {
            CreateDS();

            foreach (SecurityTest.Question ob in LIST)
            {
                DataRow RRR = ttt.NewRow();
                RRR["Num"] = ob.Num;
                RRR["Text"] = ob.Text;
                RRR["PictureQ"] = "";
                int index_and = 1;
                foreach (SecurityTest.Answers ans in ob.Answer)
                {
                    DataRow RRR_A = vvv.NewRow();
                    RRR_A["Num"] = ob.Num;
                    RRR_A["Index"] = index_and.ToString();
                    RRR_A["Text"] = ans.Text;
                    RRR_A["PictureA"] = "";//загруженная картинка
                    vvv.Rows.Add(RRR_A);
                    index_and++;
                }
                RRR["IndexValid"] = ob.ValidAnswer;
                ttt.Rows.Add(RRR);
            }
            DS.WriteXml(fileName);
            //DS.WriteXmlSchema(fileName);
            

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
            //Curent.Answer = ans;
            LIST.Add(Curent);
            //SaveTable(); //сохранение текущего состояния
            SaveDS();
            NextOb();
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //string tablename = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                //textBox1.Text = tablename;
                //ttt = new DataTable(tablename);
                //ttt.ReadXmlSchema(ofd.FileName);
                //ttt.ReadXml(ofd.FileName);
                //LIST.Clear();
                //foreach (DataRow RRR in ttt.Rows)
                //{

                //    Curent.Num = int.Parse(RRR["Num"].ToString());
                //    Curent.Text = RRR["Text"].ToString();
                //    Curent.setAnswer(RRR["Answer"].ToString());
                //    Curent.ValidAnswer = int.Parse(RRR["Valid"].ToString());
                //    LIST.Add(Curent);
                //    NextOb();
                //}
                //fileName = ofd.FileName;
                //button2.Enabled = true;

                string tablename = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                textBox1.Text = tablename;
                fileName = ofd.FileName;
                LoadDS();

                /*
                 foreach (SecurityTest.Question ob in LIST)
                    {
                        DataRow RRR = ttt.NewRow();
                        RRR["Num"] = ob.Num;
                        RRR["Text"] = ob.Text;
                        int index_and = 1;
                        foreach (string ans in ob.Answer)
                        {
                            DataRow RRR_A = vvv.NewRow();
                            RRR_A["Num"] = index_and.ToString();
                            RRR_A["Text"] = ans;
                            vvv.Rows.Add(RRR_A);
                            index_and++;
                        }
                        RRR["Valid"] = ob.ValidAnswer;
                        ttt.Rows.Add(RRR);
                    }
                 
                 */

                
               
            }
        }

        public void LoadDS()
        {
            CreateDS();
            DS.ReadXml(fileName);

            LIST.Clear();
            foreach (DataRow RRR in DS.Tables[0].Rows)
            {
                Curent.Num = int.Parse(RRR["Num"].ToString());
                Curent.Text = RRR["Text"].ToString();
                Curent.PictureQ = RRR["PictureQ"].ToString();
                string Answer = "";
                List<SecurityTest.Answers> tmp_lst = new List<SecurityTest.Answers>();
                foreach (DataRow RRR_And in DS.Tables[1].Rows)
                {
                    if (Curent.Num == int.Parse(RRR_And["Num"].ToString()))
                    {
                        //Answer += "@" + RRR_And["Text"].ToString();
                        SecurityTest.Answers newOb = new SecurityTest.Answers();
                        newOb.PictureA = "";
                        newOb.originIndex = int.Parse(RRR_And["Index"].ToString());
                        newOb.Text = RRR_And["Text"].ToString();
                        tmp_lst.Add(newOb);
                    }
                }
                Curent.setAnswer(tmp_lst.ToArray());
                Curent.ValidAnswer = int.Parse(RRR["IndexValid"].ToString());
                LIST.Add(Curent);
                NextOb();
            }
            fileName = ofd.FileName;
            comboBox1.Items.AddRange(LIST.ToArray());
            button2.Enabled = true;
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

        private void FormCreate_Shown(object sender, EventArgs e)
        {
            textBox1.Text = fileName;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            TextBox t1 = new TextBox();
            t1.Width = textBox1.Width;
            tableLayoutPanel1.Controls.Add(t1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.SuspendLayout();

            for (int i = tableLayoutPanel1.Controls.Count - 1; i >= 0; i--)
            {
                tableLayoutPanel1.Controls.RemoveAt(i);
                tableLayoutPanel1.RowStyles.RemoveAt(0);
            }

            tableLayoutPanel1.RowCount = 0;
            tableLayoutPanel1.RowStyles.Clear();
            SecurityTest.Question SelectItem = (SecurityTest.Question) comboBox1.SelectedItem;
            textBox2.Text = SelectItem.Text;
            int j = 0;
            foreach (SecurityTest.Answers item in SelectItem.Answer)
            {
                tableLayoutPanel1.RowCount++;
                //tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                Label lll = new Label();
                TextBox tb = new TextBox();
                lll.Text = item.originIndex.ToString();
                tb.Text = item.Text;

                //tableLayoutPanel1.Controls.Add(lll);

                tableLayoutPanel1.Controls.Add(lll,0,j);
                tableLayoutPanel1.Controls.Add(tb, 1, j);

                lll.Dock = DockStyle.Fill;
                tb.Dock = DockStyle.Fill;

                //tableLayoutPanel1.RowCount++;
                //tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                //TextBox tb = new TextBox();
                //tb.Text = item.Text;
                //tableLayoutPanel1.Controls.Add(tb);

                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                PictureBox pbox = new PictureBox();
                if (item.PictureA.Length > 0)
                    pbox.Height = tableLayoutPanel1.Width;
                else
                    pbox.Height = 20;
                tableLayoutPanel1.Controls.Add(pbox);
                j++;
            }

            tableLayoutPanel1.ResumeLayout();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            //// Configure open file dialog box 
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.Filter = "";

            //ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            //string sep = string.Empty;

            //foreach (var c in codecs)
            //{
            //    string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
            //    dlg.Filter = String.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, codecName, c.FilenameExtension);
            //    sep = "|";
            //}

            ////dlg.Filter = String.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, "All Files", "*.*");

            //dlg.DefaultExt = ".png"; // Default file extension 

            Bitmap image; //Bitmap для открываемого изображения

            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Файлы изображений(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                // Open document 
                //string fileName = dlg.FileName;
                //pictureBox1.Load(dlg.FileName);
                //pictureBox1.Visible = true;
                //pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                //// Do something with fileName  

                try
                {
                    //image = new Bitmap(open_dialog.FileName);
                    ////вместо pictureBox1 укажите pictureBox, в который нужно загрузить изображение 
                    //pictureBox1.Visible = true;
                    //pictureBox1.Image = image;
                    ////pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                    ////pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                    //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    //pictureBox1.Invalidate();

                    pictureBox1.Visible = true;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = Image.FromFile(open_dialog.FileName);
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormPic fp = new FormPic();
            fp.pictureBox1.Image = pictureBox1.Image;
            fp.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            fp.ShowDialog();
        }
    }
}

