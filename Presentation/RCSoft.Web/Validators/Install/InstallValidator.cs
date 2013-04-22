using FluentValidation;
using RCSoft.Web.Models.Install;
using RCSoft.Web.Infrastructure.Installation;

namespace RCSoft.Web.Validators.Install
{
    public class InstallValidator : AbstractValidator<InstallModel>
    {
        public InstallValidator(IInstallationLocalizationService locService)
        {
            RuleFor(x => x.AdminEmail).NotEmpty().WithMessage(locService.GetResource("AdminUserRequired"));
            RuleFor(x => x.AdminEmail).EmailAddress().WithMessage(locService.GetResource("AdminMailValid"));
            RuleFor(x => x.AdminPassword).NotEmpty().WithMessage(locService.GetResource("AdminPasswordRequired"));
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(locService.GetResource("ComfirmPasswordRequired"));
            RuleFor(x => x.AdminPassword).Equal(x => x.ConfirmPassword).WithMessage(locService.GetResource("PasswordsDoNotMatch"));
            RuleFor(x => x.SqlServerName).NotEmpty().WithMessage(locService.GetResource("SqlServerNameRequired"));
            RuleFor(x => x.SqlDatabaseName).NotEmpty().WithMessage(locService.GetResource("DataBaseNameRequired"));
            RuleFor(x => x.SqlServerUsername).NotEmpty().WithMessage(locService.GetResource("SqlServerUserNameRequired"));
            RuleFor(x => x.SqlServerPassword).NotEmpty().WithMessage(locService.GetResource("SQLServerPasswordRequired"));
        }
    }
}