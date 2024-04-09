using ActiveReports.Samples.CustomTileProviders.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveReports.Samples.CustomTileProviders
{
	public abstract class BaseTileProvider
	{
		private string GetCopyright()
		{
			var titleNameKey = GetType().Name + "_TileProviderCopyright";
			var title = Resources.ResourceManager.GetString(titleNameKey);
			if (!string.IsNullOrEmpty(title) && title != titleNameKey)
			{
				return string.Format(title, DateTime.Now.Year);
			}

			return string.Empty;
		}

		public virtual string Copyright => GetCopyright();
	}
}
