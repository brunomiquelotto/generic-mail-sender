using FluentValidation;
using Generic.MailSender.Api.Entities;

namespace Generic.MailSender.Api.Validations
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Destinatary).NotNull().WithMessage("Destinatary.Should.Not.Be.Null");
            RuleFor(x => x.Destinatary).NotEmpty().WithMessage("Destinatary.Should.Not.Be.Empty");
            RuleFor(x => x.Destinatary).ForEach(x => x.EmailAddress().WithMessage("Destinatary.Should.Be.Valid.Mail"));
        }
    }
}
