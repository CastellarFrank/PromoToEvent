

namespace PromoToEvents.Logic.DataBase
{
    public class InitializeDataBase
    {
        public static void Init()
        {
            var dBInstance = new Promo2EventEntities();
            AfiliadoRepository.Init(dBInstance);
        }
    }
}