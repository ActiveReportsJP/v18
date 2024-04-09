using System;
using System.IO;
using System.Net.Http;

namespace ActiveReports.Samples.CustomTileProviders
{
	internal static class WebRequestHelper
	{
		/// <summary>
		/// 指定したUrlからMemoryStreamに生データを読み込みします。
		/// </summary>
		public static void DownloadDataAsync(string url, int timeoutMilliseconds, Action<MemoryStream, string> success, Action<Exception> error, string userAgent = null)
		{
			using (var client = new HttpClient())
			{

				if (!string.IsNullOrEmpty(userAgent))
					client.DefaultRequestHeaders.Add("User-Agent", userAgent);

				if (timeoutMilliseconds > 0)
				{
					client.Timeout = new TimeSpan(0, 0, 0, 0, timeoutMilliseconds);
				}

				var task = client.GetAsync(url).ContinueWith((responseTask) =>
				{
					try
					{
						var response = responseTask.Result;

						if (!response.IsSuccessStatusCode)
						{
							var errorTask = response.Content.ReadAsStringAsync();
							errorTask.Wait();

							throw new Exception(errorTask.Result);
						}

						var readTask = response.Content.ReadAsStreamAsync();
						readTask.Wait();

						//ブッファからデータをコピーします（データをコピーしない場合は、ブッファがオーバーフローされて、レスポンスが受信されません。）
						var stream = new MemoryStream();
						using (Stream responseStream = readTask.Result)
						{
							if (responseStream != null)
							{
								responseStream.CopyTo(stream);
								success(stream, response.Content.Headers.ContentType.MediaType);
							}
							else
							{
								error(new NullReferenceException(nameof(responseStream)));
							}
						}
					}
					catch (Exception exception)
					{
						error(exception);
					}
				});

				task.Wait();
			}

		}
	}
}
