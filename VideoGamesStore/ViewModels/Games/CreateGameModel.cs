using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGamesStore.ViewModels.Games
{
    public class CreateGameModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название игры")]
        [Display(Name = "Название игры")]
        public string NameGame { get; set; }

        [Required(ErrorMessage = "Введите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Введите разработчика")]
        [Display(Name = "Разработчик")]
        public string GameDeveloper { get; set; }

        [Required(ErrorMessage = "Введите год выпуска")]
        [Display(Name = "Год выпуска")]
        public DateTime YearIssue { get; set; }

        [Required(ErrorMessage = "Введите описание игры")]
        [Display(Name = "Описание")]
        public string GameDescription { get; set; }

        [Required(ErrorMessage = "Введите платформу")]
        [Display(Name = "Платформа")]
        public string Platform { get; set; }

    }
}
