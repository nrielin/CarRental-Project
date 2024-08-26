using System;
using System.Collections.Generic;

namespace BTLPhuongTienGiaoThong.Models;

public partial class TblHangXe
{
    public int CarMake { get; set; }

    public string? NameMake { get; set; }

    public virtual ICollection<TblCar> TblCars { get; set; } = new List<TblCar>();
}
