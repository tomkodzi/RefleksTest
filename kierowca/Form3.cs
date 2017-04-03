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
    public partial class Form3 : Form
    {
        Login l;
        public static DateTime time1, time2, timeint;
        private static System.Timers.Timer sek = null;
        int i, j = 0;
        TimeSpan timesum = new TimeSpan(0, 0, 0, 0);
        public static string nazwa;

       // Form1 start = new Form1(l);
        Double Interval;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        List<TimeSpan> TimeDisplay = new List<TimeSpan>();
        double[] Czasy = new double[6];
        int d = 0;
        
        public Form3(Login l)
        {
            this.l = l;
            InitializeComponent();
            chart1.Hide();
        }

        Form1 start = new Form1();
        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void button1_Click(object sender, EventArgs e) // kliknięcie start
        {
            if (j == 0) MessageBox.Show("Najpierw spróbuj swoich sił bez pomiaru czasu.");
            Interval = GetRandomNumber(1000, 4000);
            sek = new System.Timers.Timer(Interval);
            sek.SynchronizingObject = this;
            sek.Elapsed += new System.Timers.ElapsedEventHandler(sek_Tick);
            sek.Start();
        }

        private void button2_Click(object sender, EventArgs e)   // kliknięcie wyjscie
        {
            Application.Exit();
        }

        private void button_click(object sender, EventArgs e) // buttony do rozpoznawania dźwięków
        {
            Button button = (Button)sender;

            if (Interval <= 2000)
            {
                buttony(button, "Burza");
            }
            else if (Interval > 2000 && Interval <= 3000)
            {
                buttony(button, "Silnik");
            }
            else if (Interval > 3000 && Interval <= 4000)
            {
                buttony(button, "Suszarka");
            }

        }

        private void sek_Tick(object sender, System.Timers.ElapsedEventArgs e)  // tyknięcie timera
        {
            if (Interval <= 2000)
            {
                playSound(@"C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\burza.wav");
                time1 = DateTime.Now;
                sek.Stop();
            }
            else if (Interval > 2000 && Interval <= 3000)
            {
                playSound(@"C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\silnik.wav");
                time1 = DateTime.Now;
                sek.Stop();
            }
            else if (Interval > 3000 && Interval <= 4000)
            {
                playSound(@"C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\suszarka.wav");
                time1 = DateTime.Now;
                sek.Stop();
            }
        }

        private void playSound(string path)  // funkcja odtwrazająca dźwięk
        {
            player.SoundLocation = path;
            player.Load();
            player.Play();
        }

        private void buttony(object sender, String arg)
        {
            Button button = (Button)sender;
            if (button.Text == arg)
            {
                player.Stop();
                time2 = DateTime.Now;
                TimeSpan timeint = time2 - time1;
                TimeDisplay.Add(timeint);
                double value = (timeint.Hours + timeint.Minutes + timeint.Seconds + timeint.Milliseconds / 1000.0);
                Czasy[d] = value;
                d++;
                if (j != 0) { MessageBox.Show("Twoj czas to: " + timeint.Seconds + ":" + timeint.Milliseconds); }
                else MessageBox.Show("Proste, prawda?\nNo to skup sie i zaczynamy ");
                if (j == 1) { textBox1.Text = timeint.ToString(); }
                if (j == 2) { textBox2.Text = timeint.ToString(); }
                if (j == 3) { textBox3.Text = timeint.ToString(); }
                if (j == 4) { textBox4.Text = timeint.ToString(); }
                if (j == 5) { textBox5.Text = timeint.ToString(); }
                sek.AutoReset = true;
                j++;
                if (j <= 5) { button1.PerformClick(); }
                else
                {
                    TimeSpan timeaverage = TimeSpan.FromMilliseconds(TimeDisplay.Average(k => k.TotalMilliseconds));
                    MessageBox.Show("Dziękuję za udział w teście\nTwój średni czas reakcji wynosi: " + timeaverage + "\nLiczba błędów: " + i);
                    i = 0;
                    j = 0;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    if (!File.Exists("C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\wyniki_test2.txt"))
                    {
                        StreamWriter sw = File.CreateText("C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\wyniki_test2.txt");
                        using (FileStream fs = new FileStream("C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\wyniki_test2.txt", FileMode.Append, FileAccess.Write))
                        using (StreamWriter sw1 = new StreamWriter(fs))
                        {
                            sw1.WriteLine("Imie: " + l.imie+" Średnia: " + timeaverage);
                            sw1.Close();

                        }
                    }
                    else
                    {
                        using (FileStream fs = new FileStream("C:\\Users\\Tomek\\Documents\\Visual Studio 2015\\Projects\\kierowca2\\kierowca\\wyniki_test2.txt", FileMode.Append, FileAccess.Write))
                        using (StreamWriter sw1 = new StreamWriter(fs))
                        {
                            sw1.WriteLine("Imie: " + l.imie+" Średnia:  " + timeaverage);
                            sw1.Close();
                        }
                    }
                    Chart();
                    chart1.Show();
                    chart1.Focus();
                }
            }
            else { MessageBox.Show("Źle! Spróbuj jeszcze raz... "); i++; }
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
