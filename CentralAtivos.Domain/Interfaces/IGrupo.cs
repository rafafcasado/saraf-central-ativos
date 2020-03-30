using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IGrupo
    {
        List<Grupo> GetByEmpresaID(int filialID);
        Grupo GetByID(int id);
        Grupo GetByCode(int empresaID, string codigo);
        void Insert(Grupo grupo);
        void Update(Grupo grupo);
        void Delete(int id);
        void DeleteAll(int empresaID);
    }
}
