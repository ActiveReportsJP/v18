using System.IO;
using System.Linq;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Rendering.Tools;
using GrapeCity.ActiveReports.Web.Designer;

namespace WebDesigner_CustomSharedDataSources.Implementation;

public class ResourceProvider : IResourceRepositoryProvider
{
	private const string SharedDataSourceExtension = ".rdsx";
	
	private readonly DirectoryInfo _rootDirectory;

	public ResourceProvider(DirectoryInfo rootDirectory)
	{
		_rootDirectory = rootDirectory;
	}
	
	public Stream GetResource(ResourceInfo resource)
	{
		string absolutePath = Path.Combine(_rootDirectory.FullName, resource.Name);

		var file = new FileInfo(absolutePath);

		if (!file.Exists)
			return null;

		return file.OpenRead();
	}

	public ResourceDescriptor[] ListResources(ResourceType resourceType)
	{
		if (resourceType == ResourceType.SharedDataSource)
		{
			var sharedDataSources = _rootDirectory
				.EnumerateFiles("*" + SharedDataSourceExtension).Select(fileInfo =>
				{
					using var stream = fileInfo.OpenRead();
					var dataSource = DataSourceTools.LoadSharedDataSource(stream);

					return new SharedDataSourceResourceDescriptor()
					{
						Id = fileInfo.Name,
						Name = fileInfo.Name,
						Type = dataSource.ConnectionProperties.DataProvider
					};
				}).ToArray();
			
			return sharedDataSources;
		}

		return Enumerable.Empty<ResourceDescriptor>().ToArray();
	}

	public ResourceDescriptor[] DescribeResources(ResourceInfo[] resources)
	{
		return Enumerable.Empty<ResourceDescriptor>().ToArray();
	}
}