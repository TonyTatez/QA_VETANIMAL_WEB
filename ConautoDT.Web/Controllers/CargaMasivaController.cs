using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using VET_ANIMAL.WEB.Models;
using VET_ANIMAL.WEB.Servicios;

using System.Text.Json;
namespace VET_ANIMAL.WEB.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration configuration;

        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;

        public CargaMasivaController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }
        // GET: CargaMasivaController
        public ActionResult CargaMasiva()
        {
            TempData["menu"] = "";
            return View();
        }

        public async Task<int[]> ObtenerNumeroClientesYNumeroMascotas()
        {
            try
            {
                string tokenValue = Request.Cookies["token"];

                // Crear la solicitud a la API para obtener el número de clientes y mascotas
                var request = new RestRequest("/api/cat/NumeroClientes", Method.Get);
                request.AddHeader("Authorization", $"Bearer {tokenValue}");

                // Realizar la solicitud
                var response = await _apiClient.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    // Deserializar la respuesta
                    var content = response.Content;
                    var data = JsonSerializer.Deserialize<Dictionary<string, int>>(content);

                    // Extraer los números de clientes y mascotas
                    int numeroClientes = data["item1"];
                    int numeroMascotas = data["item2"];

                    // Devolver los números de clientes y mascotas en forma de matriz
                    return new int[] { numeroClientes, numeroMascotas };
                }
                else
                {
                    // Manejar el caso en que la solicitud no fue exitosa
                    _log.Error($"Error al obtener el número de clientes y mascotas. Código de estado: {response.StatusCode}, Mensaje: {response.ErrorMessage}");
                    throw new Exception("Error al obtener el número de clientes y mascotas");
                }
            }
            catch (Exception ex)
            {
                // Manejar errores según tus necesidades
                _log.Error(ex, "Error al obtener el número de clientes y mascotas");
                throw;
            }
        }

        // ... (otros métodos)



        // GET: CargaMasivaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CargaMasivaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargaMasivaController/Create
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

        

        // GET: CargaMasivaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CargaMasivaController/Edit/5
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

        // GET: CargaMasivaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> TrainModel(IFormFile file)
        {
            try
            {
                string tokenValue = Request.Cookies["token"];

                // Crear la solicitud a la API para entrenar el modelo
                var request = new RestRequest("/api/Prediccion/train", Method.Post);
                request.AddParameter("Authorization", $"Bearer {tokenValue}", ParameterType.HttpHeader);
                request.AddFile("ArchivoEntrenamiento", ReadFully(file.OpenReadStream()), file.FileName);

                // Realizar la solicitud
                var response = await _apiClient.ExecuteAsync(request, Method.Post);

                if (response.IsSuccessful)
                {
                    // Manejar la respuesta del servidor
                    var responseData = response.Content;
                    return Json(new { success = true, message = responseData });
                }
                else
                {
                    // Manejar el caso en que la solicitud no fue exitosa
                    return Json(new { success = false, message = response.Content });
                }
            }
            catch (Exception ex)
            {
                // Manejar errores según tus necesidades
                _log.Error(ex, "Error al entrenar el modelo");
                return Json(new { success = false, message = "Error al entrenar el modelo" });
            }
        }




        // POST: CargaMasivaController/Delete/5
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



        [HttpPost]
        public async Task<JsonResult> Upload(IFormFile Excel)
        {
            string tokenValue = Request.Cookies["token"];

            //var client = new RestClient("URL_DEL_SERVIDOR"); // Reemplaza con la URL correcta
            var request = new RestRequest("/api/sync/Catalogos", Method.Post);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddFile("Excel", ReadFully(Excel.OpenReadStream()), Excel.FileName);

            // Realizar la solicitud
            var response = await _apiClient.ExecuteAsync(request, Method.Post);

            if (response.IsSuccessful)
            {
                // Manejar la respuesta del servidor
                var responseData = response.Content;
                return Json(new { success = true, message = responseData });
            }
            else
            {
                // Manejar el caso en que la solicitud no fue exitosa
                return Json(new { success = false, message = response.Content });
            }
        }

        // Método para convertir el flujo de entrada del archivo en un array de bytes
        private byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
