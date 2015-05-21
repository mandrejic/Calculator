using System;
using System.Linq;

namespace XFormsRPNCalculator
{
    public class Calculator
    {
        private const int Max = 12;

        private const string Exp = "0.000000e+00";

        private bool _isNew = true;

        private double _memory = 0.0;

        private double _xreg = 0.0;

        private string _format = "0.00";

        private string _output = "0.00";

        private double? _accumulator = null;

        private string _operator = string.Empty;

        private bool _isMemoryRecalled = false;

        public string Output
        {
            get
            {
                return _output;
            }
        }

        public void Execute(string command)
        {
            switch (command)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    HandleDigitPressed(command);
                    break;
                case "<-":
                    DeleteLastDigit();
                    break;
                case "±":
                    ChangeSign();
                    break;
                case "+":
                case "-":
                case "x":
                case "÷":
                case "%":
                    HandleBinaryOperationPressed(command);
                    break;
                case "=":
                    HandleCalculatePressed();
                    break;
                case "C":
                    Clear();
                    break;
                case ".":
                    HandleDecimalPoint();
                    break;
                case "M+":
                    AddToMemory();
                    break;
                case "MC":
                    ClearMemory();
                    break;
                case "MR":
                    MemoryRecall();
                    break;
            }
        }

        private void AddToMemory()
        {
            _memory += _xreg;
            _isNew = true;
        }

        private void ClearMemory()
        {
            _memory = 0.0;
            _isNew = true;
        }

        private void MemoryRecall()
        {
            _xreg = _memory;
            _output = StringifyXRegister();
            _isMemoryRecalled = true;
        }

        private void HandleDecimalPoint()
        {
            if (this._isNew)
            {
                this._isNew = false;
                this._output = ".";
                this._xreg = 0.0;
            }
            else if (this._output.Length < Max && !this._output.Contains("."))
            {
                this._output += '.';
                this._xreg = Convert.ToDouble(this._output);
            }
        }

        private void HandleCalculatePressed()
        {
            Calculate();
            this._accumulator = null;
            this._isNew = true;
        }

        private void HandleBinaryOperationPressed(string command)
        {
            Calculate();
            this._operator = command;
            this._isNew = true;
        }

        private void ChangeSign()
        {
            this._xreg = -this._xreg;
            this._output = StringifyXRegister();
        }

        private void DeleteLastDigit()
        {
            if (this._output.Length > 0 && !this._isNew)
            {
                this._output = this._output.Substring(0, this._output.Length - 1);
                this._xreg = this._output.Length == 0 ? 0.0 : Convert.ToDouble(this._output);
            }
        }

        private void HandleDigitPressed(string command)
        {
            _isMemoryRecalled = false;
            if (this._isNew)
            {
                // If this is the first character in a new entry, push the current
                // value of the X register onto the stack and clear the display
                this._isNew = false;
                this._output = String.Empty;
            }

            // Add the new character to the display and update the X register
            if (this._output.Length < Max)
            {
                this._output += command;
                this._xreg = Convert.ToDouble(this._output);
            }
        }

        private void Calculate()
        {
            if (!_isNew)
            {
                if (!_accumulator.HasValue)
                {
                    _accumulator = _xreg;
                }
                else
                {
                    DoCalculation();
                }
            }
            else if (_isMemoryRecalled && _accumulator.HasValue)
            {
                DoCalculation();
                _isMemoryRecalled = false;
            }
            else
            {
                _accumulator = _xreg;
            }
        }

        private void DoCalculation()
        {
            switch (_operator)
            {
                case "+":
                    _accumulator += _xreg;
                    break;
                case "-":
                    _accumulator -= _xreg;
                    break;
                case "x":
                    _accumulator *= _xreg;
                    break;
                case "÷":
                    _accumulator /= _xreg;
                    break;
                case "%":
                    _accumulator *= _xreg / 100.0;
                    break;
            }

            _xreg = _accumulator.Value;
            _output = StringifyXRegister();
        }

        private void Clear()
        {
            _accumulator = null;
            _xreg = 0.0;
            _isNew = true;
            _output = StringifyXRegister();
        }

        private string StringifyValue(double value)
        {
            var output = value.ToString(_format);

            // Switch to exponential notation if number is too large
            if (output.Length > Max)
            {
                output = _xreg.ToString(Exp);
            }

            // Switch to exponential notation if number is too small
            if (value != 0.0)
            {
                var nonzero = output.Cast<char>().Any(ch => ch >= '1' && ch <= '9');

                if (!nonzero)
                {
                    output = _xreg.ToString(Exp);
                }
            }

            // Return the result
            return output;
        }

        private string StringifyXRegister()
        {
            var output = StringifyValue(_xreg);

            // Reset internal state
            this._isNew = true;
            this._isMemoryRecalled = false;

            // Return the result
            return output;
        }

        public CalculatorState GetCalculatorState()
        {
            return new CalculatorState()
                {
                    Output = _output,
                    Format = _format,
                    IsNewPending = this._isNew,
                    Memory = _memory,
                    XRegister = _xreg,
                    Accumulator = _accumulator
                };
        }

        public void SetCalculatorState(CalculatorState state)
        {
            _output = state.Output;
            _format = state.Format;
            _isNew = state.IsNewPending;
            _memory = state.Memory;
            _xreg = state.XRegister;
            _accumulator = state.Accumulator;
        }
    }
}
