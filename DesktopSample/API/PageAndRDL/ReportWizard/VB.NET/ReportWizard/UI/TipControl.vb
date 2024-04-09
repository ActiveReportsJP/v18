Imports System.ComponentModel
Imports System.Windows.Forms
Partial Public Class TipControl
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub
    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overloads Overrides Property Text() As String
        Get
            Return tipText.Text
        End Get
        Set(ByVal value As String)
            tipText.Text = value
        End Set
    End Property
End Class
