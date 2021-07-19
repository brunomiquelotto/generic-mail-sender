using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Generic.MailSender.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(Entities.Email request, [FromServices] IFluentEmailFactory factory, [FromServices] AbstractValidator<Entities.Email> validator)
        {
            var results = validator.Validate(request);
            if (!results.IsValid)
            {
                return BadRequest(results.Errors);
            }

            var response = await factory.Create().Body(request.Subject).To(request.Destinatary.Select(x => new Address(x))).SendAsync();
            Console.WriteLine(JsonConvert.SerializeObject(response));
            return Ok(response);
        }
    }
}
