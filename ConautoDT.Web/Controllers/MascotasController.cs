using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using SendGrid;
using Utf8Json;
using VET_ANIMAL.WEB.Models;
using VET_ANIMAL.WEB.Servicios;

namespace VET_ANIMAL.WEB.Controllers
{
    public class MascotasController : Controller
    {
        private readonly IConfiguration configuration;

        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;

        public MascotasController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }

        // GET: ClientesController
        public ActionResult Index()
        {
            MascotasViewModel model = new MascotasViewModel();

            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/cat/Mascotas", Method.Get);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------
            // request.AddQueryParameter("Tipo", Tipo);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;
                List<ItemMascota> ListaMascota = System.Text.Json.JsonSerializer.Deserialize<List<ItemMascota>>(content);
                model.ListaMascota = ListaMascota;
            }
            else
            {
                model.ListaMascota = null;
            }


            // model.tipoColor = Tipo;

            TempData["menu"] = "";

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> GuardaryEditarInfo(ItemMascota model)
        {
            string tokenValue = Request.Cookies["token"];

            var guardarMascotaViewModel = new ItemMascota
            {
                idMascota = model.idMascota,
                idCliente = model.idCliente,
                codigo = model.codigo,
                raza = model.raza,
                nombreMascota = model.nombreMascota,
                sexo = model.sexo,
                peso = model.peso,
                cliente = model.cliente,
                fechaNacimiento = model.fechaNacimiento,
            };

            var request = new RestRequest("/api/cat/Mascota", Method.Post);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            request.AddJsonBody(guardarMascotaViewModel);

            if (model.nombreMascota == null)
            {
                TempData["MensajeError"] = "Rellene todos los campos";
                return Redirect("Index");
            }

            try
            {
                // Validación del idCliente
                if (model.idCliente <= 0)
                {
                    throw new ArgumentException("Cedula de Cliente no encontrada o invalida");
                }

                if (model.nombreMascota != null)
                {
                    if (ModelState.IsValid)
                    {
                        _log.Info("Accediendo al API");
                        var response = await _apiClient.ExecuteAsync(request, Method.Post);
                        _log.Info("Registrando Mascota");
                        if (response.IsSuccessful)
                        {
                            if (model.idMascota == 0)
                            {
                                // SweetAlert para registro exitoso
                                TempData["MensajeExito"] = "Registro Exitoso";
                            }
                            else
                            {
                                // SweetAlert para edición exitosa
                                TempData["MensajeExito"] = "Se editó correctamente";
                            }
                            return RedirectToAction("Index", "Mascotas");
                        }
                        TempData["MensajeError"] = response.Content;
                        return RedirectToAction("Index", "Mascotas");
                    }
                    // SweetAlert para campos no válidos
                    TempData["MensajeError"] = "Rellene todos los campos";
                    return View(model);
                }
                TempData["MensajeError"] = "Cedula de Cliente incorrecta";
                return RedirectToAction("Index", "Mascotas");
            }
            catch (ArgumentException ex)
            {
                // Manejo específico para ArgumentException
                TempData["MensajeError"] = ex.Message;
                return RedirectToAction("Index", "Mascotas");
            }
            catch (JsonParsingException e)
            {
                _log.Error(e, "Error Obteniendo Token");
                _log.Error(e.GetUnderlyingStringUnsafe());
                TempData["MensajeError"] = e.Message.ToString();
                return View(model);
            }
            catch (Exception e)
            {
                // SweetAlert para error general
                _log.Error(e, "Error al iniciar sesión");
                TempData["MensajeError"] = e.Message;
                return Redirect("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditarInfo(ItemMascota model)
        {
            string tokenValue = Request.Cookies["token"];

            var EditarMascotaViewModel = new ItemMascota
            {
                idMascota = model.idMascota,
                idCliente = model.idCliente,
                codigo = model.codigo,
                raza = model.raza,
                nombreMascota = model.nombreMascota,
                sexo = model.sexo,
                peso = model.peso,
                cliente = model.cliente,
                fechaNacimiento = model.fechaNacimiento,
            };

            var request = new RestRequest("/api/cat/Mascota", Method.Put);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            request.AddJsonBody(EditarMascotaViewModel);

            if (model.nombreMascota == null)
            {
                TempData["MensajeError"] = "Rellene todos los campos";
                return Redirect("Index");
            }

            try
            {
                if (model.nombreMascota != null)
                {
                    if (ModelState.IsValid)
                    {
                        _log.Info("Accediendo al API");
                        var response = await _apiClient.ExecuteAsync(request, Method.Put);
                        _log.Info("Registrando Mascota");
                        if (response.IsSuccessful)
                        {
                            if (model.idMascota == 0)
                            {
                                // SweetAlert para campos no válidos
                                TempData["MensajeError"] = "Ocurrio un error, mascota no encontrada";
                            }
                            else
                            {
                                // SweetAlert para edición exitosa
                                TempData["MensajeExito"] = "Registro editado correctamente";
                            }
                            return RedirectToAction("Index", "Mascotas");
                        }
                        TempData["MensajeError"] = response.Content;
                        return RedirectToAction("Index", "Mascotas");
                    }
                    TempData["MensajeError"] = "Rellene todos los campos";
                }
                TempData["MensajeError"] = "Cedula de Cliente incorrecta";
                return RedirectToAction("Index", "Mascotas");
            }
            catch (JsonParsingException e)
            {
                _log.Error(e, "Error Obteniendo Token");
                _log.Error(e.GetUnderlyingStringUnsafe());
                TempData["MensajeError"] = e.Message.ToString();
                return View(model);
            }
            catch (Exception e)
            {
                _log.Error(e, "Error al iniciar sesión");
                TempData["MensajeError"] = e.Message;
                return Redirect("Index");
            }
        }

        // POST: CiudadController/Edit/5
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

        // GET: CiudadController/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteInformacionM(ItemMascota model)
        {
            string tokenValue = Request.Cookies["token"];
            long id = model.idMascota;
            var request = new RestRequest("/api/cat/Mascota", Method.Delete/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("IdMascota", id);
            //   request.AddJsonBody(model);

            try
            {
                if (model.idMascota != 0)
                {
                    if (ModelState.IsValid)
                    {
                        _log.Info("Accediendo al API");
                        var response = await _apiClient.ExecuteAsync(request, Method.Delete);
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

                            return RedirectToAction("Index", "Mascotas");
                        }
                        TempData["MensajeError"] = response.Content;
                        return View(model);
                    }
                    TempData["MensajeError"] = "No se pudo eliminar la Mascota";
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

        [HttpPost]
        public async Task<ActionResult> GuardarFichaControl(ItemMascota model)
        {
            if (!ModelState.IsValid || model.observacion == null)
            {
                TempData["MensajeError"] = "Rellene todos los campos";
                return RedirectToAction("Index", "Mascotas");
            }

            string tokenValue = Request.Cookies["token"];

            var guardarFicha = new ItemMascota
            {
                idHistoriaClinica = model.idHistoriaClinica,
                idMotivo = model.idMotivo,
                motivo = model.motivo,
                peso = model.peso,
                observacion = model.observacion
            };

            var request = new RestRequest("/api/Consulta/FichaControl", Method.Post);
            request.AddParameter("Authorization", $"Bearer {tokenValue}", ParameterType.HttpHeader);
            request.AddJsonBody(guardarFicha);

            _log.Info("Accediendo al API");
            var response = await _apiClient.ExecuteAsync(request, Method.Post);
            _log.Info("Registrando Mascota");

            if (response.IsSuccessful)
            {
                TempData["MensajeExito"] = model.idHistoriaClinica != 0 ? "Ficha Control Registrada Exitosamente" : "Se editó correctamente";
                return RedirectToAction("Index", "Mascotas");
            }
            else
            {
                TempData["MensajeError"] = response.Content;
                return RedirectToAction("Index", "Mascotas");
            }
        }

        //[HttpPost]
        //public async Task<ActionResult> GuardarFichaControl(ItemMascota model)
        //{
        //    string tokenValue = Request.Cookies["token"];

        //    var guardarFicha = new ItemMascota
        //    {
        //        idHistoriaClinica = model.idHistoriaClinica,
        //        idMotivo = model.idMotivo,
        //        motivo = model.motivo,
        //        peso = model.peso,
        //        observacion = model.observacion,

        //    };

        //    var request = new RestRequest("/api/Consulta/FichaControl", Method.Post);
        //    request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

        //    request.AddJsonBody(guardarFicha);

        //    if (model.observacion == null)
        //    {
        //        TempData["MensajeError"] = "Rellene todos los campos";
        //        return Redirect("Index");
        //    }

        //    try
        //    {
        //        // Validación del idHistoriaClinica
        //        if (model.idHistoriaClinica <= 0)
        //        {
        //            throw new ArgumentException("Historia Clinica no encontrada ");
        //        }

        //        if (model.idHistoriaClinica != 0)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                _log.Info("Accediendo al API");
        //                var response = await _apiClient.ExecuteAsync(request, Method.Post);
        //                _log.Info("Registrando Mascota");
        //                if (response.IsSuccessful)
        //                {
        //                    if (model.idHistoriaClinica != 0)
        //                    {
        //                        // SweetAlert para registro exitoso
        //                        TempData["MensajeExito"] = "Ficha Control Registrada Exitosamente";
        //                    }
        //                    else
        //                    {
        //                        // SweetAlert para edición exitosa
        //                        TempData["MensajeExito"] = "Se editó correctamente";
        //                    }
        //                    return RedirectToAction("Index", "Mascotas");
        //                }
        //                TempData["MensajeError"] = response.Content;
        //                return RedirectToAction("Index", "Mascotas");
        //            }
        //            // SweetAlert para campos no válidos
        //            TempData["MensajeError"] = "Rellene todos los campos";
        //            return View(model);
        //        }
        //        TempData["MensajeError"] = "Rellene todos los campos";
        //        return RedirectToAction("Index", "Mascotas");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        // Manejo específico para ArgumentException
        //        TempData["MensajeError"] = ex.Message;
        //        return RedirectToAction("Index", "Mascotas");
        //    }
        //    catch (JsonParsingException e)
        //    {
        //        _log.Error(e, "Error Obteniendo Token");
        //        _log.Error(e.GetUnderlyingStringUnsafe());
        //        TempData["MensajeError"] = e.Message.ToString();
        //        return View(model);
        //    }
        //    catch (Exception e)
        //    {
        //        // SweetAlert para error general
        //        _log.Error(e, "Error al iniciar sesión");
        //        TempData["MensajeError"] = e.Message;
        //        return Redirect("Index");
        //    }
        //}


        // POST: CiudadController/Delete/5
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