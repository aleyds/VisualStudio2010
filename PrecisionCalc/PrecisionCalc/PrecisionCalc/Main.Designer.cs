namespace PrecisionCalc
{
    partial class Main
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textCVSFileTxt = new System.Windows.Forms.TextBox();
            this.openCSVFileButton = new System.Windows.Forms.Button();
            this.openCSVFile = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "误差文件";
            // 
            // textCVSFileTxt
            // 
            this.textCVSFileTxt.Location = new System.Drawing.Point(78, 6);
            this.textCVSFileTxt.Name = "textCVSFileTxt";
            this.textCVSFileTxt.Size = new System.Drawing.Size(489, 21);
            this.textCVSFileTxt.TabIndex = 2;
            // 
            // openCSVFileButton
            // 
            this.openCSVFileButton.Location = new System.Drawing.Point(573, 6);
            this.openCSVFileButton.Name = "openCSVFileButton";
            this.openCSVFileButton.Size = new System.Drawing.Size(75, 23);
            this.openCSVFileButton.TabIndex = 3;
            this.openCSVFileButton.Text = "打开";
            this.openCSVFileButton.UseVisualStyleBackColor = true;
            this.openCSVFileButton.Click += new System.EventHandler(this.OnOpenCsvFileClick);
            // 
            // openCSVFile
            // 
            this.openCSVFile.FileName = "openFileDialog1";
            this.openCSVFile.Filter = "SCV文件|*.csv|所有文件|*.*";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 562);
            this.Controls.Add(this.openCSVFileButton);
            this.Controls.Add(this.textCVSFileTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Main";
            this.Text = "整体误差测量技术的齿距误差测量";
            this.Resize += new System.EventHandler(this.FormSizeChange);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textCVSFileTxt;
        private System.Windows.Forms.Button openCSVFileButton;
        private System.Windows.Forms.OpenFileDialog openCSVFile;
    }
}

