using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using VET_ANIMAL.WEB.Engines;
using VET_ANIMAL.WEB.Models;
using VET_ANIMAL.WEB.Servicios;

namespace VET_ANIMAL.WEB.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IConfiguration configuration;

        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");

        private string responseContent { get; }
        private AccountService _AccountService;
        private readonly ExcelFormatsHandler excelHandler = new ExcelFormatsHandler();
        private readonly GemboxReportingEngine _reportingEngine = new GemboxReportingEngine();

      


        public ReportesController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }

        public async Task<ActionResult> Index(FiltroReporteInspeccion filtro, string operacion)
        {
            TempData["menu"] = "";
            MascotasViewModel model = new MascotasViewModel();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/HistoricoInspeccionLlanta/ListarHistoricoInspeccionesLlanta", Method.Get);
            if (filtro.desde == null)
                filtro.desde = DateTime.Today.AddDays(-7);
            if (filtro.hasta == null)
                filtro.hasta = DateTime.Today;
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("FechaInicio", filtro.desde.ToString());
            request.AddQueryParameter("FechaFin", filtro.hasta.ToString());
           
            model.Filtro = filtro;
            var response = await client.ExecuteAsync(request);
            

            TempData["menu"] = "";

            return View(model);
        }

        public async Task<ActionResult> Indexxxxx(FiltroReporteInspeccion filtro, string operacion)
        {
            TempData["menu"] = "";
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);

            // Inicializar la lista de resultados
            List<HistoricoInspeccion> resultados = new List<HistoricoInspeccion>();

            // Iterar sobre cada valor de enfermedades, razas y sexos
            foreach (var enfermedad in filtro.enfermedades)
            {
                foreach (var raza in filtro.razas)
                {
                    foreach (var sexo in filtro.sexo)
                    {
                        var request = new RestRequest("/api/Reportes/ReportesHistorico", Method.Get);
                        if (filtro.desde == null)
                            filtro.desde = DateTime.UtcNow.AddDays(-7);
                        if (filtro.hasta == null)
                            filtro.hasta = DateTime.UtcNow;
                        request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                        request.AddQueryParameter("FechaInicio", filtro.desde.ToString());
                        request.AddQueryParameter("FechaFin", filtro.hasta.ToString());
                        request.AddQueryParameter("enfermedades", enfermedad);
                        request.AddQueryParameter("razas", raza);
                        request.AddQueryParameter("sexo", sexo);

                        var response = await client.ExecuteAsync(request);
                        if (response.IsSuccessful)
                        {
                            var content = response.Content;
                            List<HistoricoInspeccion> ListaDiagnostico = JsonConvert.DeserializeObject<List<HistoricoInspeccion>>(content);
                            resultados.AddRange(ListaDiagnostico);
                        }
                    }
                }
            }

            // Verifica si no hay resultados y, si es así, inicialízalos con un valor por defecto
            if (resultados.Count == 0)
            {
                resultados.Add(new HistoricoInspeccion()); // o cualquier otro valor por defecto que necesites
            }

            return Json(resultados);
        }

        //public async Task<ActionResult> Indexxxxx(FiltroReporteInspeccion filtro, string operacion)
        //{
        //    TempData["menu"] = "";
        //    HistoricoInspeccion model = new HistoricoInspeccion();
        //    string tokenValue = Request.Cookies["token"];
        //    var client = new RestClient(configuration["APIClient"]);
        //    var request = new RestRequest("/api/Reportes/ReportesHistorico", Method.Get);
        //    if (filtro.desde == null)
        //        filtro.desde = DateTime.UtcNow.AddDays(-7);
        //    if (filtro.hasta == null)
        //        filtro.hasta = DateTime.UtcNow;
        //    request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
        //    request.AddQueryParameter("FechaInicio", filtro.desde.ToString());
        //    request.AddQueryParameter("FechaFin", filtro.hasta.ToString());
        //    request.AddQueryParameter("enfermedades", string.Join(",", filtro.enfermedades));
        //    request.AddQueryParameter("razas", string.Join(",", filtro.razas));
        //    request.AddQueryParameter("sexo", string.Join(",", filtro.sexo));

        //    var response = await client.ExecuteAsync(request);
        //    if (response.IsSuccessful)
        //    {
        //        var content = response.Content;
        //        List<HistoricoInspeccion> ListaDiagnostico = JsonConvert.DeserializeObject<List<HistoricoInspeccion>>(content);
        //        return Json(ListaDiagnostico); // Devuelve la lista de razas como JSON

        //    }

        //    // Verifica si 'model' sigue siendo null y, si es así, inicialízalo con un valor por defecto
        //    if (model == null)
        //    {
        //        model = new HistoricoInspeccion(); // o cualquier otro valor por defecto que necesites
        //    }

        //    return Json(model);
        //}

        public ActionResult ListaDiagnostico()
        {
            string tokenValue = Request.Cookies["token"];
            var request = new RestRequest("/api/Reportes/ListarDiagnostico", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = _apiClient.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;
                List<ItemDiagnostico> ListaDiagnostico = JsonConvert.DeserializeObject<List<ItemDiagnostico>>(content);
                return Json(ListaDiagnostico); // Devuelve la lista de razas como JSON
            }
            else
            {
                return Json(null); // Devuelve null si no hay razas
            }
        }



        // GET: ReportesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReportesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportesController/Create
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

        // GET: ReportesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReportesController/Edit/5
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

        // GET: ReportesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReportesController/Delete/5
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
