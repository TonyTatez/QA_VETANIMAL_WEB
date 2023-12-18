using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using VET_ANIMAL.WEB.Models;
using VET_ANIMAL.WEB.Servicios;

namespace VET_ANIMAL.WEB.Controllers
{
    public class HistoriaClinicaController : Controller
    {
        private readonly IConfiguration configuration;

        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;


        public HistoriaClinicaController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }
        // GET: HistorialClinico
        public ActionResult Index()
        {
            TempData["menu"] = "";
            return View();
        }


        [HttpGet]
        public ActionResult BuscarFichasControlById([FromQuery] long CI)
        {
            try
            {
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/consulta/historial", Method.Get);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("CI", CI);

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = response.Content;
                    Console.WriteLine(content);
                    var Fichas = System.Text.Json.JsonSerializer.Deserialize<FichasControlViewModel>(content);


                    if (Fichas != null)
                    {
                        // Buscar el cliente por cédula en la lista

                        // Puedes devolver el IdCliente y la Cedula como un objeto JSON
                        return Json(Fichas);// Json(new { IdMascota = Mascotas.idMascota, peso = Mascotas.peso, raza = Mascotas.raza, sexo = Mascotas.sexo, fechaNacimiento = Mascotas.fechaNacimiento, nombreMascota = Mascotas.nombreMascota,
                                            // });
                    }
                    else
                    {
                        return Json(new { Mensaje = "Cliente no encontrado" });
                    }
                }
                else
                {
                    return Json(new { Mensaje = $"Error al obtener la lista de clientes. Código de estado: {response.StatusCode}" });
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí y registrar información de depuración si es necesario
                return Json(new { Mensaje = $"Error: {ex.Message}" });
            }
        }


        [HttpGet]
        public ActionResult BuscarClientePorCI([FromQuery] string CI)
        {
            try
            {
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/cat/Cliente", Method.Get);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("CI", CI);

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = response.Content;
                    var Clientes = System.Text.Json.JsonSerializer.Deserialize<ClientesViewModel>(content);

                    // Buscar el cliente por cédula en la lista

                    if (Clientes != null)
                    {
                        // Puedes devolver el IdCliente y la Cedula como un objeto JSON
                        return Json(new { IdCliente = Clientes.idCliente, Cedula = Clientes.identificacion, nombres = Clientes.nombres, direccion = Clientes.direccion, correo = Clientes.correo, telefono = Clientes.telefono,
                             });
                    }
                    else
                    {
                        return Json(new { Mensaje = "Cliente no encontrado" });
                    }
                }
                else
                {
                    return Json(new { Mensaje = $"Error al obtener la lista de clientes. Código de estado: {response.StatusCode}" });
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí y registrar información de depuración si es necesario
                return Json(new { Mensaje = $"Error: {ex.Message}" });
            }
        }

        [HttpGet]
        public ActionResult BuscarMascotaPorCI([FromQuery] string CI)
        {
            try
            {
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/cat/Cliente/mascotas", Method.Get);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("CI", CI);

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = response.Content;
                    Console.WriteLine(content);
                    var Mascotas = System.Text.Json.JsonSerializer.Deserialize<List<MascotasViewModel>>(content);


                    if (Mascotas != null)
                    {
                        // Buscar el cliente por cédula en la lista

                        // Puedes devolver el IdCliente y la Cedula como un objeto JSON
                        return Json(Mascotas);// Json(new { IdMascota = Mascotas.idMascota, peso = Mascotas.peso, raza = Mascotas.raza, sexo = Mascotas.sexo, fechaNacimiento = Mascotas.fechaNacimiento, nombreMascota = Mascotas.nombreMascota,
                            // });
                    }
                    else
                    {
                        return Json(new { Mensaje = "Cliente no encontrado" });
                    }
                }
                else
                {
                    return Json(new { Mensaje = $"Error al obtener la lista de clientes. Código de estado: {response.StatusCode}" });
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí y registrar información de depuración si es necesario
                return Json(new { Mensaje = $"Error: {ex.Message}" });
            }
        }
        // GET: HistorialClinico/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HistorialClinico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HistorialClinico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: HistorialClinico/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HistorialClinico/Edit/5
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

        // GET: HistorialClinico/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HistorialClinico/Delete/5
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