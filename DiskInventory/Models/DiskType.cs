using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskType
    {
        public DiskType()
        {
            DiskTables = new HashSet<DiskTable>();
        }

        public int DiskTypeId { get; set; }
        public string Descrip { get; set; }

        public virtual ICollection<DiskTable> DiskTables { get; set; }
    }
}
