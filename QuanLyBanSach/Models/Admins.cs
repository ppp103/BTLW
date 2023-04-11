using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanSach.Models;

public partial class Admins
{
    public int MaAd { get; set; }

	[RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$",ErrorMessage = "Mật khẩu có tối thiểu 8 ký tự, có chữ cái đầu viết hoa, có chữ số, ký tự đặc biệt")]
	public string? MatKhau { get; set; }

    public string? TenDangNhap { get; set; }

    public string? HoTenAd { get; set; }
}
