using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class InventarioConfigRepository : IInventarioConfig
    {
        public List<InventarioConfig> GetByInventarioID(int id)
        {
            List<InventarioConfig> configs = null;

            using(var ctx = new Context.Context())
            {
                configs = ctx.InventarioConfigs.Include("EntidadeCampo").Where(x => x.InventarioID == id && x.DataExclusao == null).OrderBy(x => x.EntidadeCampo.NomeCampo).ToList();
            }

            return configs;
        }

        public InventarioConfig GetByID(int id)
        {
            InventarioConfig config = null;

            using (var ctx = new Context.Context())
            {
                config = ctx.InventarioConfigs.Where(x => x.ID == id).SingleOrDefault();
            }

            return config;
        }

        public void Update(InventarioConfig config)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(config).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
