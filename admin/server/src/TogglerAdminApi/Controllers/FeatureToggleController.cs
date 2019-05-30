using System;
using System.Collections.Generic;
using System.Linq;
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
            if (IsValid(model))
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
        [Route("{id}")]
        public async Task<ActionResult> Patch(string id, [FromBody] IEnumerable<PatchFeatureToggleRequest> patches)
        {
            if (patches == null || !patches.Any()
                || !patches.All(patch => PatchFeatureToggleRequest.Operations.Contains(patch.Operation.ToLowerInvariant())))
            {
                return BadRequest();
            }

            var toggle = await _service.GetById(id);

            if (toggle.IsEmpty)
            {
                return NotFound();
            }

            foreach (var patch in patches)
            {
                switch (patch.Operation)
                {
                    case PatchFeatureToggleRequest.OperationEnable:
                        if (!bool.TryParse(patch.Value, out var enable))
                        {
                            return BadRequest();
                        }

                        toggle.Enabled = enable;

                        break;

                    case PatchFeatureToggleRequest.OperationSetDescription:
                        toggle.Description = patch.Value;

                        break;
                    default:

                        return BadRequest();
                }
            }

            await _service.Update(toggle, GetContext());

            return NoContent();
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

        private bool IsValid(FeatureToggleViewModel model)
        {
            return !string.IsNullOrWhiteSpace(model.Name);
        }
    }
}
