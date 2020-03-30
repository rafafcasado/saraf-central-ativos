using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.IO;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class ItemImagemController : ApiController
    {
        private readonly IItem _repository;

        public ItemImagemController(IItem repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Inclui uma Imagem no Item
        /// </summary>
        /// <param name="id">ID do Item relacionado à Imagem</param>
        /// <param name="imagem">String Base64</param>
        [HttpPost, Route("api/itemImagem/{id}")]
        public IHttpActionResult Post(int id, [FromBody] Imagem imagem)
        {
            try
            {
                var item = _repository.GetByID(id);

                var path = System.Configuration.ConfigurationManager.AppSettings["GaleriaImagens"] + item.ID + "\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var fileName = path + string.Format("{0:fffssmmHHyyyyMMDD}", DateTime.Now) + ".jpg";

                imagem.Conteudo = imagem.Conteudo.Replace("data:image/png;base64,", "");
                imagem.Conteudo = imagem.Conteudo.Replace("data:image/jpeg;base64,", "");

                Helpers.Imagem.SalvarImagem(imagem.Conteudo, fileName);

                item.Imagens = new System.Collections.Generic.List<string>();

                var files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    FileInfo info = new FileInfo(file);

                    item.Imagens.Add(System.Configuration.ConfigurationManager.AppSettings["GaleriaImagensLogico"] + item.ID + "/" + info.Name);
                }

                return Ok(item);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um Item
        /// </summary>
        [HttpDelete(), Route("api/itemImagem/{id}")]
        public IHttpActionResult Delete(int id, string imagem)
        {
            try
            {
                Item item = _repository.GetByID(id);

                if (item == null)
                    return NotFound();

                var path = System.Configuration.ConfigurationManager.AppSettings["GaleriaImagens"] + id;

                if (File.Exists(path + "\\" + imagem))
                    File.Delete(path + "\\" + imagem);

                var files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    FileInfo info = new FileInfo(file);

                    item.Imagens.Add(System.Configuration.ConfigurationManager.AppSettings["GaleriaImagensLogico"] + item.ID + "/" + info.Name);
                }

                return Ok(item);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
