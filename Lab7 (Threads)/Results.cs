using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7__Threads_
{
    public partial class Results : Form
    {
        public static int Result;
        public static bool Victory;
        public static bool Restart = false;
        public Results()
        {
            InitializeComponent();
        }

        private void Results_Load(object sender, EventArgs e)
        {
            if (Victory)
            {
                lb_Text.Text = "И перед нами победитель! Ваш счет:" + Result;
            }
            else
            {
                lb_Text.Text = "Поражение! Ваш счет:" + Result;
            }
            Restart = false;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Restart_Click(object sender, EventArgs e)
        {
            Restart = true;
            Close();
        }
    }
}
