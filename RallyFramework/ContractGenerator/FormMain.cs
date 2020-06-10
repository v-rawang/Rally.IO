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
using Newtonsoft.Json;
using Rally.Lib.Protocol.Contract;
using Rally.Lib.Utility.Common;

namespace ContractGenerator
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private ContractCompiler contractCompiler = new ContractCompiler();

        private IDictionary<string, int[]> fieldMappins;

        private void saveAppState()
        {
            Dictionary<string, object> appState = new Dictionary<string, object>();

            appState.Add("Namespace", this.textBoxNamespace.Text);
            appState.Add("ClassName", this.textBoxClassname.Text);
            appState.Add("AssemblyName", this.textBoxAssemblyName.Text);
            appState.Add("OutputDir", this.textBoxOutputDir.Text);
            appState.Add("ShouldCompile", this.checkBoxShouldCompile.Checked);
            appState.Add("FieldMappings", this.fieldMappins);

            byte[] appStateBytes = CommonUtility.BinarySerialize(appState);

            using (FileStream fileStream = new FileStream("appState.dat", FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                fileStream.Write(appStateBytes, 0, appStateBytes.Length);
            }
        }

        private void loadAppState()
        {
            if (File.Exists("appState.dat"))
            {
                byte[] appStateBytes = new byte[1024];

                using (FileStream fileStream = new FileStream("appState.dat", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    appStateBytes = new byte[fileStream.Length];
                    fileStream.Read(appStateBytes, 0, appStateBytes.Length);
                }

                Dictionary<string, object> appState = CommonUtility.BinaryDeserialize(appStateBytes) as Dictionary<string, object>;

                if (appState != null && appState.Count > 0)
                {
                    this.textBoxAssemblyName.Text = appState["AssemblyName"].ToString();
                    this.textBoxClassname.Text = appState["ClassName"].ToString();
                    this.textBoxNamespace.Text = appState["Namespace"].ToString();
                    this.textBoxOutputDir.Text = appState["OutputDir"].ToString();
                    this.checkBoxShouldCompile.Checked = (bool)appState["ShouldCompile"];
                    this.fieldMappins = appState["FieldMappings"] as IDictionary<string, int[]>;

                    this.ucContractItem1.Populate(this.fieldMappins);
                }
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            this.ucContractItem1.ClassName = this.textBoxClassname.Text;
            this.ucContractItem1.Namespace = this.textBoxNamespace.Text;

            string meta = "";

            string code = this.ucContractItem1.Generate(out meta);

            string outputDir = this.textBoxOutputDir.Text;

            string classFileName = $"{outputDir}\\{this.ucContractItem1.ClassName}.cs", metaFileName = $"{outputDir}\\{this.ucContractItem1.ClassName}.json", assemblyName=$"{outputDir}\\{this.textBoxAssemblyName.Text}.dll";

            using (FileStream fileStream = new FileStream(classFileName, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(code);
                }
            }

            using (FileStream fileStream = new FileStream(metaFileName, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(meta);
                }
            }

            if (this.checkBoxShouldCompile.Checked)
            {
                this.contractCompiler.CompileContract(new string[] { classFileName }, assemblyName);

                MessageBox.Show($"3个文件（\"{classFileName}\",\"{metaFileName}\", \"{assemblyName}\"）创建成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"2个文件（\"{classFileName}\",\"{metaFileName}\"）创建成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }       
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialogOutputDir.ShowDialog() == DialogResult.OK)
            {
                this.textBoxOutputDir.Text = this.folderBrowserDialogOutputDir.SelectedPath;
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (this.openFileDialogOpenMetaJson.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.openFileDialogOpenMetaJson.FileName;
                string json = "";

                using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        json = streamReader.ReadToEnd();
                    }
                }

                JsonSerializer jsonSerializer = new JsonSerializer();
                System.Dynamic.ExpandoObject expando = jsonSerializer.Deserialize(new StringReader(json), typeof(System.Dynamic.ExpandoObject)) as System.Dynamic.ExpandoObject;

                if (expando != null)
                {
                    IDictionary<string, object> expandoDic = expando as IDictionary<string, object>;

                    this.fieldMappins = new Dictionary<string, int[]>();

                    object[] itemValues = null;

                    List<int> itemIntValueList = null;

                    foreach (var item in expandoDic)
                    {
                        itemValues = (item.Value as List<object>).ToArray();

                        itemIntValueList = new List<int>();

                        foreach (var val in itemValues)
                        {
                            itemIntValueList.Add(int.Parse(val.ToString()));
                        }

                        fieldMappins.Add(item.Key, itemIntValueList.ToArray());
                    }

                    this.ucContractItem1.Populate(fieldMappins);
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //this.ucContractItem1.Populate(null);

            this.loadAppState();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.saveAppState();
        }
    }
}
