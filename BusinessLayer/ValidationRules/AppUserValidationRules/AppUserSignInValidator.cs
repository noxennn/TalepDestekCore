using DTOLayer.DTOs.AppUserDTOs;
using FluentValidation;

namespace BusinessLayer.ValidationRules.AppUserValidationRules
{
    public class AppUserSignInValidator : AbstractValidator<AppUserSignInDTO>
    {
        public AppUserSignInValidator()
        {
            //Tc Kimlik No
            RuleFor(x => x.UserName)
                .Matches(@"^\d{11}$").WithMessage("TC Kimlik No 11 haneli olmalıdır.")
                .Custom((UserName, context) =>
                {
                    if (!IsValidTcKimlikNo(UserName))
                    {
                        context.AddFailure("TC Kimlik No geçerli bir numara olmalıdır.");
                    }
                });
            //Şifre
            RuleFor(x => x.Password)
                .NotNull().WithMessage("Parola alanı boş geçilemez!");


        }

        private bool IsValidTcKimlikNo(string tcKimlikNo)
        {
            try
            {
                // TC Kimlik No doğrulama
                var digits = tcKimlikNo.Select(d => int.Parse(d.ToString())).ToArray();

                // İlk 10 hanenin toplamı
                int totalSum = digits.Take(10).Sum();

                // Son haneyi bul
                int lastDigit = digits[10];

                // Kontrol
                return totalSum % 10 == lastDigit;
            }
            catch (Exception ex)
            {
                return false;
            };
        }
    }
}
