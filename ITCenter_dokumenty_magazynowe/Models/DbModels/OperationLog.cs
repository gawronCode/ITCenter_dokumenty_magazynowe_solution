using System;
using System.ComponentModel.DataAnnotations;


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

        public DateTime? Date { get; set; }

    }
}
