using System;
using System.Windows.Input;

namespace AnyStatus.Apps.Windows.Infrastructure.Mvvm.ContextMenu;

internal class DefaultContextMenuItem<TContext> : ContextMenu<TContext>
{
    public DefaultContextMenuItem()
    {
        Name    = "No actions available";
        Command = new NoActionsAvailableCommand();
    }

    private class NoActionsAvailableCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }  // Optionally implement an add/remove handler if needed
            remove { }
        }

        public bool CanExecute(object parameter) => false;
        public void Execute(object    parameter) => throw new NotImplementedException();
    }
}