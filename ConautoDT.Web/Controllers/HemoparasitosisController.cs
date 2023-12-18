using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using System.Collections.Generic;
using VET_ANIMAL.WEB.Models;
using VET_ANIMAL.WEB.Servicios;

namespace VET_ANIMAL.WEB.Controllers
{
    public class HemoparasitosisController : Controller
    {
        private readonly IConfiguration configuration;

        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;


        public HemoparasitosisController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }

        //GET: HemoparasitosisController
        public ActionResult Index()
        {
            
            
                MascotasViewModel model = new MascotasViewModel();
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/consulta/FichasControlSospecha", Method.Get);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = response.Content;
                    List<ItemMascota> ListaFichas= System.Text.Json.JsonSerializer.Deserialize<List<ItemMascota>>(content);
                    model.ListaMascota = ListaFichas;

                   
                }
                else
                {
                    model.ListaMascota = null;
                }
                // model.tipoColor = Tipo;

                TempData["menu"] = "";

                return View(model);


        }

        //[HttpGet]
        //public  ActionResult SospechaHemo()
        //{
        //    try
        //    {

        //        string tokenValue = Request.Cookies["token"];
        //        var client = new RestClient(configuration["APIClient"]);
        //        var request = new RestRequest("/api/consulta/FichasControlSospecha", Method.Get);
        //        request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

        //        var response = client.Execute(request);

        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            var content = response.Content;
        //            var fichasControl = System.Text.Json.JsonSerializer.Deserialize<List<MascotasViewModel>>(content);
        //            if (fichasControl != null)
        //            {

        //                return Json(fichasControl);
        //            }
        //            else
        //            {
        //                return Json(new { Mensaje = "Ficha no encontrada" });
        //            }

        //        }
        //        else
        //        {
        //        return Json(new { Mensaje = $"Error al obtener la lista de clientes. Código de estado: {response.StatusCode}" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //            // Manejar excepciones aquí y registrar información de depuración si es necesario
        //     return Json(new { Mensaje = $"Error: {ex.Message}" });
        //    }


        //}

        [HttpPost]
        public ActionResult PredecirEnfermedad(int idMascota, string nombreMascota, string[] sintomas)
        {
            try
            {
                // Lógica de predicción basada en las condiciones especificadas
                bool tieneEsplenomegalia = sintomas.Contains("ESPLENOMEGALIA");
                bool tieneAnorexia = sintomas.Contains("ANOREXIA");
                bool tieneSintomasExcluidos = sintomas.Any(s => s == "EPISTAXIS" || s == "HEMORRAGIAS EN MUCOSAS" || s == "ICTERICIA" || s == "HEMOGLOBINURIA" || s == "PROBLEMAS RESPIRATORIOS");

                if (tieneEsplenomegalia && tieneAnorexia && !tieneSintomasExcluidos)
                {
                    // Si cumple con las condiciones, la predicción es ANAPLASMOSIS
                    return Json(new { idMascota, nombreMascota, enfermedad = "ANAPLASMOSIS" });
                }

                // Validación para ICTERICIA
                bool tieneEpistaxis = sintomas.Contains("EPISTAXIS");
                bool tieneHemorragiasMucosas = sintomas.Contains("HEMORRAGIAS EN MUCOSAS");
                bool tieneProblemaRespiratporios = sintomas.Contains("PROBLEMAS RESPIRATORIOS");
                bool tieneSintomasExcluidos2 = sintomas.Any(s => s == "ESPLENOMEGALIA" || s == "ANOREXIA" || s == "ICTERICIA" || s == "HEMOGLOBINURIA" );
                
                if (tieneEpistaxis && tieneHemorragiasMucosas && !tieneSintomasExcluidos2)
                {
                    // Si cumple con las condiciones, la predicción es BABESIOSIS
                    return Json(new { idMascota, nombreMascota, enfermedad = "EHRLICHIOSIS" });
                }

                // Validación para BABESIOSIS
                bool tiene1 = sintomas.Contains("ICTERICIA");
                bool tiene2 = sintomas.Contains("HEMOGLOBINURIA");
                bool tieneSintomasExcluidos3 = sintomas.Any(s => s == "ESPLENOMEGALIA" || s == "ANOREXIA" || s == "PROBLEMAS RESPIRATORIOS" ||  s == "EPISTAXIS" || s == "HEMORRAGIAS EN MUCOSAS");

                if (tiene1 && tiene2 && !tieneSintomasExcluidos3)
                {
                    // Si cumple con las condiciones, la predicción es BABESIOSIS
                    return Json(new { idMascota, nombreMascota, enfermedad = "BABESIOSIS" });
                }
                else
                {
                    // Si se presentan síntomas que no deberían ser, la predicción es "Enfermedad Desconocida"
                    return Json(new { idMascota, nombreMascota,  enfermedad = "Enfermedad Desconocida" });
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí y registrar información de depuración si es necesario
                return Json(new { Mensaje = $"Error: {ex.Message}" });
            }
        }


        // GET: HemoparasitosisController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HemoparasitosisController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HemoparasitosisController/Create
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

        // GET: HemoparasitosisController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HemoparasitosisController/Edit/5
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

        // GET: HemoparasitosisController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HemoparasitosisController/Delete/5
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
