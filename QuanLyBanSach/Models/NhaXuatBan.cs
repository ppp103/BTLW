﻿using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class NhaXuatBan
{
    public int MaNxb { get; set; }

    public string? TenNxb { get; set; }

    public virtual ICollection<Sach> Saches { get; } = new List<Sach>();
}