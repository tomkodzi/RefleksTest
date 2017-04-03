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
    public partial class Form1 : Form
    {

       public static string tekst;
        Form2 test1;
        int i = 1;

        Login l;
        public Form1()             
        {
            l = new Login();
            InitializeComponent();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            okno5();
        }
        public void okno5()
        {
            do
            {
                Form5 f5 = new Form5(l);
                f5.ShowInTaskbar = false;
                f5.MaximizeBox = false;
                f5.MinimizeBox = false;
                f5.ShowDialog();
                i++;
            }
            while (i == 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            i++;
            test1 = new Form2(l);
            test1.Show();
            

            this.Hide();
            test1.Location = this.Location;
            MessageBox.Show("Po naciśnięciu przyciku start Timer wystartuje i w momencie\nkiedy w okienku pojawi sie znak stop kliknij na niego\n Najpierw spróbuj swoich sił bez pomiaru czasu ;)");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            i++;
            Form3 test2 = new Form3(l);
            test2.Show();												   //uruchomienie testu dźwiekowego
            this.Hide();
            test2.Location = this.Location;
            MessageBox.Show("Witaj w teście dźwiękowym!\nPo naciśnięciu przycisku start, w losowym momencie usłyszysz dźwięk.\nTwoim zadaniem jest jak najszybsze naciśnięcie przycisku odpowiadającemu odtwrzanemu dźwiękowi");       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            i++;														  //uruchomienie testu graficznego
            Form4 form4 = new Form4(l);
            form4.Location = this.Location;
            this.Hide();
            form4.Show();
           
            MessageBox.Show("Witaj w teście graficznym 2!\nPo naciśnięciu przycisku Start pojawiają się losowe liczby z zakresu <100,999>, które znikają po niecałej sekundzie.\nTwoim zadaniem jest jak najszybsze ich porównanie za pomocą znaków porównania: <, >, =. \nPowodzenia!");
        }

        private void close(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

         
    }
}
