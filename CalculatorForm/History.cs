using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;

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
            string fileName = "history.json";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);

            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    hElements.Add(JsonSerializer.Deserialize<HElement>(s));
                }
            }
        }

        public void Write(ObservableCollection<HElement> hElements, HElement hElement)
        {
            string fileName = "history.json";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(JsonSerializer.Serialize(hElement));
            }
        }
    }

    //class dbHistoryStorage : IHistoryStorage
    //{
    //    public void Read(ObservableCollection<HElement> hElements)
    //    {
    //        using (var connection =
    //            new System.Data.SQLite.SQLiteConnection("Data Source=C:\\Programming\\SQLiteStudio\\HistoryDataBase"))
    //        {
    //            connection.Open();
    //            var result = connection.Query<HElement>("select * from HistoryDataTable");

    //            foreach (var element in result)
    //            {
    //                hElements.Add(element);
    //            }
    //        }
    //    }

    //    public void Write(ObservableCollection<HElement> hElements, HElement hElement)
    //    {
    //        using (var connection =
    //            new System.Data.SQLite.SQLiteConnection("Data Source=C:\\Programming\\SQLiteStudio\\HistoryDataBase"))
    //        {
    //            connection.Open();
    //            var command = connection.CreateCommand();
    //            command.CommandText = "Insert into HistoryDataTable (Equation, Answer) values ('" + hElement.Equation.ToString() + "', '"
    //                                  + hElement.Answer.ToString() + "')";
    //            command.ExecuteReader();
    //        }
    //    }
    //}
}
