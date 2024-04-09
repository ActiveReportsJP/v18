namespace ActiveReports.Samples.TestDesignerPro
{
	partial class HelperForm
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// デザイナサポートに必要なメソッド - 変更しない
		/// コードエディタでは、このメソッドの内容を表示します。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelperForm));
			this.rtfHelp = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// rtfHelp
			// 
			resources.ApplyResources(this.rtfHelp, "rtfHelp");
			this.rtfHelp.Name = "rtfHelp";
			this.rtfHelp.ReadOnly = true;
			this.rtfHelp.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtfHelp_LinkClicked);
			this.rtfHelp.TextChanged += new System.EventHandler(this.rtfHelp_TextChanged);
			// 
			// HelperForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.rtfHelp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "HelperForm";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HelperForm_FormClosing);
			this.ResumeLayout(false);

		}
		#endregion
		private System.Windows.Forms.RichTextBox rtfHelp;
	}
}
