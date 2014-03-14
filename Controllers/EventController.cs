using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PromoToEvents.Logic.DataBase;
using PromoToEvents.Logic.Session;
using PromoToEvents.Models;

namespace PromoToEvents.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        //
        // GET: /Afiliado/
        private readonly EventoRepository _eventoRepository = EventoRepository.GetInstance;
        private readonly CiudadRepository _ciudadRepo = CiudadRepository.GetInstance;
        private readonly PaisRepository _paisRepo = PaisRepository.GetInstance;
        private readonly EstadoRepository _estadoRepo = EstadoRepository.GetInstance;
        private readonly CategoriaRepository _categoriaRepository = CategoriaRepository.GetInstance;

        public ActionResult Index()
        {
            return View(_eventoRepository.Query(x => x).ToList()
                .Select(x => new DisplayEventoModel
                {
                    Name = x.nombreEvento,
                    Active = x.activo,
                    ActiveLabel = _eventoRepository.GetActiveEventLabel(x.activo),
                    Address = x.direccion,
                    City = x.Ciudad.nombreCiudad,
                    Country = x.Pais.nombrePais,
                    State = x.Estado.nombreEstado,
                    StartDate = x.fechaInicio.ToShortDateString(),
                    IdAfiliado = x.Afiliado.nombreAfiliado,
                    ImgPath = x.imgUrl,
                    FinishDate = x.fechaExpiracion.ToShortDateString(),
                    Category = x.Categoria.nombreCategoria,
                    Description = x.descripcion,
                    IdEvento = x.idEvento,
                    ScoreAverage = x.totalPuntaje / (x.cantPuntuaciones == 0 ? 1 : x.cantPuntuaciones)
                }));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var evento = _eventoRepository.GetById(id);

            var model = new EditEventoModel
            {
                Active = _eventoRepository.GetActiveEventLabel(evento.activo),
                Address = evento.direccion,
                City = evento.idCiudad,
                Country = evento.idPais,
                State = evento.idEstado,
                Category = evento.idCategoria,
                Name = evento.nombreEvento,
                StartDate = evento.fechaInicio.ToShortDateString(),
                EndDate = evento.fechaExpiracion.ToShortDateString(),
                Description = evento.descripcion,
                IdEvento = evento.idEvento
            };

            ViewBag.Active = _eventoRepository.GetActiveCategoryList(evento.activo);
            ViewBag.Category = _categoriaRepository.GetCategories(evento.idCategoria);
            ViewBag.Country = _paisRepo.GetCountries(evento.idPais);
            ViewBag.City = _ciudadRepo.GetCities(evento.idCiudad);
            ViewBag.State = _estadoRepo.GetStates(evento.idEstado);


            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditEventoModel model)
        {
            var evento = _eventoRepository.GetById(model.IdEvento);
            
            if (model.PictureFile != null && model.PictureFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.PictureFile.FileName);
                if (fileName != null)
                {
                    var temp = "~/Content/dataImg/eventsImages";
                    var name = evento.idEvento.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(model.PictureFile.FileName);
                    var path = Path.Combine(Server.MapPath(temp), name);
                    model.PictureFile.SaveAs(path);
                    temp += "/" + name;
                    evento.imgUrl = temp;
                }
            }

            evento.direccion = model.Address;
            evento.fechaInicio = DateTime.Parse(model.StartDate);
            evento.fechaExpiracion = DateTime.Parse(model.EndDate);
            evento.nombreEvento = model.Name;
            evento.activo = _eventoRepository.GetActiveEventValue(model.Active);
            evento.idCiudad = model.City;
            //evento.Ciudad = _ciudadRepo.Filter(x => x.idCiudad == model.City).First();
            evento.idEstado = model.State;
            //evento.Estado = _estadoRepo.Filter(x => x.idEstado == model.State).First();
            evento.idPais = model.Country;
            //evento.Pais = _paisRepo.Filter(x => x.idPais == model.Country).First();
            evento.descripcion = model.Description;
            evento.idCategoria = model.Category;
            
            _eventoRepository.Update(evento);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Active = _eventoRepository.GetActiveCategoryList(false);
            ViewBag.Category = _categoriaRepository.GetCategories(1);
            ViewBag.Country = _paisRepo.GetCountries(1);
            ViewBag.City = _ciudadRepo.GetCities(1);
            ViewBag.State = _estadoRepo.GetStates(1);

            return View();
        }

        [HttpPost]
        public ActionResult Add(RegisterEventoModel model)
        {
            int idAfiliado = SessionLayer.Instance.GetUserLoggedId();
            var evento = new Evento
            {
                /*Ciudad = _ciudadRepo.Filter(x => x.idCiudad == model.Country).First(),
                Estado = _estadoRepo.Filter(x => x.idEstado == model.State).First(),
                Pais = _paisRepo.Filter(x => x.idPais == model.Country).First(),
                Afiliado = _afiliadoRepository.Filter(x => x.idAfiliado == idAfiliado).First(),*/
                direccion = model.Address,
                fechaInicio = DateTime.Parse(model.StartDate),
                fechaExpiracion = DateTime.Parse(model.EndDate),
                idCiudad = model.City,
                idEstado = model.State,
                idPais = model.Country,
                nombreEvento = model.Name,
                activo = _eventoRepository.GetActiveEventValue(model.Active),
                descripcion = model.Description,
                idCategoria = model.Category,
                idAfiliado = idAfiliado
            };

            _eventoRepository.Create(evento);

            if (model.PictureFile != null && model.PictureFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.PictureFile.FileName);
                if (fileName != null)
                {
                    var temp = "~/Content/dataImg/eventsImages";
                    var name = evento.idEvento.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(model.PictureFile.FileName);
                    var path = Path.Combine(Server.MapPath(temp), name);
                    model.PictureFile.SaveAs(path);
                    temp += "/" + name;
                    evento.imgUrl = temp;
                    _eventoRepository.Update(evento);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _eventoRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
