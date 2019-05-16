using System.Collections.Generic;
using System.Threading.Tasks;
using TogglerAdmin.Abstractions.Data.Models;

namespace TogglerAdmin.Abstractions.Data
{
    public interface IFeatureToggleRepository
    {
        Task<IEnumerable<IFeatureToggleModel>> Get();
        Task<IFeatureToggleModel> Get(string id);
        Task<IFeatureToggleModel> GetByName(string name);
        Task<IFeatureToggleModel> Create(IFeatureToggleModel model);
        Task Update(string id, IFeatureToggleModel model);
        Task Remove(string id);
        IFeatureToggleModel CreateModel();
        Task Enable(string id, bool enable);
    }
}
