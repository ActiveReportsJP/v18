namespace ActiveReports.Samples.CustomDataProviderUI
{
	partial class CSVFileSelector
	{
		/// <summary>
		///  デザイナ変数を必要とした。

		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。

		/// </summary>
		///<param name="disposing">真マネージリソースが破棄されるべきである場合はtrue、それ以外の場合はfalse</ PARAM>

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}



		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CSVFileSelector));
			this.btnSelectFile = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnSelectFile
			// 
			resources.ApplyResources(this.btnSelectFile, "btnSelectFile");
			this.btnSelectFile.Name = "btnSelectFile";
			this.btnSelectFile.UseVisualStyleBackColor = true;
			this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
			// 
			// CSVFileSelector
			// 
			this.BackColor = System.Drawing.Color.DeepSkyBlue;
			this.Controls.Add(this.btnSelectFile);
			this.Name = "CSVFileSelector";
			resources.ApplyResources(this, "$this");
			this.ResumeLayout(false);

		}


		private System.Windows.Forms.Button btnSelectFile;
	}
}
