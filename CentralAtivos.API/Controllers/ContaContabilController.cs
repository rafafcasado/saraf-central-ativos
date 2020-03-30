using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class ContaContabilController : ApiController
    {
        private readonly IContaContabil _repository;

        public ContaContabilController(IContaContabil repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista das Principais Contas Contábeis Ativas Cadastrados para uma Empresa de acordo com o ID informado
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet]
        [Route("api/contaContabil/getPrincipaisByEmpresaID/{empresaID}")]
        public IHttpActionResult GetPrincipaisByEmpresaID(int empresaID)
        {
            try
            {
                var contas = _repository.GetByEmpresaID(empresaID);

                contas = contas.Where(x => x.PaiID == null).ToList();

                return Ok(contas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Lista das Sub Contas Contáveis Ativas Cadastradas para uma Conta Contábil de acordo com o ID informado
        /// </summary>
        /// <param name="contaContabilID">ID da Conta Contábil</param>
        [HttpGet]
        [Route("api/contaContabil/getSubContasByContaID/{contaContabilID}")]
        public IHttpActionResult GetSubContasByContaID(int contaContabilID)
        {
            try
            {
                var conta = _repository.GetByID(contaContabilID);

                if (conta == null)
                    return NotFound();

                return Ok(conta.Filhos.Where(x => x.DataExclusao == null));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Todas as Contas Contábeis de acordo com o ID da Empresa informada
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet, Route("api/contaContabil/getAll/{empresaID}")]
        public IHttpActionResult GetAll(int empresaID)
        {
            try
            {
                var contasContabeis = _repository.GetByEmpresaID(empresaID);

                if (contasContabeis.Count > 0)
                {                    
                    foreach (var conta in contasContabeis)
                    {
                        bool fim = false;
                        var pai = conta.PaiID == null ? null : contasContabeis.Where(x => x.ID == conta.PaiID).SingleOrDefault();

                        while (!fim)
                        {
                            if (pai == null)
                            {
                                fim = true;
                                conta.CaminhoPao = conta.CaminhoPao == null || conta.CaminhoPao.Length <= 0 ? string.Format("{0} {1}", conta.Codigo, conta.Nome) : (conta.CaminhoPao + (string.Format("{0} {1}", conta.Codigo, conta.Nome)));
                            }
                            else
                            {
                                fim = false;
                                conta.CaminhoPao = string.Format("{0} {1}", pai.Codigo, pai.Nome) + " > " + conta.CaminhoPao;
                                pai = contasContabeis.Where(x => x.ID == pai.PaiID).SingleOrDefault();                                
                            }
                        }
                        
                    }
                }

                contasContabeis = contasContabeis.OrderBy(x => x.CodigoInterno).ToList();

                return Ok(contasContabeis);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Retorna Conta Contábil relacionada ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var contaContabil = _repository.GetByID(id);

                if (contaContabil == null)
                    return NotFound();

                return Ok(contaContabil);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui uma Conta Contábil
        /// </summary>
        /// <param name="contaContabil">Objeto do tipo ContaContabil</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] ContaContabil contaContabil)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var contas = _repository.GetByEmpresaID(contaContabil.EmpresaID);

                if (contas.Count == 0)
                    contaContabil.CodigoInterno = 1;
                else
                {
                    if (contaContabil.PaiID == null)
                        contaContabil.CodigoInterno = contas.Where(x => x.PaiID == null).Max(x => x.CodigoInterno) + 1;
                    else
                    {
                        var pai = contas.Where(x => x.ID == contaContabil.PaiID).Single();

                        double proximoCodigo = 0;

                        if (pai.Filhos.Count == 0)
                            proximoCodigo = Convert.ToDouble(pai.CodigoInterno.ToString().Split(',').Count() == 1 ? pai.CodigoInterno.ToString() + "," + "1" : pai.CodigoInterno.ToString().Split(',')[0] + "," + pai.CodigoInterno.ToString().Split(',')[1] + "1");
                        else
                        {
                            double ultimoCodigo = pai.Filhos.Max(x => x.CodigoInterno);
                            string codigo = (Convert.ToInt32(ultimoCodigo.ToString().Split(',')[1]) + 1).ToString();
                            proximoCodigo = Convert.ToDouble(ultimoCodigo.ToString().Split(',')[0] + "," + codigo);
                        }

                        contaContabil.CodigoInterno = proximoCodigo;
                    }
                }

                _repository.Insert(contaContabil);

                return Ok(contaContabil);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma Conta Contábil
        /// </summary>
        /// <param name="contaContabil">Objeto do tipo ContaContabil</param>
        /// <param name="id">ID da Conta Contábil que será atualizado</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] ContaContabil contaContabil)
        {
            try
            {
                var contaContabilDB = _repository.GetByID(id);

                if (contaContabilDB == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                contaContabilDB.Codigo = contaContabil.Codigo;
                contaContabilDB.EmpresaID = contaContabil.EmpresaID;
                contaContabilDB.Nome = contaContabil.Nome;
                contaContabilDB.NomeAbreviado = contaContabil.NomeAbreviado;
                contaContabilDB.PaiID = contaContabil.PaiID;

                _repository.Update(contaContabilDB);

                return Ok(contaContabilDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma Conta Contábil
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var contaContabil = _repository.GetByID(id);

                if (contaContabil == null)
                    return NotFound();

                var filhos = _repository.GetByEmpresaID(contaContabil.EmpresaID).Where(x => x.CodigoInterno.ToString().StartsWith(contaContabil.CodigoInterno.ToString()));

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
