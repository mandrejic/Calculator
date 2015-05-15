using System;
using System.Windows.Input;

namespace XFormsRPNCalculator
{
    public class CalculatorCommand : ICommand
    {
        private readonly CalculatorViewModel _vm;

        public event EventHandler CanExecuteChanged;

        public CalculatorCommand(CalculatorViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _vm.Execute((string)parameter);
        }
    }
}
