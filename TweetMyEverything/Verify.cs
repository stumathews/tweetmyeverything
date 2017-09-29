using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TweetMyEverything
{
    public partial class Verify : Form
    {
        public Verify()
        {
            InitializeComponent();
        }
        public string getCode()
        {
            return inputBox.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (inputBox.Text.Length > 0)
            {
                Close();
            }           
        }
    }
}
