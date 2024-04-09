using System;
using System.IO;
using System.Linq;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Web.Designer;
using WebDesignerCustomStore.Implementation.CustomStore;
using WebDesignerCustomStore.Implementation.Storage;

namespace WebDesignerCustomStore.Implementation
{
	public class ResourceProvider : IResourceRepositoryProvider
	{
		private readonly ICustomStorage _store;

		public ResourceProvider(ICustomStorage store)
		{
			_store = store;
		}
		
		public Stream GetResource(ResourceInfo resource)
		{
			return resource.Type switch
			{
				ResourceType.Image => _store.GetImage(resource.Name),
				ResourceType.Theme => _store.GetTheme(resource.Name),
				ResourceType.ReportTemplate => _store.GetTemplate(resource.Name),
				ResourceType.DataSetTemplate => DataSetTemplatesStore.GetDataSet(resource.Name),
				_ => Stream.Null
			};
		}

		public ResourceDescriptor[] ListResources(ResourceType resourceType)
		{
			return resourceType switch
			{
				ResourceType.Image => _store.GetImagesList().ToArray(),
				ResourceType.Theme => _store.GetThemesList().ToArray(),
				ResourceType.ReportTemplate => _store.GetTemplatesList().ToArray(),
				ResourceType.DataSetTemplate => DataSetTemplatesStore.GetDataSetsList(),
				_ => Enumerable.Empty<ResourceDescriptor>().ToArray()
			};
		}

		public ResourceDescriptor[] DescribeResources(ResourceInfo[] resources)
		{
			var resource = resources.FirstOrDefault();
			
			return resource.Type switch
			{
				ResourceType.ReportTemplate => new []{ _store.GetTemplatesDescriptor(resource.Name) },
				_ => Enumerable.Empty<ResourceDescriptor>().ToArray()
			};
		}
	}
}