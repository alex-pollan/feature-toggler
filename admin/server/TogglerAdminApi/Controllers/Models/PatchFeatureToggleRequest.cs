namespace TogglerAdmin.Api.Controllers.Models
{
    public class PatchFeatureToggleRequest
    {
        public const string PropertyEnable = "enable";

        public string Id { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }
}
