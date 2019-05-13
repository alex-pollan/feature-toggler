using System;

namespace TogglerAdmin.Abstractions.Data.Models
{
    public interface IFeatureToggleModel
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool Enabled { get; set; }
        string Creator { get; set; }
        DateTime CreatedAt { get; set; }
        string Modifier { get; set; }
        DateTime? ModifiedAt { get; set; }
    }
}
