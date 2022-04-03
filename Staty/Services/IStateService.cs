using Staty.Data;
using Staty.Filters;

namespace Staty.Services
{
    public interface IStateService
    {
        TableDataModel GetAll(bool useCache);
        TableDataModel Filer(StateFilter filter);
    }
}