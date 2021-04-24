using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        public FormCreate()
        {
            InitializeComponent();

            ofd.InitialDirectory = Application.ExecutablePath;
            ofd.Title = "Загрузка изображения";
            ofd.Filter = "Все файлы (*.*)|*.*";
            
            linkLabel1.BackColor = Color.Transparent;
            //linkLabel1.Parent = pictureBox1;

            tableLayoutPanel1.RowCount = 0;
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowCount++;
            // для добавления варианта
            LinkLabel addLabel = new LinkLabel();
            addLabel.Text = "Добавить вариант ответа...";
            addLabel.AutoSize = true;
            addLabel.Click += AddLabel_Click;
            addLabel.Dock = DockStyle.Top;
            addLabel.TextAlign = ContentAlignment.TopLeft;
            tableLayoutPanel1.Controls.Add(addLabel, 1, 0);
            // для удаления варианта
            LinkLabel delLabel = new LinkLabel();
            delLabel.Text = "Удалить последний...";
            delLabel.AutoSize = true;
            delLabel.Click += DelLabel_Click;
            delLabel.Dock = DockStyle.Top;
            delLabel.TextAlign = ContentAlignment.TopLeft;
            delLabel.Visible = false;
            tableLayoutPanel1.Controls.Add(delLabel, 2, 0);

            AddNext();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveDS();
            button1.BackColor = SystemColors.Control;
        }

        private void DataIsChange()
        {
            button1.BackColor = SystemColors.ActiveCaption;
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
                if (ob.PictureQ.Length > 0)
                    RRR["PictureQ"] = ob.PictureQ;
                else
                    RRR["PictureQ"] = "";
                int index_and = 1;
                foreach (SecurityTest.Answers ans in ob.Answer)
                {
                    DataRow RRR_A = vvv.NewRow();
                    RRR_A["Num"] = ob.Num;
                    RRR_A["Index"] = index_and.ToString();
                    RRR_A["Text"] = ans.Text;
                    if (ans.PictureA.Length > 0)
                        RRR_A["PictureA"] = ans.PictureA;
                    else
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

        public void LoadDS()
        {
            CreateDS();
            DS.ReadXml(fileName);
            comboBox1.Items.Clear();
            LIST.Clear();
            foreach (DataRow RRR in DS.Tables[0].Rows)
            {
                Curent = new SecurityTest.Question();
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
                        newOb.PictureA = RRR_And["PictureA"].ToString();
                        newOb.originIndex = int.Parse(RRR_And["Index"].ToString());
                        newOb.Text = RRR_And["Text"].ToString();
                        tmp_lst.Add(newOb);
                    }
                }
                Curent.setAnswer(tmp_lst.ToArray());
                Curent.ValidAnswer = int.Parse(RRR["IndexValid"].ToString());
                LIST.Add(Curent);
                
            }
            
            textBox1.Text = fileName;
            comboBox1.Items.AddRange(LIST.ToArray());
            AddNext();
            
        }
        /// <summary>
        /// Добавляет в конец списка 
        /// </summary>
        private void AddNext()
        {
            SecurityTest.Question lastOb = new SecurityTest.Question();
            lastOb.Num = comboBox1.Items.Count + 1;
            lastOb.PictureQ = "";
            lastOb.Text = "Новый вопрос...";
            LIST.Add(lastOb);
            comboBox1.Items.Add(lastOb);
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            DataIsChange();
        }


        private void FormCreate_Shown(object sender, EventArgs e)
        {
            textBox1.Text = fileName;
        }

        private void CreateAnswer(SecurityTest.Question SelectItem, SecurityTest.Answers item, int j)
        {
            tableLayoutPanel1.RowCount++;
            //tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, pictureBox1.Height));
            Label lll = new Label();
            TextBox tb = new TextBox();
            lll.Text = item.originIndex.ToString();
            tb.TextChanged += Lll_TextChanged;
            tb.Name = "tb" + j.ToString();
            lll.Name = "lab" + j.ToString();
            lll.Height = 30;
            tb.Text = item.Text;

            tableLayoutPanel1.Controls.Add(lll, 0, j);
            tableLayoutPanel1.Controls.Add(tb, 1, j);

            lll.Dock = DockStyle.Fill;
            lll.Font = label6.Font;
            lll.TextAlign = ContentAlignment.MiddleCenter;
            tb.Multiline = true;
            tb.Dock = DockStyle.Fill;
            Panel PPP = CreatePanel(j);
            PPP.Name = "pan" + j.ToString();
            tableLayoutPanel1.Controls.Add(PPP, 2, j);
            PPP.Dock = DockStyle.Fill;
            RadioButton rb = new RadioButton();
            rb.Name = "rb" + j.ToString();
            rb.Dock = DockStyle.Fill;
            rb.TextAlign = ContentAlignment.MiddleCenter;
            if (SelectItem.ValidAnswer == (j + 1))
                rb.Checked = true;
            else
                rb.Checked = false;
            rb.CheckedChanged += Rb_CheckedChanged;
            tableLayoutPanel1.Controls.Add(rb, 3, j);
        }

        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (sender as RadioButton);
            if (rb.Checked)
            {
                int IndexAnswer = int.Parse(rb.Name.Replace("rb", ""));
                (comboBox1.SelectedItem as SecurityTest.Question).ValidAnswer = IndexAnswer+1;
                DataIsChange();
            }
        }

        private void Lll_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (sender as TextBox);
            int IndexAnswer = int.Parse(tb.Name.Replace("tb", ""));
            (comboBox1.SelectedItem as SecurityTest.Question).Answer[IndexAnswer].Text = tb.Text;
            DataIsChange();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.SuspendLayout();

            ClearLayout();

            SecurityTest.Question SelectItem = (SecurityTest.Question) comboBox1.SelectedItem;
            label6.Text = (comboBox1.SelectedIndex + 1).ToString();
            textBox2.Text = SelectItem.Text;
            int j = 0;
            tableLayoutPanel1.RowCount++;
            foreach (SecurityTest.Answers item in SelectItem.Answer)
            {
                CreateAnswer(SelectItem, item, j);
                j++;
            }
            // для добавления варианта
            LinkLabel addLabel = new LinkLabel();
            addLabel.Text = "Добавить вариант ответа...";
            addLabel.AutoSize = true;
            addLabel.Click += AddLabel_Click;
            addLabel.Dock = DockStyle.Top;
            addLabel.TextAlign = ContentAlignment.TopLeft;
            tableLayoutPanel1.Controls.Add(addLabel, 1, j);

            // для удаления варианта
            LinkLabel delLabel = new LinkLabel();
            delLabel.Text = "Удалить последний...";
            delLabel.AutoSize = true;
            delLabel.Click += DelLabel_Click;
            delLabel.Dock = DockStyle.Top;
            delLabel.TextAlign = ContentAlignment.TopLeft;
            tableLayoutPanel1.Controls.Add(delLabel, 2, j);
            if (j == 0)
                delLabel.Visible = false;
            tableLayoutPanel1.ResumeLayout();
            if (SelectItem.PictureQ.Length > 0)
            {
                //pictureBox1.Visible = true;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = stringToImage(SelectItem.PictureQ);
            }
            else
            {
                pictureBox1.Image = null;
                //pictureBox1.Visible = false;
            }

        }

        private void AddLabel_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.Controls.RemoveAt(tableLayoutPanel1.Controls.Count - 1);
            tableLayoutPanel1.Controls.RemoveAt(tableLayoutPanel1.Controls.Count - 1);
            if (tableLayoutPanel1.RowCount > 1)
                tableLayoutPanel1.RowCount--;

            SecurityTest.Question SelectItem = (SecurityTest.Question)comboBox1.SelectedItem;
            int j = SelectItem.Answer.Count;
            tableLayoutPanel1.RowCount++;
            
            CreateAnswer(SelectItem, SelectItem.NewAnswer(), j);
            j++;
            //для добавления варианта
            LinkLabel addLabel = new LinkLabel();
            addLabel.Text = "Добавить новый вариант ответа...";
            addLabel.AutoSize = true;
            addLabel.Click += AddLabel_Click;
            addLabel.Dock = DockStyle.Top;
            addLabel.TextAlign = ContentAlignment.TopLeft;
            tableLayoutPanel1.Controls.Add(addLabel, 1, j);
            // для удаления
            LinkLabel delLabel = new LinkLabel();
            delLabel.Text = "Удалить последний...";
            delLabel.AutoSize = true;
            delLabel.Click += DelLabel_Click; 
            delLabel.Dock = DockStyle.Top;
            delLabel.TextAlign = ContentAlignment.TopLeft;
            tableLayoutPanel1.Controls.Add(delLabel, 2, j);
            if (j == 0)
                delLabel.Visible = false;
            DataIsChange();
            tableLayoutPanel1.ResumeLayout();

        }

        private void DelLabel_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.SuspendLayout();
            int indexDel = (comboBox1.SelectedItem as SecurityTest.Question).Answer.Count - 1;
            (comboBox1.SelectedItem as SecurityTest.Question).Answer.RemoveAt(indexDel);
            tableLayoutPanel1.Controls.RemoveByKey("pan" + indexDel.ToString());
            tableLayoutPanel1.Controls.RemoveByKey("lab" + indexDel.ToString());
            tableLayoutPanel1.Controls.RemoveByKey("tb" + indexDel.ToString());
            tableLayoutPanel1.Controls.RemoveByKey("rb" + indexDel.ToString());

            tableLayoutPanel1.Controls.RemoveAt(tableLayoutPanel1.Controls.Count - 1);
            tableLayoutPanel1.Controls.RemoveAt(tableLayoutPanel1.Controls.Count - 1);

            tableLayoutPanel1.RowCount--;

            //для добавления варианта
            LinkLabel addLabel = new LinkLabel();
            addLabel.Text = "Добавить новый вариант ответа...";
            addLabel.AutoSize = true;
            addLabel.Click += AddLabel_Click;
            addLabel.Dock = DockStyle.Top;
            addLabel.TextAlign = ContentAlignment.TopLeft;
            tableLayoutPanel1.Controls.Add(addLabel, 1, indexDel);
            // для удаления
            LinkLabel delLabel = new LinkLabel();
            delLabel.Text = "Удалить последний...";
            delLabel.AutoSize = true;
            delLabel.Click += DelLabel_Click;
            delLabel.Dock = DockStyle.Top;
            delLabel.TextAlign = ContentAlignment.TopLeft;
            tableLayoutPanel1.Controls.Add(delLabel, 2, indexDel);
            if (indexDel == 0)
                delLabel.Visible = false;
            DataIsChange();
            tableLayoutPanel1.ResumeLayout();
        }

        private Panel CreatePanel(int indexAnswer)
        {
            Panel pan = new Panel();
            pan.Width = pictureBox1.Width;
            pan.Height = pictureBox1.Height;
            LinkLabel llab = new LinkLabel();
            llab.Text = "Выбрать картинку...";
            llab.AutoSize = true;
            llab.Click += Llab_Click;
            PictureBox pbox = new PictureBox();
            pbox.Visible = true;
            pbox.Name = "ib";
            pbox.Width = pan.Width;
            pbox.Height = pan.Height;
            if ((comboBox1.SelectedItem as SecurityTest.Question).Answer[indexAnswer].PictureA.Length > 0)
            {
                pbox.Image = stringToImage((comboBox1.SelectedItem as SecurityTest.Question).Answer[indexAnswer].PictureA);
                pbox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            pbox.Click += pictureBox1_Click;
            pbox.Dock = DockStyle.Fill;
            pan.Controls.Add(pbox);
            //pan.Controls.Add(llab);
            llab.Parent = pbox;
            llab.BackColor = Color.Transparent;
            llab.Dock = DockStyle.Top;
            llab.TextAlign = ContentAlignment.MiddleCenter;
            return pan;
        }

        private void Llab_Click(object sender, EventArgs e)
        {
            Bitmap image; //Bitmap для открываемого изображения
            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Файлы изображений(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    PictureBox pbox = (sender as Control).Parent as PictureBox;
                    //PictureBox pbox = (PictureBox)pan.Controls.Find("ib",false)[0];
                    pbox.Visible = true;
                    pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbox.Image = Image.FromFile(open_dialog.FileName);

                    Panel pan = pbox.Parent as Panel;
                    int IndexAnswer = int.Parse(pan.Name.Replace("pan",""));
                    using (Image UseImage = Image.FromFile(open_dialog.FileName))
                    {
                        using (System.IO.MemoryStream m = new System.IO.MemoryStream())
                        {
                            UseImage.Save(m, UseImage.RawFormat);
                            byte[] imageBytes = m.ToArray();
                            string base64String = Convert.ToBase64String(imageBytes);
                            (comboBox1.SelectedItem as SecurityTest.Question).Answer[IndexAnswer].PictureA = base64String;
                            DataIsChange();
                        }
                    }
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть либо преобразовать в строку выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Bitmap image;
            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Файлы изображений(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = Image.FromFile(open_dialog.FileName);

                    using (Image UseImage = Image.FromFile(open_dialog.FileName))
                    {
                        using (System.IO.MemoryStream m = new System.IO.MemoryStream())
                        {
                            UseImage.Save(m, UseImage.RawFormat);
                            byte[] imageBytes = m.ToArray();
                            string base64String = Convert.ToBase64String(imageBytes);
                            (comboBox1.SelectedItem as SecurityTest.Question).PictureQ = base64String;
                            DataIsChange();
                        }
                    }
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть либо преобразовать в строку выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            (comboBox1.SelectedItem as SecurityTest.Question).Text = textBox2.Text;
            DataIsChange();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddNext();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить вопрос №"+label6.Text + " ?","Подтвердите",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int selectInd = comboBox1.SelectedIndex;
                LIST.RemoveAt(comboBox1.SelectedIndex);
                comboBox1.Items.RemoveAt(selectInd);
                if (selectInd > comboBox1.Items.Count - 1)
                    selectInd = comboBox1.Items.Count - 1;
                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedIndex = selectInd;
                else
                    ClearLayout();
            }
        }
    }
}

