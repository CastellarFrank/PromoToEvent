using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace PromoToEvents.Logic.DataBase
{
    public interface ICategoriaRepository
    {
        Categoria First(Expression<Func<Categoria, Categoria>> query);
        Categoria GetById(long id);
        Categoria Create(Categoria itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<Categoria, TResult>> expression);
        IQueryable<Categoria> Filter(Expression<Func<Categoria, bool>> expression);
        Categoria Update(Categoria itemToUpdate);
        Categoria Delete(long id);
        void SaveChanges();
    }

    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly Promo2EventEntities _context;
        private static CategoriaRepository _instance;

        private const string ActiveCategoryLabel = "Active";
        private const string InactiveCategoryLabel = "Inactive";

        private CategoriaRepository(Promo2EventEntities ctx)
        {
            _context = ctx;
        }

        public static void Init(Promo2EventEntities context)
        {
            if (_instance == null)
                _instance = new CategoriaRepository(context);
        }

        public static CategoriaRepository GetInstance
        {
            get { return _instance ?? (_instance = new CategoriaRepository(new Promo2EventEntities())); }
        }

        public Categoria First(Expression<Func<Categoria, Categoria>> query)
        {
            var categorias = _context.Categoria.Select(query);
            return categorias.Count() != 0 ? categorias.First() : null;
        }

        public Categoria GetById(long id)
        {
            var categorias = _context.Categoria.Where(x => x.idCategoria == id);
            return categorias.Count() != 0 ? categorias.First() : null;
        }

        public Categoria Create(Categoria itemToCreate)
        {
            var categoria = _context.Categoria.Add(itemToCreate);
            _context.SaveChanges();
            return categoria;
        }

        public IQueryable<TResult> Query<TResult>(Expression<Func<Categoria, TResult>> expression)
        {
            return _context.Categoria.Select(expression);

        }

        public IQueryable<Categoria> Filter(Expression<Func<Categoria, bool>> expression)
        {
            return _context.Categoria.Where(expression);
        }

        public Categoria Update(Categoria itemToUpdate)
        {
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Categoria Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Categoria.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public SelectList GetCategories(int? selectedCategoryId)
        {
            
            return new SelectList(
                Query(x => x),
                "idCategoria",
                "nombreCategoria",
                selectedCategoryId
            );
        }

        public string GetActiveCategoryLabel(bool isActive)
        {
            return isActive ? ActiveCategoryLabel : InactiveCategoryLabel;
        }

        public bool ActiveCategoryValue(string categoryLabel)
        {
            return categoryLabel.Equals(ActiveCategoryLabel);
        }

        public SelectList GetActiveCategoryList(bool memberActive)
        {
            return new SelectList(new List<SelectListItem>
                {
                    new SelectListItem
                   {
                       Text = GetActiveCategoryLabel(true),
                       Value = GetActiveCategoryLabel(true),
                   },
                   new SelectListItem
                   {
                       Text = GetActiveCategoryLabel(false),
                       Value = GetActiveCategoryLabel(false),
                   }
                },
               "Value",
               "Text",
               GetActiveCategoryLabel(memberActive)
           );
        }

    }


}