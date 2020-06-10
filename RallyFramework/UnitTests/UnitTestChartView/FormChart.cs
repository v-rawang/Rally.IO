using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitTestChartView
{
    public partial class FormChart : Form
    {
        public FormChart()
        {
            InitializeComponent();
        }

        public void Populate()
        {
            for (int i = 0; i < 1204; i++)
            {
                this.chart1.Series[0].Points.AddXY(DateTime.Now, i);
            }
        }

        private System.Timers.Timer timer;

        private void FormChart_Load(object sender, EventArgs e)
        {
            //this.Populate();

            this.timer = new System.Timers.Timer(1000) { Enabled = true };
            this.timer.Elapsed += (s, ea) => {
                this.Invoke(new Action(()=> {
                    this.chart1.Series[0].Points.AddXY(ea.SignalTime, ea.SignalTime.Second);
                }));      
            };
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => {
                this.chart1.Series[0].Points.Clear();
            }));     
        }

        private void buttonDisplay_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(()=> {
                this.Populate();
            }));           
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Points.Clear();
            this.timer.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.timer.Stop();
        }
    }
}
