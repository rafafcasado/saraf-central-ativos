using CentralAtivos.Domain.Entities;
using System.Collections.Generic;

namespace CentralAtivos.Domain.Interfaces
{
    public interface IUpload
    {
        List<Upload> GetAll();
        Upload GetByID(int id);
        void Insert(Upload upload);
        void Update(Upload upload);
    }
}
