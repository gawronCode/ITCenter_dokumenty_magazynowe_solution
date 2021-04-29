using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public int NetPrice { get; set; }
        [Required]
        public int GrossPrice { get; set; }
        [Required]
        public int WarehouseDocId { get; set; }
        public WarehouseDoc WarehouseDoc { get; set; }

    }
}
