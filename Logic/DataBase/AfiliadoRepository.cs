using System;
using System.Linq;
using System.Linq.Expressions;

namespace PromoToEvents.Logic.DataBase
{
    public interface IAfiliadoRepository
    {
        Afiliado First(Expression<Func<Afiliado, Afiliado>> query);
        Afiliado GetById(long id);
        Afiliado Create(Afiliado itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<Afiliado, TResult>> expression);
        IQueryable<Afiliado> Filter(Expression<Func<Afiliado, bool>> expression);
        Afiliado Update(Afiliado itemToUpdate, bool updateRole);
        Afiliado Delete(long id);
        void SaveChanges();
    }

    public class AfiliadoRepository : IAfiliadoRepository
    {
        private readonly Promo2EventEntities _context;
        private static AfiliadoRepository _instance;

        private const string UserTypeMortalLabel = "Mortal";
        private const string UserTypeInmortalLabel = "Inmortal";
        private const string ActiveUserActiveLabel = "Active";
        private const string ActiveUserInactiveLabel = "Inactive";

        private AfiliadoRepository(Promo2EventEntities ctx)
        {
            _context = ctx;
        }

        public static void Init(Promo2EventEntities context)
        {
            if(_instance == null)
                _instance = new AfiliadoRepository(context);
        }

        public static AfiliadoRepository GetInstance
        {
            get { return _instance ?? (_instance = new AfiliadoRepository(new Promo2EventEntities())); }
        }

        public Afiliado First(Expression<Func<Afiliado, Afiliado>> query)
        {
            var afiliados = _context.Afiliado.Select(query);
            return afiliados.Count() != 0 ? afiliados.First() : null;}

        public Afiliado GetById(long id)
        {
            var afiliados = _context.Afiliado.Where(x => x.idAfiliado == id);
            return afiliados.Count() != 0 ? afiliados.First() : null;
        }

        public Afiliado Create(Afiliado itemToCreate)
        {
            var afiliado = _context.Afiliado.Add(itemToCreate);
            _context.SaveChanges();
            return afiliado;
        }

        public IQueryable<TResult> Query<TResult>(Expression<Func<Afiliado, TResult>> expression)
        {
            return _context.Afiliado.Select(expression);
            
        }

        public IQueryable<Afiliado> Filter(Expression<Func<Afiliado, bool>> expression)
        {
            return _context.Afiliado.Where(expression);
        }

        public Afiliado Update(Afiliado itemToUpdate, bool updateRole)
        {
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Afiliado Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Afiliado.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public string UserTypeLabel(int raizVal)
        {
            return raizVal > 0 ? UserTypeMortalLabel : UserTypeInmortalLabel;
        }

        public string ActiveUserLabel(bool isActive)
        {
            return isActive ? ActiveUserActiveLabel : ActiveUserInactiveLabel;
        }
    }
}