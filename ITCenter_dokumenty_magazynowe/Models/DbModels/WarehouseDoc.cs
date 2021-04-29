using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public int NetPrice { get; set; }
        [Required]
        public int GrossPrice { get; set; }

    }
}
