Public Class WebForm1
	Inherits Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		messageLabel.Text = My.Resources.Resource.bodyOfMessage
	End Sub
End Class
