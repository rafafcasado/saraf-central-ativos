using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class InventarioRepository : IInventario
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var inventario = ctx.Inventarios.Find(id);

                if (inventario != null)
                {
                    inventario.DataExclusao = DateTime.Now;

                    ctx.Entry(inventario).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
        }

        public List<Inventario> GetByEmpresaID(int empresaID)
        {
            List<Inventario> inventarios = null;

            using (var ctx = new Context.Context())
            {
                inventarios = ctx.Inventarios.Include("Empresa").Include("Status").Include("InventarioFiliais.Filial").Include("InventarioLocais.Local.Filial").Include("Usuarios").Where(x => x.DataExclusao == null && x.EmpresaID == empresaID).ToList();
            }

            return inventarios;
        }

        public List<Inventario> GetByInventarioUsuarioID(int usuarioID)
        {
            var ctx = new Context.Context();

            var inventarios = (from iu in ctx.InventarioUsuarios
                               join i in ctx.Inventarios on iu.InventarioID equals i.ID
                               where iu.UsuarioID == usuarioID && iu.DataExclusao == null
                               select i).Distinct();

            return inventarios.ToList();
        }

        public Inventario GetByID(int id)
        {
            Inventario inventario = null;

            using (var ctx = new Context.Context())
            {
                inventario = ctx.Inventarios.Where(x => x.DataExclusao == null && x.ID == id).SingleOrDefault();
            }

            return inventario;
        }

        public void Insert(Inventario inventario)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Inventarios.Add(inventario);
                ctx.SaveChanges();

                var campos = ctx.EntidadeCampos.Where(x => x.Entidade == Enums.Entidade.ITEM && x.DataExclusao == null);

                foreach (var c in campos)
                {
                    ctx.InventarioConfigs.Add(new InventarioConfig() { EntidadeCampoID = c.ID, InventarioID = inventario.ID, Obrigatorio = true, Visivel = true });
                }

                ctx.SaveChanges();
            }
        }

        public void Update(Inventario inventario)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(inventario).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

    }
}
