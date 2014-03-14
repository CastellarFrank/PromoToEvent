using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace PromoToEvents.Logic.DataBase
{
    public interface IEventoRepository
    {
        Evento First(Expression<Func<Evento, Evento>> query);
        Evento GetById(long id);
        Evento Create(Evento itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<Evento, TResult>> expression);
        IQueryable<Evento> Filter(Expression<Func<Evento, bool>> expression);
        Evento Update(Evento itemToUpdate);
        Evento Delete(long id);
        void SaveChanges();
    }

    public class EventoRepository : IEventoRepository
    {
        private readonly Promo2EventEntities _context;
        private static EventoRepository _instance;

        private const string ActiveEventLabel = "Active";
        private const string InactiveEventLabel = "Inactive";

        private EventoRepository(Promo2EventEntities ctx)
        {
            _context = ctx;
        }

        public static void Init(Promo2EventEntities context)
        {
            if(_instance == null)
                _instance = new EventoRepository(context);
        }

        public static EventoRepository GetInstance
        {
            get { return _instance ?? (_instance = new EventoRepository(new Promo2EventEntities())); }
        }

        public Evento First(Expression<Func<Evento, Evento>> query)
        {
            var eventos = _context.Evento.Select(query);
            return eventos.Count() != 0 ? eventos.First() : null;}

        public Evento GetById(long id)
        {
            var eventos = _context.Evento.Where(x => x.idEvento == id);
            return eventos.Count() != 0 ? eventos.First() : null;
        }

        public Evento Create(Evento itemToCreate)
        {
            var evento = _context.Evento.Add(itemToCreate);
            _context.SaveChanges();
            return evento;
        }

        public IQueryable<TResult> Query<TResult>(Expression<Func<Evento, TResult>> expression)
        {
            return _context.Evento.Select(expression);
            
        }

        public IQueryable<Evento> Filter(Expression<Func<Evento, bool>> expression)
        {
            return _context.Evento.Where(expression);
        }

        public Evento Update(Evento itemToUpdate)
        {
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Evento Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Evento.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public string GetActiveEventLabel(bool isActive)
        {
            return isActive ? ActiveEventLabel : InactiveEventLabel;
        }

        public bool GetActiveEventValue(string userLabel)
        {
            return userLabel.Equals(ActiveEventLabel);
        }

        public SelectList GetActiveCategoryList(bool memberActive)
        {
             return new SelectList(new List<SelectListItem>
                {
                    new SelectListItem
                   {
                       Text = GetActiveEventLabel(true),
                       Value = GetActiveEventLabel(true),
                   },
                   new SelectListItem
                   {
                       Text = GetActiveEventLabel(false),
                       Value = GetActiveEventLabel(false),
                   }
                },
                "Value",
                "Text",
                GetActiveEventLabel(memberActive)
            );
        }
    }
}