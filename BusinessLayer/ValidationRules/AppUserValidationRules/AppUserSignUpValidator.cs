using DTOLayer.DTOs.AppUserDTOs;
using EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace BusinessLayer.ValidationRules.AppUserValidationRules
{
	public class AppUserSignUpValidator : AbstractValidator<AppUserSignUpDTO>
	{
		private readonly UserManager<AppUser> _userManager;

		public AppUserSignUpValidator(UserManager<AppUser> userManager)
		{
			_userManager = userManager;

			// Name
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Ad alanı boş geçilemez!");

			// Surname
			RuleFor(x => x.Surname)
				.NotEmpty().WithMessage("Soyad alanı boş geçilemez!");

			// TC Kimlik No
			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage("TC Kimlik No alanı boş geçilemez!")
				.Matches(@"^\d{11}$").WithMessage("TC Kimlik No 11 haneli olmalıdır.")
				.Custom((tcKimlikNo, context) =>
				{
					if (!IsValidTcKimlikNo(tcKimlikNo))
					{
						context.AddFailure("TC Kimlik No geçerli bir numara olmalıdır.");
					}
				});

			// Email
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("E-posta alanı boş geçilemez!");

			// Password
			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Parola alanı boş geçilemez!")
				.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$").WithMessage("Parola en az bir büyük harf, bir küçük harf ve bir rakam içermelidir!")
				.MinimumLength(8).WithMessage("Parola minimum 8 karakterden oluşmalıdır.")
				.MaximumLength(50).WithMessage("Parola maksimum 50 karakterden oluşmalıdır.");

			// ConfirmPassword
			RuleFor(x => x.ConfirmPassword)
				.NotEmpty().WithMessage("Parola doğrulama alanı boş geçilemez!")
				.Equal(y => y.Password).WithMessage("Belirlediğiniz parola tekrar girdiğiniz parolayla uyuşmadı. Lütfen tekrar deneyin!");

			// PhoneNumber
			RuleFor(x => x.PhoneNumber)
				.NotEmpty().WithMessage("Telefon numarası alanı boş geçilemez!")
				.Matches(@"^0 \d{3} \d{3} \d{2} \d{2}$").WithMessage("Telefon numarası geçerli formatta değil. Örnek: 0 000 000 00 00");


		}

		public static bool IsValidTcKimlikNo(string tcKimlikNo)
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
