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
    public partial class FlowDataItemMenuControl : FlowLayoutPanel
    {
        public FlowDataItemMenuControl()
        {
            InitializeComponent();
        }

        public Dictionary<string, string> DataItems { get; set; }
        public Func<object, object> ItemAction { get; set; }

        public void DrawItems()
        {
            if (this.DataItems != null && this.DataItems.Count > 0)
            {
                Button button;
                foreach (string key in this.DataItems.Keys)
                {
                    button = new Button()
                    {
                        Text = this.DataItems[key],
                        Tag = key
                    };

                    button.Click += (s, e) => { if (this.ItemAction != null) { this.ItemAction(key); this.Visible = false; } };
                    this.Controls.Add(button);
                }
            }
        }

        public void DrawItems<T>(Func<T, string, string, T> ExtensionFunction) where T : Control
        {
            if (this.DataItems != null && this.DataItems.Count > 0)
            {
                T control = default(T);

                foreach (string key in this.DataItems.Keys)
                {
                    //control =  new T() { Text = this.DataItems[key], Tag = key };
                    //control.Click += (s, e) => { if (this.ItemAction != null) { this.ItemAction(key); this.Visible = false; } };

                    if (ExtensionFunction != null)
                    {
                       control =  ExtensionFunction(control, key, this.DataItems[key]);
                    }

                    if (control != null)
                    {
                        control.Click += (s, e) => { if (this.ItemAction != null) { this.ItemAction(key); this.Visible = false; } };
                        this.Controls.Add(control);
                    }                    
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {        
            base.OnPaint(pe);
        }
    }
}
