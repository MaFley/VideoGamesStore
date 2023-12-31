﻿using System.ComponentModel.DataAnnotations;

namespace VideoGamesStore.ViewModels.Users
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "Введите E-mail")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Введите корретный E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        public string NickName { get; set; }

    }
}
