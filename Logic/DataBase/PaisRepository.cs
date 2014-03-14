using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace PromoToEvents.Logic.DataBase
{
    public interface IPaisRepository
    {
        Pais First(Expression<Func<Pais, Pais>> query);
        Pais GetById(long id);
        Pais Create(Pais itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<Pais, TResult>> expression);
        IQueryable<Pais> Filter(Expression<Func<Pais, bool>> expression);
        Pais Update(Pais itemToUpdate);
        Pais Delete(long id);
        void SaveChanges();
    }

    public class PaisRepository : IPaisRepository
    {
        private readonly Promo2EventEntities _context;
        private static PaisRepository _instance;

        private PaisRepository(Promo2EventEntities ctx)
        {
            _context = ctx;
        }

        public static void Init(Promo2EventEntities context)
        {
            if (_instance == null)
                _instance = new PaisRepository(context);
        }

        public static PaisRepository GetInstance
        {
            get { return _instance ?? (_instance = new PaisRepository(new Promo2EventEntities())); }
        }

        public Pais First(Expression<Func<Pais, Pais>> query)
        {
            var paises = _context.Pais.Select(query);
            return paises.Count() != 0 ? paises.First() : null;
        }

        public Pais GetById(long id)
        {
            var paises = _context.Pais.Where(x => x.idPais == id);
            return paises.Count() != 0 ? paises.First() : null;
        }

        public Pais Create(Pais itemToCreate)
        {
            var pais = _context.Pais.Add(itemToCreate);
            _context.SaveChanges();
            return pais;
        }

        public IQueryable<TResult> Query<TResult>(Expression<Func<Pais, TResult>> expression)
        {
            return _context.Pais.Select(expression);

        }

        public IQueryable<Pais> Filter(Expression<Func<Pais, bool>> expression)
        {
            return _context.Pais.Where(expression);
        }

        public Pais Update(Pais itemToUpdate)
        {
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Pais Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Pais.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public SelectList GetCountries(int? selectedCountryId)
        {
            return new SelectList(
                Query(x => x),
                "idPais",
                "nombrePais",
                selectedCountryId
            );
        }
    }


}