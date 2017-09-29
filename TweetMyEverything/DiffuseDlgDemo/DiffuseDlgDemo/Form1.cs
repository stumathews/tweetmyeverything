using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Abhinaba.TransDlg;

namespace DiffuseDlgDemo
{
	public class Form1 : TransDialog
	{
        private System.Windows.Forms.Button button1;
		private System.ComponentModel.Container components = null;

        #region Ctor init and dispose code
		public Form1()
            : base (true)
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
        #endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(88, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "Show Notification";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(320, 102);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }
		#endregion

        #region Event handler
        private void button1_Click(object sender, System.EventArgs e)
        {
            Notification notifForm = new Notification();
            notifForm.Show();
        }
        #endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

	}
}
