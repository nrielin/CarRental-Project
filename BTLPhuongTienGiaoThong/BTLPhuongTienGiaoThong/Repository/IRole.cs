using BTLPhuongTienGiaoThong.Models;

namespace BTLPhuongTienGiaoThong.Repository
{
    public interface IRole
    {
        TblRole Add(TblRole role);
        TblRole  Update(TblRole role);
        TblRole  Delete(string tenrole);
        TblRole  GetHangXe(string  role);
        IEnumerable<TblRole> GetAllMauXe();
    }
}
