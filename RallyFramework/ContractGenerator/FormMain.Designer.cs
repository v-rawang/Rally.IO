namespace ContractGenerator
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.textBoxNamespace = new System.Windows.Forms.TextBox();
            this.labelNamespace = new System.Windows.Forms.Label();
            this.labelClassname = new System.Windows.Forms.Label();
            this.textBoxClassname = new System.Windows.Forms.TextBox();
            this.ucContractItem1 = new ContractGenerator.UCContractItem();
            this.labelAssemblyName = new System.Windows.Forms.Label();
            this.textBoxAssemblyName = new System.Windows.Forms.TextBox();
            this.labelOutputDir = new System.Windows.Forms.Label();
            this.textBoxOutputDir = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialogOutputDir = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.openFileDialogOpenMetaJson = new System.Windows.Forms.OpenFileDialog();
            this.checkBoxShouldCompile = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(931, 13);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 0;
            this.buttonGenerate.Text = "生成";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // textBoxNamespace
            // 
            this.textBoxNamespace.Location = new System.Drawing.Point(167, 15);
            this.textBoxNamespace.Name = "textBoxNamespace";
            this.textBoxNamespace.Size = new System.Drawing.Size(100, 21);
            this.textBoxNamespace.TabIndex = 1;
            // 
            // labelNamespace
            // 
            this.labelNamespace.AutoSize = true;
            this.labelNamespace.Location = new System.Drawing.Point(96, 18);
            this.labelNamespace.Name = "labelNamespace";
            this.labelNamespace.Size = new System.Drawing.Size(65, 12);
            this.labelNamespace.TabIndex = 2;
            this.labelNamespace.Text = "命名空间：";
            // 
            // labelClassname
            // 
            this.labelClassname.AutoSize = true;
            this.labelClassname.Location = new System.Drawing.Point(272, 18);
            this.labelClassname.Name = "labelClassname";
            this.labelClassname.Size = new System.Drawing.Size(53, 12);
            this.labelClassname.TabIndex = 4;
            this.labelClassname.Text = "类名称：";
            // 
            // textBoxClassname
            // 
            this.textBoxClassname.Location = new System.Drawing.Point(331, 15);
            this.textBoxClassname.Name = "textBoxClassname";
            this.textBoxClassname.Size = new System.Drawing.Size(100, 21);
            this.textBoxClassname.TabIndex = 3;
            // 
            // ucContractItem1
            // 
            this.ucContractItem1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucContractItem1.ClassName = null;
            this.ucContractItem1.Location = new System.Drawing.Point(9, 53);
            this.ucContractItem1.Name = "ucContractItem1";
            this.ucContractItem1.Namespace = null;
            this.ucContractItem1.Size = new System.Drawing.Size(1075, 620);
            this.ucContractItem1.TabIndex = 5;
            // 
            // labelAssemblyName
            // 
            this.labelAssemblyName.AutoSize = true;
            this.labelAssemblyName.Location = new System.Drawing.Point(437, 18);
            this.labelAssemblyName.Name = "labelAssemblyName";
            this.labelAssemblyName.Size = new System.Drawing.Size(77, 12);
            this.labelAssemblyName.TabIndex = 7;
            this.labelAssemblyName.Text = "程序集名称：";
            // 
            // textBoxAssemblyName
            // 
            this.textBoxAssemblyName.Location = new System.Drawing.Point(520, 15);
            this.textBoxAssemblyName.Name = "textBoxAssemblyName";
            this.textBoxAssemblyName.Size = new System.Drawing.Size(112, 21);
            this.textBoxAssemblyName.TabIndex = 6;
            // 
            // labelOutputDir
            // 
            this.labelOutputDir.AutoSize = true;
            this.labelOutputDir.Location = new System.Drawing.Point(638, 20);
            this.labelOutputDir.Name = "labelOutputDir";
            this.labelOutputDir.Size = new System.Drawing.Size(65, 12);
            this.labelOutputDir.TabIndex = 9;
            this.labelOutputDir.Text = "输出目录：";
            // 
            // textBoxOutputDir
            // 
            this.textBoxOutputDir.Location = new System.Drawing.Point(709, 15);
            this.textBoxOutputDir.Name = "textBoxOutputDir";
            this.textBoxOutputDir.Size = new System.Drawing.Size(135, 21);
            this.textBoxOutputDir.TabIndex = 8;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(850, 13);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 10;
            this.buttonBrowse.Text = "浏览...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(9, 13);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 11;
            this.buttonOpen.Text = "打开";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // openFileDialogOpenMetaJson
            // 
            this.openFileDialogOpenMetaJson.FileName = "openFileDialog1";
            this.openFileDialogOpenMetaJson.Filter = "JSON(*.json)|*.json|所有文件(*.*)|*.*";
            // 
            // checkBoxShouldCompile
            // 
            this.checkBoxShouldCompile.AutoSize = true;
            this.checkBoxShouldCompile.Checked = true;
            this.checkBoxShouldCompile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShouldCompile.Location = new System.Drawing.Point(1024, 18);
            this.checkBoxShouldCompile.Name = "checkBoxShouldCompile";
            this.checkBoxShouldCompile.Size = new System.Drawing.Size(48, 16);
            this.checkBoxShouldCompile.TabIndex = 12;
            this.checkBoxShouldCompile.Text = "编译";
            this.checkBoxShouldCompile.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 685);
            this.Controls.Add(this.checkBoxShouldCompile);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.labelOutputDir);
            this.Controls.Add(this.textBoxOutputDir);
            this.Controls.Add(this.labelAssemblyName);
            this.Controls.Add(this.textBoxAssemblyName);
            this.Controls.Add(this.ucContractItem1);
            this.Controls.Add(this.labelClassname);
            this.Controls.Add(this.textBoxClassname);
            this.Controls.Add(this.labelNamespace);
            this.Controls.Add(this.textBoxNamespace);
            this.Controls.Add(this.buttonGenerate);
            this.Name = "FormMain";
            this.Text = "合约生成器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.TextBox textBoxNamespace;
        private System.Windows.Forms.Label labelNamespace;
        private System.Windows.Forms.Label labelClassname;
        private System.Windows.Forms.TextBox textBoxClassname;
        private UCContractItem ucContractItem1;
        private System.Windows.Forms.Label labelAssemblyName;
        private System.Windows.Forms.TextBox textBoxAssemblyName;
        private System.Windows.Forms.Label labelOutputDir;
        private System.Windows.Forms.TextBox textBoxOutputDir;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogOutputDir;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialogOpenMetaJson;
        private System.Windows.Forms.CheckBox checkBoxShouldCompile;
    }
}

