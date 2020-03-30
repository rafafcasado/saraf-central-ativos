using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IPermissao
    {
        List<Permissao> GetByPerfilID(int PerfilID);
        void Insert(Permissao permissao);
        void Update(int perfilID, string funcionalidade, string metodo);
        void Delete(int id);
    }
}
