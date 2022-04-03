namespace Staty.Handlers
{
    public interface IConsoleHandler
    {
        void ContinentFilter();
        void NameFilter();
        void ResetAllFilters();
        void SetItemsPerPage();
    }
}