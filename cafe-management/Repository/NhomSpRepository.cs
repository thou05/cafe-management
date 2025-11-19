using Microsoft.EntityFrameworkCore;
using cafe_management.Models;

namespace  cafe_management.Repository
{
    public class NhomSpRepository : INhomSpRepository
    {
        private readonly CafeDBContext _context;

        public NhomSpRepository(CafeDBContext context)
        {
            _context = context;
        }

        public TbCategory Add(TbCategory nhomSanPham)
        {
            _context.TbCategories.Add(nhomSanPham);
            _context.SaveChanges();
            return nhomSanPham;
        }

        public TbCategory Delete(string maNhomSp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TbCategory> GetAllNhomSp()
        {
            return _context.TbCategories;
        }

        public TbCategory GetAllNhomSp(string maNhomSp)
        {
            return _context.TbCategories.Find(maNhomSp);
        }

        public TbCategory Update(TbCategory nhomSp)
        {
            _context.Update(nhomSp);
            _context.SaveChanges();
            return nhomSp;
        }
    }
}