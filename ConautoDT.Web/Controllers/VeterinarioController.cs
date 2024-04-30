using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using Utf8Json;
using VET_ANIMAL.WEB.Models;
using VET_ANIMAL.WEB.Servicios;

namespace VET_ANIMAL.WEB.Controllers
{
    public class VeterinarioController : Controller
    {
        private readonly IConfiguration configuration;

        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;

        public VeterinarioController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }

        public ActionResult ListaRazas()
        {
            string tokenValue = Request.Cookies["token"];
            var request = new RestRequest("/api/Razas/ListarRazas", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = _apiClient.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;
                List<ItemRazas> ListaRazas = JsonConvert.DeserializeObject<List<ItemRazas>>(content);
                return Json(ListaRazas); // Devuelve la lista de razas como JSON
            }
            else
            {
                return Json(null); // Devuelve null si no hay razas
            }
        }
        public ActionResult Index()
        {
            VeterinariosViewModel model = new VeterinariosViewModel();

            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Veterinario/ListarVeterinarios", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            // request.AddQueryParameter("Tipo", Tipo);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                List<ItemVeterinarios> ListaVeterinarios = System.Text.Json.JsonSerializer.Deserialize<List<ItemVeterinarios>>(content);
                model.ItemVeterinarios = ListaVeterinarios;
            }
            else
            {
                model.ItemVeterinarios = null;
            }

            // model.tipoColor = Tipo;

            TempData["menu"] = "";

            return View(model);
        }

        
        [HttpPost]
        public async Task<ActionResult> GuardaryEditarInfo(ItemVeterinarios model)
        {
            string tokenValue = Request.Cookies["token"];

            var VeterinariosViewModel = new ItemVeterinarios
            {
                id = model.id,
                nombres = model.nombres,
                identificacion = model.identificacion,
                sede = model.sede,
                telefono = model.telefono,
                correo = model.correo,
                rol = model.rol
            };
            var request = new RestRequest("/api/Veterinario/NuevoVeterinario", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            request.AddJsonBody(VeterinariosViewModel);

            if (model.id == 0)
            {
                request.AddJsonBody(new { id = 0, nombres = model.nombres });
            }
            else
            {
                request.AddJsonBody(new { id = model.id, nombres = model.nombres, telefono = model.telefono, correo = model.correo, identificacion = model.identificacion, 
                    rol = model.rol});
            }

            request.AddJsonBody(model);

            if (model.nombres == null)
            {
                TempData["MensajeError"] = "Rellene todos los campos";
                return Redirect("Index");
            }

            // TempData["menu"] = null;
            try
            {
                if (model != null)
                {
                    if (ModelState.IsValid)
                    {
                        _log.Info("Accediendo al API");
                        var response = await _apiClient.ExecuteAsync(request, Method.Post);
                        _log.Info("Registrando Pais "/* + Tipo*/);
                        //responseContent = ;
                        if (response.IsSuccessful)
                        {
                            // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                            // Crear una cookie para almacenar el token
                            //Response.Cookies.Append("token", LogedData.token);
                            //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                            // Response.Cookies.Append("user", model.User);

                            if (model.id == 0)
                            {
                                TempData["MensajeExito"] = "Registro Exitoso";
                            }
                            else
                            {
                                TempData["MensajeExito"] = "Se edito correctamente";
                            }

                            return RedirectToAction("Index", "Veterinario");
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

        // GET: PaisController/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteInformacion(ItemVeterinarios model)
        {
            string tokenValue = Request.Cookies["token"];
            long id = model.id;
            var request = new RestRequest("/api/Veterinario/EliminarVeterinario", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("Id", id);
            //   request.AddJsonBody(model);

            try
            {
                if (model.id != 0)
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

                            return RedirectToAction("Index", "Veterinario");
                        }
                        TempData["MensajeError"] = response.Content;
                        return View(model);
                    }
                    TempData["MensajeError"] = "No se pudo eliminar la Pais";
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

            // return View(model);
        }

        // POST: PaisController/Delete/5
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