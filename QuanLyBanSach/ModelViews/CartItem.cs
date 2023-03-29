using QuanLyBanSach.Models;

namespace QuanLyBanSach.ModelViews
{
    public class CartItem
    {
        public int Id { get; set; }
        public Sach sach { get; set; } 
        public int soLuong { get; set; }
        public double tongTien => (double)(sach.GiaBan.Value * soLuong);
    }
}
