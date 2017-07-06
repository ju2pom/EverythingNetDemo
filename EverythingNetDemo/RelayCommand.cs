using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Common.MvvM
{
  public class RelayCommand : ICommand
  {
    #region Fields

    private readonly Action<object> execute;
    private readonly Predicate<object> canExecute;

    #endregion // Fields

    #region Constructors

    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {
      if (execute == null)
        throw new ArgumentNullException("execute");

      this.execute = execute;
      this.canExecute = canExecute;
    }

    #endregion // Constructors

    #region ICommand Members

    [DebuggerStepThrough]
    public bool CanExecute(object parameter)
    {
      return canExecute == null || canExecute(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object parameter)
    {
      execute(parameter);
    }

    #endregion // ICommand Members
  }
}
