using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace kierowca
{
    public partial class Form2 : Form
    {
        Login l;
        public static string tekst2;
        public Form2(Login l)
        {
            this.l = l;
            InitializeComponent();
            chart1.Hide();
        }

        Form1 start = new Form1();
        public static DateTime time1, time2, timeint;
        private static System.Timers.Timer sek = null;
        int i = 0;
        TimeSpan timesum = new TimeSpan(0,0,0,0);
        List<TimeSpan> TimeDisplay = new List<TimeSpan>();
        //TimeSpan[] Czasy;
        double[] Czasy = new double[5];
        int d = 0;
        

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sek = new System.Timers.Timer(GetRandomNumber(1000,5000));
            sek.SynchronizingObject = this;
            sek.Elapsed += new System.Timers.ElapsedEventHandler(sek_Tick);
            sek.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (i == 0)
            {
                MessageBox.Show("Proste, prawda?\n A teraz skup się i zaczynamy... ");
                pictureBox1.Hide();
            }
            else
            {
                time2 = DateTime.Now;
                TimeSpan timeint = time2 - time1 ;
                TimeDisplay.Add(timeint);
				double value = (timeint.Hours + timeint.Minutes + timeint.Seconds + timeint.Milliseconds / 1000.0);
				Czasy[d] = value;
				d++;

				MessageBox.Show("Twoj czas to: " + timeint.Seconds + ":" + timeint.Milliseconds);
                if (i == 1) { textBox1.Text = timeint.ToString(); }
                if (i == 2) { textBox2.Text = timeint.ToString(); }
                if (i == 3) { textBox3.Text = timeint.ToString(); }
                if (i == 4) { textBox4.Text = timeint.ToString(); }
                if (i == 5) { textBox5.Text = timeint.ToString(); }
				
			}
                i++;
                pictureBox1.Hide();
                sek.AutoReset = true;
            if (i <= 5) { button1.PerformClick(); }
            else {
                TimeSpan timeaverage = TimeSpan.FromMilliseconds(TimeDisplay.Average(k => k.TotalMilliseconds));
                MessageBox.Show("Dziękuję za udział w teście\nTwój średni czas reakcji wynosi: "+timeaverage+"\nImie: "+l.imie);
                i = 0;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                if (!File.Exists("C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\wyniki_test1.txt"))
                {
                    StreamWriter sw = File.CreateText("C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\wyniki_test1.txt");
                    using (FileStream fs = new FileStream("C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\wyniki_test1.txt", FileMode.Append, FileAccess.Write))
                    using (StreamWriter sw1 = new StreamWriter(fs))
                    {
                        sw1.WriteLine("Imie: " + l.imie + " Średnia: " + timeaverage);
                        sw1.Close();
                    }
                }
                else
                {
                    using (FileStream fs = new FileStream("C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\wyniki_test1.txt", FileMode.Append, FileAccess.Write))
                    using (StreamWriter sw1 = new StreamWriter(fs))
                    {
                        sw1.WriteLine("Imie: " + l.imie + " Średnia:  " + timeaverage);
                        sw1.Close();
                    }
                }
                Chart();
                chart1.Show();
                chart1.Focus();
               
            }
        }

        private void sek_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            Random rand = new Random();
            int a = rand.Next(25, 336);
            int b = rand.Next(44, 326);
            pictureBox1.Location = new Point(a, b);
            pictureBox1.Show();
            time1 = DateTime.Now;
            sek.Stop();
        }


        private void closing(object sender, FormClosedEventArgs e)
        {
            start.Show();
        }
        private void Chart()
        {
            var series = new Series("czas");
            series.Points.DataBindXY(new[] { 1, 2, 3, 4, 5 }, new[] { Czasy[0], Czasy[1], Czasy[2], Czasy[3], Czasy[4] });
            chart1.Series.Add(series);
        }
        private void Chart_close(object sender, MouseEventArgs e)
        {	   
            this.Hide();
            start.Show();
        }
    }
}
