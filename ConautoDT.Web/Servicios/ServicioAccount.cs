//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using System.Text;
//using Microsoft.Extensions.Logging;
//using static SendGrid.BaseClient;
using NLog;
using RestSharp;

namespace VET_ANIMAL.WEB.Servicios
{
    public class AccountService
    {
        private readonly IConfiguration configuration;
        private static HttpClient client = new HttpClient();
        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");

        public AccountService(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
                                                                    //    _apiClient.ThrowOnAnyError = true;
                                                                    //    _apiClient.Timeout = 120000;
                                                                    //    _apiClient.UseUtf8Json();
        }

        //public async Task<List<ItemRolesViewModel>> GetAllRolesAsync()
        //{
        //    //TODO: CAMBIAR ENDPOINT
        //    var request = new RestRequest("/clientes/ListarClientes", Method.GET, DataFormat.Json);
        //    try
        //    {
        //        //IRestResponse response = await _apiClient.ExecuteAsync(request, Method.GET);
        //        List<ItemRolesViewModel> RolesLst = new List<ItemRolesViewModel>();
        //        RolesLst.Add(new ItemRolesViewModel
        //        {
        //            Descripcion = "Super usuario",
        //            IdRol = 1,
        //            Rol = "Admin",
        //        });
        //        //if (response.IsSuccessful)
        //        //{
        //        //    RolesLst = JsonSerializer.Deserialize<List<ItemRolesViewModel>>(response.Content);
        //        //    return RolesLst;
        //        //}
        //        return RolesLst;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex, "Error al cargar los clientes");
        //        throw;
        //    }
        //}
        //public async Task<HttpStatusCode> CreateRolAsync(CreateRolesViewModel RolModel)
        //{
        //    //TODO: CAMBIAR ENDPOINT
        //    var request = new RestRequest("/clientes/RegistarClientes", Method.POST, DataFormat.Json);
        //    request.AddBody(RolModel);
        //    try
        //    {
        //        //IRestResponse response = await _apiClient.ExecuteAsync(request, Method.POST);
        //        //if (response.IsSuccessful)
        //        //{
        //        //    return response.StatusCode;
        //        //}
        //        //throw new Exception(response.StatusCode.ToString());
        //        return HttpStatusCode.OK;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex, "Error al intentar registar nuevo rol");
        //        throw;
        //    }
        //}
        //public async Task<EditRolesViewModel> GetRolAsync(long IdRol)
        //{
        //    //TODO: CAMBIAR ENDPOINT
        //    var request = new RestRequest("/clientes/RegistarClientes", Method.GET, DataFormat.Json);
        //    request.AddParameter("IdRol", IdRol.ToString());
        //    try
        //    {
        //        //IRestResponse response = await _apiClient.ExecuteAsync(request, Method.POST);
        //        //if (response.IsSuccessful)
        //        //{
        //        //    EditRolesViewModel RolModel = JsonSerializer.Deserialize<EditRolesViewModel>(response.Content);
        //        //    return RolModel;
        //        //}
        //        //throw new Exception(response.StatusCode.ToString());
        //        EditRolesViewModel RolModel = new EditRolesViewModel();
        //        RolModel.IdRol = IdRol;
        //        RolModel.Rol = "Admin";
        //        RolModel.Descripcion = "Super usuario";
        //        return RolModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex, "Error al intentar obtener el rol: " + IdRol);
        //        throw;
        //    }
        //}
        //public async Task<HttpStatusCode> EditRolAsync(EditRolesViewModel RolModel)
        //{
        //    //TODO: CAMBIAR ENDPOINT
        //    var request = new RestRequest("/clientes/RegistarClientes", Method.PATCH, DataFormat.Json);
        //    request.AddBody(RolModel);
        //    try
        //    {
        //        //IRestResponse response = await _apiClient.ExecuteAsync(request, Method.POST);
        //        //if (response.IsSuccessful)
        //        //{
        //        //    return response.StatusCode;
        //        //}
        //        //throw new Exception(response.StatusCode.ToString());
        //        return HttpStatusCode.OK;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex, "Error al intentar actualizar rol");
        //        throw;
        //    }
        //}
        //public async Task<HttpStatusCode> DeleteRolAsync(long IdRol)
        //{
        //    //TODO: CAMBIAR ENDPOINT

        //    try
        //    {
        //        var RolDelete = await GetRolAsync(IdRol);
        //        var request = new RestRequest("/clientes/RegistarClientes", Method.PATCH, DataFormat.Json);
        //        request.AddBody(RolDelete);
        //        //IRestResponse response = await _apiClient.ExecuteAsync(request, Method.PATCH);
        //        //if (response.IsSuccessful)
        //        //{
        //        //    return response.StatusCode;
        //        //}
        //        //throw new Exception(response.StatusCode.ToString());
        //        return HttpStatusCode.OK;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex, "Error al intentar actualizar rol");
        //        throw;
        //    }
        //}
    }
}