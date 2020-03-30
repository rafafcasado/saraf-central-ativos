namespace CentralAtivos.Repository.Migrations
{
    using CentralAtivos.Domain.Entities;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context.Context context)
        {
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista dos Principais Centros de Custo Ativos Cadastrados para uma Empresa de acordo com o ID informado", Entidade = "CentroCusto", MetodoHTTP = "GET", NomeMetodo = "GetPrincipaisByEmpresaID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista dos Sub Centros de Custo Ativos Cadastrados para um Centro de Custo de acordo com o ID informado", Entidade = "CentroCusto", MetodoHTTP = "GET", NomeMetodo = "GetSubCentrosByCentroID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Centro de Custo relacionado ao ID informado", Entidade = "CentroCusto", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna todos os Centros de Custo de acordo com o ID da Empresa informada", Entidade = "CentroCusto", MetodoHTTP = "GET", NomeMetodo = "GetAllByEmpresaID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui um Centro de Custo", Entidade = "CentroCusto", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Atualiza os dados de um Centro de Custo", Entidade = "CentroCusto", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um Centro de Custo", Entidade = "CentroCusto", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista das Principais Contas Contabeis Ativas Cadastradas para uma Empresa de acordo com o ID informado", Entidade = "ContaContabil", MetodoHTTP = "GET", NomeMetodo = "GetPrincipaisByEmpresaID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista das Sub Contas Contábeis Ativas Cadastradas para uma Conta Contábil de acordo com o ID informado", Entidade = "ContaContabil", MetodoHTTP = "GET", NomeMetodo = "GetSubContasByContaID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Todas as Contas Contábeis de acordo com o ID da Empresa informada", Entidade = "ContaContabil", MetodoHTTP = "GET", NomeMetodo = "GetAll" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Conta Contábil relacionada ao ID informado", Entidade = "ContaContabil", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui uma Conta Contábil", Entidade = "ContaContabil", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Atualiza os dados de uma Conta Contábil", Entidade = "ContaContabil", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui uma Conta Contábil", Entidade = "ContaContabil", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Empresas Ativas Cadastrados", Entidade = "Empresa", MetodoHTTP = "GET", NomeMetodo = "GetEmpresas" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Empresa relacionada ao ID informado", Entidade = "Empresa", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui uma Empresa", Entidade = "Empresa", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Atualiza os dados de uma Empresa", Entidade = "Empresa", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui uma Empresa", Entidade = "Empresa", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Empresas com Inventários Cadastrados de Acordo com o ID do Usuário", Entidade = "Empresa", MetodoHTTP = "GET", NomeMetodo = "GetByInventarioUsuarioID" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Filiais Ativas Cadastradas para a Empresa de acordo com o ID informado", Entidade = "Filial", MetodoHTTP = "GET", NomeMetodo = "GetByEmpresaID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Filial relacionada ao ID informado", Entidade = "Filial", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui uma Filial", Entidade = "Filial", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Atualiza os dados de uma Filial", Entidade = "Filial", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui uma Filial", Entidade = "Filial", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista dos Principais Locais Ativos Cadastrados para uma Filial de acordo com o ID informado", Entidade = "Local", MetodoHTTP = "GET", NomeMetodo = "GetPrincipaisByFilialID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista dos Sub Locais Ativos Cadastrados para um Local de acordo com o ID informado", Entidade = "Local", MetodoHTTP = "GET", NomeMetodo = "GetSubLocaisByLocalID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna todos os Locais de acordo com o ID da Filial informada", Entidade = "Local", MetodoHTTP = "GET", NomeMetodo = "GetAllByFilialID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Locais de acordo com vários IDs de Filiais informados", Entidade = "Local", MetodoHTTP = "GET", NomeMetodo = "GetAllByManyFiliaisID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Local relacionado ao ID informado", Entidade = "Local", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui um Local", Entidade = "Local", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Atualiza os dados de um Local", Entidade = "Local", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um Local", Entidade = "Local", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna todos os Locais de acordo com o ID da empresa informada", Entidade = "Local", MetodoHTTP = "GET", NomeMetodo = "GetByEmpresaID" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Perfis Ativos Cadastrados", Entidade = "Perfil", MetodoHTTP = "GET", NomeMetodo = "GetPerfis" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Perfil relacionado ao ID informado", Entidade = "Perfil", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui um Perfil", Entidade = "Perfil", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Atualiza os dados de um Perfil", Entidade = "Perfil", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um Perfil", Entidade = "Perfil", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Permissões do Perfil", Entidade = "Perfil", MetodoHTTP = "GET", NomeMetodo = "GetPermissoes" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Permissões relacionadas ao ID do Perfil informado", Entidade = "Permissao", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui uma Permissão", Entidade = "Permissao", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui uma Permissão", Entidade = "Permissao", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Atualiza Permissão do Perfil", Entidade = "Permissao", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Permissões do Usuário do Token de acordo com a Funcionalidade", Entidade = "Permissao", MetodoHTTP = "GET", NomeMetodo = "GetByFuncionalidade" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Lista de Usuários Ativos Cadastrados", Entidade = "Usuario", MetodoHTTP = "GET", NomeMetodo = "GetUsuarios" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Usuários Ativos Cadastrados de acordo com ID da Empresa enviada como parâmetro", Entidade = "Usuario", MetodoHTTP = "GET", NomeMetodo = "GetByEmpresaID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna os dados de um Usuário pelo ID informado", Entidade = "Usuario", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Insere Novo Usuário", Entidade = "Usuario", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Altera os dados de um Usuário cadastrado", Entidade = "Usuario", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um Usuário", Entidade = "Usuario", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Notifica Primeiro Acesso do Usuário de acordo com ID informado", Entidade = "Usuario", MetodoHTTP = "GET", NomeMetodo = "NotificarPrimeiroAcesso" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Busca Dados do Usuário Logado", Entidade = "Helpers", MetodoHTTP = "GET", NomeMetodo = "GetUsuarioLogado" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Primeiro Acesso", Entidade = "Helpers", MetodoHTTP = "POST", NomeMetodo = "PrimeiroAcesso" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista dos Grupos Ativos Cadastrados para uma Empresa de acordo com o ID informado", Entidade = "Grupo", MetodoHTTP = "GET", NomeMetodo = "GetByEmpresaID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Grupo relacionado ao ID informado", Entidade = "Grupo", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Insere Novo Grupo", Entidade = "Grupo", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Altera os dados de um Grupo cadastrado", Entidade = "Grupo", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um Grupo", Entidade = "Grupo", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista dos Espécies Ativas Cadastradas para um Grupo de acordo com o ID informado", Entidade = "Especie", MetodoHTTP = "GET", NomeMetodo = "GetByGrupoID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Espécie relacionada ao ID informado", Entidade = "Especie", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Insere Nova Espécie", Entidade = "Especie", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Altera os dados de uma Espécie cadastrada", Entidade = "Especie", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui uma Espécie", Entidade = "Especie", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista dos Espécies Ativas Cadastradas de acordo com a empresa informada", Entidade = "Especie", MetodoHTTP = "GET", NomeMetodo = "GetByEmpresaID" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista dos Propriedade Ativas Cadastradas para uma Empresa de acordo com o ID informado", Entidade = "Propriedade", MetodoHTTP = "GET", NomeMetodo = "GetByEmpresaID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Insere Nova Propriedade", Entidade = "Propriedade", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Propriedade relacionada ao ID informado", Entidade = "Propriedade", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Atualiza os dados de uma Propriedade", Entidade = "Propriedade", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui uma Propriedade", Entidade = "Propriedade", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista dos Propriedades vinculadas à Espécie de acordo com ID", Entidade = "EspeciePropriedade", MetodoHTTP = "GET", NomeMetodo = "GetByEspecieID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui vínculo Espécie Propriedades", Entidade = "EspeciePropriedade", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um vínculo Espécie Propriedade", Entidade = "EspeciePropriedade", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Responsáveis Ativos Cadastrados para a Empresa de acordo com o ID informado", Entidade = "Responsavel", MetodoHTTP = "GET", NomeMetodo = "GetByEmpresaID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Responsável relacionado ao ID informado", Entidade = "Responsavel", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Insere Novo Responsável", Entidade = "Responsavel", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Altera os dados de um Responsável cadastrado", Entidade = "Responsavel", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um Responsável", Entidade = "Responsavel", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Itens Cadastrados para a Empresa de acordo com o ID informado", Entidade = "Item", MetodoHTTP = "GET", NomeMetodo = "GetByEmpresaID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Item relacionado ao ID informado", Entidade = "Item", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui um Item", Entidade = "Item", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Atualiza os dados de um Item", Entidade = "Item", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um Item", Entidade = "Item", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Itens Cadastrados para a Empresa de acordo com o LocalID informado", Entidade = "Item", MetodoHTTP = "GET", NomeMetodo = "GetByLocalID" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui Novo Inventário", Entidade = "Inventario", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Inventários Cadastrados para a Empresa de acordo com o ID informado", Entidade = "Inventario", MetodoHTTP = "GET", NomeMetodo = "GetByEmpresaID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Inventário relacionado ao ID informado", Entidade = "Inventario", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Atualiza os dados de um Inventário", Entidade = "Inventario", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Carga Inicial do Inventário", Entidade = "Inventario", MetodoHTTP = "GET", NomeMetodo = "CargaInicial" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Usuários atribuidos ao Inventário de acordo com o ID informado", Entidade = "InventarioUsuario", MetodoHTTP = "GET", NomeMetodo = "GetByInventarioID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista dos Inventarios de acordo com o ID do Usuário vinculado", Entidade = "InventarioUsuario", MetodoHTTP = "GET", NomeMetodo = "GetByUsuarioID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui um Usuário à Equipe do Inventário", Entidade = "InventarioUsuario", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um Usuário do Inventário", Entidade = "InventarioUsuario", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Locais atribuidos ao Inventário de acordo com o ID informado", Entidade = "InventarioLocal", MetodoHTTP = "GET", NomeMetodo = "GetByInventarioID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui um Local ao Inventário", Entidade = "InventarioLocal", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um Local do Inventário", Entidade = "InventarioLocal", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um Valor vinculado à Propriedade", Entidade = "PropriedadeValor", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Todos os Estados dos Itens", Entidade = "ItemEstado", MetodoHTTP = "GET", NomeMetodo = "GetAll" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui um Novo Processo de Upload", Entidade = "Upload", MetodoHTTP = "POST", NomeMetodo = "Novo" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inicia Processamento de Arquivo", Entidade = "Upload", MetodoHTTP = "POST", NomeMetodo = "Processar" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Uploads Cadastrados", Entidade = "Upload", MetodoHTTP = "GET", NomeMetodo = "GetUploads" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Envia Arquivo de Sincronização", Entidade = "Sincronizacao", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Sincronizações", Entidade = "Sincronizacao", MetodoHTTP = "GET", NomeMetodo = "GetSincronizacoes" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inicia Processamento de Arquivo de Sincronização", Entidade = "Sincronizacao", MetodoHTTP = "POST", NomeMetodo = "Processar" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Requisições Cadastradas", Entidade = "Requisicao", MetodoHTTP = "GET", NomeMetodo = "GetRequisicoes" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Motivos de Ordem de Serviço Cadastrados", Entidade = "OrdemServicoMotivo", MetodoHTTP = "GET", NomeMetodo = "GetMotivos" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Ordens de Serviço Cadastradas", Entidade = "OrdemServico", MetodoHTTP = "GET", NomeMetodo = "GetOrdensServico" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Ordem de Serviço relacionado ao ID informado", Entidade = "OrdemServico", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui uma Ordem de Serviço", Entidade = "OrdemServico", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui Anexos à Ordem de Servico", Entidade = "OrdemServico", MetodoHTTP = "PUT", NomeMetodo = "EnviarAnexos" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui uma Ordem de Serviço", Entidade = "OrdemServico", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Atualiza o Status de uma Ordem de Serviço", Entidade = "OrdemServico", MetodoHTTP = "PUT", NomeMetodo = "AlteraStatus" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Menus Cadastrados", Entidade = "Menu", MetodoHTTP = "GET", NomeMetodo = "GetMenus" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Menus de acordo com o ID do Perfil informado", Entidade = "Menu", MetodoHTTP = "GET", NomeMetodo = "GetByPerfilID" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Vincula um Menu ao Perfil", Entidade = "PerfilMenu", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Desvincula um Menu ao Perfil", Entidade = "PerfilMenu", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Coletores Cadastrados", Entidade = "Coletor", MetodoHTTP = "GET", NomeMetodo = "GetColetores" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Coletor relacionado ao ID informado", Entidade = "Coletor", MetodoHTTP = "GET", NomeMetodo = "Get" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Insere Novo Coletor", Entidade = "Coletor", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Altera os dados de um Coletor Cadastrado", Entidade = "Coletor", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um Coletor", Entidade = "Coletor", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Inclui um Grupo de Placas", Entidade = "PlacaGrupo", MetodoHTTP = "POST", NomeMetodo = "Post" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Grupos de Placa Cadastrados de acordo com ID do Inventário enviado", Entidade = "PlacaGrupo", MetodoHTTP = "GET", NomeMetodo = "GetByInventarioID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Pulos de Placas de acordo com o ID do Grupo de Placas enviado", Entidade = "PlacaGrupo", MetodoHTTP = "GET", NomeMetodo = "GetJumpsByPlacaGrupoID" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Exclui um Grupo de Placas", Entidade = "PlacaGrupo", MetodoHTTP = "DELETE", NomeMetodo = "Delete" });

            //context.Requisicoes.Add(new Requisicao() { Descricao = "Atualiza os dados de uma Placa", Entidade = "Placa", MetodoHTTP = "PUT", NomeMetodo = "Put" });
            //context.Requisicoes.Add(new Requisicao() { Descricao = "Retorna Lista de Placas de acordo com o ID do Grupo de Placas enviado", Entidade = "Placa", MetodoHTTP = "GET", NomeMetodo = "GetByPlacaGrupoID" });

            //context.SaveChanges();

            //context.Perfis.Add(new Perfil() { ID = 1, Nome = "Administrador" });

            //context.SaveChanges();

            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Dashboard" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Empresa" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Perfil" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Sincronizacao" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Upload" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Usuario" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Coletor" });

            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "CentroCusto" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "ContaContabil" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Especie" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Filial" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Grupo" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Inventario" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Item" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Local" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Propriedade" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "OrdemServico" });
            //context.Funcionalidades.Add(new Funcionalidade() { Nome = "Responsavel" });

            //context.SaveChanges();

            //foreach (var f in context.Funcionalidades)
            //{
            //    context.Permissoes.Add(new Permissao() { PerfilID = 1, FuncionalidadeID = f.ID, Metodos = "GET,POST,PUT,DELETE" });
            //}

            //context.SaveChanges();

            //context.Usuarios.Add(new Usuario() { Email = "rafael@pontualsys.com.br", Nome = "Rafael Fernandes Casado", PerfilID = 1, PrimeiroAcesso = true });

            //context.SaveChanges();

            //context.Empresas.Add(new Empresa() { Bairro = "Santana", CEP = "02038-030", Cidade = "São Paulo", CNPJ = "11.627.118/0001-60", Complemento = "sala 85", Endereco = "Rua José Debieux", NomeFantasia = "Pontual Systems", Numero = 35, RazaoSocial = "Pontual Systems Desenvolvimento de Sistemas Ltda.", UF = "SP" });

            //context.SaveChanges();

            //context.Filiais.Add(new Filial() { Bairro = "Santana", CEP = "02038-030", Cidade = "São Paulo", CNPJ = "11.627.118/0001-60", Codigo = "00", Complemento = "sala 85", EmpresaID = 1, Endereco = "Rua José Debieux", Nome = "Matriz", Numero = 35, UF = "SP" });

            //context.SaveChanges();

            //context.Status.Add(new Status() { Nome = "NOVO" });

            //context.SaveChanges();

            //context.ItemEstados.Add(new ItemEstado() { Nome = "NOVO" });
            //context.ItemEstados.Add(new ItemEstado() { Nome = "USADO" });

            //context.SaveChanges();

            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.FILIAL, NomeCampo = "Codigo", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 20 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.FILIAL, NomeCampo = "Nome", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 100 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.FILIAL, NomeCampo = "CNPJ", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 20, Regex = @"[0-9]{2}\.?[0-9]{3}\.?[0-9]{3}\/?[0-9]{4}\-?[0-9]{2}" });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.FILIAL, NomeCampo = "CEP", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 9, Regex = @"[0-9]{5}-[\d]{3}" });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.FILIAL, NomeCampo = "Endereco", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 150 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.FILIAL, NomeCampo = "Numero", Obrigatorio = false, Tipo = Enums.TipoItem.INT });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.FILIAL, NomeCampo = "Complemento", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.FILIAL, NomeCampo = "Bairro", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 150 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.FILIAL, NomeCampo = "Cidade", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 150 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.FILIAL, NomeCampo = "UF", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 2 });

            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.GRUPO, NomeCampo = "ContaContabilCodigo", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.GRUPO, NomeCampo = "Codigo", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.GRUPO, NomeCampo = "Nome", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 150 });

            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.PROPRIEDADE, NomeCampo = "Codigo", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.PROPRIEDADE, NomeCampo = "Nome", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 150 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.PROPRIEDADE, NomeCampo = "Fixa", Obrigatorio = true, Tipo = Enums.TipoItem.BOOLEAN });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.PROPRIEDADE, NomeCampo = "Valores", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR });

            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.RESPONSAVEL, NomeCampo = "Nome", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 150 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.RESPONSAVEL, NomeCampo = "Cargo", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 100 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.RESPONSAVEL, NomeCampo = "Matricula", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 20 });

            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "ItemEstado", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 20 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "LocalCodigo", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "EspecieCodigo", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "Nome", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 30 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "Codigo", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 20 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "Incorporacao", Obrigatorio = false, Tipo = Enums.TipoItem.INT });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "CodigoAnterior", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 20 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "IncorporacaoAnterior", Obrigatorio = false, Tipo = Enums.TipoItem.INT });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "CodigoPM", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 20 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "DadosPM", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "LocalPM", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 100 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "Tag", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "Marca", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "Modelo", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "NumeroSerie", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "Observacao", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 150 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "Latitude", Obrigatorio = false, Tipo = Enums.TipoItem.DECIMAL });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ITEM, NomeCampo = "Longitude", Obrigatorio = false, Tipo = Enums.TipoItem.DECIMAL });

            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ESPECIE, NomeCampo = "GrupoCodigo", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ESPECIE, NomeCampo = "Codigo", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.ESPECIE, NomeCampo = "Nome", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });

            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.LOCAL, NomeCampo = "CentroCustoCodigo", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.LOCAL, NomeCampo = "FilialCodigo", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.LOCAL, NomeCampo = "Codigo", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.LOCAL, NomeCampo = "Nome", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 150 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.LOCAL, NomeCampo = "CodigoPai", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });

            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.CENTRO_CUSTO, NomeCampo = "CodigoPai", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.CENTRO_CUSTO, NomeCampo = "Codigo", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.CENTRO_CUSTO, NomeCampo = "Nome", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 150 });

            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.CONTA_CONTABIL, NomeCampo = "CodigoPai", Obrigatorio = false, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.CONTA_CONTABIL, NomeCampo = "Codigo", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 50 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.CONTA_CONTABIL, NomeCampo = "Nome", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 150 });
            //context.EntidadeCampos.Add(new EntidadeCampo() { Entidade = Enums.Entidade.CONTA_CONTABIL, NomeCampo = "NomeAbreviado", Obrigatorio = true, Tipo = Enums.TipoItem.VARCHAR, Tamanho = 80 });

            //context.SaveChanges();

            //context.OrdemServicoMotivos.Add(new OrdemServicoMotivo() { ID = 1, Nome = "Transferência de Local Interno" });
            //context.OrdemServicoMotivos.Add(new OrdemServicoMotivo() { ID = 2, Nome = "Transferência de Filial" });
            //context.OrdemServicoMotivos.Add(new OrdemServicoMotivo() { ID = 3, Nome = "Remessa Conserto" });
            //context.OrdemServicoMotivos.Add(new OrdemServicoMotivo() { ID = 4, Nome = "Sinistro" });
            //context.OrdemServicoMotivos.Add(new OrdemServicoMotivo() { ID = 5, Nome = "Doação" });
            //context.OrdemServicoMotivos.Add(new OrdemServicoMotivo() { ID = 6, Nome = "Sucata / Obsolescência" });
            //context.OrdemServicoMotivos.Add(new OrdemServicoMotivo() { ID = 7, Nome = "Venda" });
            //context.OrdemServicoMotivos.Add(new OrdemServicoMotivo() { ID = 8, Nome = "Aquisição" });
            //context.OrdemServicoMotivos.Add(new OrdemServicoMotivo() { ID = 9, Nome = "Manutenção" });

            //context.SaveChanges();

            //context.OrdemServicoCampos.Add(new OrdemServicoCampo() { ID = 1, NomeCampo = "FilialOrigemID" });
            //context.OrdemServicoCampos.Add(new OrdemServicoCampo() { ID = 2, NomeCampo = "LocalOrigemID" });
            //context.OrdemServicoCampos.Add(new OrdemServicoCampo() { ID = 3, NomeCampo = "FilialDestinoID" });
            //context.OrdemServicoCampos.Add(new OrdemServicoCampo() { ID = 4, NomeCampo = "LocalDestinoID" });
            //context.OrdemServicoCampos.Add(new OrdemServicoCampo() { ID = 5, NomeCampo = "Justificativa" });
            //context.OrdemServicoCampos.Add(new OrdemServicoCampo() { ID = 6, NomeCampo = "ResponsavelOrigemID" });
            //context.OrdemServicoCampos.Add(new OrdemServicoCampo() { ID = 7, NomeCampo = "ResponsavelDestinoID" });
            //context.OrdemServicoCampos.Add(new OrdemServicoCampo() { ID = 8, NomeCampo = "LocalExterno" });
            //context.OrdemServicoCampos.Add(new OrdemServicoCampo() { ID = 9, NomeCampo = "TipoSinistro" });
            //context.OrdemServicoCampos.Add(new OrdemServicoCampo() { ID = 10, NomeCampo = "GrupoID" });
            //context.OrdemServicoCampos.Add(new OrdemServicoCampo() { ID = 11, NomeCampo = "EspecieID" });
            //context.OrdemServicoCampos.Add(new OrdemServicoCampo() { ID = 12, NomeCampo = "Quantidade" });

            //context.SaveChanges();

            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 9, OrdemServicoCampoID = 1 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 9, OrdemServicoCampoID = 2 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 9, OrdemServicoCampoID = 6 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 9, OrdemServicoCampoID = 7 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 9, OrdemServicoCampoID = 5 });

            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 8, OrdemServicoCampoID = 1 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 8, OrdemServicoCampoID = 2 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 8, OrdemServicoCampoID = 6 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 8, OrdemServicoCampoID = 7 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 8, OrdemServicoCampoID = 5 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 8, OrdemServicoCampoID = 10 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 8, OrdemServicoCampoID = 11 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 8, OrdemServicoCampoID = 12 });

            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 7, OrdemServicoCampoID = 1 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 7, OrdemServicoCampoID = 2 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 7, OrdemServicoCampoID = 6 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 7, OrdemServicoCampoID = 7 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 7, OrdemServicoCampoID = 5 });

            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 6, OrdemServicoCampoID = 1 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 6, OrdemServicoCampoID = 2 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 6, OrdemServicoCampoID = 6 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 6, OrdemServicoCampoID = 7 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 6, OrdemServicoCampoID = 5 });

            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 5, OrdemServicoCampoID = 1 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 5, OrdemServicoCampoID = 2 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 5, OrdemServicoCampoID = 6 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 5, OrdemServicoCampoID = 7 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 5, OrdemServicoCampoID = 5 });

            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 4, OrdemServicoCampoID = 1 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 4, OrdemServicoCampoID = 2 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 4, OrdemServicoCampoID = 9 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 4, OrdemServicoCampoID = 6 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 4, OrdemServicoCampoID = 7 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 4, OrdemServicoCampoID = 5 });

            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 3, OrdemServicoCampoID = 1 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 3, OrdemServicoCampoID = 2 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 3, OrdemServicoCampoID = 8 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 3, OrdemServicoCampoID = 5 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 3, OrdemServicoCampoID = 6 });

            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 2, OrdemServicoCampoID = 1 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 2, OrdemServicoCampoID = 2 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 2, OrdemServicoCampoID = 3 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 2, OrdemServicoCampoID = 4 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 2, OrdemServicoCampoID = 5 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 2, OrdemServicoCampoID = 6 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 2, OrdemServicoCampoID = 7 });

            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 1, OrdemServicoCampoID = 1 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 1, OrdemServicoCampoID = 2 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 1, OrdemServicoCampoID = 3 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 1, OrdemServicoCampoID = 4 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 1, OrdemServicoCampoID = 5 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 1, OrdemServicoCampoID = 6 });
            //context.OrdemServicoMotivoCampos.Add(new OrdemServicoMotivoCampo() { OrdemServicoMotivoID = 1, OrdemServicoCampoID = 7 });

            //context.SaveChanges();

            //context.Database.ExecuteSqlCommand("UPDATE ORDEMSERVICOMOTIVO SET ACAOFINAL = 1 WHERE ID = 1");
            //context.Database.ExecuteSqlCommand("UPDATE ORDEMSERVICOMOTIVO SET ACAOFINAL = 1 WHERE ID = 2");
            //context.Database.ExecuteSqlCommand("UPDATE ORDEMSERVICOMOTIVO SET ACAOFINAL = 3 WHERE ID = 3");
            //context.Database.ExecuteSqlCommand("UPDATE ORDEMSERVICOMOTIVO SET ACAOFINAL = 2 WHERE ID = 4");
            //context.Database.ExecuteSqlCommand("UPDATE ORDEMSERVICOMOTIVO SET ACAOFINAL = 2 WHERE ID = 5");
            //context.Database.ExecuteSqlCommand("UPDATE ORDEMSERVICOMOTIVO SET ACAOFINAL = 2 WHERE ID = 6");
            //context.Database.ExecuteSqlCommand("UPDATE ORDEMSERVICOMOTIVO SET ACAOFINAL = 2 WHERE ID = 7");
            //context.Database.ExecuteSqlCommand("UPDATE ORDEMSERVICOMOTIVO SET ACAOFINAL = 1 WHERE ID = 8");
            //context.Database.ExecuteSqlCommand("UPDATE ORDEMSERVICOMOTIVO SET ACAOFINAL = 3 WHERE ID = 9");

            //context.SaveChanges();

            //context.Menus.Add(new Menu() { ID = 1, TipoMenu = Enums.TipoMenu.ADMINISTRADOR, TituloMenu = "Perfil", Icone = "fa-id-card-alt", Url = "/Perfil", Descricao = "Lista de Perfis Cadastrados" });
            //context.Menus.Add(new Menu() { ID = 2, TipoMenu = Enums.TipoMenu.ADMINISTRADOR, TituloMenu = "Usuario", Icone = "fa-user", Url = "/Usuario", Descricao = "Lista de Usuários Cadastrados" });
            //context.Menus.Add(new Menu() { ID = 3, TipoMenu = Enums.TipoMenu.ADMINISTRADOR, TituloMenu = "Empresa", Icone = "fa-building", Url = "/Empresa", Descricao = "Lista de Empresas Cadastradas" });
            //context.Menus.Add(new Menu() { ID = 4, TipoMenu = Enums.TipoMenu.ADMINISTRADOR, TituloMenu = "Uploads", Icone = "fa-upload", Url = "/Upload", Descricao = "Gerenciamento de Uploadas" });
            //context.Menus.Add(new Menu() { ID = 5, TipoMenu = Enums.TipoMenu.ADMINISTRADOR, TituloMenu = "Sincronizações", Icone = "fa-sync", Url = "/Sincronizacao", Descricao = "Gerenciamento de Sincronizações" });
            //context.Menus.Add(new Menu() { ID = 6, TipoMenu = Enums.TipoMenu.ADMINISTRADOR, TituloMenu = "Dashboard", Icone = "fa-chart-pie", Url = "/Home", Descricao = "Dashboard" });
            //context.Menus.Add(new Menu() { ID = 7, TipoMenu = Enums.TipoMenu.ADMINISTRADOR, TituloMenu = "Coletores", Icone = "fa-mobile-alt", Url = "/Coletor", Descricao = "Lista de Coletores Cadastrados" });

            //context.Menus.Add(new Menu() { ID = 8, TipoMenu = Enums.TipoMenu.EMPRESA, TituloMenu = "Dashboard", Icone = "", Url = "/Home", Descricao = "Dashboard" });
            //context.Menus.Add(new Menu() { ID = 9, TipoMenu = Enums.TipoMenu.EMPRESA, TituloMenu = "Contas Contábeis", Icone = "", Url = "/ContasContabeis", Descricao = "Lista de Contas Contábeis Cadastradas" });
            //context.Menus.Add(new Menu() { ID = 10, TipoMenu = Enums.TipoMenu.EMPRESA, TituloMenu = "Centros de Custo", Icone = "", Url = "/CentroCusto", Descricao = "Lista de Centros de Custo Cadastrados" });
            //context.Menus.Add(new Menu() { ID = 11, TipoMenu = Enums.TipoMenu.EMPRESA, TituloMenu = "Filiais", Icone = "", Url = "/Filial", Descricao = "Lista de Filiais Cadastradas" });
            //context.Menus.Add(new Menu() { ID = 12, TipoMenu = Enums.TipoMenu.EMPRESA, TituloMenu = "Grupos/Espécies", Icone = "", Url = "/Grupo", Descricao = "Gerenciamento de Grupos, Espécies e Propriedades" });
            //context.Menus.Add(new Menu() { ID = 13, TipoMenu = Enums.TipoMenu.EMPRESA, TituloMenu = "Inventários", Icone = "", Url = "/Inventario", Descricao = "Gerenciamento de Inventários" });
            //context.Menus.Add(new Menu() { ID = 14, TipoMenu = Enums.TipoMenu.EMPRESA, TituloMenu = "Itens", Icone = "", Url = "/Item", Descricao = "Lista de Itens Cadastrados" });
            //context.Menus.Add(new Menu() { ID = 15, TipoMenu = Enums.TipoMenu.EMPRESA, TituloMenu = "Locais", Icone = "", Url = "/Local/Filiais", Descricao = "Lista de Locais Cadastrados" });
            //context.Menus.Add(new Menu() { ID = 16, TipoMenu = Enums.TipoMenu.EMPRESA, TituloMenu = "Propriedades", Icone = "", Url = "/Propriedade", Descricao = "Lista de Propriedades Cadastradas" });
            //context.Menus.Add(new Menu() { ID = 17, TipoMenu = Enums.TipoMenu.EMPRESA, TituloMenu = "Responsáveis", Icone = "", Url = "/Responsavel", Descricao = "Lista de Responsáveis Cadastrados" });
            //context.Menus.Add(new Menu() { ID = 18, TipoMenu = Enums.TipoMenu.EMPRESA, TituloMenu = "Ordens de Serviço", Icone = "", Url = "/OrdemServico", Descricao = "Gerenciamento de Ordens de Serviço" });

            //context.SaveChanges();

            //foreach (var menu in context.Menus)
            //{
            //    context.PerfilMenus.Add(new Domain.Entities.PerfilMenu() { PerfilID = 1, MenuID = menu.ID });
            //}

            //context.SaveChanges();

        }
    }
}
