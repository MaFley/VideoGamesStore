using System.ComponentModel.DataAnnotations;

namespace VideoGamesStore.ViewModels.Reviews
{
    public class EditReviewViewModel
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
