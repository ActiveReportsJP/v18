namespace ActiveReports.Samples.ReportWizard.UI.WizardSteps 
{
	partial class BaseStep
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseStep));
			this.SuspendLayout();
			// 
			// BaseStep
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.MaximumSize = new System.Drawing.Size(638, 270);
			this.MinimumSize = new System.Drawing.Size(638, 270);
			this.Name = "BaseStep";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
