using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskHasBorrower
    {
        public int DiskHasBorrowerId { get; set; }
        [Required(ErrorMessage = "Please select a disk")]
        public int DiskId { get; set; }
        [Required(ErrorMessage = "Please select a borrower")]
        public int BorrowerId { get; set; }
        [Required(ErrorMessage = "Please select a borrowed date")]
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public virtual Borrower Borrower { get; set; }
        public virtual DiskTable Disk { get; set; }
    }
}
