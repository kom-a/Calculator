using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using CalculatorNS;

namespace CalculatorForm
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Calculator _calculator = new Calculator();
        private string _expression = "";

        public static RoutedCommand MyCommand = new RoutedCommand();

        public string Expression
        {
            get { return _expression; }
            set
            { 
                _expression = value;
                OnPropertyChanged(nameof(Expression));
            }
        }

        public void ProcessInput(object obj)
        {
            string param = obj as string;
            
            if(param == "=")
            {
                try
                {
                    Expression = _calculator.Calculate(_expression).ToString();
                } catch(Exception e)
                {
                    Expression = "Error";
                }
                
            }
            else if(param == "B")
            {
                if(Expression != "")
                    Expression = Expression.Substring(0, Expression.Length - 1);
            }
            else if(param == "C")
            {
                Expression = "";
            }
            else
            {
                Expression += param;
            }
        }

        public void TestHelloWorld(object obj)
        {
            Debug.WriteLine("Test: Hello world!");
        }

        private void MyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Debug.WriteLine("MyCommandExecuted: Hello world!");
        }

        public ICommand InputCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(ProcessInput));
            }
        }

        //public ICommand KeyboardInputCommand
        //{
        //}
    }
}
