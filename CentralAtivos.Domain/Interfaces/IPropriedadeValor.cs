using CentralAtivos.Domain.Entities;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IPropriedadeValor
    {
        PropriedadeValor GetByID(int id);
        void Insert(PropriedadeValor propriedadeValor);

        void Update(PropriedadeValor propriedadeValor);

        void Delete(int id);
    }
}
