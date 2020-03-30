using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IColetor
    {
        List<Coletor> GetAll();
        Coletor GetByID(int id);
        void Insert(Coletor coletor);
        void Update(Coletor coletor);
        void Delete(int id);
    }
}
