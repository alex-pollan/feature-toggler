using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TogglerAdmin.Abstractions;
using TogglerAdmin.Abstractions.Domain;
using TogglerAdmin.Domain.ViewModels;

namespace TogglerAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureToggleController : ControllerBase
    {
        private readonly IFeatureToggleService _service;

        public FeatureToggleController(IFeatureToggleService service)
        {
            _service = service;
        }

        public ActionResult<IEnumerable<FeatureToggleViewModel>> Get()
        {
            return Ok(_service.Get());
        }

        [HttpPost]
        public ActionResult Post(FeatureToggleViewModel model)
        {
            var savedModel = _service.Create(model, GetContext());

            return Ok(new { Id = savedModel.Id.ToString() });
        }

        [HttpGet]
        [Route("exists/{name}")]
        public ActionResult<bool> Exists(string name)
        {
            return Ok(new { Exists = !_service.GetByName(name).IsEmpty });
        }

        private IAppOperationContext GetContext()
        {
            return new AppOperationContext(HttpContext.User.Identity.Name);
        }
    }
}
