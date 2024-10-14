using DTOLayer.DTOs.AppUserDTOs;
using DTOLayer.DTOs.ProfileDTOs;
using FluentValidation;
namespace BusinessLayer.ValidationRules.ProfileValidationRules
{
    public class ProfileChangePasswordValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ProfileChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter uzunluğunda olmalı.")
                .Matches("[A-Z]").WithMessage("Şifrede en az bir büyük harf bulunmalı.")
                .Matches("[a-z]").WithMessage("Şifrede en az bir küçük harf bulunmalı.")
                .Matches("[0-9]").WithMessage("Şifrede en az bir rakam bulunmalı.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifre onayı boş olamaz.")
                .Equal(x => x.NewPassword).WithMessage("Şifreler uyuşmuyor.");
        }
    }
}