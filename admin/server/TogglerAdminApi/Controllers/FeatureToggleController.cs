using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<ActionResult<IEnumerable<FeatureToggleViewModel>>> Get()
        {
            return Ok(await _service.Get());
        }

        [HttpPost]
        public async Task<ActionResult> Post(FeatureToggleViewModel model)
        {
            var savedModel = await _service.Create(model, GetContext());

            return Ok(new { Id = savedModel.Id.ToString() });
        }

        [HttpGet]
        [Route("exists/{name}")]
        public async Task<ActionResult<bool>> Exists(string name)
        {
            return Ok(new { Exists = !(await _service.GetByName(name)).IsEmpty });
        }

        private IAppOperationContext GetContext()
        {
            return new AppOperationContext(HttpContext.User.Identity.Name);
        }
    }
}
