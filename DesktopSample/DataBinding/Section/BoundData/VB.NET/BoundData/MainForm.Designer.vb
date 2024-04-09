 _
Partial Class MainForm
	Inherits System.Windows.Forms.Form



Private components As System.ComponentModel.Container = Nothing

Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.pnlOptions = New System.Windows.Forms.Panel()
        Me.tabDataBinding = New System.Windows.Forms.TabControl()
        Me.tabDataSet = New System.Windows.Forms.TabPage()
        Me.lblDataSet = New System.Windows.Forms.Label()
        Me.btnDataSet = New System.Windows.Forms.Button()
        Me.tabDataReader = New System.Windows.Forms.TabPage()
        Me.btnDataReader = New System.Windows.Forms.Button()
        Me.lblDataReader = New System.Windows.Forms.Label()
        Me.tabDataView = New System.Windows.Forms.TabPage()
        Me.cbCompanyName = New System.Windows.Forms.ComboBox()
        Me.lblDataViewOption = New System.Windows.Forms.Label()
        Me.btnDataView = New System.Windows.Forms.Button()
        Me.lblDataView = New System.Windows.Forms.Label()
        Me.tabDataTable = New System.Windows.Forms.TabPage()
        Me.lblDataTable = New System.Windows.Forms.Label()
        Me.btnDataTable = New System.Windows.Forms.Button()
        Me.tabSqliteDb = New System.Windows.Forms.TabPage()
        Me.btnSqliteDb = New System.Windows.Forms.Button()
        Me.lblSqliteDb = New System.Windows.Forms.Label()
        Me.tabXML = New System.Windows.Forms.TabPage()
        Me.btnXML = New System.Windows.Forms.Button()
        Me.btnGenerateXML = New System.Windows.Forms.Button()
        Me.lblXML = New System.Windows.Forms.Label()
        Me.tabCSV = New System.Windows.Forms.TabPage()
        Me.btnCSV = New System.Windows.Forms.Button()
        Me.rbtnHeader = New System.Windows.Forms.RadioButton()
        Me.rbtnNoHeader = New System.Windows.Forms.RadioButton()
        Me.rbtnHeaderTab = New System.Windows.Forms.RadioButton()
        Me.rbtnNoHeaderComma = New System.Windows.Forms.RadioButton()
        Me.lblCSVDelData = New System.Windows.Forms.Label()
        Me.lblFixWData = New System.Windows.Forms.Label()
        Me.lblCSV = New System.Windows.Forms.Label()
        Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
        Me.dlgSave = New System.Windows.Forms.SaveFileDialog()
        Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
        Me.pnlOptions.SuspendLayout()
        Me.tabDataBinding.SuspendLayout()
        Me.tabDataSet.SuspendLayout()
        Me.tabDataReader.SuspendLayout()
        Me.tabDataView.SuspendLayout()
        Me.tabDataTable.SuspendLayout()
        Me.tabSqliteDb.SuspendLayout()
        Me.tabXML.SuspendLayout()
        Me.tabCSV.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlOptions
        '
        Me.pnlOptions.Controls.Add(Me.tabDataBinding)
        resources.ApplyResources(Me.pnlOptions, "pnlOptions")
        Me.pnlOptions.Name = "pnlOptions"
        '
        'tabDataBinding
        '
        Me.tabDataBinding.Controls.Add(Me.tabDataSet)
        Me.tabDataBinding.Controls.Add(Me.tabDataReader)
        Me.tabDataBinding.Controls.Add(Me.tabDataView)
        Me.tabDataBinding.Controls.Add(Me.tabDataTable)
        Me.tabDataBinding.Controls.Add(Me.tabSqliteDb)
        Me.tabDataBinding.Controls.Add(Me.tabXML)
        Me.tabDataBinding.Controls.Add(Me.tabCSV)
        resources.ApplyResources(Me.tabDataBinding, "tabDataBinding")
        Me.tabDataBinding.Name = "tabDataBinding"
        Me.tabDataBinding.SelectedIndex = 0
        '
        'tabDataSet
        '
        Me.tabDataSet.Controls.Add(Me.lblDataSet)
        Me.tabDataSet.Controls.Add(Me.btnDataSet)
        resources.ApplyResources(Me.tabDataSet, "tabDataSet")
        Me.tabDataSet.Name = "tabDataSet"
        '
        'lblDataSet
        '
        resources.ApplyResources(Me.lblDataSet, "lblDataSet")
        Me.lblDataSet.Name = "lblDataSet"
        '
        'btnDataSet
        '
        resources.ApplyResources(Me.btnDataSet, "btnDataSet")
        Me.btnDataSet.Name = "btnDataSet"
        '
        'tabDataReader
        '
        Me.tabDataReader.Controls.Add(Me.btnDataReader)
        Me.tabDataReader.Controls.Add(Me.lblDataReader)
        resources.ApplyResources(Me.tabDataReader, "tabDataReader")
        Me.tabDataReader.Name = "tabDataReader"
        '
        'btnDataReader
        '
        resources.ApplyResources(Me.btnDataReader, "btnDataReader")
        Me.btnDataReader.Name = "btnDataReader"
        '
        'lblDataReader
        '
        resources.ApplyResources(Me.lblDataReader, "lblDataReader")
        Me.lblDataReader.Name = "lblDataReader"
        '
        'tabDataView
        '
        Me.tabDataView.Controls.Add(Me.cbCompanyName)
        Me.tabDataView.Controls.Add(Me.lblDataViewOption)
        Me.tabDataView.Controls.Add(Me.btnDataView)
        Me.tabDataView.Controls.Add(Me.lblDataView)
        resources.ApplyResources(Me.tabDataView, "tabDataView")
        Me.tabDataView.Name = "tabDataView"
        '
        'cbCompanyName
        '
        resources.ApplyResources(Me.cbCompanyName, "cbCompanyName")
        Me.cbCompanyName.Name = "cbCompanyName"
        '
        'lblDataViewOption
        '
        resources.ApplyResources(Me.lblDataViewOption, "lblDataViewOption")
        Me.lblDataViewOption.Name = "lblDataViewOption"
        '
        'btnDataView
        '
        resources.ApplyResources(Me.btnDataView, "btnDataView")
        Me.btnDataView.Name = "btnDataView"
        '
        'lblDataView
        '
        resources.ApplyResources(Me.lblDataView, "lblDataView")
        Me.lblDataView.Name = "lblDataView"
        '
        'tabDataTable
        '
        Me.tabDataTable.Controls.Add(Me.lblDataTable)
        Me.tabDataTable.Controls.Add(Me.btnDataTable)
        resources.ApplyResources(Me.tabDataTable, "tabDataTable")
        Me.tabDataTable.Name = "tabDataTable"
        '
        'lblDataTable
        '
        resources.ApplyResources(Me.lblDataTable, "lblDataTable")
        Me.lblDataTable.Name = "lblDataTable"
        '
        'btnDataTable
        '
        resources.ApplyResources(Me.btnDataTable, "btnDataTable")
        Me.btnDataTable.Name = "btnDataTable"
        '
        'tabSqliteDb
        '
        Me.tabSqliteDb.Controls.Add(Me.btnSqliteDb)
        Me.tabSqliteDb.Controls.Add(Me.lblSqliteDb)
        resources.ApplyResources(Me.tabSqliteDb, "tabSqliteDb")
        Me.tabSqliteDb.Name = "tabSqliteDb"
        '
        'btnSqliteDb
        '
        resources.ApplyResources(Me.btnSqliteDb, "btnSqliteDb")
        Me.btnSqliteDb.Name = "btnSqliteDb"
        '
        'lblSqliteDb
        '
        resources.ApplyResources(Me.lblSqliteDb, "lblSqliteDb")
        Me.lblSqliteDb.Name = "lblSqliteDb"
        '
        'tabXML
        '
        Me.tabXML.Controls.Add(Me.btnXML)
        Me.tabXML.Controls.Add(Me.btnGenerateXML)
        Me.tabXML.Controls.Add(Me.lblXML)
        resources.ApplyResources(Me.tabXML, "tabXML")
        Me.tabXML.Name = "tabXML"
        '
        'btnXML
        '
        resources.ApplyResources(Me.btnXML, "btnXML")
        Me.btnXML.Name = "btnXML"
        '
        'btnGenerateXML
        '
        resources.ApplyResources(Me.btnGenerateXML, "btnGenerateXML")
        Me.btnGenerateXML.Name = "btnGenerateXML"
        '
        'lblXML
        '
        resources.ApplyResources(Me.lblXML, "lblXML")
        Me.lblXML.Name = "lblXML"
        '
        'tabCSV
        '
        Me.tabCSV.BackColor = System.Drawing.SystemColors.Control
        Me.tabCSV.Controls.Add(Me.btnCSV)
        Me.tabCSV.Controls.Add(Me.rbtnHeader)
        Me.tabCSV.Controls.Add(Me.rbtnNoHeader)
        Me.tabCSV.Controls.Add(Me.rbtnHeaderTab)
        Me.tabCSV.Controls.Add(Me.rbtnNoHeaderComma)
        Me.tabCSV.Controls.Add(Me.lblCSVDelData)
        Me.tabCSV.Controls.Add(Me.lblFixWData)
        Me.tabCSV.Controls.Add(Me.lblCSV)
        resources.ApplyResources(Me.tabCSV, "tabCSV")
        Me.tabCSV.Name = "tabCSV"
        '
        'btnCSV
        '
        resources.ApplyResources(Me.btnCSV, "btnCSV")
        Me.btnCSV.Name = "btnCSV"
        Me.btnCSV.UseVisualStyleBackColor = True
        '
        'rbtnHeader
        '
        resources.ApplyResources(Me.rbtnHeader, "rbtnHeader")
        Me.rbtnHeader.Name = "rbtnHeader"
        Me.rbtnHeader.UseVisualStyleBackColor = True
        '
        'rbtnNoHeader
        '
        resources.ApplyResources(Me.rbtnNoHeader, "rbtnNoHeader")
        Me.rbtnNoHeader.Name = "rbtnNoHeader"
        Me.rbtnNoHeader.UseVisualStyleBackColor = True
        '
        'rbtnHeaderTab
        '
        resources.ApplyResources(Me.rbtnHeaderTab, "rbtnHeaderTab")
        Me.rbtnHeaderTab.Name = "rbtnHeaderTab"
        Me.rbtnHeaderTab.UseVisualStyleBackColor = True
        '
        'rbtnNoHeaderComma
        '
        resources.ApplyResources(Me.rbtnNoHeaderComma, "rbtnNoHeaderComma")
        Me.rbtnNoHeaderComma.Checked = True
        Me.rbtnNoHeaderComma.Name = "rbtnNoHeaderComma"
        Me.rbtnNoHeaderComma.TabStop = True
        Me.rbtnNoHeaderComma.UseVisualStyleBackColor = True
        '
        'lblCSVDelData
        '
        resources.ApplyResources(Me.lblCSVDelData, "lblCSVDelData")
        Me.lblCSVDelData.Name = "lblCSVDelData"
        '
        'lblFixWData
        '
        resources.ApplyResources(Me.lblFixWData, "lblFixWData")
        Me.lblFixWData.Name = "lblFixWData"
        '
        'lblCSV
        '
        resources.ApplyResources(Me.lblCSV, "lblCSV")
        Me.lblCSV.Name = "lblCSV"
        '
        'arvMain
        '
        Me.arvMain.BackColor = System.Drawing.SystemColors.Control
        Me.arvMain.CurrentPage = 0
        resources.ApplyResources(Me.arvMain, "arvMain")
        Me.arvMain.Name = "arvMain"
        Me.arvMain.PreviewPages = 0
        '
        '
        '
        '
        '
        '
        Me.arvMain.Sidebar.ParametersPanel.ContextMenu = Nothing
        Me.arvMain.Sidebar.ParametersPanel.Width = 200
        '
        '
        '
        Me.arvMain.Sidebar.SearchPanel.ContextMenu = Nothing
        Me.arvMain.Sidebar.SearchPanel.Width = 200
        '
        '
        '
        Me.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
        Me.arvMain.Sidebar.ThumbnailsPanel.Width = 200
        Me.arvMain.Sidebar.ThumbnailsPanel.Zoom = 0.1R
        '
        '
        '
        Me.arvMain.Sidebar.TocPanel.ContextMenu = Nothing
        Me.arvMain.Sidebar.TocPanel.Expanded = True
        Me.arvMain.Sidebar.TocPanel.Width = 200
        Me.arvMain.Sidebar.Width = 200
        '
        'MainForm
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.arvMain)
        Me.Controls.Add(Me.pnlOptions)
        Me.Name = "MainForm"
        Me.pnlOptions.ResumeLayout(False)
        Me.tabDataBinding.ResumeLayout(False)
        Me.tabDataSet.ResumeLayout(False)
        Me.tabDataReader.ResumeLayout(False)
        Me.tabDataView.ResumeLayout(False)
        Me.tabDataTable.ResumeLayout(False)
        Me.tabSqliteDb.ResumeLayout(False)
        Me.tabXML.ResumeLayout(False)
        Me.tabCSV.ResumeLayout(False)
        Me.tabCSV.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private pnlOptions As System.Windows.Forms.Panel
