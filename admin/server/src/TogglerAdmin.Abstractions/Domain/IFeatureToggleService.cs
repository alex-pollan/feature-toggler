using System.Collections.Generic;
using System.Threading.Tasks;
using TogglerAdmin.Abstractions.Domain.ViewModels;

namespace TogglerAdmin.Abstractions.Domain
{
    public interface IFeatureToggleService
    {
        Task<IEnumerable<IFeatureToggleViewModel>> Get();
        Task<IFeatureToggleViewModel> GetByName(string name);
        Task<IFeatureToggleViewModel> Create(IFeatureToggleViewModel viewModel, IAppOperationContext context);
        Task Update(IFeatureToggleViewModel viewModel, IAppOperationContext context);
        Task Enable(string id, bool enable);
    }
}
