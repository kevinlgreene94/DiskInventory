using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskGenre
    {
        public DiskGenre()
        {
            DiskTables = new HashSet<DiskTable>();
        }

        public int GenreId { get; set; }
        public string Descrip { get; set; }

        public virtual ICollection<DiskTable> DiskTables { get; set; }
    }
}
