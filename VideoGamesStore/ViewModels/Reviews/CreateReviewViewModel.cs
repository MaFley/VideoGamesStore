using System.ComponentModel.DataAnnotations;
using VideoGamesStore.Controllers;

namespace VideoGamesStore.ViewModels.Reviews
{
    public class CreateReviewViewModel
    {
        [Required]
        [Display(Name = "Оценка")]
        public byte Rating { get; set; }

        [Display(Name = "Текст отзыва")]
        public string? ReviewText { get; set; }

        [Required]
        [Display(Name = "Дата отзыва")]
        public DateTime ReviewDateTime { get; set; }

        [Required]
        [Display(Name = "Имя пользователя")]
        public string IdUser { get; set; }

        [Required]
        [Display(Name = "Название игры")]
        public int IdGame { get; set; }
    }
}
