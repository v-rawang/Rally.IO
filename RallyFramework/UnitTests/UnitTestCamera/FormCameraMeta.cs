using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rally.Lib.Camera.Core;
using Rally.Lib.Camera.Core.Parameter;
using Rally.Lib.Camera.Facade;

namespace UnitTestCamera
{
    public partial class FormCameraMeta : Form
    {
        public FormCameraMeta()
        {
            InitializeComponent();
        }

        public FormCameraMeta(CameraMeta Meta)
        {
            InitializeComponent();

            this.cameraMeta = Meta;
            this.metaItem = new CameraMetaItem();
        }

        private CameraMeta cameraMeta;
        private CameraMetaItem metaItem;

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void FormCameraMeta_Load(object sender, EventArgs e)
        {
            this.propertyGridCameraMeta.SelectedObject = this.cameraMeta;
        }
    }
}
