using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentValidation;
using Generic.MailSender.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic.MailSender.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Entities.Email request, [FromServices] IFluentEmailFactory factory, [FromServices] AbstractValidator<Entities.Email> validator)
        {
            var results = validator.Validate(request);
            if (!results.IsValid)
            {
                return BadRequest(results.Errors.Select(x => new ValidationErrorViewModel(x.PropertyName, x.ErrorMessage)));
            }

            var obj = JObject.Parse(request.JsonData);

            var response = await factory.Create()
                .Body(request.Subject)
                .UsingTemplateFromFile($"/var/mail-templates/{request.TemplateName}.cshtml", obj)
                .To(request.Destinatary.Select(x => new Address(x))).SendAsync();

            Console.WriteLine(JsonConvert.SerializeObject(response));
            return Ok(response);
        }

    }
}
