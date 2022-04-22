using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage ="Please enter a disk name.")]
        public string DiskName { get; set; }
        [Required(ErrorMessage = "Please enter a release date.")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Please select a disk type.")]
        public int? DiskTypeId { get; set; }
        [Required(ErrorMessage = "Please select a disk genre.")]
        public int? GenreId { get; set; }
        [Required(ErrorMessage = "Please select a disk status.")]
        public int? StatusId { get; set; }

        public virtual DiskType DiskType { get; set; }
        public virtual DiskGenre Genre { get; set; }
        public virtual DiskStatus Status { get; set; }
        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
    }
}
