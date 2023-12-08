using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VideoGamesStore.Models.Data;

namespace VideoGamesStore.ViewModels.Orders
{
    public class EditOrderModel
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
    }
}
