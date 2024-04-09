Imports System.IO
Imports System.Net.Http

Friend Module WebRequestHelper
    ''' <summary>
    ''' 指定したUrlからMemoryStreamに生データを読み込みします。
    ''' </summary>
    Public Sub DownloadDataAsync(ByVal url As String, ByVal timeoutMilliseconds As Integer, ByVal success As Action(Of MemoryStream, String), ByVal [error] As Action(Of Exception), ByVal Optional userAgent As String = Nothing)
        Using client As New HttpClient()

            If Not String.IsNullOrEmpty(userAgent) Then client.DefaultRequestHeaders.Add("User-Agent", userAgent)

            If timeoutMilliseconds > 0 Then
                client.Timeout = New TimeSpan(0, 0, 0, 0, timeoutMilliseconds)
            End If

            Dim task = client.GetAsync(url).ContinueWith(Sub(responseTask)
                                                             Try
                                                                 Dim response As HttpResponseMessage = responseTask.Result

                                                                 If Not response.IsSuccessStatusCode Then
                                                                     Dim errorTask = response.Content.ReadAsStringAsync()
                                                                     errorTask.Wait()

                                                                     Throw New Exception(errorTask.Result)
                                                                 End If

                                                                 Dim readTask = response.Content.ReadAsStreamAsync()
                                                                 readTask.Wait()

                                                                 'ブッファからデータをコピーします（データをコピーしない場合は、ブッファがオーバーフローされて、レスポンスが受信されません。）
                                                                 Dim stream = New MemoryStream()
                                                                 Using responseStream As Stream = readTask.Result
                                                                     If responseStream IsNot Nothing Then
                                                                         responseStream.CopyTo(stream)
                                                                         success(stream, response.Content.Headers.ContentType.MediaType)
                                                                     Else
                                                                         [error](New NullReferenceException(NameOf(responseStream)))
                                                                     End If
                                                                 End Using
                                                             Catch exception As Exception
                                                                 [error](exception)
                                                             End Try
                                                         End Sub)
            task.Wait()
        End Using
    End Sub
End Module

