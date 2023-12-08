using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VideoGamesStore.Models.Data;

namespace VideoGamesStore.ViewModels.Orders
{
    public class CreateOrderModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [Required]
        public string IdUser { get; set;}

        [Required]
        [Display(Name = "Ид игры")]
        public int IdGame { get; set;}

        [Display(Name = "Количество")]
        public int Count { get; set;}
    }
}
