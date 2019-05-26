using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TogglerAdmin.Abstractions;
using TogglerAdmin.Abstractions.Domain;
using TogglerAdmin.Abstractions.Exceptions;
using TogglerAdmin.Api.Controllers.Models;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeatureToggleViewModel>>> Get()
        {
            return Ok(await _service.Get());
        }

        [HttpPost]
        public async Task<ActionResult> Post(FeatureToggleViewModel model)
        {
            if (await IsValid(model))
            {
                try
                {
                    var savedModel = await _service.Create(model, GetContext());

                    return Ok(new { Id = savedModel.Id.ToString() });
                }
                catch (DuplicateFeatureToggleNameException)
                {
                    return Conflict("duplicate toggle name");
                }
            }

            return BadRequest();
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(PatchFeatureToggleRequest request)
        {
            switch (request.PropertyName)
            {
                case PatchFeatureToggleRequest.PropertyEnable:
                    return await Enable(request.Id, request.PropertyValue);

                default:
                    return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.Delete(id);
            return NoContent();
        }

        [HttpGet]
        [Route("exists/{name}")]
        public async Task<ActionResult<bool>> Exists(string name)
        {
            return base.Ok(new { Exists = !await ExistsName(name) });
        }

        private async Task<bool> ExistsName(string name)
        {
            return (await _service.GetByName(name)).IsEmpty;
        }

        private IAppOperationContext GetContext()
        {
            return new AppOperationContext(HttpContext.User.Identity.Name);
        }

        private async Task<bool> IsValid(FeatureToggleViewModel model)
        {
            return !string.IsNullOrWhiteSpace(model.Name);
        }

        private async Task<ActionResult> Enable(string id, string enableString)
        {
            if (!bool.TryParse(enableString, out var enable))
            {
                return BadRequest();
            }

            await _service.Enable(id, enable);

            return Ok();
        }
    }
}
