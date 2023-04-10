namespace QuanLyBanSach.Models.BooksModels
{
    public class Books
    {

        public int? MaDm { get; set; } 

        public string? TenDm { get; set; }
		public int? MaTg { get; set; } 

		public string? TenTg { get; set; }
		public int? MaSach { get; set; }

		public string? TenSach { get; set; }
		public decimal? GiaBan { get; set; }
		public string? Anh { get; set; }
	}
}
