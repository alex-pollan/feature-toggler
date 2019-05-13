using System;
using TogglerAdmin.Abstractions.Data.Models;
using TogglerAdmin.Abstractions.Domain.ViewModels;

namespace TogglerAdmin.Domain.ViewModels
{
    public class FeatureToggleViewModel : IFeatureToggleViewModel
    {
        public FeatureToggleViewModel() { }

        public FeatureToggleViewModel(IFeatureToggleModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Enabled = model.Enabled;
            Creator = model.Creator;
            CreatedAt = model.CreatedAt;
            Modifier = model.Modifier;
            ModifiedAt = model.ModifiedAt;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public string Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Modifier { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

}
