using FluentValidation;

namespace Tarker.Booking.Application.Validators.User
{
    public class GetUserByUserNameAndPasswordValidator : AbstractValidator<(string UserName, string Password)>
    {
        public GetUserByUserNameAndPasswordValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(10);
        }
    }
}
