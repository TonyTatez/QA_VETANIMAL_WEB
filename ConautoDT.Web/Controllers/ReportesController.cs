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

        //public async Task<ActionResult> Index (FiltroReporteInspeccion filtro, string operacion)
        //{
        //    TempData["menu"] = "";
        //    MascotasViewModel model = new MascotasViewModel();
        //    string tokenValue = Request.Cookies["token"];
        //    var client = new RestClient(configuration["APIClient"]);
        //    var request = new RestRequest("/api/HistoricoInspeccionLlanta/ListarHistoricoInspeccionesLlanta", Method.Get);
        //    if (filtro.desde == null)
        //        filtro.desde = DateTime.Today.AddDays(-7);
        //    if (filtro.hasta == null)
        //        filtro.hasta = DateTime.Today;
        //    request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
        //    request.AddQueryParameter("FechaInicio", filtro.desde.ToString());
        //    request.AddQueryParameter("FechaFin", filtro.hasta.ToString());
        //    request.AddQueryParameter("Termico", filtro.Termico);
        //    request.AddQueryParameter("IdTipoLlanta", filtro.IdTipoLlanta);
        //    model.Filtro = filtro;
        //    var response = await client.ExecuteAsync(request);
        //    if (response.Content.Length > 2 && response.IsSuccessful == true)
        //    {
        //        var content = response.Content;

        //        List<HistoricoInspeccionLlantaViewModel> ListaInspecciones = System.Text.Json.JsonSerializer.Deserialize<List<HistoricoInspeccionLlantaViewModel>>(content);

        //        model.ListaInspeccion = ListaInspecciones;
        //        if (operacion == "excel")
        //        {
        //            var reporteExcel = ListaInspecciones.Select(x => new ReporteExcelInspeccionLlanta
        //            {
        //                idHistoInspeLlanta = x.idHistoInspeLlanta,
        //                termico = x.termico,
        //                placa = x.placa,
        //                estadoLlanta = x.estadoLlanta,
        //                tipoVehiculo = x.tipoVehiculo,
        //                numeroPosicion = x.numeroPosicion,
        //                posicion = x.posicion,
        //                fechaInspeccion = x.fechaInspeccion,
        //                marcaLlanta = x.marcaLlanta,
        //                medidaLlanta = x.medidaLlanta,
        //                disenioLlanta = x.disenioLlanta
        //            }).ToList();
        //            var reporte = _reportingEngine.GenerateReportToByteArray(reporteExcel);
        //            return File(reporte, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte Inspecciones Llantas.xlsx");
        //        }
        //    }
        //    else
        //    {
        //        model.ListaInspeccion = null;
        //    }

        //    ////TIPO LLANTA
        //    //request = new RestRequest("/api/Tipos/Llantas/Listar", Method.Get);
        //    //request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
        //    //response = client.Execute(request);

        //    //if (response.Content.Length > 2 && response.IsSuccessful == true)
        //    //{
        //    //    var content = response.Content;

        //    //    List<TipoViewModel> ListaTipoLlanta = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);
        //    //    ListaTipoLlanta.Add(new TipoViewModel
        //    //    {
        //    //        idTipo = 0,
        //    //        descripcion = "TODOS"
        //    //    });
        //    //    model.ListaTipoLlanta = ListaTipoLlanta;
        //    //}
        //    //else
        //    //{
        //    //    model.ListaTipoLlanta = null;
        //    //}
        //    //return View(model);

        //    TempData["menu"] = "";

        //    return View(model);
        //}
       
        // GET: ReportesController
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
            request.AddQueryParameter("Termico", filtro.Termico);
            request.AddQueryParameter("IdTipoLlanta", filtro.IdMascota);
            model.Filtro = filtro;
            var response = await client.ExecuteAsync(request);
            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<HistoricoInspeccionLlantaViewModel> ListaInspecciones = System.Text.Json.JsonSerializer.Deserialize<List<HistoricoInspeccionLlantaViewModel>>(content);

                model.ListaInspeccion = ListaInspecciones;
                if (operacion == "excel")
                {
                    var reporteExcel = ListaInspecciones.Select(x => new ReporteExcelInspeccionLlanta
                    {
                        idHistoInspeLlanta = x.idHistoInspeLlanta,
                        termico = x.termico,
                        placa = x.placa,
                        estadoLlanta = x.estadoLlanta,
                        tipoVehiculo = x.tipoVehiculo,
                        numeroPosicion = x.numeroPosicion,
                        posicion = x.posicion,
                        fechaInspeccion = x.fechaInspeccion,
                        marcaLlanta = x.marcaLlanta,
                        medidaLlanta = x.medidaLlanta,
                        disenioLlanta = x.disenioLlanta
                    }).ToList();
                    var reporte = _reportingEngine.GenerateReportToByteArray(reporteExcel);
                    return File(reporte, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte Inspecciones Llantas.xlsx");
                }
            }
            else
            {
                model.ListaInspeccion = null;
            }
            return View();
        }

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
