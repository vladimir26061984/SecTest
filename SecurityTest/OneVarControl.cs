using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SecurityTest
{
    public class SampleEventArgs
    {
        public SampleEventArgs(bool value) { Value = value; }
        public bool Value { get; } // readonly
    }

    public partial class OneVarControl : UserControl
    {
        public delegate void SampleEventHandler(object sender, SampleEventArgs e);

        public event SampleEventHandler SampleEvent;


        public OneVarControl()
        {
            InitializeComponent();
            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SampleEvent?.Invoke(this, new SampleEventArgs((radioButton1.Checked)));
        }

        public string Text
        {
            set {
                label1.Text = value;
            }
            get
            {
                return label1.Text;
            }
        }

        public bool Checked
        {
            set
            {
                radioButton1.Checked = value;
            }
            get
            {
                return radioButton1.Checked;
            }
        }
    }
}
