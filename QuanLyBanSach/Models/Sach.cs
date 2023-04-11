using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanSach.Models;

public partial class Sach
{
    public int MaSach { get; set; }

    public string? TenSach { get; set; }

    public decimal? GiaBan { get; set; }

    public string? MoTa { get; set; }

    public int? SoLuongBs { get; set; }

    public string? Anh { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
    public DateTime? NgayCapNhat { get; set; }

    public int? SoLuongCon { get; set; }

    public int? MaNxb { get; set; }

    public int? MaTg { get; set; }

    public int? MaDm { get; set; }

    public virtual ICollection<BanSaoSach> BanSaoSaches { get; } = new List<BanSaoSach>();

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; } = new List<ChiTietDonHang>();

    public virtual DanhMucSach? MaDmNavigation { get; set; }

    public virtual NhaXuatBan? MaNxbNavigation { get; set; }

    public virtual TacGia? MaTgNavigation { get; set; }
}
