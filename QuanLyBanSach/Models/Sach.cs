using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanSach.Models;

public partial class Sach
{
    public int MaSach { get; set; }

    [Display(Name = "tên sách")]
    [Required(ErrorMessage = "Vui lòng nhập giá trị cho {0}")] 
    public string? TenSach { get; set; }

    [Display(Name = "giá bán")]
    [Required(ErrorMessage = "Vui lòng nhập giá trị cho {0}")]
    public decimal? GiaBan { get; set; }
    
    [Display(Name = "mô tả")]
    [Required(ErrorMessage = "Vui lòng nhập giá trị cho {0}")]
    public string? MoTa { get; set; }

    public int? SoLuongBs { get; set; }

    public string? Anh { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    [Display(Name = "số lượng")]
    [Required(ErrorMessage = "Vui lòng chọn {0}")]
    [Range(0,1000, ErrorMessage = "Nhập số từ 0-1000")]
    public int? SoLuongCon { get; set; }

    [Display(Name = "mã nhà xuất bản")]
    [Required(ErrorMessage = "Vui lòng chọn {0}")]
    public int? MaNxb { get; set; }

    [Display(Name = "mã tác giả")]
    [Required(ErrorMessage = "Vui lòng chọn {0}")]
    public int? MaTg { get; set; }

    [Display(Name = "mã danh mục")]
    [Required(ErrorMessage = "Vui lòng chọn {0}")]
    public int? MaDm { get; set; }

    public virtual ICollection<BanSaoSach> BanSaoSaches { get; } = new List<BanSaoSach>();

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; } = new List<ChiTietDonHang>();

    public virtual DanhMucSach? MaDmNavigation { get; set; }

    public virtual NhaXuatBan? MaNxbNavigation { get; set; }

    public virtual TacGium? MaTgNavigation { get; set; }
}
