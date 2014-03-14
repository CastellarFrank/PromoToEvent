using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace PromoToEvents.Logic.DataBase
{
    public interface ICiudadRepository
    {
        Ciudad First(Expression<Func<Ciudad, Ciudad>> query);
        Ciudad GetById(long id);
        Ciudad Create(Ciudad itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<Ciudad, TResult>> expression);
        IQueryable<Ciudad> Filter(Expression<Func<Ciudad, bool>> expression);
        Ciudad Update(Ciudad itemToUpdate);
        Ciudad Delete(long id);
        void SaveChanges();
    }

    public class CiudadRepository : ICiudadRepository
    {
        private readonly Promo2EventEntities _context;
        private static CiudadRepository _instance;

        private CiudadRepository(Promo2EventEntities ctx)
        {
            _context = ctx;
        }

        public static void Init(Promo2EventEntities context)
        {
            if (_instance == null)
                _instance = new CiudadRepository(context);
        }

        public static CiudadRepository GetInstance
        {
            get { return _instance ?? (_instance = new CiudadRepository(new Promo2EventEntities())); }
        }

        public Ciudad First(Expression<Func<Ciudad, Ciudad>> query)
        {
            var ciudades = _context.Ciudad.Select(query);
            return ciudades.Count() != 0 ? ciudades.First() : null;
        }

        public Ciudad GetById(long id)
        {
            var ciudades = _context.Ciudad.Where(x => x.idCiudad == id);
            return ciudades.Count() != 0 ? ciudades.First() : null;
        }

        public Ciudad Create(Ciudad itemToCreate)
        {
            var ciudad = _context.Ciudad.Add(itemToCreate);
            _context.SaveChanges();
            return ciudad;
        }

        public IQueryable<TResult> Query<TResult>(Expression<Func<Ciudad, TResult>> expression)
        {
            return _context.Ciudad.Select(expression);

        }

        public IQueryable<Ciudad> Filter(Expression<Func<Ciudad, bool>> expression)
        {
            return _context.Ciudad.Where(expression);
        }

        public Ciudad Update(Ciudad itemToUpdate)
        {
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Ciudad Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Ciudad.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public SelectList GetCities(int? selectedCiudadId)
        {
            return new SelectList(
                Query(x => x),
                "idCiudad",
                "nombreCiudad",
                selectedCiudadId
            );
        }
    }


}