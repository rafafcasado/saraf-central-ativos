using CentralAtivos.Domain.Interfaces;
using CentralAtivos.Repository.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CentralAtivos.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<ICentroCusto, CentroCustoRepository>(Lifestyle.Scoped);
            container.Register<IColetor, ColetorRepository>(Lifestyle.Scoped);
            container.Register<IContaContabil, ContaContabilRepository>(Lifestyle.Scoped);
            container.Register<IEmpresa, EmpresaRepository>(Lifestyle.Scoped);
            container.Register<IEntidadeCampo, EntidadeCampoRepository>(Lifestyle.Scoped);
            container.Register<IEspecie, EspecieRepository>(Lifestyle.Scoped);
            container.Register<IEspeciePropriedade, EspeciePropriedadeRepository>(Lifestyle.Scoped);
            container.Register<IFilial, FilialRepository>(Lifestyle.Scoped);
            container.Register<IFuncionalidade, FuncionalidadeRepository>(Lifestyle.Scoped);
            container.Register<IGrupo, GrupoRepository>(Lifestyle.Scoped);
            container.Register<IInventario, InventarioRepository>(Lifestyle.Scoped);
            container.Register<IInventarioConfig, InventarioConfigRepository>(Lifestyle.Scoped);
            container.Register<IInventarioFilial, InventarioFilialRepository>(Lifestyle.Scoped);
            container.Register<IInventarioLocal, InventarioLocalRepository>(Lifestyle.Scoped);
            container.Register<IInventarioUsuario, InventarioUsuarioRepository>(Lifestyle.Scoped);
            container.Register<IItem, ItemRepository>(Lifestyle.Scoped);
            container.Register<IItemEstado, ItemEstadoRepository>(Lifestyle.Scoped);
            container.Register<ILocal, LocalRepository>(Lifestyle.Scoped);
            container.Register<ILogUsuario, LogUsuarioRepository>(Lifestyle.Scoped);
            container.Register<IMenu, MenuRepository>(Lifestyle.Scoped);
            container.Register<IOrdemServico, OrdemServicoRepository>(Lifestyle.Scoped);
            container.Register<IOrdemServicoAnexo, OrdemServicoAnexoRepository>(Lifestyle.Scoped);
            container.Register<IOrdemServicoCampo, OrdemServicoCampoRepository>(Lifestyle.Scoped);
            container.Register<IOrdemServicoItem, OrdemServicoItemRepository>(Lifestyle.Scoped);
            container.Register<IOrdemServicoMotivo, OrdemServicoMotivoRepository>(Lifestyle.Scoped);
            container.Register<IOrdemServicoMotivoCampo, OrdemServicoMotivoCampoRepository>(Lifestyle.Scoped);
            container.Register<IOrdemServicoMotivoCampoValor, OrdemServicoMotivoCampoValorRepository>(Lifestyle.Scoped);
            container.Register<IPerfil, PerfilRepository>(Lifestyle.Scoped);
            container.Register<IPerfilMenu, PerfilMenuRepository>(Lifestyle.Scoped);
            container.Register<IPermissao, PermissaoRepository>(Lifestyle.Scoped);
            container.Register<IPlaca, PlacaRepository>(Lifestyle.Scoped);
            container.Register<IPlacaGrupo, PlacaGrupoRepository>(Lifestyle.Scoped);
            container.Register<IPropriedade, PropriedadeRepository>(Lifestyle.Scoped);
            container.Register<IPropriedadeValor, PropriedadeValorRepository>(Lifestyle.Scoped);
            container.Register<IRequisicao, RequisicaoRepository>(Lifestyle.Scoped);
            container.Register<IResponsavel, ResponsavelRepository>(Lifestyle.Scoped);
            container.Register<ISincronizacao, SincronizacaoRepository>(Lifestyle.Scoped);
            container.Register<IUsuario, UsuarioRepository>(Lifestyle.Scoped);
            container.Register<IUpload, UploadRepository>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
