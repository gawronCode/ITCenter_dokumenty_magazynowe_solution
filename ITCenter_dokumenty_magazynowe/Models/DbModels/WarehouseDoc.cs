using System;
using System.ComponentModel.DataAnnotations;


namespace ITCenter_dokumenty_magazynowe.Models.DbModels
{
    public class WarehouseDoc
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ClientNumber { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public double NetPrice { get; set; }
        [Required]
        public double GrossPrice { get; set; }

    }
}
