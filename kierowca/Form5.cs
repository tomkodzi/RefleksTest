using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kierowca
{
    public partial class Form5 : Form
    {
        Login l;
        public Form5(Login l)
        {
            this.l = l;
            InitializeComponent();
            this.Focus();
        }
       public static string imie;

        public void Form5_Load(object sender, EventArgs e)
        {
            
        }                                   

        public void button1_Click(object sender, EventArgs e)
        {																   //logowanie
            if (name.Text.Length > 1)
            {
                l.Imie = this.name.Text;
                this.Hide();
            }
                
        }
    }
}
