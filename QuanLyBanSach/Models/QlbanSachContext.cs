using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanSach.Models;

public partial class QlbanSachContext : DbContext
{
    public QlbanSachContext()
    {
    }

    public QlbanSachContext(DbContextOptions<QlbanSachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<BanSaoSach> BanSaoSaches { get; set; }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<DanhMucSach> DanhMucSaches { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<TacGium> TacGia { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    public virtual DbSet<VanChuyen> VanChuyens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QLBanSach;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.MaAd).HasName("PK__Admin__27247E467A98CAA7");

            entity.ToTable("Admin");

            entity.Property(e => e.MaAd)
                .HasMaxLength(50)
                .HasColumnName("MaAD");
            entity.Property(e => e.HoTenAd)
                .HasMaxLength(50)
                .HasColumnName("HoTenAD");
            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
        });

        modelBuilder.Entity<BanSaoSach>(entity =>
        {
            entity.HasKey(e => e.MaBanSao).HasName("PK__BanSaoSa__488BCC422210094F");

            entity.ToTable("BanSaoSach");

            entity.Property(e => e.MaBanSao).HasMaxLength(50);
            entity.Property(e => e.MaSach).HasMaxLength(50);
            entity.Property(e => e.TinhTrangBs)
                .HasMaxLength(50)
                .HasColumnName("TinhTrangBS");

            entity.HasOne(d => d.MaSachNavigation).WithMany(p => p.BanSaoSaches)
                .HasForeignKey(d => d.MaSach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BanSaoSac__MaSac__4BAC3F29");
        });

        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => new { e.MaDonHang, e.MaSach }).HasName("PK__ChiTietD__D9B6D3EFA8DA574C");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.MaDonHang).HasMaxLength(50);
            entity.Property(e => e.MaSach).HasMaxLength(50);
            entity.Property(e => e.ThanhTien).HasColumnType("money");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDonHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDo__MaDon__4E88ABD4");

            entity.HasOne(d => d.MaSachNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDo__MaSac__4F7CD00D");
        });

        modelBuilder.Entity<DanhMucSach>(entity =>
        {
            entity.HasKey(e => e.MaDm).HasName("PK__DanhMucS__2725866EF84E3E9C");

            entity.ToTable("DanhMucSach");

            entity.Property(e => e.MaDm)
                .HasMaxLength(50)
                .HasColumnName("MaDM");
            entity.Property(e => e.TenDm)
                .HasMaxLength(50)
                .HasColumnName("TenDM");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang).HasName("PK__DonHang__129584ADAE2160DE");

            entity.ToTable("DonHang");

            entity.Property(e => e.MaDonHang).HasMaxLength(50);
            entity.Property(e => e.DiaDiemGh)
                .HasMaxLength(200)
                .HasColumnName("DiaDiemGH");
            entity.Property(e => e.GhiChu).HasMaxLength(200);
            entity.Property(e => e.MaNd)
                .HasMaxLength(50)
                .HasColumnName("MaND");
            entity.Property(e => e.NgayDat).HasColumnType("date");
            entity.Property(e => e.NgayGiao).HasColumnType("date");
            entity.Property(e => e.TenKh)
                .HasMaxLength(50)
                .HasColumnName("TenKH");
            entity.Property(e => e.TongTien).HasColumnType("money");
            entity.Property(e => e.TrangThaiDh)
                .HasMaxLength(50)
                .HasColumnName("TrangThaiDH");

            entity.HasOne(d => d.MaNdNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaNd)
                .HasConstraintName("FK__DonHang__MaND__3E52440B");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNd).HasName("PK__NguoiDun__2725D72449BE0FFF");

            entity.ToTable("NguoiDung");

            entity.Property(e => e.MaNd)
                .HasMaxLength(50)
                .HasColumnName("MaND");
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.DienThoai).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.HoTenNd)
                .HasMaxLength(50)
                .HasColumnName("HoTenND");
            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.NgayTao).HasColumnType("date");
            entity.Property(e => e.TaiKhoan).HasMaxLength(50);
            entity.Property(e => e.TrangThaiNd)
                .HasMaxLength(50)
                .HasColumnName("TrangThaiND");
        });

        modelBuilder.Entity<NhaXuatBan>(entity =>
        {
            entity.HasKey(e => e.MaNxb).HasName("PK__NhaXuatB__3A19482C6CBAECE0");

            entity.ToTable("NhaXuatBan");

            entity.Property(e => e.MaNxb)
                .HasMaxLength(50)
                .HasColumnName("MaNXB");
            entity.Property(e => e.TenNxb)
                .HasMaxLength(50)
                .HasColumnName("TenNXB");
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.MaSach).HasName("PK__Sach__B235742DCA9807B7");

            entity.ToTable("Sach");

            entity.Property(e => e.MaSach).HasMaxLength(50);
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.GiaBan).HasColumnType("money");
            entity.Property(e => e.MaDm)
                .HasMaxLength(50)
                .HasColumnName("MaDM");
            entity.Property(e => e.MaNxb)
                .HasMaxLength(50)
                .HasColumnName("MaNXB");
            entity.Property(e => e.MaTg)
                .HasMaxLength(50)
                .HasColumnName("MaTG");
            entity.Property(e => e.MoTa).HasMaxLength(200);
            entity.Property(e => e.NgayCapNhat).HasColumnType("date");
            entity.Property(e => e.SoLuongBs).HasColumnName("SoLuongBS");
            entity.Property(e => e.TenSach).HasMaxLength(50);

            entity.HasOne(d => d.MaDmNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaDm)
                .HasConstraintName("FK__Sach__MaDM__48CFD27E");

            entity.HasOne(d => d.MaNxbNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaNxb)
                .HasConstraintName("FK__Sach__MaNXB__46E78A0C");

            entity.HasOne(d => d.MaTgNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaTg)
                .HasConstraintName("FK__Sach__MaTG__47DBAE45");
        });

        modelBuilder.Entity<TacGium>(entity =>
        {
            entity.HasKey(e => e.MaTg).HasName("PK__TacGia__272500747CE6D955");

            entity.Property(e => e.MaTg)
                .HasMaxLength(50)
                .HasColumnName("MaTG");
            entity.Property(e => e.TenTg)
                .HasMaxLength(50)
                .HasColumnName("TenTG");
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaTt).HasName("PK__ThanhToa__272500791092A663");

            entity.ToTable("ThanhToan");

            entity.Property(e => e.MaTt)
                .HasMaxLength(50)
                .HasColumnName("MaTT");
            entity.Property(e => e.MaDonHang).HasMaxLength(50);
            entity.Property(e => e.TenTt)
                .HasMaxLength(50)
                .HasColumnName("TenTT");
            entity.Property(e => e.TrangThaiTt)
                .HasMaxLength(50)
                .HasColumnName("TrangThaiTT");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK__ThanhToan__MaDon__412EB0B6");
        });

        modelBuilder.Entity<VanChuyen>(entity =>
        {
            entity.HasKey(e => e.MaVc).HasName("PK__VanChuye__272510299EABC646");

            entity.ToTable("VanChuyen");

            entity.Property(e => e.MaVc)
                .HasMaxLength(50)
                .HasColumnName("MaVC");
            entity.Property(e => e.MaDonHang).HasMaxLength(50);
            entity.Property(e => e.TenVc)
                .HasMaxLength(50)
                .HasColumnName("TenVC");
            entity.Property(e => e.TrangThaiVc)
                .HasMaxLength(50)
                .HasColumnName("TrangThaiVC");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.VanChuyens)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK__VanChuyen__MaDon__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
