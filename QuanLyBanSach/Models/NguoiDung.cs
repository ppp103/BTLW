using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanSach.Models;

public partial class NguoiDung
{
    public int MaNd { get; set; }

	[Display(Name = "tên người dùng")]
	[Required(ErrorMessage = "Vui lòng nhập {0}")]
	public string? HoTenNd { get; set; }

	[Display(Name = "Email")]
	[Required(ErrorMessage = "Vui lòng nhập {0}")]
	[RegularExpression(@"^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$", ErrorMessage = "Email không hợp lệ")]
	public string? Email { get; set; }

	[Display(Name = "mật khẩu")]
	[Required(ErrorMessage = "Vui lòng nhập {0}")]
	[RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Mật khẩu có tối thiểu 8 ký tự, có chữ cái đầu viết hoa, có chữ số, ký tự đặc biệt")]
	public string? MatKhau { get; set; }

    public string? DienThoai { get; set; }

	[Display(Name = "tài khoản")]
	[Required(ErrorMessage = "Vui lòng nhập {0}")]
	public string? TaiKhoan { get; set; }

    public string? TrangThaiNd { get; set; }

	[Display(Name = "ảnh")]
	[Required(ErrorMessage = "Vui lòng nhập {0}")]
	[FileExtensions(Extensions = "jpg,png,jpeg", ErrorMessage = "Nhập sai định dạng file")]
	public string? Anh { get; set; }

	[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
	public DateTime? NgayTao { get; set; }

	[Display(Name = "địa chỉ")]
	[Required(ErrorMessage = "Vui lòng nhập {0}")]
	public string? DiaChi { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; } = new List<DonHang>();
}
