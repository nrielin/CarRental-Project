using System;
using System.Collections.Generic;

namespace BTLPhuongTienGiaoThong.Models;

public partial class TblUser
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int IUserRoleId { get; set; }

    public virtual TblRole? IUserRole { get; set; } 
}
