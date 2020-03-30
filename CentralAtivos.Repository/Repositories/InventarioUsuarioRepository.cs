using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class InventarioUsuarioRepository : IInventarioUsuario
    {
        public void Delete(int id)
        {
            using (var ctx = new Context.Context())
            {
                var inventarioUsuario = ctx.InventarioUsuarios.Find(id);

                if (inventarioUsuario != null)
                {
                    inventarioUsuario.DataExclusao = DateTime.Now;

                    ctx.Entry(inventarioUsuario).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }                    
            }
        }

        public List<InventarioUsuario> GetByInventarioID(int inventarioID)
        {
            List<InventarioUsuario> inventarioUsuarios = null;

            using (var ctx = new Context.Context())
            {
                inventarioUsuarios = ctx.InventarioUsuarios.Include("Usuario.Empresa").Include("Usuario.Perfil").Include("Perfil").Where(x => x.DataExclusao == null && x.InventarioID == inventarioID).ToList();
            }

            return inventarioUsuarios;
        }

        public InventarioUsuario GetByID(int id)
        {
            InventarioUsuario inventarioUsuario = null;

            using (var ctx = new Context.Context())
            {
                inventarioUsuario = ctx.InventarioUsuarios.Where(x => x.ID == id).SingleOrDefault();
            }

            return inventarioUsuario;
        }

        public void Insert(InventarioUsuario inventarioUsuario)
        {
            using (var ctx = new Context.Context())
            {
                var inventarioUsuarioCadastrado = ctx.InventarioUsuarios.Where(x => x.DataExclusao == null && x.InventarioID == inventarioUsuario.InventarioID && x.UsuarioID == inventarioUsuario.UsuarioID).SingleOrDefault();

                if (inventarioUsuarioCadastrado == null)
                {
                    ctx.InventarioUsuarios.Add(inventarioUsuario);
                    ctx.SaveChanges();
                }
                else
                    throw new ArgumentException("Usuário já vinculado a esse Inventário.");
            }
        }

    }
}
