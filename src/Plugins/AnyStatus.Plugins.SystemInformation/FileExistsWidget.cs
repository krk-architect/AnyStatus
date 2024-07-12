using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AnyStatus.API.Widgets;

namespace AnyStatus.Plugins.SystemInformation.FileSystem;

[DisplayName(DefaultName)]
[Category("System Information")]
[Description("Checks whether a file exists")]
public class FileExistsWidget : StatusWidget, IPollable, ICommonWidget
{
    private const string DefaultName = "File Exists";

    public FileExistsWidget() { Name = DefaultName; }

    [Required]
    public string Path { get; set; }
}