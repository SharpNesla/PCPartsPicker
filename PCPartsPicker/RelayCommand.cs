using System;
using System.Windows.Input;

namespace PCPartsPicker
{
    /// <summary>
    /// Generic command, takes delegate of procedure
    /// </summary>
    /// <summary xml:lang=ru>
    /// Обобщённая команда, принимающая в качестве
    /// действия делегат процедуры без параметра
    /// </summary>
    class RelayCommand : ICommand
    {
        private readonly Action _executeAction;

        public RelayCommand(Action executeAction)
        {
            _executeAction = executeAction;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _executeAction();
        }

        public event EventHandler CanExecuteChanged;
    }
}