
Imports System
Imports System.Text
Imports System.Windows.Forms


Namespace IListBindingSample
   
   Class Program
	  
	  <STAThread()>  _
	  Public Shared Sub Main()
		  Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
		  Application.Run(New BindIListToDataGridSample())
	  End Sub 'Main
   End Class 'Program
End Namespace 'IListBindingSample
