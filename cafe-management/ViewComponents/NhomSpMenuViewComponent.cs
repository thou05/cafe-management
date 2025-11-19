using Microsoft.AspNetCore.Mvc;
using cafe_management.Repository;

namespace cafe_management.ViewComponents
{
    public class NhomSpMenuViewComponent : ViewComponent
    {
        private readonly INhomSpRepository _nhomSp;
        public NhomSpMenuViewComponent(INhomSpRepository nhomSpRepository)
        {
            _nhomSp = nhomSpRepository;
        }

        public IViewComponentResult Invoke()
        {
            var nhomSp = _nhomSp.GetAllNhomSp().OrderBy(x => x.Name);
            return View(nhomSp);
        }
    }
}
