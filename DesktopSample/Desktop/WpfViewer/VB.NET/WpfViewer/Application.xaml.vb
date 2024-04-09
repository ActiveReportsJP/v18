Imports System.Text
Imports System.Windows

Class Application
	' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
	' can be handled in this file.
	Protected Overrides Sub OnStartup(e As StartupEventArgs)
		MyBase.OnStartup(e)
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
	End Sub
End Class
