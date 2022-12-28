using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EShop.Web.Models
{
    /*    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]*/
    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && value is bool && (bool)value;
        }
    }

    public class Registration
    {
        [DisplayName("Imię")]
        public string Name { get; set; }

        [DisplayName("Nazwisko")]
        public string Surname { get; set; }

        [Range(18, 100)]
        [DisplayName("Aktualny Wiek*")]
        public int Age { get; set; }

        [DisplayName("Nazwa użytkownika*")]
        [Required(ErrorMessage = "Pole nazwa użytkownika jest wymagane.")]
        [StringLength(30, MinimumLength = 4)]
        public string Username { get; set; }

        [DisplayName("Hasło*")]
        [StringLength(30, MinimumLength = 8)]
        [Required(ErrorMessage = "Pole hasło jest wymagane.")]
        public string Password { get; set; }

        [DisplayName("Powtórz Hasło*")]
        [Required(ErrorMessage = "Pole powtórz hasło jest wymagane.")]
        [Compare("Password", ErrorMessage = "Powtórzone hasło musi być takie samo jak hasło.")]
        public string ConfirmPassword { get; set; }

        [DisplayName("E-mail*")]
        [Required(ErrorMessage = "Pole email jest wymagane.")]
        [RegularExpression(".+\\@.+\\..+$", ErrorMessage = "Email jest nieprawidłowy")]
        public string Email { get; set; }

        [DisplayName("Numer telefonu*")]
        [Required(ErrorMessage = "Pole numer telefonu jest wymagane.")]
        [RegularExpression(@"\d{9}$", ErrorMessage = "Numer telefonu nieprawidłowy.")]
        public string PhoneNumber { get; set; }

        [DisplayName("Zgoda formalna")]
        [MustBeTrue(ErrorMessage = "Musisz zakceptować regulamin, żeby się zarejestrować.")]
        public bool terms { get; set; }
    }

    public class Login
    {
        [DisplayName("Nazwa użytkownika")]
        [Required(ErrorMessage = "Podaj nazwę użytkownika.")]
        [StringLength(30, MinimumLength = 4)]
        public string Username { get; set; }

        [DisplayName("Hasło")]
        [StringLength(30, MinimumLength = 8)]
        [Required(ErrorMessage = "Podaj hasło.")]
        public string Password { get; set; }
    }

    public class ForgotPassword
    {
        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Podaj  email.")]
        [RegularExpression(".+\\@.+\\..+$", ErrorMessage = "Email jest nieprawidłowy")]
        public string Email { get; set; }
    }


}