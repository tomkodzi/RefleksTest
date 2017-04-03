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
    public partial class Form4 : Form
    {
        Login l;
        public Form4(Login l)
        {
            this.l = l;
            InitializeComponent();
            chart1.Hide();
        }

        
        int number1;
        int number2;
        public static DateTime time1, time2, timeint;
        private static System.Timers.Timer sek = null;
        int i, j = 0;
        TimeSpan timesum = new TimeSpan(0, 0, 0, 0);
        List<TimeSpan> TimeDisplay = new List<TimeSpan>();
        Form1 start = new Form1();
        double[] Czasy = new double[5];
        int d = 0;


        private void button1_Click(object sender, EventArgs e)     //wyjście
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)  //start
        {

            Random random = new Random();
            number1 = random.Next(100, 999);
            number2 = random.Next(100, 999);
                                           
            label1.Text = number1.ToString();
            label2.Text = number2.ToString();
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

            sek = new System.Timers.Timer(500);
            sek.SynchronizingObject = this;
            sek.Elapsed += new System.Timers.ElapsedEventHandler(sek_Tick);
            sek.Start();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (number1==number2)
            {
                Compare(button, "=");
            }
            else if (number1>number2)
            {
                Compare(button, ">");
            }
            else if (number1<number2)
            {
                Compare(button, "<");
            }

        }

        private void Compare(object sender, String sign)
        {
            Button button = (Button)sender;
            if (button.Text == sign)
            {
                if (i == 0)
                {
                    MessageBox.Show("BRAWO! Teraz zaczyna się pomiar");
                    label1.Text = "";
                    label2.Text = "";
                }
                else
                {
                    time2 = DateTime.Now;
                    TimeSpan timeint = time2 - time1;
                    TimeDisplay.Add(timeint);
                    double value = (timeint.Hours + timeint.Minutes + timeint.Seconds + timeint.Milliseconds / 1000.0);
                    Czasy[d] = value;
                    d++;
                    MessageBox.Show("BRAWO!\n"+number1+sign+number2+"\nTwoj czas to: " + timeint.Seconds + ":" + timeint.Milliseconds);
                    label1.Text = "";
                    label2.Text = "";
                    if (i == 1) { textBox1.Text = timeint.ToString(); }
                    if (i == 2) { textBox2.Text = timeint.ToString(); }
                    if (i == 3) { textBox3.Text = timeint.ToString(); }
                    if (i == 4) { textBox4.Text = timeint.ToString(); }
                    if (i == 5) { textBox5.Text = timeint.ToString(); }
                    sek.AutoReset = true;  
                }

                i++;
                if (i <= 5) { button2.PerformClick(); }
               
                else {
                TimeSpan timeaverage = TimeSpan.FromMilliseconds(TimeDisplay.Average(k => k.TotalMilliseconds));
                MessageBox.Show("Dziękuję za udział w teście\nTwój średni czas reakcji wynosi: "+timeaverage+"\nLiczba błędów: "+j);
                i = 0;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                if (!File.Exists("C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\wyniki_test3.txt"))
                {
                    StreamWriter sw = File.CreateText("C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\wyniki_test3.txt");
                    using (FileStream fs = new FileStream("C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\wyniki_test3.txt", FileMode.Append, FileAccess.Write))
                    using (StreamWriter sw1 = new StreamWriter(fs))
                    {
                        sw1.WriteLine("Imie: " + l.imie+ " Średnia: "+timeaverage);
                        sw1.Close();
                    }
                }
                else
                {
                    using (FileStream fs = new FileStream("C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\wyniki_test3.txt", FileMode.Append, FileAccess.Write))
                    using (StreamWriter sw1 = new StreamWriter(fs))
                    {
                        
                        sw1.WriteLine("Imie: " + l.imie +" Średnia:  " + timeaverage);
                        sw1.Close();
                    }
                }
                Chart();
                chart1.Show();
                chart1.Focus();
              }
 
            }
            else
            { 
                MessageBox.Show("Spróbuj jeszcze raz");
                label1.Text = number1.ToString();
                label2.Text = number2.ToString();
                j++;
            }


           
        }

        private void sek_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            label1.Text = "";
            label2.Text = "";
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
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
