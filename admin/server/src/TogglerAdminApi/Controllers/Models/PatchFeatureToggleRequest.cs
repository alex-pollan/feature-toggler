namespace TogglerAdmin.Api.Controllers.Models
{
    public class PatchFeatureToggleRequest
    {
        public const string OperationEnable = "enable";
        public const string OperationSetDescription = "setdescription";
        public static readonly string[] Operations = new[] { OperationEnable, OperationSetDescription };

        public string Operation { get; set; }
        public string Value { get; set; }
    }
}
