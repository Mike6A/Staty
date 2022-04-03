using Staty.Data;

namespace Staty.Services
{
    public interface IReadStateService
    {
        TableDataModel GetCachedTableData { get; }
        TableDataModel GetAllStates();
    }
}