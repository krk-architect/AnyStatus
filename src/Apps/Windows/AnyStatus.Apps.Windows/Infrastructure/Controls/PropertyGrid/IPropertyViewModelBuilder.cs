using System.Collections.Generic;
using AnyStatus.Apps.Windows.Infrastructure.Mvvm.Controls.PropertyGrid;

namespace AnyStatus.Apps.Windows.Infrastructure.Controls.PropertyGrid;

internal interface IPropertyViewModelBuilder
{
    IEnumerable<IPropertyViewModel> Build(object source);
}