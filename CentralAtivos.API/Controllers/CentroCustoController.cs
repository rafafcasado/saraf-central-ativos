using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class CentroCustoController : ApiController
    {
        private readonly ICentroCusto _repository;

        public CentroCustoController(ICentroCusto repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista dos Principais Centros de Custo Ativos Cadastrados para uma Empresa de acordo com o ID informado
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet]
        [Route("api/centroCusto/getPrincipaisByEmpresaID/{empresaID}")]
        public IHttpActionResult GetPrincipaisByEmpresaID(int empresaID)
        {
            try
            {
                var centros = _repository.GetByEmpresaID(empresaID);

                centros = centros.Where(x => x.PaiID == null).ToList();

                return Ok(centros);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Lista dos Sub Centros de Custo Ativos Cadastrados para um Centro de Custo de acordo com o ID informado
        /// </summary>
        /// <param name="centroCustoID">ID do Centro de Custo</param>
        [HttpGet]
        [Route("api/centroCusto/getSubCentrosByCentroID/{centroCustoID}")]
        public IHttpActionResult GetSubCentrosByCentroID(int centroCustoID)
        {
            try
            {
                var centro = _repository.GetByID(centroCustoID);

                if (centro == null)
                    return NotFound();

                return Ok(centro.Filhos.Where(x => x.DataExclusao == null));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna todos os Centros de Custo de acordo com o ID da Empresa informada
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet, Route("api/centroCusto/getAllByEmpresaID/{empresaID}")]
        public IHttpActionResult GetAllByEmpresaID(int empresaID)
        {
            try
            {
                var centros = _repository.GetByEmpresaID(empresaID);

                if (centros.Count > 0)
                {
                    foreach (var centro in centros)
                    {
                        bool fim = false;
                        var pai = centro.PaiID == null ? null : centros.Where(x => x.ID == centro.PaiID).SingleOrDefault();

                        while (!fim)
                        {
                            if (pai == null)
                            {
                                fim = true;
                                centro.CaminhoPao = centro.CaminhoPao == null || centro.CaminhoPao.Length <= 0 ? centro.Nome : (centro.CaminhoPao + centro.Nome);
                            }
                            else
                            {

                                fim = false;
                                centro.CaminhoPao = pai.Nome + " > " + centro.CaminhoPao;
                                pai = centros.Where(x => x.ID == pai.PaiID).SingleOrDefault();
                            }
                        }

                    }
                }

                centros = centros.OrderBy(x => x.CodigoInterno).ToList();

                return Ok(centros);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Centro de Custo relacionado ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var centroCusto = _repository.GetByID(id);

                if (centroCusto == null)
                    return NotFound();

                return Ok(centroCusto);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui um Centro de Custo
        /// </summary>
        /// <param name="centroCusto">Objeto do tipo CentroCusto</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] CentroCusto centroCusto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var centros = _repository.GetByEmpresaID(centroCusto.EmpresaID);

                if (centros.Count == 0)
                    centroCusto.CodigoInterno = 1;
                else
                {
                    if (centroCusto.PaiID == null)
                        centroCusto.CodigoInterno = centros.Where(x => x.PaiID == null).Max(x => x.CodigoInterno) + 1;
                    else
                    {
                        var pai = centros.Where(x => x.ID == centroCusto.PaiID).Single();

                        double proximoCodigo = 0;

                        if (pai.Filhos.Count == 0)
                            proximoCodigo = Convert.ToDouble(pai.CodigoInterno.ToString().Split(',').Count() == 1 ? pai.CodigoInterno.ToString() + "," + "1" : pai.CodigoInterno.ToString().Split(',')[0] + "," + pai.CodigoInterno.ToString().Split(',')[1] + "1");
                        else
                        {
                            double ultimoCodigo = pai.Filhos.Max(x => x.CodigoInterno);
                            string codigo = (Convert.ToInt32(ultimoCodigo.ToString().Split(',')[1]) + 1).ToString();
                            proximoCodigo = Convert.ToDouble(ultimoCodigo.ToString().Split(',')[0] + "," + codigo);
                        }

                        centroCusto.CodigoInterno = proximoCodigo;
                    }
                }

                _repository.Insert(centroCusto);

                return Ok(centroCusto);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um Centro de Custo
        /// </summary>
        /// <param name="centroCusto">Objeto do tipo CentroCusto</param>
        /// <param name="id">ID do Centro de Custo que será atualizado</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] CentroCusto centroCusto)
        {
            try
            {
                var centroCustoDB = _repository.GetByID(id);

                if (centroCustoDB == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                centroCustoDB.Codigo = centroCusto.Codigo;
                centroCustoDB.EmpresaID = centroCusto.EmpresaID;
                centroCustoDB.Nome = centroCusto.Nome;
                centroCustoDB.PaiID = centroCusto.PaiID;
                centroCustoDB.ResponsavelID = centroCusto.ResponsavelID;

                _repository.Update(centroCustoDB);

                return Ok(centroCustoDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um Centro de Custo
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var centroCusto = _repository.GetByID(id);

                if (centroCusto == null)
                    return NotFound();

                var filhos = _repository.GetByEmpresaID(centroCusto.EmpresaID).Where(x => x.CodigoInterno.ToString().StartsWith(centroCusto.CodigoInterno.ToString()));

                foreach (var filho in filhos)
                {
                    _repository.Delete(filho.ID);
                }

                _repository.Delete(id);

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
