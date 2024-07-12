using System.Windows.Controls;
using System.Windows.Input;
using AnyStatus.API.Widgets;
using AnyStatus.Apps.Windows.Features.Launchers;

namespace AnyStatus.Apps.Windows.Features.Dashboard;

public partial class DashboardView : UserControl
{
    public DashboardView() { InitializeComponent(); }

    private async void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is not TreeViewItem treeViewItem)
        {
            return;
        }

        if (treeViewItem.DataContext is not IOpenInApp openInAppWidget)
        {
            return;
        }

        var viewModel = (DashboardViewModel)DataContext;
        var mediator  = viewModel.Mediator;

        await mediator.Send(new LaunchURL.Request(openInAppWidget.URL));
    }
}