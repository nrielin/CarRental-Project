using BTLPhuongTienGiaoThong.Models;
namespace BTLPhuongTienGiaoThong.Repository
{
    public interface IHangXeRepository
    {
        TblHangXe Add(TblHangXe hangXe);
        TblHangXe Update(TblHangXe hangXe);
        TblHangXe Delete(string hang);
        TblHangXe GetHangXe(string hang);
        IEnumerable<TblHangXe> GetAllHangXe();

    }
}
