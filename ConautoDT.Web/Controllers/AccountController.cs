using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using Utf8Json;
using VET_ANIMAL.WEB.Models;
using VET_ANIMAL.WEB.Servicios;
using JsonSerializer = Utf8Json.JsonSerializer;

namespace VET_ANIMAL.WEB.Controllers
{
    public class AccountController : Controller
    {
        private Boolean visibleAccount = false;
        private readonly IConfiguration configuration;
        private static HttpClient client = new HttpClient();
        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;
        //AjaxResposeOK ok = new AjaxResposeOK();
        //AjaxResposeErr err = new AjaxResposeErr();

        // GET: AccountController
        public AccountController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }

        public async Task<ActionResult> Index()
        {
            Login model = new Login();  
                return View(model);
            
        }
        [HttpPost]
        public async Task<ActionResult> Index(Login model)
        {
            var request = new RestRequest("/api/Security/login", Method.Post/*, DataFormat.Json*/);
            request.AddJsonBody(new { email = model.User, password = model.Password });
            request.AddJsonBody(model);
            TempData["menu"] = null;
            try
            {
                if (model.Password != null && model.User != null)
                {
                    if (ModelState.IsValid)
                    {
                        _log.Info("Iniciando Auterticacion");
                        var response = await _apiClient.ExecuteAsync(request, Method.Post);
                        _log.Info("Verificando credenciales");
                        if (response.IsSuccessful)
                        {
                            LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);
                            // Crear una cookie para almacenar el token
                            Response.Cookies.Append("token", LogedData.token);
                            Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                            Response.Cookies.Append("user", model.User);
                            //TempData["MensajeExito"] = "Ingreso Exitoso";
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensajeError"] = response.Content;
                        return View(model);
                    }
                }
                TempData["MensajeError"] = "Rellene todos los campos";
                return View(model);
            }
            catch (JsonParsingException e)
            {
                _log.Error(e, "Error Obteniendo Token");
                _log.Error(e.GetUnderlyingStringUnsafe());
                TempData["MensajeError"] = e.Message.ToString();
                //return RedirectToAction("Index", "Home");
                return View(model);
            }
            catch (Exception e)
            {
                _log.Error(e, "Error al iniciar sesión");
                _log.Error(responseContent);
                TempData["MensajeError"] = e.Message;
                return Redirect("Index");
            }
        }

        public ActionResult CerrarSession()
        {
            Response.Cookies.Delete("token");
            Response.Cookies.Delete("expiracion");
            Response.Cookies.Delete("username");
            return RedirectToAction(nameof(Index));
        }
    }
}