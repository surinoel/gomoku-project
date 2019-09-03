using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void Single_Button_Click(object sender, EventArgs e)
        {
            Hide();
            SinglePlay sp = new SinglePlay();
            sp.FormClosed += new FormClosedEventHandler(Form_Closed);
            sp.Show();
        }

        private void Exit_Button_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void Multi_Button_Click(object sender, EventArgs e)
        {
            Hide();
            MultiPlay sp = new MultiPlay();
            sp.FormClosed += new FormClosedEventHandler(Form_Closed);
            sp.Show();
        }

        void Form_Closed(object sender, FormClosedEventArgs e)
        {
            Show();
        }
    }
}
