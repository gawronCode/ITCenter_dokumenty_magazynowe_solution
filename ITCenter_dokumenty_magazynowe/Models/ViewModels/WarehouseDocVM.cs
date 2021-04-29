using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCenter_dokumenty_magazynowe.Models.NewFolder
{
    public class WarehouseDocVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientNumber { get; set; }
        public DateTime? Date { get; set; }
        public int NetPrice { get; set; }
        public int GrossPrice { get; set; }
    }
}
