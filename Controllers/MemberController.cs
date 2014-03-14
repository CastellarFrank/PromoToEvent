using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PromoToEvents.Logic.DataBase;
using PromoToEvents.Models;

namespace PromoToEvents.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        //
        // GET: /Afiliado/
        private readonly AfiliadoRepository _afiliadoRepo = AfiliadoRepository.GetInstance;
        private readonly CiudadRepository _ciudadRepo = CiudadRepository.GetInstance;
        private readonly PaisRepository _paisRepo = PaisRepository.GetInstance;
        private readonly EstadoRepository _estadoRepo = EstadoRepository.GetInstance;

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
            var member = _afiliadoRepo.GetById(id);

            var model = new EditAfiliadoModel
            {
                Active = _afiliadoRepo.ActiveUserLabel(member.statusAfiliado),
                Address = member.direccionAfiliado,
                City = member.idCiudad,
                Country = member.idPais,
                Email = member.emailAfiliado,
                IdAfiliado = member.idAfiliado,
                Name = member.nombreAfiliado,
                State = member.idEstado,
                UserType = _afiliadoRepo.UserTypeLabel(member.raizVal),
                ConfirmPassword = member.passwordAfiliado,
                Password = member.passwordAfiliado
            };

            ViewBag.Active = _afiliadoRepo.GetActiveMemberList(member.statusAfiliado);
            ViewBag.UserType = _afiliadoRepo.GetMemberTypeList(member.raizVal);
            ViewBag.Country = _paisRepo.GetCountries(member.idPais);
            ViewBag.City = _ciudadRepo.GetCities(member.idCiudad);
            ViewBag.State = _estadoRepo.GetStates(member.idEstado);


            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditAfiliadoModel model)
        {
            var member = _afiliadoRepo.GetById(model.IdAfiliado);
            
            if (model.PictureFile != null && model.PictureFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.PictureFile.FileName);
                if (fileName != null)
                {
                    var temp = "~/Content/dataImg/membersImages";
                    var name = member.idAfiliado.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(model.PictureFile.FileName);
                    var path = Path.Combine(Server.MapPath(temp), name);
                    model.PictureFile.SaveAs(path);
                    temp += "/" + name;
                    member.imgPathAfiliado = temp;
                }
            }

            member.direccionAfiliado = model.Address;
            member.emailAfiliado = model.Email;
            member.fechaModiAfiliado = DateTime.Now;
            member.nombreAfiliado = model.Name;
            member.passwordAfiliado = model.Password;
            member.raizVal = _afiliadoRepo.UserTypeValue(model.UserType);
            member.statusAfiliado = _afiliadoRepo.ActiveUserValue(model.Active);
            
            member.idCiudad = model.City;
            //member.Ciudad = _ciudadRepo.Filter(x => x.idCiudad == model.City).First();
            member.idEstado = model.State;
            //member.Estado = _estadoRepo.Filter(x => x.idEstado == model.State).First();
            member.idPais = model.Country;
            //member.Pais = _paisRepo.Filter(x => x.idPais == model.Country).First();
            
            _afiliadoRepo.Update(member);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Active = _afiliadoRepo.GetActiveMemberList(false);
            ViewBag.UserType = _afiliadoRepo.GetMemberTypeList(1);
            ViewBag.Country = _paisRepo.GetCountries(1);
            ViewBag.City = _ciudadRepo.GetCities(1);
            ViewBag.State = _estadoRepo.GetStates(1);

            return View();
        }

        [HttpPost]
        public ActionResult Add(RegisterAfiliadoModel model)
        {
            var currentDate = DateTime.Now;

            var member = new Afiliado
            {
                /*Ciudad = _ciudadRepo.Filter(x => x.idCiudad == model.Country).First(),
                Estado = _estadoRepo.Filter(x => x.idEstado == model.State).First(),
                Pais = _paisRepo.Filter(x => x.idPais == model.Country).First(),*/
                raizVal = _afiliadoRepo.UserTypeValue(model.UserType),
                direccionAfiliado = model.Address,
                emailAfiliado = model.Email,
                fechaCreoAfiliado = currentDate,
                fechaModiAfiliado = currentDate,
                idCiudad = model.City,
                idEstado = model.State,
                idPais = model.Country,
                nombreAfiliado = model.Name,
                passwordAfiliado = model.Password,
                statusAfiliado = _afiliadoRepo.ActiveUserValue(model.Active),
            };

            _afiliadoRepo.Create(member);

            if (model.PictureFile != null && model.PictureFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.PictureFile.FileName);
                if (fileName != null)
                {
                    var temp = "~/Content/dataImg/membersImages";
                    var name = member.idAfiliado.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(model.PictureFile.FileName);
                    var path = Path.Combine(Server.MapPath(temp), name);
                    model.PictureFile.SaveAs(path);
                    temp += "/" + name;
                    member.imgPathAfiliado = temp;
                    _afiliadoRepo.Update(member);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _afiliadoRepo.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
