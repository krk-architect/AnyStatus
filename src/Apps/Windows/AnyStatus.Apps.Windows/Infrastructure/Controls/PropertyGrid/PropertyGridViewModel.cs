using System.Collections.Generic;
using AnyStatus.Apps.Windows.Infrastructure.Controls.PropertyGrid;

namespace AnyStatus.Apps.Windows.Infrastructure.Mvvm.Controls.PropertyGrid;

internal class PropertyGridViewModel : BaseViewModel, IPropertyGridViewModel
{
    private IEnumerable<IPropertyViewModel> _properties;
    private object                          _target;

    public PropertyGridViewModel(IPropertyViewModelBuilder propertyBuilder)
    {
        PropertyChanged += (_, e) =>
                           {
                               if (e.PropertyName.Equals(nameof(Target)) && Target is not null)
                               {
                                   Properties = propertyBuilder.Build(Target);
                               }
                           };
    }

    public object Target
    {
        get => _target;
        set => Set(ref _target, value);
    }

    public IEnumerable<IPropertyViewModel> Properties
    {
        get => _properties;
        private set => Set(ref _properties, value);
    }
}