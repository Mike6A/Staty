using System;
using Staty.Handlers;

namespace Staty.Utils
{
    public class Pager : IPager
    {
        private readonly IDataHandler _dh;

        public int ItemsPerPage => Console.WindowHeight - UiHelper.InfoRowsCount;

        public int ActivePageIndex { get; private set; }
        public int MaxPageIndex => _dh.DataModel.States.Count / ItemsPerPage;
        public int SkipItemsCount => ItemsPerPage * ActivePageIndex;

        public Pager(IDataHandler dh)
        {
            _dh = dh;
        }

        public void PrintDataModel() => 
            UiHelper.PrintDataModel(this, _dh.DataModel);

        public void NextPage() =>
            SetPage(ActivePageIndex + 1);

        public void PrevPage() =>
            SetPage(ActivePageIndex - 1);

        public void SetPage(int newIndex)
        {
            if (newIndex < 0 || newIndex > MaxPageIndex)
                return;

            ActivePageIndex = newIndex;
            PrintDataModel();
        }
    }
}
