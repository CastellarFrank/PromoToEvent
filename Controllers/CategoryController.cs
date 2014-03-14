using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PromoToEvents.Logic.DataBase;
using PromoToEvents.Models;

namespace PromoToEvents.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        //
        // GET: /Afiliado/
        private readonly CategoriaRepository _categoriaRepo = CategoriaRepository.GetInstance;

        public ActionResult Index()
        {
            return View(_categoriaRepo.Query(x => x).ToList()
                .Select(x => new DisplayCategoriaModel
                {
                    IdCategoria = x.idCategoria,
                    ImgPath = x.imgPathCategoria,
                    Name = x.nombreCategoria,
                    LabelStatus = _categoriaRepo.GetActiveCategoryLabel(x.statusCategoria),
                    Status = x.statusCategoria
                }));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = _categoriaRepo.GetById(id);

            var model = new EditCategoriaModel
            {

                Active = _categoriaRepo.GetActiveCategoryLabel(category.statusCategoria),
                ImgPath = category.imgPathCategoria,
                Name = category.nombreCategoria,
                IdCategoria = category.idCategoria
            };

            ViewBag.Active = _categoriaRepo.GetActiveCategoryList(category.statusCategoria);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoriaModel model)
        {
            var category = _categoriaRepo.GetById(model.IdCategoria);
            
            if (model.PictureFile != null && model.PictureFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.PictureFile.FileName);
                if (fileName != null)
                {
                    var temp = "~/Content/dataImg/categoriesImages";
                    var name = category.idCategoria.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(model.PictureFile.FileName);
                    var path = Path.Combine(Server.MapPath(temp), name);
                    model.PictureFile.SaveAs(path);
                    temp += "/" + name;
                    category.imgPathCategoria = temp;
                }
            }

            category.nombreCategoria = model.Name;
            category.statusCategoria = _categoriaRepo.ActiveCategoryValue(model.Active);

            _categoriaRepo.Update(category);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Active = _categoriaRepo.GetActiveCategoryList(true);

            return View();
        }

        [HttpPost]
        public ActionResult Add(RegisterCategoriaModel model)
        {
            var category = new Categoria
            {
                nombreCategoria = model.Name,
                statusCategoria = _categoriaRepo.ActiveCategoryValue(model.Active)
            };

            _categoriaRepo.Create(category);

            if (model.PictureFile != null && model.PictureFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.PictureFile.FileName);
                if (fileName != null)
                {
                    var temp = "~/Content/dataImg/categoriesImages";
                    var name = category.idCategoria.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(model.PictureFile.FileName);
                    var path = Path.Combine(Server.MapPath(temp), name);
                    model.PictureFile.SaveAs(path);
                    temp += "/" + name;
                    category.imgPathCategoria = temp;
                    _categoriaRepo.Update(category);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _categoriaRepo.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
