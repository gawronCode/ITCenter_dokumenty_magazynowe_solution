using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITCenter_dokumenty_magazynowe.Models.DbModels
{
    public class OperationLog
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ObjectId { get; set; }
        [Required]
        public string Info { get; set; }

    }
}
