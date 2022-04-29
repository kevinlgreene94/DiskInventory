using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskInventory.Models
{
    public class DiskHasBorrowerViewModel 
    {
        public int DiskHasBorrowerId { get; set; }
        public int DiskId { get; set; }
        public int BorrowerId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public virtual Borrower Borrower { get; set; }
        public virtual DiskTable Disk { get; set; }
        public List<Borrower> Borrowers { get; set; }
        public List<DiskTable> Disks { get; set; }
    }
}
