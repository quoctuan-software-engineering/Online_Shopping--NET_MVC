using System;
using System.Collections.Generic;

namespace FashionShop.Data;

public partial class KhachHang
{
    public int MaKh { get; set; }

    public string MatKhau { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public bool? GioiTinh { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string DiaChi { get; set; } = null!;

    public string DienThoai { get; set; } = null!;

    public string? Email { get; set; }

    public string? Hinh { get; set; }

    public bool HieuLuc { get; set; }

    public int VaiTro { get; set; }

    public string? RandomKey { get; set; }

    public virtual ICollection<BanBe> BanBes { get; set; } = new List<BanBe>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual ICollection<YeuThich> YeuThiches { get; set; } = new List<YeuThich>();
}
