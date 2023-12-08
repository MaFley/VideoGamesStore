using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace VideoGamesStore.Models.Data
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Введите никнейм")]

        //отображение Фамилия вместо LastName
        [Display(Name = "Введите никнейм")]
        public string NickName { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime DateOfReg { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
