using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace PromoToEvents.Logic.DataBase
{
    public interface IEstadoRepository
    {
        Estado First(Expression<Func<Estado, Estado>> query);
        Estado GetById(long id);
        Estado Create(Estado itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<Estado, TResult>> expression);
        IQueryable<Estado> Filter(Expression<Func<Estado, bool>> expression);
        Estado Update(Estado itemToUpdate);
        Estado Delete(long id);
        void SaveChanges();
    }

    public class EstadoRepository : IEstadoRepository
    {
        private readonly Promo2EventEntities _context;
        private static EstadoRepository _instance;

        private EstadoRepository(Promo2EventEntities ctx)
        {
            _context = ctx;
        }

        public static void Init(Promo2EventEntities context)
        {
            if (_instance == null)
                _instance = new EstadoRepository(context);
        }

        public static EstadoRepository GetInstance
        {
            get { return _instance ?? (_instance = new EstadoRepository(new Promo2EventEntities())); }
        }

        public Estado First(Expression<Func<Estado, Estado>> query)
        {
            var estados = _context.Estado.Select(query);
            return estados.Count() != 0 ? estados.First() : null;
        }

        public Estado GetById(long id)
        {
            var estados = _context.Estado.Where(x => x.idEstado == id);
            return estados.Count() != 0 ? estados.First() : null;
        }

        public Estado Create(Estado itemToCreate)
        {
            var estado = _context.Estado.Add(itemToCreate);
            _context.SaveChanges();
            return estado;
        }

        public IQueryable<TResult> Query<TResult>(Expression<Func<Estado, TResult>> expression)
        {
            return _context.Estado.Select(expression);

        }

        public IQueryable<Estado> Filter(Expression<Func<Estado, bool>> expression)
        {
            return _context.Estado.Where(expression);
        }

        public Estado Update(Estado itemToUpdate)
        {
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Estado Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Estado.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public SelectList GetStates(int? selectedEstadoId)
        {
            return new SelectList(
                Query(x => x),
                "idEstado",
                "nombreEstado",
                selectedEstadoId
            );
        }
    }


}