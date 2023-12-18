using GemBox.Spreadsheet;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using VET_ANIMAL.WEB.Helpers;
using VET_ANIMAL.WEB.Interfaces;

namespace VET_ANIMAL.WEB.Engines
{
    public class GemboxReportingEngine : IServerSideReportingEngine
    {
        public GemboxReportingEngine()
        {
            //SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            SpreadsheetInfo.SetLicense("ETFX-IT24-6RC5-B8TL");
            SpreadsheetInfo.FreeLimitReached += (sender, e) => e.FreeLimitReachedAction = FreeLimitReachedAction.ContinueAsTrial;
        }

        private ExcelFile _excelFile;

        private int _rowPointer = 0;

        private int _colPointer = 0;

        internal ExcelWorksheet RenderDataListHeader<T>(ExcelWorksheet excelWorksheet, IEnumerable<T> dataList)
        {
            Type dataItemType = dataList.GetType().GetGenericArguments()[0];
            PropertyInfo[] dataItemProperties = dataItemType.GetProperties();
            foreach (var property in dataItemProperties)
            {
                if (!property.PropertyType.IsGenericType || (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() != typeof(List<>)))
                {
                    var reportExcluded = property.GetCustomAttribute<ReportingAttributes.ReportExcludedAttribute>(false);
                    if (reportExcluded == null)
                    {
                        var displayName = property.GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();
                        var headerCell = excelWorksheet.Cells[_rowPointer, _colPointer];
                        headerCell.Value = displayName?.Name ?? property.Name;
                        headerCell.Column.Width = 6000;
                        headerCell.Style.Font.Weight = ExcelFont.BoldWeight;
                        headerCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                        headerCell.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Medium);
                        if (property.Name == "idHistoInspeLlanta")
                            headerCell.Value = "ID";
                        if (property.Name == "termico")
                            headerCell.Value = "Termico";
                        if (property.Name == "placa")
                            headerCell.Value = "Placa";
                        if (property.Name == "estadoLlanta")
                            headerCell.Value = "Estado de la Llanta";
                        if (property.Name == "tipoVehiculo")
                            headerCell.Value = "Tipo de Vehículo";
                        if (property.Name == "numeroPosicion")
                            headerCell.Value = "Número de Posición";
                        if (property.Name == "posicion")
                            headerCell.Value = "Posición";
                        if (property.Name == "fechaInspeccion")
                            headerCell.Value = "Fecha de Inspección";
                        if (property.Name == "marcaLlanta")
                            headerCell.Value = "Marca de Llanta";
                        if (property.Name == "medidaLlanta")
                            headerCell.Value = "Medida de Llanta";
                        if (property.Name == "disenioLlanta")
                            headerCell.Value = "Diseño de Llanta";
                        //if (property.Name == "ProveedorRuc")
                        //    headerCell.Value = "RUC del Proveedor";
                        //if (property.Name == "ProveedorNombre")
                        //    headerCell.Value = "Proveedor";
                        //if (property.Name == "ClienteRuc")
                        //    headerCell.Value = "RUC del Cliente";
                        //if (property.Name == "ClienteNombre")
                        //    headerCell.Value = "Cliente";
                        //if (property.Name == "TransportistaRuc")
                        //    headerCell.Value = "RUC del Transportista";
                        //if (property.Name == "TransportistaNombre")
                        //    headerCell.Value = "Transportista";
                        //if (property.Name == "FacturaNumero")
                        //    headerCell.Value = "Número de factura";
                        //if (property.Name == "NumeroContenedor")
                        //    headerCell.Value = "Número de contenedor";
                        //if (property.Name == "FechaHoraTurno")
                        //    headerCell.Value = "Fecha de Turno";
                        //if (property.Name == "MetodoPago")
                        //    headerCell.Value = "Método de pago";
                        //if (property.Name == "OVDocNum")
                        //    headerCell.Value = "Orden Venta DocNum";
                        //if (property.Name == "OVFechaSap")
                        //    headerCell.Value = "Orden Venta FechaSap";
                        headerCell.Style.FillPattern.SetSolid(System.Drawing.Color.Blue);
                        headerCell.Style.Font.Color = System.Drawing.Color.White;
                        _colPointer++;
                    }
                }
            }
            _rowPointer++;
            return excelWorksheet;
        }

