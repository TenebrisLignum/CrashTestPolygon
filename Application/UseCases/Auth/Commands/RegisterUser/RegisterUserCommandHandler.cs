using Application.Messaging;
using Domain;
using Domain.Entities.Users;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IValidator<RegisterUserCommand> _validator;

        public RegisterUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IValidator<RegisterUserCommand> validator
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _validator = validator;
        }
        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            if (await _userManager.FindByEmailAsync(request.Email) is not null)
                throw new BadRequestException($"User with email {request.Email} already exist.");

            if (await _userManager.FindByNameAsync(request.Username) is not null)
                throw new BadRequestException($"User with username {request.Username} already exist.");

            var user = new ApplicationUser { UserName = request.Username, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, Consts.UserRoleString);

            return result;
        }
    }
}
