using System.IO;
using System.Linq;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Web.Designer;

namespace WebDesigner_Custom.Implementation;

public class ResourceProvider : IResourceRepositoryProvider
{
	private static readonly DirectoryInfo ResourcesRootDirectory =
		new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "resources"));
	
	public Stream GetResource(ResourceInfo resource)
	{
		var absolutePath = Path.Combine(ResourcesRootDirectory.FullName, resource.Name);

		var file = new FileInfo(absolutePath);

		if (!file.Exists)
			return Stream.Null;

		return file.OpenRead();
	}

	public ResourceDescriptor[] ListResources(ResourceType resourceType)
	{
		return Enumerable.Empty<ResourceDescriptor>().ToArray();
	}

	public ResourceDescriptor[] DescribeResources(ResourceInfo[] resources)
	{
		return Enumerable.Empty<ResourceDescriptor>().ToArray();
	}
}