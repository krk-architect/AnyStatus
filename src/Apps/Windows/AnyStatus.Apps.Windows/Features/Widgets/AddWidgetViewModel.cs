using System.Collections.Generic;
using AnyStatus.API.Widgets;
using AnyStatus.Apps.Windows.Infrastructure.Mvvm;
using AnyStatus.Core.Services;
using AnyStatus.Core.Widgets;
using MediatR;

namespace AnyStatus.Apps.Windows.Features.Widgets;

public class AddWidgetViewModel : BaseViewModel
{
    private Category _category;
    private IWidget  _parent;
    private Template _template;

    public AddWidgetViewModel(IMediator mediator)
    {
        Commands.Add("Save", new Command(async _ => await mediator.Send(new CreateWidget.Request(Template, Parent)), CanAdd));
    }

    public IEnumerable<Category> Categories => Scanner.GetWidgetCategories();

    public Category Category
    {
        get => _category;
        set => Set(ref _category, value);
    }

    public Template Template
    {
        get => _template;
        set => Set(ref _template, value);
    }

    public IWidget Parent
    {
        get => _parent;
        set => Set(ref _parent, value);
    }

    private bool CanAdd(object p) => Category != null && Template != null && Parent != null;
}