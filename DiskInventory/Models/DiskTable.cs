using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskTable
    {
        public DiskTable()
        {
            DiskHasBorrowers = new HashSet<DiskHasBorrower>();
        }

        public int DiskId { get; set; }
        public string DiskName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DiskTypeId { get; set; }
        public int GenreId { get; set; }
        public int StatusId { get; set; }

        public virtual DiskType DiskType { get; set; }
        public virtual DiskGenre Genre { get; set; }
        public virtual DiskStatus Status { get; set; }
        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
    }
}
