using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class PerfilMenuRepository : IPerfilMenu
    {
        public void Delete(int id)
        {
            PerfilMenu perfilMenu = null;

            using (var ctx = new Context.Context())
            {
                perfilMenu = ctx.PerfilMenus.Find(id);

                if (perfilMenu != null)
                {
                    ctx.PerfilMenus.Remove(perfilMenu);
                    ctx.SaveChanges();
                }                
            }
        }

        public List<PerfilMenu> GetByPerfilID(int perfilID)
        {
            List<PerfilMenu> perfilMenus = null;

            using (var ctx = new Context.Context())
            {
                perfilMenus = ctx.PerfilMenus.Include("Menu").Where(x => x.DataExclusao == null && x.PerfilID == perfilID).ToList();
            }

            return perfilMenus;
        }

        public PerfilMenu GetByID(int id)
        {
            PerfilMenu perfilMenu = null;

            using (var ctx = new Context.Context())
            {
                perfilMenu = ctx.PerfilMenus.Find(id);
            }

            return perfilMenu;
        }

        public void Insert(PerfilMenu perfilMenu)
        {
            using (var ctx = new Context.Context())
            {
                ctx.PerfilMenus.Add(perfilMenu);
                ctx.SaveChanges();
            }
        }
    }
}
