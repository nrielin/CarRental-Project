using System;
using System.Collections.Generic;

namespace BTLPhuongTienGiaoThong.Models;

public partial class TblRole
{
    public int IRoleId { get; set; }

    public string? SRoleName { get; set; }

    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
