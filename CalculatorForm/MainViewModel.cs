using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using CalculatorNS;

namespace CalculatorForm
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Calculator _calculator = new Calculator();
        private string _expression = "";

        public static RoutedCommand MyCommand = new RoutedCommand();

        private IHistoryStorage _historyStorage;
        private ObservableCollection<HElement> _history;
        private bool _showHistory = false;
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel(IHistoryStorage historyStorage)
        {
            _historyStorage = historyStorage;
            _history = new ObservableCollection<HElement>();
        }

        public string Expression
        {
            get { return _expression; }
            set
            { 
                _expression = value;
                OnPropertyChanged(nameof(Expression));
            }
        }

        public ObservableCollection<HElement> History
        {
            get { return _history; }
            set
            {
                _history = value;
                OnPropertyChanged(nameof(History));
            }
        }

        public bool ShowHistory
        {
            get { return _showHistory; }
            set
            {
                _showHistory = value;
                OnPropertyChanged(nameof(ShowHistory));
            }
        }


        public void ProcessInput(object obj)
        {
            string param = obj as string;
            
            if(param == "=")
            {
                try
                {
                    double result = _calculator.Calculate(_expression);
                    HElement historyElement = new HElement(Expression, result.ToString());
                    History.Add(historyElement);
                    _historyStorage.Write(_history, historyElement);
                    Expression = result.ToString();
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

        public ICommand InputCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(ProcessInput));
            }
        }
    }
}
