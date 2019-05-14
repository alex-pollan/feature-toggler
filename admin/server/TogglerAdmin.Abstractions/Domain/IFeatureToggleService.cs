using System.Collections.Generic;
using TogglerAdmin.Abstractions.Domain.ViewModels;

namespace TogglerAdmin.Abstractions.Domain
{
    public interface IFeatureToggleService
    {
        IEnumerable<IFeatureToggleViewModel> Get();
        IFeatureToggleViewModel GetByName(string name);
        IFeatureToggleViewModel Create(IFeatureToggleViewModel viewModel, IAppOperationContext context);
        void Update(IFeatureToggleViewModel viewModel, IAppOperationContext context);
    }
}
