using System.ComponentModel.DataAnnotations;


namespace ITCenter_dokumenty_magazynowe.Models.DbModels
{
    public class Position
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double NetPrice { get; set; }
        [Required]
        public double GrossPrice { get; set; }
        [Required]
        public int WarehouseDocId { get; set; }
        public WarehouseDoc WarehouseDoc { get; set; }

    }
}
