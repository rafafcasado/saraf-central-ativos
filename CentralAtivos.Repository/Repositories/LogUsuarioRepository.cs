using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;

namespace CentralAtivos.Repository.Repositories
{
    public class LogUsuarioRepository : ILogUsuario
    {

        public void Insert(LogUsuario logUsuario)
        {
            using (var ctx = new Context.Context())
            {
                ctx.LogsUsuarios.Add(logUsuario);
                ctx.SaveChanges();
            }
        }
    }
}
