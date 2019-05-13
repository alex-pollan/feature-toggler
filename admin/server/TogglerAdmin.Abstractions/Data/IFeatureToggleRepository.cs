using System.Collections.Generic;
using TogglerAdmin.Abstractions.Data.Models;

namespace TogglerAdmin.Abstractions.Data
{
    public interface IFeatureToggleRepository
    {
        IEnumerable<IFeatureToggleModel> Get();
        IFeatureToggleModel Get(string id);
        IFeatureToggleModel Create(IFeatureToggleModel model);
        void Update(string id, IFeatureToggleModel model);
        void Remove(string id);
        IFeatureToggleModel CreateModel();
    }
}