Private arvMain As GrapeCity.ActiveReports.Viewer.Win.Viewer
Private WithEvents tabDataBinding As System.Windows.Forms.TabControl
Private tabDataSet As System.Windows.Forms.TabPage
Private tabDataTable As System.Windows.Forms.TabPage
Private tabDataView As System.Windows.Forms.TabPage
Private tabDataReader As System.Windows.Forms.TabPage
	Private tabSqliteDb As System.Windows.Forms.TabPage
	Private tabXML As System.Windows.Forms.TabPage
	Private WithEvents btnDataSet As System.Windows.Forms.Button
	Private lblDataSet As System.Windows.Forms.Label
	Private WithEvents btnDataTable As System.Windows.Forms.Button
	Private lblDataTable As System.Windows.Forms.Label
	Private WithEvents btnDataView As System.Windows.Forms.Button
	Private lblDataViewOption As System.Windows.Forms.Label
	Private WithEvents cbCompanyName As System.Windows.Forms.ComboBox
	Private lblDataView As System.Windows.Forms.Label
	Private lblDataReader As System.Windows.Forms.Label
	Private WithEvents btnDataReader As System.Windows.Forms.Button
	Private lblSqliteDb As System.Windows.Forms.Label
	Private WithEvents btnSqliteDb As System.Windows.Forms.Button
	Private lblXML As System.Windows.Forms.Label
	Private WithEvents btnGenerateXML As System.Windows.Forms.Button
Private dlgSave As System.Windows.Forms.SaveFileDialog
Private dlgOpen As System.Windows.Forms.OpenFileDialog
	Private WithEvents btnXML As System.Windows.Forms.Button
	Friend WithEvents tabCSV As System.Windows.Forms.TabPage
	Friend WithEvents btnCSV As System.Windows.Forms.Button
	Friend WithEvents rbtnHeader As System.Windows.Forms.RadioButton
	Friend WithEvents rbtnNoHeader As System.Windows.Forms.RadioButton
	Friend WithEvents rbtnHeaderTab As System.Windows.Forms.RadioButton
	Friend WithEvents rbtnNoHeaderComma As System.Windows.Forms.RadioButton
	Friend WithEvents lblCSVDelData As System.Windows.Forms.Label
	Friend WithEvents lblFixWData As System.Windows.Forms.Label
	Friend WithEvents lblCSV As System.Windows.Forms.Label

End Class
