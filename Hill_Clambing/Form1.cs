using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace hill_climbing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var decimals = comboBox1.SelectedIndex + 3;
            var dok = Math.Pow(10, (-1 * decimals));
            var a = Int32.Parse(textBox1.Text);
            var b = Int32.Parse(textBox2.Text);
            var T = Int32.Parse(textBox3.Text);
            Konwersja konwersja = new Konwersja();
            Random rand = new Random();
            HillClimbing hc = new HillClimbing();

           var best = hc.Wyzazanie(a, b, decimals, dok, T);

            chart1.Series.Clear();

            chart1.Series.Add("f(xbest)");
            chart1.Series["f(xbest)"].ChartType = SeriesChartType.Line;
            chart1.Series.Add("f(vm)");
            chart1.Series["f(vm)"].ChartType = SeriesChartType.Line;
            for (int i = 0; i < hc.najlepszy.Count; i++)
            {
                chart1.Series["f(xbest)"].Points.AddY(hc.najlepszy[i]);
                chart1.Series["f(vm)"].Points.AddY(hc.VMs[i]);
            }


            label6.Text = "Najlepszy otrzymany wynik to - xreal:"+ konwersja.ToReal(a, b, konwersja.ToIntFromBin(best), dok, decimals)+" feval:"+ konwersja.Feval(konwersja.ToReal(a, b, konwersja.ToIntFromBin(best), dok, decimals))+ " xbin:"+ best;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tests te = new Tests(1000);
            var results = te.DoTheTesting();

            chart2.Series.Clear();

            chart2.Series.Add("%");
            chart2.Series["%"].ChartType = SeriesChartType.Column;

            for (int i = 0; i < results.Length; i++)
            {
                chart2.Series["%"].Points.AddY(results[i]);
            }

        }
    }
    public struct Possible
    {
        public int bitToChange;
        public double feval;
    }
}
