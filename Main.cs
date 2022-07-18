using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace steam_checker
{
    public partial class Main : Form
    {
       
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            steam_plugin check = new steam_plugin();
            utilities utils = new utilities();
            check.loginAccount(textBox1.Text, textBox2.Text);           
            //check.checkAccount(textBox1.Text, textBox2.Text, false);
            //check.changePassword("antlips", "vash", "v");
        }
    }
}
