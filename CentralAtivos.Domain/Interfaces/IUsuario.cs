using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IUsuario
    {
        List<Usuario> GetAll();
        List<Usuario> GetByEmpresaID(int empresaID);
        Usuario GetByID(int id);
        void Insert(Usuario usuario);
        void Update(Usuario usuario);
        void Delete(int id);
        Usuario Login(string login, string senha);
        bool EmailExists(string email);
    }
}
