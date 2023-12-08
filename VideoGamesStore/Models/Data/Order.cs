using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VideoGamesStore.Models.Data
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public int Id { get; set; }


        [Display(Name = "Дата заказа")]
        public DateTime OrderDate { get; set; }

        [Required]
        public string IdUser { get; set; }


        [Required]
        public int IdGame { get; set; }


        [Display(Name = "Количество")]
        public int Count { get; set; }


        //навигационные свойства
        [ForeignKey("IdUser")]
        [Display(Name = "Ид пользователя")]
        public User User { get; set; }

        [ForeignKey("IdGame")]
        [Display(Name = "Ид игры")]
        public Game Game { get; set; }
    }
}
