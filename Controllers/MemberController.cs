using System.Linq;
using System.Web.Mvc;
using PromoToEvents.Logic.DataBase;
using PromoToEvents.Models;

namespace PromoToEvents.Controllers
{
    public class MemberController : Controller
    {
        //
        // GET: /Afiliado/
        private readonly AfiliadoRepository _afiliadoRepo = AfiliadoRepository.GetInstance;

        public ActionResult Index()
        {
            return View(_afiliadoRepo.Query(x => x).ToList()
                .Select(x => new DisplayAfiliadoModel
                {
                    Name = x.nombreAfiliado,
                    Active = x.statusAfiliado,
                    ActiveLabel = _afiliadoRepo.ActiveUserLabel(x.statusAfiliado),
                    Address = x.direccionAfiliado,
                    City = x.Ciudad.nombreCiudad,
                    Country = x.Pais.nombrePais,
                    CreatedDate = x.fechaCreoAfiliado.ToShortDateString(),
                    Email = x.emailAfiliado,
                    IdAfiliado = x.idAfiliado,
                    ImgPath = x.imgPathAfiliado,
                    ModifyDate = x.fechaModiAfiliado.ToShortDateString(),
                    State = x.Estado.nombreEstado,
                    UserType = _afiliadoRepo.UserTypeLabel(x.raizVal)
                }));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}
