using cafe_management.Models;
namespace cafe_management.Repository
{
    public interface INhomSpRepository
    {
        TbCategory Add(TbCategory nhomSp);
        TbCategory Update(TbCategory nhomSp);
        TbCategory Delete(String maNhomSp);
        TbCategory GetAllNhomSp(String maNhomSp);
        IEnumerable<TbCategory> GetAllNhomSp();
    }
}
