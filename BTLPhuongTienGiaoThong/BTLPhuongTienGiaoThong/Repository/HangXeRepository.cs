using BTLPhuongTienGiaoThong.Models;

namespace BTLPhuongTienGiaoThong.Repository
{
    public class HangXeRepository : IHangXeRepository
    {
        private readonly HireCarContext _context;
        public HangXeRepository(HireCarContext context)
        {
            _context = context; 
        }

        public TblHangXe Add(TblHangXe hangXe)
        {
            _context.TblHangXes.Add(hangXe);
            _context.SaveChanges();
            return hangXe;
        }

        public TblHangXe Delete(string hang)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TblHangXe> GetAllHangXe()
        {
            return _context.TblHangXes;
        }

        public TblHangXe GetHangXe(string hang)
        {
            return _context.TblHangXes.Find(hang);
        }

        public TblHangXe Update(TblHangXe hangXe)
        {
            _context.Update(hangXe);

            _context.SaveChanges();
            return hangXe;  
        }
    }
}
