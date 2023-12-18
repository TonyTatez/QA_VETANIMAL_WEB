using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using Utf8Json;
using VET_ANIMAL.WEB.Models;
using VET_ANIMAL.WEB.Servicios;

namespace VET_ANIMAL.WEB.Controllers
{
    public class ConfiguracionParametroController : Controller
    {
        // string Tipo = "Llanta";
        private readonly IConfiguration configuration;

        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");

        private string responseContent { get; }
        private AccountService _AccountService;
        // GET: ConfiguracionController1

        public ConfiguracionParametroController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }

        public ActionResult Index()
        {
            ConfiguracionParametroViewModel model = new ConfiguracionParametroViewModel();

            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/ConfiguracionParametros/ListarConfiguracionParametros", Method.Get);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------
            // request.AddQueryParameter("Tipo", Tipo);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                List<ItemConfiguracionParametros> ListaConfiguracions = System.Text.Json.JsonSerializer.Deserialize<List<ItemConfiguracionParametros>>(content);
                model.ItemConfiguracionParametros = ListaConfiguracions;
            }
            else
            {
                model.ItemConfiguracionParametros = null;
            }
            // model.tipoConfiguracion = Tipo;

            TempData["menu"] = "";

            return View(model);
        }

        // POST: ConfiguracionController1/Create
        [HttpPost]
        public async Task<ActionResult> GuardaryEditarInfo(ConfiguracionParametroViewModel model)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/ConfiguracionParametros/NuevaConfiguracionParametros", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            if (model.idConfigParam == 0)
            {
                request.AddJsonBody(new { idConfigParam = 0, codigoParam = model.codigoParam, descripcionParam = model.descripcionParam, valor = model.valor });
            }
            else
            {
                request.AddJsonBody(new { idConfigParam = model.idConfigParam, codigoParam = model.codigoParam, descripcionParam = model.descripcionParam, valor = model.valor });
            }

            request.AddJsonBody(model);

            if (model.descripcionParam == null)
            {
                TempData["MensajeError"] = "Rellene todos los campos";
                return Redirect("Index");
            }

            // TempData["menu"] = null;
            try
            {
                if (model.descripcionParam != null)
                {
                    if (ModelState.IsValid)
                    {
                        _log.Info("Accediendo al API");
                        var response = await _apiClient.ExecuteAsync(request, Method.Post);
                        _log.Info("Registrando Configuracion "/* + Tipo*/);
                        //responseContent = ;
                        if (response.IsSuccessful)
                        {
                            // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                            // Crear una cookie para almacenar el token
                            //Response.Cookies.Append("token", LogedData.token);
                            //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                            // Response.Cookies.Append("user", model.User);

                            if (model.idConfigParam == 0)
                            {
                                TempData["MensajeExito"] = "Registro Exitoso";
                            }
                            else
                            {
                                TempData["MensajeExito"] = "Se edito correctamente";
                            }

                            return RedirectToAction("Index", "ConfiguracionParametro");
                        }
                        TempData["MensajeError"] = response.Content;
                        return View(model);
                    }
                    TempData["MensajeError"] = "Rellene todos los campos";
                }
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

        // POST: ConfiguracionController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConfiguracionController1/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteInformacion(ConfiguracionParametroViewModel model)
        {
            string tokenValue = Request.Cookies["token"];
            long id = model.idConfigParam;
            if (id > 0)
            {
                var request = new RestRequest("/api/ConfiguracionParametros/EliminaConfiguracionParametros", Method.Post/*, DataFormat.Json*/);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("IdConfiguracionParametros", id);
                //   request.AddJsonBody(model);

                try
                {
                    if (model.idConfigParam != 0)
                    {
                        if (ModelState.IsValid)
                        {
                            _log.Info("Accediendo al API");
                            var response = await _apiClient.ExecuteAsync(request, Method.Post);
                            // _log.Info("Registrando Tipo de" + Tipo);
                            //responseContent = ;
                            if (response.IsSuccessful)
                            {
                                // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                                // Crear una cookie para almacenar el token
                                //Response.Cookies.Append("token", LogedData.token);
                                //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                                // Response.Cookies.Append("user", model.User);

                                TempData["MensajeExito"] = "Eliminacion Exitosa";

                                return RedirectToAction("Index", "ConfiguracionParametro");
                            }
                            TempData["MensajeError"] = response.Content;
                            return View(model);
                        }
                        TempData["MensajeError"] = "No se pudo eliminar la Configuracion";
                    }
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
            return RedirectToAction("Index", "ConfiguracionParametro");
            // return View(model);
        }

        // POST: ConfiguracionController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}