        internal ExcelWorksheet RenderDataListHeaderReporteMonitoreo<T>(ExcelWorksheet excelWorksheet, IEnumerable<T> dataList)
        {
            Type dataItemType = dataList.GetType().GetGenericArguments()[0];
            PropertyInfo[] dataItemProperties = dataItemType.GetProperties();
            foreach (var property in dataItemProperties)
            {
                if (!property.PropertyType.IsGenericType || (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() != typeof(List<>)))
                {
                    var reportExcluded = property.GetCustomAttribute<ReportingAttributes.ReportExcludedAttribute>(false);
                    if (reportExcluded == null)
                    {
                        var displayName = property.GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();
                        var headerCell = excelWorksheet.Cells[_rowPointer, _colPointer];
                        headerCell.Value = displayName?.Name ?? property.Name;
                        headerCell.Column.Width = 6000;
                        headerCell.Style.Font.Weight = ExcelFont.BoldWeight;
                        headerCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                        headerCell.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Medium);
                        if (property.Name == "FechaCreacion")
                            headerCell.Value = "Fecha Creación";
                        if (property.Name == "Planta")
                            headerCell.Value = "Planta / Hacienda";
                        if (property.Name == "TransportistaRUC")
                            headerCell.Value = "Ruc Transportista";
                        if (property.Name == "TipoContenedor")
                            headerCell.Value = "Tipo Contenedor";
                        if (property.Name == "CedulaConductor")
                            headerCell.Value = "Cédula Conductor";
                        if (property.Name == "TelefonoConductor")
                            headerCell.Value = "Teléfono Conductor";
                        if (property.Name == "FechaSolicitadaPlanta")
                            headerCell.Value = "Fecha Solicitada Planta";
                        if (property.Name == "FechaLlegadaPlanta")
                            headerCell.Value = "Fecha Llegada Planta";
                        if (property.Name == "FechaSalidaPlanta")
                            headerCell.Value = "Fecha Salida Planta";
                        if (property.Name == "FechaEntregaPuerto")
                            headerCell.Value = "Fecha Entrega Puerto";
                        if (property.Name == "HorasEnPlanta")
                            headerCell.Value = "Horas en Planta";
                        if (property.Name == "AplicaGenerador")
                            headerCell.Value = "Aplica Generador";
                        if (property.Name == "TipoGenerador")
                            headerCell.Value = "Tipo Generador";
                        if (property.Name == "ProveedorGenerador")
                            headerCell.Value = "Proveedor Generador";
                        if (property.Name == "PlacaGenerador")
                            headerCell.Value = "Placa Generador";
                        headerCell.Style.FillPattern.SetSolid(System.Drawing.Color.BlueViolet);
                        headerCell.Style.Font.Color = System.Drawing.Color.White;
                        _colPointer++;
                    }
                }
            }
            _rowPointer++;
            return excelWorksheet;
        }

        internal ExcelWorksheet RenderDataListToRows<T>(ExcelWorksheet excelWorksheet, IEnumerable<T> dataList)
        {
            Type dataListType = dataList.GetType().GetGenericArguments()[0];
            PropertyInfo[] dataListProperties = dataListType.GetProperties();
            foreach (var dataItem in dataList)
            {
                _colPointer = 0;
                foreach (var property in dataListProperties)
                {
                    if (!property.PropertyType.IsGenericType || (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() != typeof(List<>)))
                    {
                        var reportExcluded = property.GetCustomAttribute<ReportingAttributes.ReportExcludedAttribute>(false);
                        if (reportExcluded == null)
                        {
                            var cell = excelWorksheet.Cells[_rowPointer, _colPointer];
                            if (dataItem == null)
                            {
                                cell.Value = null;
                            }
                            else
                            {
                                cell.Value = dataItem.GetType().GetProperty(property.Name).GetValue(dataItem);
                            }
                            cell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                            cell.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Medium);
                            _colPointer++;
                        }
                    }
                }
                _rowPointer++;
            }
            return excelWorksheet;
        }

        internal byte[] SaveAsXlsx()
        {
            byte[] byteArray;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                _excelFile.Save(memoryStream, SaveOptions.XlsxDefault);
                byteArray = memoryStream.ToArray();
            }
            return byteArray;
        }

        public byte[] GenerateReportToByteArray<T>(IEnumerable<T> dataList)
        {
            _excelFile = new ExcelFile();
            var workSheet = _excelFile.Worksheets.Add("Reporte");
            var range = workSheet.Cells.GetSubrange("A1:R1");
            //range.Style.Font.Color = System.Drawing.Color.White;
            //range.Style.FillPattern.SetSolid(System.Drawing.Color.Purple);
            RenderDataListHeader(workSheet, dataList);
            RenderDataListToRows(workSheet, dataList);
            //workSheet.Cells[0, 0].Value = "ID";
            //workSheet.Cells[0, 2].Value = "Año";
            //workSheet.Cells[0, 3].Value = "Código de Reserva";
            //workSheet.Cells[0, 4].Value = "Booking/BL";
            //workSheet.Cells[0, 7].Value = "Tipo de operación";
            //workSheet.Cells[0, 10].Value = "Cédula del chofer";
            //workSheet.Cells[0, 12].Value = "Tipo de contenedor";
            //workSheet.Cells[0, 15].Value = "Fecha de Inicio de Operación";
            //workSheet.Cells[0, 16].Value = "Fecha de Fin de Operación";
            //workSheet.Cells[0, 17].Value = "Fecha de Caducidad";
            //for (var i = 0; i < 19; i++)
            //{
            //    workSheet.Columns[i].AutoFit();
            //}
            return SaveAsXlsx();
        }

        public byte[] GenerateReporteExcelMonitoreoFormato<T>(IEnumerable<T> dataList)
        {
            _excelFile = new ExcelFile();
            var workSheet = _excelFile.Worksheets.Add("Reporte_Monitoreo");
            //var range = workSheet.Cells.GetSubrange("A1:AG1");
            //range.Style.Font.Color = System.Drawing.Color.White;
            //range.Style.FillPattern.SetSolid(System.Drawing.Color.Blue);
            RenderDataListHeaderReporteMonitoreo(workSheet, dataList);
            RenderDataListToRows(workSheet, dataList);
            //workSheet.Cells[0, 0].Value = "ID";
            //workSheet.Cells[0, 2].Value = "Año";
            //workSheet.Cells[0, 3].Value = "Código de Reserva";
            //workSheet.Cells[0, 4].Value = "Booking/BL";
            //workSheet.Cells[0, 7].Value = "Tipo de operación";
            //workSheet.Cells[0, 10].Value = "Cédula del chofer";
            //workSheet.Cells[0, 12].Value = "Tipo de contenedor";
            //workSheet.Cells[0, 15].Value = "Fecha de Inicio de Operación";
            //workSheet.Cells[0, 16].Value = "Fecha de Fin de Operación";
            //workSheet.Cells[0, 17].Value = "Fecha de Caducidad";
            for (var i = 0; i < 18; i++)
            {
                workSheet.Columns[i].AutoFit();
            }
            return SaveAsXlsx();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public byte[] GenerateReportToByteArray<T>(IEnumerable<T> dataList, out string reportFileName)
        {
            throw new NotImplementedException();
        }

        public byte[] GenerateReportToByteArray<T>(IEnumerable<T> dataList, out string reportFileName, out TimeSpan reportGenerationTime)
        {
            throw new NotImplementedException();
        }

        public MemoryStream GenerateReportToStream<T>(IEnumerable<T> dataList)
        {
            throw new NotImplementedException();
        }

        public MemoryStream GenerateReportToStream<T>(IEnumerable<T> dataList, out string reportFileName)
        {
            throw new NotImplementedException();
        }

        public MemoryStream GenerateReportToStream<T>(IEnumerable<T> dataList, out string reportFileName, out TimeSpan reportGenerationTime)
        {
            throw new NotImplementedException();
        }
    }
}