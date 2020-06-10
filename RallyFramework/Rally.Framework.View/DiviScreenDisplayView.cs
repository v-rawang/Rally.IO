using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rally.Framework.View
{
    public class DiviScreenDisplayView : TableLayoutPanel
    {
        private int cameraCount;
        private IList<string> cameras;
        private IDictionary<string, PictureBox> screens;

        public DiviScreenDisplayView() : base()
        {
            
        }

        public int CameraCount { get => this.cameraCount; set => this.cameraCount = value; }
        public IDictionary<string, PictureBox> Screens { get => this.screens; }

        public void Compute()
        {

        }
    }
}
