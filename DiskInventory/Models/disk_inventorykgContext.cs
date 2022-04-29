using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DiskInventory.Models
{
    public partial class disk_inventorykgContext : DbContext
    {
        public disk_inventorykgContext()
        {
        }

        public disk_inventorykgContext(DbContextOptions<disk_inventorykgContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<DiskGenre> DiskGenres { get; set; }
        public virtual DbSet<DiskHasBorrower> DiskHasBorrowers { get; set; }
        public virtual DbSet<DiskStatus> DiskStatuses { get; set; }
        public virtual DbSet<DiskTable> DiskTables { get; set; }
        public virtual DbSet<DiskType> DiskTypes { get; set; }
        public virtual DbSet<ViewBorrowerNoLoan> ViewBorrowerNoLoans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLDEV01;Database=disk_inventorykg;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.ToTable("borrower");

                entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("fname");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("lname");

                entity.Property(e => e.PhoneNum)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone_num");
            });

            modelBuilder.Entity<DiskGenre>(entity =>
            {
                entity.HasKey(e => e.GenreId)
                    .HasName("PK__disk_gen__18428D42274CE240");

                entity.ToTable("disk_genre");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Descrip)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("descrip");
            });

            modelBuilder.Entity<DiskHasBorrower>(entity =>
            {
                entity.ToTable("disk_has_borrower");

                entity.Property(e => e.DiskHasBorrowerId).HasColumnName("disk_has_borrower_id");

                entity.Property(e => e.BorrowDate).HasColumnName("borrow_date");

                entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");

                entity.Property(e => e.DiskId).HasColumnName("disk_id");

                entity.Property(e => e.ReturnDate).HasColumnName("return_date");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.DiskHasBorrowers)
                    .HasForeignKey(d => d.BorrowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_has___borro__31EC6D26");

                entity.HasOne(d => d.Disk)
                    .WithMany(p => p.DiskHasBorrowers)
                    .HasForeignKey(d => d.DiskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_has___disk___30F848ED");
            });

            modelBuilder.Entity<DiskStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__disk_sta__3683B531AED5345B");

                entity.ToTable("disk_status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Descrip)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("descrip");
            });

            modelBuilder.Entity<DiskTable>(entity =>
            {
                entity.HasKey(e => e.DiskId)
                    .HasName("PK__disk_tab__2B6346F041448F47");

                entity.ToTable("disk_table");

                entity.Property(e => e.DiskId).HasColumnName("disk_id");

                entity.Property(e => e.DiskName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("disk_name");

                entity.Property(e => e.DiskTypeId).HasColumnName("disk_type_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("release_date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.DiskType)
                    .WithMany(p => p.DiskTables)
                    .HasForeignKey(d => d.DiskTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_tabl__disk___2C3393D0");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.DiskTables)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_tabl__genre__2D27B809");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.DiskTables)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_tabl__statu__2E1BDC42");
            });

            modelBuilder.Entity<DiskType>(entity =>
            {
                entity.ToTable("disk_type");

                entity.Property(e => e.DiskTypeId).HasColumnName("disk_type_id");

                entity.Property(e => e.Descrip)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("descrip");
            });

            modelBuilder.Entity<ViewBorrowerNoLoan>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Borrower_No_Loans");

                entity.Property(e => e.BorrowerId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("borrower_id");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("fname");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("lname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
