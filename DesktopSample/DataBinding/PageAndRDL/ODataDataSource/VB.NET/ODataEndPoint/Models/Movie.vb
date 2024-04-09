Imports System.ComponentModel.DataAnnotations

Public Class Movie
	<Key>
	Public Property ID() As Long
	Public Property Title() As String
	Public Property MPAA() As String
	Public Property YearReleased() As Long
End Class
