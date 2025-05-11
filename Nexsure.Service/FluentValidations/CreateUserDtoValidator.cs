using FluentValidation;
using Nexsure.Entities.Business_Model.Request_Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexsure.Service.FluentValidations
{
    public class CreateUserDtoValidator : AbstractValidator<UserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number must be in a valid format.");
        }
    }

}
