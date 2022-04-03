namespace Staty.Utils
{
    public interface IPager
    {
        int ActivePageIndex { get; }
        int MaxPageIndex { get; }
        int SkipItemsCount { get; }
        int ItemsPerPage { get; }

        void PrintDataModel();
        void NextPage();
        void PrevPage();
        void SetPage(int newIndex);
    }
}