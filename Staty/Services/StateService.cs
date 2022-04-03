using System.Linq;
using Staty.Data;
using Staty.Extensions;
using Staty.Filters;

namespace Staty.Services
{
    public class StateService : IStateService
    {
        private readonly IReadStateService _readStateService;

        public StateService(IReadStateService readStateService)
        {
            _readStateService = readStateService;
        }

        public TableDataModel GetAll(bool useCache)
        {
            return useCache
                ? _readStateService.GetCachedTableData
                : _readStateService.GetAllStates();
        }

        public TableDataModel Filer(StateFilter filter)
        {
            if (filter == null)
                return TableDataModel.Empty;

            filter.MakePropertiesUpperCase();

            var model = GetAll(filter.UseCache);
            model.States = model.States
                .WhereIfNotNull(s => s.Name.ToUpper().StartsWith(filter.Name), filter.Name)
                .WhereIfNotNull(s => s.Shortcut.ToUpper().StartsWith(filter.Shortcut), filter.Shortcut)
                .WhereIfNotNull(s => s.Continent.ToUpper().StartsWith(filter.Continent), filter.Continent)
                .ToList();

            return model;
        }
    }
}