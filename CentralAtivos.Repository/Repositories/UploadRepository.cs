using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CentralAtivos.Repository.Repositories
{
    public class UploadRepository : IUpload
    {
        public void Insert(Upload upload)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Uploads.Add(upload);
                ctx.SaveChanges();
            }
        }

        public Upload GetByID(int id)
        {
            Upload upload = null;

            using (var ctx = new Context.Context())
            {
                upload = ctx.Uploads.Find(id);
            }

            return upload;
        }

        public List<Upload> GetAll()
        {
            List<Upload> uploads = null;

            using (var ctx = new Context.Context())
            {
                uploads = ctx.Uploads.Include("Empresa").Include("Usuario.Perfil").Where(x => x.DataExclusao == null).ToList();
            }

            return uploads;
        }

        public void Update(Upload upload)
        {
            using (var ctx = new Context.Context())
            {
                ctx.Entry(upload).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
