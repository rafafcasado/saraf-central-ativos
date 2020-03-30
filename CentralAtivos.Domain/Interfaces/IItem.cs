using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IItem
    {
        List<Item> GetByEmpresaID(int empresaID);
        List<Item> GetByLocalID(int localID);
        Item GetByID(int id);
        Item GetByCodigo(int empresaID, string codigo, int incorporacao);
        void Insert(Item item);
        void Update(Item item, bool apenasItem = false);
        void Delete(int id);
        void DeleteAll(int empresaID);
        bool VerifyIfExists(int empresaID, int filialID, string codigo, int incorporacao);
    }
}
