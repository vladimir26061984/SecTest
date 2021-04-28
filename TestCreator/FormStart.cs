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
    public partial class FormStart : Form
    {
        SaveFileDialog sfd = new SaveFileDialog();
        OpenFileDialog ofd = new OpenFileDialog();

        public FormStart()
        {
            InitializeComponent();

            sfd.InitialDirectory = Application.ExecutablePath;
            sfd.Title = "Сохранение файла с тестами";
            sfd.Filter = "Файлы вопросов (*.xml)|*.xml|Все файлы (*.*)|*.*";

            ofd.InitialDirectory = Application.ExecutablePath;
            ofd.RestoreDirectory = true;
            ofd.AutoUpgradeEnabled = false;
            ofd.Title = "Загрузка файла с тестами для продолжения заполнения";
            ofd.Filter = "Файлы вопросов (*.xml)|*.xml|Все файлы (*.*)|*.*";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = radioButton1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormCreate fff = new FormCreate();
            
            if (radioButton2.Checked)
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fff.fileName = ofd.FileName;
                    fff.LoadDS();
                }
                else return;
            }
            if (radioButton1.Checked)
            {
                sfd.FileName = textBox1.Text;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    fff.fileName = sfd.FileName;
                    fff.SaveDS();
                }
                else return;
            }
            
            ////
            //fff.fileName = "D:\\2021\\Test\\SecTest.git\\trunk\\TestCreator\\bin\\Debug\\DDD.xml"; //TODO: Убрать
            //fff.LoadDS();
            ///



            Visible = false;
            if (fff.ShowDialog() == DialogResult.Cancel)
            {
                Close();
            }
        }
    }
}
