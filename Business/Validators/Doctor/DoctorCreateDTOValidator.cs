using Business.DTOs.Doctor.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Doctor
{
    public class DoctorCreateDTOValidator : AbstractValidator<DoctorCreateDTO>
    {
        public DoctorCreateDTOValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name must be added");

            RuleFor(x => x.Name).MinimumLength(3).WithMessage("name must be minimum 3 charachters");
            RuleFor(x => x.Surname).NotNull().WithMessage("Name must be added");
            RuleFor(x => x.Surname).MinimumLength(3).WithMessage("name must be minimum 3 charachters");
            RuleFor(x => x.Duty).NotNull().WithMessage("Duty must be included");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must be included");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not correct format");
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage("Phone Number must be included");
        }
    }
}
