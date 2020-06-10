using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rally.Framework.View
{
    public partial class MessageAlertBox : Form
    {
        public MessageAlertBox()
        {
            InitializeComponent();
        }

        private System.Timers.Timer timer;
        private AlertType alertType;
        private int duration;
        private string message;

        public MessageAlertBox(string Message, AlertType AlertType, int Duration)
        {
            this.message = Message;
            this.duration = Duration;
            this.alertType = AlertType;

            InitializeComponent();
        }

        public static DialogResult Show(string Message, AlertType AlertType = AlertType.Info, int Duration = 3)
        {
           return new MessageAlertBox(Message, AlertType, Duration).ShowDialog();
        }

        public AlertType AlertType { get=>this.alertType; set=>this.alertType=value; }
        public string Message { get=>this.message; set=>this.message=value; }
        public int Duration { get=>this.duration; set=>this.duration=value; }

        private void MessageAlertBox_Load(object sender, EventArgs e)
        {
            this.labelMessageBody.Text = this.message;

            this.timer = new System.Timers.Timer(1000) { Enabled = true };
            this.timer.Elapsed += (s, ea) => {
                this.Invoke(new Action(() => {
                    this.labelTime.Text = $"{this.duration}";
                }));
                
                if (this.duration == 0)
                {
                    this.timer.Stop();

                    this.Invoke(new Action(() => {
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }));                
                }
                else
                {
                    this.duration--;
                }
            };
        }
    }

    public enum AlertType
    {
        Info = 0,
        Alarm = 1,
        Fatal = 2
    }
}
