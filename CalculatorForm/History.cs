using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace CalculatorForm
{
    public class HElement
    {
        private string _expression;
        public string Expression
        {
            get => _expression;
            set
            {
                if (_expression == value) return;

                _expression = value;
            }
        }
        private string _answer;
        public string Answer
        {
            get => _answer;

            set
            {
                if (_answer == value) return;

                _answer = value;
            }
        }
        public HElement(string expression, string answer)
        {
            _expression = expression;
            _answer = answer;
        }
    }

    public interface IHistoryStorage
    {
        void Read(ObservableCollection<HElement> hElements);
        void Write(ObservableCollection<HElement> hElements, HElement hElement);
    }

    class MemoryHistoryStorage : IHistoryStorage
    {
        public void Read(ObservableCollection<HElement> hElements)
        {

        }

        public void Write(ObservableCollection<HElement> hElements, HElement hElement)
        {
            hElements.Add(hElement);
        }
    }

    class FileHistoryStorage : IHistoryStorage
    {
        public void Read(ObservableCollection<HElement> hElements)
        {
            string historyFilename = "./history.json";
            if (!File.Exists(historyFilename))
            {
                File.Create(historyFilename);
            }

            foreach (string line in System.IO.File.ReadLines(historyFilename))
            {
                HElement element = JsonConvert.DeserializeObject<HElement>(line);
                hElements.Add(element);
            }
        }

        public void Write(ObservableCollection<HElement> hElements, HElement hElement)
        {
            string historyFilename = "./history.json";
            if (!File.Exists(historyFilename))
            {
                File.Create(historyFilename);
            }

            using (StreamWriter sw = File.AppendText(historyFilename))
            {
                sw.WriteLine(JsonConvert.SerializeObject(hElement));
            }
        }
    }

    class DatabaseHistoryStorage : IHistoryStorage
    {
        public void Read(ObservableCollection<HElement> hElements)
        {
            using (var connection =
                new System.Data.SQLite.SQLiteConnection("Data Source=C:\\Users\\kamil\\source\\repos\\Calculator\\CalculatorForm\\historyDB.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select * from historyTable";
                var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    string expression = Convert.ToString(reader["expression"]);
                    string answer = Convert.ToString(reader["answer"]);
                    hElements.Add(new HElement(expression, answer));
                }

            }
        }

        public void Write(ObservableCollection<HElement> hElements, HElement hElement)
        {
            using (var connection =
                new System.Data.SQLite.SQLiteConnection("Data Source=C:\\Users\\kamil\\source\\repos\\Calculator\\CalculatorForm\\historyDB.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                string expression = hElement.Expression;
                string answer = hElement.Answer;
                command.CommandText = "Insert into historyTable (expression, answer) values ('" + expression + "', '"
                                      + answer + "')";
                command.ExecuteReader();
            }
        }
    }
}
