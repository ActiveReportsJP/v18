﻿namespace ActiveReports.Samples.ReportWizard.UI
{
	partial class TipControl
	{
		/// <summary> 
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}
		#region Component Designer generated code
		/// <summary>
		/// デザイナサポートに必要なメソッド - 変更しない
		/// コードエディタでは、このメソッドの内容を表示します。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TipControl));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.tipText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::ActiveReports.Samples.ReportWizard.Properties.Resources.Info_02;
			resources.ApplyResources(this.pictureBox1, "pictureBox1");
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.TabStop = false;
			// 
			// tipText
			// 
			resources.ApplyResources(this.tipText, "tipText");
			this.tipText.Name = "tipText";
			// 
			// TipControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tipText);
			this.Controls.Add(this.pictureBox1);
			this.Name = "TipControl";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label tipText;
	}
}
