namespace QuanLyBanSach.Models.BooksModels
{
    public class Books
    {

        public int? MaDm { get; set; } 

        public string? TenDm { get; set; }
		public int? MaTg { get; set; } 

		public string? TenTg { get; set; }
		public int? MaSach { get; set; }
		public int? SoLuong { get; set; }

		public string? TenSach { get; set; }
		public decimal? GiaBan { get; set; }
		public string? Anh { get; set; }
		public string? AnhTg { get; set; }

		public string? MoTa { get; set; }
		public DateTime? NgayCapNhat { get; set; }

		public int? MaNxb { get; set; }
		public string? TenNxb { get; set; }
	}
}
