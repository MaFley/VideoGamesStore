using System.ComponentModel.DataAnnotations;

namespace VideoGamesStore.ViewModels.Users
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Введите E-mail")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Введите корретный E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Никнейм")]
        public string NickName { get; set; }

    }
}
