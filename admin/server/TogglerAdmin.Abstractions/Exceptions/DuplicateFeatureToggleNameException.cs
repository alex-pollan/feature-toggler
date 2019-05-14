using System;

namespace TogglerAdmin.Abstractions.Exceptions
{
    public class DuplicateFeatureToggleNameException : ApplicationException
    {
        public DuplicateFeatureToggleNameException(string name) : base($"A feature toggle with the name '{name}' already exists")
        {
            Name = name;
        }

        public string Name { get; }
    }
}
