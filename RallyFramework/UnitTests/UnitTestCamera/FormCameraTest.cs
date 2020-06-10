using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Rally.Lib.Camera.Core;
using Rally.Lib.Camera.Core.Parameter;
using Rally.Lib.Camera.Facade;

namespace UnitTestCamera
{
    public partial class FormCameraTest : Form
    {
        public FormCameraTest()
        {
            InitializeComponent();

            this.cameraMetaItemDict = CameraFacade.LoadMeta("cameras.xml");

            if (this.cameraMetaItemDict != null)
            {
                this.cameraCatalog = new List<string>();
                this.cameraInstanceCache = new Dictionary<string, IVideoCamera>();
                this.cameraParameterDict = new Dictionary<string, IPCameraParameter>();

                this.cameraMeta = new CameraMeta() { MetaItems = new List<CameraMetaItem>() };

                IVideoCamera cameraInst = null;
                IPCameraParameter cameraParam = null;
                string cameraAssemblyFilePath, cameraParamFilePath;

                foreach (string key in this.cameraMetaItemDict.Keys)
                {
                    this.cameraMeta.MetaItems.Add(this.cameraMetaItemDict[key]);

                    this.cameraCatalog.Add(key);

                    cameraAssemblyFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}{this.cameraMetaItemDict[key].AssemblyFilePath}";

                    cameraInst = CameraFacade.LoadCamera(cameraAssemblyFilePath, this.cameraMetaItemDict[key].TypeName);

                    //if (cameraInst != null)
                    //{
                    //    this.cameraInstanceCache.Add(key, cameraInst);
                    //}

                    this.cameraInstanceCache.Add(key, cameraInst);

                    cameraParamFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}{this.cameraMetaItemDict[key].AssemblyName}.Parameter.xml";

                    if (File.Exists(cameraParamFilePath))
                    {
                        cameraParam = CameraFacade.LoadParameter(cameraParamFilePath);
                        this.cameraParameterDict.Add(key, cameraParam);
                    }
                    else
                    {
                        cameraParam = new IPCameraParameter();
                        CameraFacade.SaveParameter(cameraParam, cameraParamFilePath);
                        this.cameraParameterDict.Add(key, cameraParam);
                    }                            
                }
            }
        }

        private Bitmap bitmap;

        private string currentImageFile = "";

        private BindingSource bindingSource = null;

        private Dictionary<string, string> imageUrlMappings = new Dictionary<string, string>();

        private string currentCameraName;
        private bool isCurrentCameraInitialized = false;
        private int currentWorkingStatus = 0;
        private IVideoCamera currentCamera;
        private IPCameraParameter currentCameraParameter;
        private IList<string> cameraCatalog;
        private CameraMeta cameraMeta;
        private IDictionary<string, CameraMetaItem> cameraMetaItemDict;
        private IDictionary<string, IPCameraParameter> cameraParameterDict;   
        private IDictionary<string, IVideoCamera> cameraInstanceCache;

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.currentCameraName) && this.currentCameraParameter != null)
            {
                string cameraParamFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}{this.cameraMetaItemDict[this.currentCameraName].AssemblyName}.Parameter.xml";
                CameraFacade.SaveParameter(this.currentCameraParameter, cameraParamFilePath);
            }
        }

        private void barcodeReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void dataGridViewRecentImages_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void pictureBoxAcquiredImage_DoubleClick(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            if (this.openFileDialogOpenParameters.ShowDialog(this) == DialogResult.OK)
            {
                this.currentCameraParameter = CameraFacade.LoadParameter(this.openFileDialogOpenParameters.FileName);

                this.propertyGridParameters.SelectedObject = this.currentCameraParameter;
                this.propertyGridParameters.Refresh();
            }
        }

        private void toolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialogSaveParameters.ShowDialog(this) == DialogResult.OK)
            {
                CameraFacade.SaveParameter(this.currentCameraParameter, this.saveFileDialogSaveParameters.FileName);

                MessageBox.Show($"当前摄像头（{this.currentCameraName}）的配置参数已成功保存到：\"{this.saveFileDialogSaveParameters.FileName}\"", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripMenuItemNew_Click(object sender, EventArgs e)
        {
            FormCameraMeta formCameraMeta = new FormCameraMeta(this.cameraMeta);
            formCameraMeta.ShowDialog(this);
        }

        private void xiViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.setControls(true);

            if (!this.isCurrentCameraInitialized)
            {
                this.isCurrentCameraInitialized = this.currentCamera.Initialize(this.currentCameraParameter);
            }

            currentWorkingStatus = 1;

            this.currentCamera.Preview(this.pictureBoxAcquiredImage.Handle, null);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.setControls(true);

            if (!this.isCurrentCameraInitialized)
            {
                this.isCurrentCameraInitialized = this.currentCamera.Initialize(this.currentCameraParameter);
            }

            currentWorkingStatus = 2;

            //this.currentCamera.RealPlay(null);

            this.currentCamera.RealPlay((p) => { p = this.pictureBoxAcquiredImage.Handle; return p; });
        }

        private void recordToolStripButton_Click(object sender, EventArgs e)
        {
            this.setControls(true);

            if (!this.isCurrentCameraInitialized)
            {
                this.isCurrentCameraInitialized = this.currentCamera.Initialize(this.currentCameraParameter);
            }

            if (this.saveFileDialogRecord.ShowDialog(this) == DialogResult.OK)
            {
                currentWorkingStatus = 3;

                this.currentCamera.Record(this.pictureBoxAcquiredImage.Handle, this.saveFileDialogRecord.FileName, null);
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isStoped = false;

            if (this.currentWorkingStatus > 0)
            {             
                switch (this.currentWorkingStatus)
                {
                    case 1:
                       isStoped = this.currentCamera.StopPreview();
                        break;
                    case 3:
                        isStoped = this.currentCamera.StopRecord();
                        break;
                    default:
                        break;
                }
            }

            if (isStoped)
            {
                currentWorkingStatus = 0;
                this.setControls(false);
            }       
        }

        private void toolStripButtonCapture_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialogCapture.ShowDialog(this) == DialogResult.OK)
            {
                this.currentCamera.Capture(this.pictureBoxAcquiredImage.Handle, this.saveFileDialogCapture.FileName, null);
            }
        }

        private void comboBoxParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.isCurrentCameraInitialized && this.currentCamera.Close())
            {
                this.isCurrentCameraInitialized = false;
            }

            this.currentCameraName = this.cameraCatalog[this.comboBoxParameters.SelectedIndex];
            this.currentCameraParameter = this.cameraParameterDict[this.currentCameraName];
            this.currentCamera = this.cameraInstanceCache[this.currentCameraName];
            this.propertyGridParameters.SelectedObject = this.currentCameraParameter;//this.parameters[this.comboBoxParameters.SelectedIndex];
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.saveParameters("parameters.xml");

            if (!String.IsNullOrEmpty(this.currentCameraName) && this.currentCameraParameter != null)
            {
                string cameraParamFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}{this.cameraMetaItemDict[this.currentCameraName].AssemblyName}.Parameter.xml";

                CameraFacade.SaveParameter(this.currentCameraParameter, cameraParamFilePath);
            }     

            this.Close();
        }

        private void FormCameraTest_Load(object sender, EventArgs e)
        {
            this.comboBoxParameters.DataSource = this.cameraCatalog;
            this.comboBoxParameters.SelectedIndex = 0;

            this.comboBoxParameters_SelectedIndexChanged(sender, e);

            //if (this.currentCameraParameter == null)
            //{
            //    this.currentCameraParameter = new IPCameraParameter();
            //}

            this.propertyGridParameters.SelectedObject = this.currentCameraParameter;

            //if (this.currentCamera != null)
            //{
            //   this.isCurrentCameraInitialized = this.currentCamera.Initialize(this.currentCameraParameter);
            //}
        }


        private void setControls(bool isDoingTasks)
        {
            this.startToolStripMenuItem.Enabled = !isDoingTasks;
            this.stopToolStripMenuItem.Enabled = isDoingTasks;
            this.toolStripButtonStart.Enabled = !isDoingTasks;
            this.toolStripButtonStop.Enabled = isDoingTasks;
            this.toolStripButtonTest.Enabled = !isDoingTasks;
            this.testToolStripMenuItem.Enabled = !isDoingTasks;
            this.newToolStripButton.Enabled = !isDoingTasks;
            this.openToolStripButton.Enabled = !isDoingTasks;
            this.toolStripMenuItemNew.Enabled = !isDoingTasks;
            this.toolStripMenuItemOpen.Enabled = !isDoingTasks;

            //this.xiViewerToolStripMenuItem.Enabled = !isDoingTasks;
            //this.frequencyDetectorToolStripMenuItem.Enabled = !isDoingTasks;
            //this.barcodeReaderToolStripMenuItem.Enabled = !isDoingTasks;
            this.toolStripButtonRecord.Enabled = !isDoingTasks;
            this.frequencyDetectorToolStripButton.Enabled = !isDoingTasks;
            this.toolStripButtonCapture.Enabled = !isDoingTasks;
            this.recordToolStripMenuItem.Enabled = !isDoingTasks;
            this.toolStripButtonRecord.Enabled = !isDoingTasks;

            this.toolStripProgressBarCurrentProgress.Visible = isDoingTasks;
            //this.toolStripStatusLabelCurrentStatus.Visible = isDoingTasks;
        }
    }
}
