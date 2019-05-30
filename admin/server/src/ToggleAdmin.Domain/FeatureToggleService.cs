using System.Collections.Generic;
using System.Linq;
using TogglerAdmin.Domain.ViewModels;
using TogglerAdmin.Abstractions;
using TogglerAdmin.Abstractions.Data;
using TogglerAdmin.Abstractions.Domain;
using TogglerAdmin.Abstractions.Domain.ViewModels;
using System.Threading.Tasks;

namespace TogglerAdmin.Domain
{
    public class FeatureToggleService : IFeatureToggleService
    {
        private readonly IFeatureToggleRepository _repository;
        private readonly ITimeProvider _timeProvider;

        public FeatureToggleService(IFeatureToggleRepository repository, ITimeProvider timeProvider)
        {
            _repository = repository;
            _timeProvider = timeProvider;
        }

        public async Task<IFeatureToggleViewModel> Create(IFeatureToggleViewModel viewModel, IAppOperationContext context)
        {
            var model = _repository.CreateModel();

            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.Enabled = viewModel.Enabled;
            model.Creator = context.UserName;
            model.CreatedAt = _timeProvider.UtcNow;

            var savedModel = await _repository.Create(model);

            return new FeatureToggleViewModel(savedModel);
        }

        public async Task Delete(string id)
        {
            await _repository.Remove(id);
        }

        public async Task Enable(string id, bool enable)
        {
            await _repository.Enable(id, enable);
        }

        public async Task<IEnumerable<IFeatureToggleViewModel>> Get()
        {
            return (await _repository.Get())
                .Select(ftm => new FeatureToggleViewModel(ftm));
        }

        public async Task<IFeatureToggleViewModel> GetByName(string name)
        {
            var model = await _repository.GetByName(name);
            return model != null ? new FeatureToggleViewModel(model) : FeatureToggleViewModel.Empty();
        }

        public async Task<IFeatureToggleViewModel> GetById(string id)
        {
            var model = await _repository.Get(id);
            return model != null ? new FeatureToggleViewModel(model) : FeatureToggleViewModel.Empty();
        }

        public async Task Update(IFeatureToggleViewModel viewModel, IAppOperationContext context)
        {
            var model = _repository.CreateModel();

            model.Id = viewModel.Id;
            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.Enabled = viewModel.Enabled;
            model.Modifier = context.UserName;
            model.ModifiedAt = _timeProvider.UtcNow;

            await _repository.Update(viewModel.Id, model);
        }
    }
}
