using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class ManutencaoBaseController : ApiController
    {
        private readonly IItem _repository;

        public ManutencaoBaseController(IItem repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Inclui um Item
        /// </summary>
        /// <param name="item">Objeto do tipo Item</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Item item)
        {
            try
            {
                item.StatusID = Enums.StatusItem.INSERIDO;

                _repository.Insert(item);

                var path = System.Configuration.ConfigurationManager.AppSettings["GaleriaImagens"] + item.ID;

                var files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    FileInfo info = new FileInfo(file);

                    item.Imagens.Add(System.Configuration.ConfigurationManager.AppSettings["GaleriaImagensLogico"] + item.ID + "/" + info.Name);
                }

                return Ok(item);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception)
            {
                return BadRequest("Erro ao Gravar Item. Favor verificar os Campos Obrigatórios e tentar novamente.");
            }
        }

        /// <summary>
        /// Atualiza os dados de um Item
        /// </summary>
        /// <param name="item">Objeto do tipo Item</param>
        /// <param name="id">ID do Item que será atualizado</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Item item)
        {
            try
            {
                var itemDB = _repository.GetByID(id);

                if (itemDB == null)
                    return NotFound();

                itemDB.StatusID = Enums.StatusItem.ALTERADO;
                itemDB.Codigo = item.Codigo;
                itemDB.CodigoAnterior = item.CodigoAnterior;
                itemDB.CodigoPM = item.CodigoPM;
                itemDB.DadosPM = item.DadosPM;
                itemDB.EspecieID = item.EspecieID;
                itemDB.Incorporacao = item.Incorporacao;
                itemDB.IncorporacaoAnterior = item.IncorporacaoAnterior;
                itemDB.ItemEstadoID = item.ItemEstadoID;
                itemDB.Latitude = item.Latitude;
                itemDB.LocalID = item.LocalID;
                itemDB.LocalPM = item.LocalPM;
                itemDB.Longitude = item.Longitude;
                itemDB.Marca = item.Marca;
                itemDB.Modelo = item.Modelo;
                itemDB.Nome = item.Nome;
                itemDB.NumeroSerie = item.NumeroSerie;
                itemDB.Observacao = item.Observacao;
                itemDB.ResponsavelID = item.ResponsavelID;
                itemDB.Tag = item.Tag;
                itemDB.Imagens = item.Imagens;
                itemDB.CamposExtras = item.CamposExtras;
                itemDB.PropriedadesValores = item.PropriedadesValores;

                _repository.Update(itemDB);

                var path = System.Configuration.ConfigurationManager.AppSettings["GaleriaImagens"] + itemDB.ID;

                var files = Directory.GetFiles(path);

                itemDB.Imagens = new System.Collections.Generic.List<string>();

                foreach (var file in files)
                {
                    FileInfo info = new FileInfo(file);

                    itemDB.Imagens.Add(System.Configuration.ConfigurationManager.AppSettings["GaleriaImagensLogico"] + itemDB.ID + "/" + info.Name);
                }

                return Ok(itemDB);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception)
            {
                return BadRequest("Erro ao Gravar Item. Favor verificar os Campos Obrigatórios e tentar novamente.");
            }
        }
    }
}
