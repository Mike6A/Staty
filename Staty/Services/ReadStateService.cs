using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Staty.Data;

namespace Staty.Services
{
    public class ReadStateService : IReadStateService
    {
        private readonly TableDataModel _cachedTableData;
        public TableDataModel GetCachedTableData => _cachedTableData.Clone();

        private readonly IDataPathManager _pathManager;

        private readonly char SEPARATOR = ';';

        public ReadStateService(IDataPathManager pathManager)
        {
            _pathManager = pathManager;
            _cachedTableData = GetAllStates();
        }

        public TableDataModel GetAllStates()
        {
            string pathStates = _pathManager.CsvPath;
            if (!CheckFileIfExists(pathStates))
                return null;

            using (StreamReader sr = new StreamReader(pathStates, Encoding.UTF8))
            {
                return ReadFileStream(sr);
            }
        }

        private TableDataModel ReadFileStream(StreamReader sr)
        {
            var model = new TableDataModel();
            var readHeader = false;
            string line;
            while((line = sr.ReadLine()) != null) {
                var values = line.Split(SEPARATOR);           
                if (!readHeader)
                {
                    SetDescriptions(model, values);
                    readHeader = true;
                    continue;
                }

                AddNewItem(model.States, ProcessLine(values));
            }

            return model;
        }

        private static void AddNewItem(List<State> states, State state)
        {
            if (state != null)
                states.Add(state);
        }

        private static State ProcessLine(string[] values)
        {
            try
            {
                return new State()
                {
                    Name = values[0],
                    Continent = values[1],
                    Shortcut = values[2],
                    StateSystem = values[3],
                    Capital = values[4],
                    Population = uint.Parse(values[5]),
                    Area = int.Parse(values[6])
                };
            }
            catch (Exception)
            {
                return State.Empty;
            }
        }

        private static void SetDescriptions(TableDataModel model, string[] values)
        {
            model.SetDescriptionFor(nameof(State.Name), values[0]);
            model.SetDescriptionFor(nameof(State.Continent), values[1]);
            model.SetDescriptionFor(nameof(State.Shortcut), values[2]);
            model.SetDescriptionFor(nameof(State.StateSystem), values[3]);
            model.SetDescriptionFor(nameof(State.Capital), values[4]);
            model.SetDescriptionFor(nameof(State.Population), values[5]);
            model.SetDescriptionFor(nameof(State.Area), values[6]);
        }

        private static bool CheckFileIfExists(string path)
        {
            if (File.Exists(path)) 
                return true;

            Console.WriteLine("Chyba! Cesta k souboru nenalezena!");
            return false;

        }
    }
}