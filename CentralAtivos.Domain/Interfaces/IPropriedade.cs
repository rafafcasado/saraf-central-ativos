using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IPropriedade
    {
        List<Propriedade> GetByEmpresaID(int empresaID);
        Propriedade GetByID(int id);
        void Insert(Propriedade propriedade);
        void Update(Propriedade propriedade);
        void Delete(int id);
        void DeleteAll(int empresaID);
    }
}
