using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class MenuRepository : IMenu
    {
        public List<Menu> GetAll()
        {
            List<Menu> menus = null;

            using (var ctx = new Context.Context())
            {
                menus = ctx.Menus.Where(x => x.DataExclusao == null).OrderBy(x => x.TituloMenu).ToList();
            }

            return menus;
        }
    }
}
