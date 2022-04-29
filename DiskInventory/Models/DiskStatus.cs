using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskStatus
    {
        public DiskStatus()
        {
            DiskTables = new HashSet<DiskTable>();
        }

        public int StatusId { get; set; }
        public string Descrip { get; set; }

        public virtual ICollection<DiskTable> DiskTables { get; set; }
    }
}
