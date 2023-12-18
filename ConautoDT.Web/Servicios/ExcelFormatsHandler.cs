using GemBox.Spreadsheet;

namespace VET_ANIMAL.WEB.Servicios
{
    public class ExcelFormatsHandler
    {
        public ExcelFormatsHandler()
        {
            SpreadsheetInfo.SetLicense("ETFX-IT24-6RC5-B8TL");
            SpreadsheetInfo.FreeLimitReached += (sender, e) => e.FreeLimitReachedAction = FreeLimitReachedAction.Stop;
        }

        public List<ExcelCreacionReservaMasiva> ConvertFormatExcelToClassRservasMasivas(Stream data)
        {
            var model = new List<ExcelCreacionReservaMasiva>();
            try
            {
                ExcelFile excelFilePlantilla = ExcelFile.Load(data, LoadOptions.XlsxDefault);
                int Row = 0, Col = 0;

                var ws = excelFilePlantilla.Worksheets[0];
                excelFilePlantilla.Worksheets.ActiveWorksheet = ws;

                int numeroFilasVacias = 0;

                for (int i = 1; i < ws.Rows.Count; i++)
                {
                    var LineNumber = ws.Cells[Row + i, Col].Value?.ToString();

                    if (!string.IsNullOrEmpty(LineNumber))
                    {
                        model.Add(new ExcelCreacionReservaMasiva
                        {
                            //Buque = ws.Cells[Row + i, Col].Value?.ToString()?.Trim() ?? "",
                            //Viaje = ws.Cells[Row + i, Col + 1].Value?.ToString()?.Trim() ?? "",
                            BookingBL = ws.Cells[Row + i, Col].Value?.ToString()?.Trim() ?? "",
                            Categoria = ws.Cells[Row + i, Col + 1].Value?.ToString()?.Trim() ?? "",
                            RUC = ws.Cells[Row + i, Col + 2].Value?.ToString()?.Trim() ?? "",
                            ExportadorConsignatario = ws.Cells[Row + i, Col + 3].Value?.ToString()?.Trim() ?? "",
                            ISO = ws.Cells[Row + i, Col + 4].Value?.ToString()?.Trim() ?? "",
                            Producto = ws.Cells[Row + i, Col + 5].Value?.ToString()?.Trim() ?? "",
                            Linea = ws.Cells[Row + i, Col + 6].Value?.ToString()?.Trim() ?? "",
                            Ruta = ws.Cells[Row + i, Col + 7].Value?.ToString()?.Trim() ?? "",
                            Planta = ws.Cells[Row + i, Col + 8].Value?.ToString()?.Trim() ?? "",
                            RUCCliente = ws.Cells[Row + i, Col + 9].Value?.ToString()?.Trim() ?? "",
                            Cliente = ws.Cells[Row + i, Col + 10].Value?.ToString()?.Trim() ?? "",
                            Ecas = ws.Cells[Row + i, Col + 11].Value?.ToString()?.Trim() ?? "",
                            DAE = ws.Cells[Row + i, Col + 12].Value?.ToString()?.Trim() ?? "",
                        });
                    }
                    else
                    {
                        numeroFilasVacias++;
                        if (numeroFilasVacias == 10) break;
                    }
                }
                return model;
            }
            catch (Exception ex) { return model; }
        }

        public List<ExcelHistoricoLlantas> ConvertFormatExcelToClassExcelHistoricoLlantas(Stream data)
        {
            var model = new List<ExcelHistoricoLlantas>();
            try
            {
                ExcelFile excelFilePlantilla = ExcelFile.Load(data, LoadOptions.XlsxDefault);
                int Row = 0, Col = 0;

                var ws = excelFilePlantilla.Worksheets[0];
                excelFilePlantilla.Worksheets.ActiveWorksheet = ws;

                int numeroFilasVacias = 0;

                for (int i = 1; i < ws.Rows.Count; i++)
                {
                    var LineNumber = ws.Cells[Row + i, Col].Value?.ToString();
                    var LineNumber2 = ws.Cells[Row + i, Col + 1].Value?.ToString();

                    //if (!string.IsNullOrEmpty(LineNumber) && !string.IsNullOrEmpty(LineNumber))
                    //{
                    model.Add(new ExcelHistoricoLlantas
                    {
                        Termico = ws.Cells[Row + i, Col].Value?.ToString()?.Trim() ?? "",
                        IdVehiculo = ws.Cells[Row + i, Col + 1].Value?.ToString()?.Trim() ?? "",
                        KmRecorridos = ws.Cells[Row + i, Col + 2].Value?.ToString()?.Trim() ?? "",
                        Posicion = ws.Cells[Row + i, Col + 3].Value?.ToString()?.Trim() ?? "",
                        Medida = ws.Cells[Row + i, Col + 4].Value?.ToString()?.Trim() ?? "",
                        MarcaNueva = ws.Cells[Row + i, Col + 5].Value?.ToString()?.Trim() ?? "",
                        Diseño = ws.Cells[Row + i, Col + 6].Value?.ToString()?.Trim() ?? "",
                        Tipo = ws.Cells[Row + i, Col + 7].Value?.ToString()?.Trim() ?? "",
                        MarcaReencauche = ws.Cells[Row + i, Col + 8].Value?.ToString()?.Trim() ?? "",
                        DiseñoReencauche = ws.Cells[Row + i, Col + 9].Value?.ToString()?.Trim() ?? "",
                        Eje = ws.Cells[Row + i, Col + 10].Value?.ToString()?.Trim() ?? "",
                        MMOriginal = ws.Cells[Row + i, Col + 11].Value?.ToString()?.Trim() ?? "",
                        MM1 = ws.Cells[Row + i, Col + 12].Value?.ToString()?.Trim() ?? "",
                        MM2 = ws.Cells[Row + i, Col + 13].Value?.ToString()?.Trim() ?? "",
                        MM3 = ws.Cells[Row + i, Col + 14].Value?.ToString()?.Trim() ?? "",
                        MM4 = ws.Cells[Row + i, Col + 15].Value?.ToString()?.Trim() ?? "",
                        MMMin = ws.Cells[Row + i, Col + 16].Value?.ToString()?.Trim() ?? "",
                        MMRetiro = ws.Cells[Row + i, Col + 17].Value?.ToString()?.Trim() ?? "",
                        PorcentajeDesgaste = ws.Cells[Row + i, Col + 18].Value?.ToString()?.Trim() ?? "",
                        FechaInspeccion = ws.Cells[Row + i, Col + 19].Value?.ToString()?.Trim() ?? "",
                        Ciudad = ws.Cells[Row + i, Col + 20].Value?.ToString()?.Trim() ?? "",
                        MarcaVehiculo = ws.Cells[Row + i, Col + 21].Value?.ToString()?.Trim() ?? "",
                        ModeloVehiculo = ws.Cells[Row + i, Col + 22].Value?.ToString()?.Trim() ?? "",
                        TipoVehiculo = ws.Cells[Row + i, Col + 23].Value?.ToString()?.Trim() ?? "",
                        TipoEstructura = ws.Cells[Row + i, Col + 24].Value?.ToString()?.Trim() ?? "",
                        Referencia = ws.Cells[Row + i, Col + 25].Value?.ToString()?.Trim() ?? "",
                        Observaciones = ws.Cells[Row + i, Col + 26].Value?.ToString()?.Trim() ?? "",
                    });
                    //}
                }
                return model;
            }
            catch (Exception ex)
            {
                return model;
            }
        }

        public List<ExcelCreacionReservaMasivaImportacion> ConvertFormatExcelToClassRservasMasivasImportacion(Stream data)
        {
            var model = new List<ExcelCreacionReservaMasivaImportacion>();
            try
            {
                ExcelFile excelFilePlantilla = ExcelFile.Load(data, LoadOptions.XlsxDefault);
                int Row = 0, Col = 0;

                var ws = excelFilePlantilla.Worksheets[0];
                excelFilePlantilla.Worksheets.ActiveWorksheet = ws;

                int numeroFilasVacias = 0;

                for (int i = 1; i < ws.Rows.Count; i++)
                {
                    var LineNumber = ws.Cells[Row + i, Col].Value?.ToString();

                    if (!string.IsNullOrEmpty(LineNumber))
                    {
                        model.Add(new ExcelCreacionReservaMasivaImportacion
                        {
                            //Buque = ws.Cells[Row + i, Col].Value?.ToString()?.Trim() ?? "",
                            //Viaje = ws.Cells[Row + i, Col + 1].Value?.ToString()?.Trim() ?? "",
                            BookingBL = ws.Cells[Row + i, Col].Value?.ToString()?.Trim() ?? "",
                            RUC = ws.Cells[Row + i, Col + 1].Value?.ToString()?.Trim() ?? "",
                            ExportadorConsignatario = ws.Cells[Row + i, Col + 2].Value?.ToString()?.Trim() ?? "",
                            ISO = ws.Cells[Row + i, Col + 3].Value?.ToString()?.Trim() ?? "",
                            Producto = ws.Cells[Row + i, Col + 4].Value?.ToString()?.Trim() ?? "",
                            Linea = ws.Cells[Row + i, Col + 5].Value?.ToString()?.Trim() ?? "",
                            Ruta = ws.Cells[Row + i, Col + 6].Value?.ToString()?.Trim() ?? "",
                            Planta = ws.Cells[Row + i, Col + 7].Value?.ToString()?.Trim() ?? "",
                            RUCCliente = ws.Cells[Row + i, Col + 8].Value?.ToString()?.Trim() ?? "",
                            Cliente = ws.Cells[Row + i, Col + 9].Value?.ToString()?.Trim() ?? "",
                            Ecas = ws.Cells[Row + i, Col + 10].Value?.ToString()?.Trim() ?? "",
                            Pedido = ws.Cells[Row + i, Col + 11].Value?.ToString()?.Trim() ?? "",
                            Parcial = ws.Cells[Row + i, Col + 12].Value?.ToString()?.Trim() ?? "",
                            Puerto = ws.Cells[Row + i, Col + 13].Value?.ToString()?.Trim() ?? "",
                            Contenedor = ws.Cells[Row + i, Col + 14].Value?.ToString()?.Trim() ?? "",
                            DepositoDevolucion = ws.Cells[Row + i, Col + 15].Value?.ToString()?.Trim() ?? "",
                            DAI = ws.Cells[Row + i, Col + 16].Value?.ToString()?.Trim() ?? "",
                        });
                    }
                    else
                    {
                        numeroFilasVacias++;
                        if (numeroFilasVacias == 10) break;
                    }
                }
                return model;
            }
            catch (Exception ex) { return model; }
        }

        public List<FormatoExcelSRI> ConvertFormatExcelToClass(Stream data)
        {
            var model = new List<FormatoExcelSRI>();
            try
            {
                ExcelFile excelFilePlantilla = ExcelFile.Load(data, LoadOptions.XlsxDefault);
                int Row = 0, Col = 0;

                var ws = excelFilePlantilla.Worksheets[0];
                excelFilePlantilla.Worksheets.ActiveWorksheet = ws;

                int numeroFilasVacias = 0;

                for (int i = 1; i < ws.Rows.Count; i++)
                {
                    var LineNumber = ws.Cells[Row + i, Col].Value?.ToString();

                    if (!string.IsNullOrEmpty(LineNumber))
                    {
                        model.Add(new FormatoExcelSRI
                        {
                            claveAcceso = ws.Cells[Row + i, Col].Value?.ToString()?.Trim() ?? "",
                            ambiente = ws.Cells[Row + i, Col + 1].Value?.ToString()?.Trim() ?? "",
                            estado = ws.Cells[Row + i, Col + 2].Value?.ToString()?.Trim() ?? "",
                            numeroAutorizacion = ws.Cells[Row + i, Col + 3].Value?.ToString()?.Trim() ?? "",
                            codDoc = ws.Cells[Row + i, Col + 4].Value?.ToString()?.Trim() ?? "",
                            NumeroDocumento = ws.Cells[Row + i, Col + 5].Value?.ToString()?.Trim() ?? "",
                            ruc = ws.Cells[Row + i, Col + 6].Value?.ToString()?.Trim() ?? "",
                            razonSocial = ws.Cells[Row + i, Col + 7].Value?.ToString()?.Trim() ?? "",
                            fechaAutorizacion = Convert.ToDateTime(ws.Cells[Row + i, Col + 8].Value?.ToString()?.Trim() ?? DateTime.MinValue.ToShortDateString()),
                            Estado = ws.Cells[Row + i, Col + 9].Value?.ToString()?.Trim() ?? "",
                            razonSocialComprador = ws.Cells[Row + i, Col + 10].Value?.ToString()?.Trim() ?? "",
                            identificacionComprador = ws.Cells[Row + i, Col + 11].Value?.ToString()?.Trim() ?? "",
                            direccionComprador = ws.Cells[Row + i, Col + 12].Value?.ToString()?.Trim() ?? "",
                            Total = Convert.ToDecimal(ws.Cells[Row + i, Col + 13].Value?.ToString()?.Trim() ?? "0"),
                            ArchivoGenerado = true,
                            estab = ws.Cells[Row + i, Col + 15].Value?.ToString()?.Trim() ?? "",
                            ptoEmi = ws.Cells[Row + i, Col + 16].Value?.ToString()?.Trim() ?? "",
                            secuencial = ws.Cells[Row + i, Col + 17].Value?.ToString()?.Trim() ?? "",
                            dirMatriz = ws.Cells[Row + i, Col + 18].Value?.ToString()?.Trim() ?? "",
                            fechaEmision = Convert.ToDateTime(ws.Cells[Row + i, Col + 19].Value?.ToString()?.Trim() ?? DateTime.MinValue.ToShortDateString()),
                            dirEstablecimiento = ws.Cells[Row + i, Col + 20].Value?.ToString()?.Trim() ?? "",
                            contribuyenteEspecial = ws.Cells[Row + i, Col + 21].Value?.ToString()?.Trim() ?? "",
                            obligadoContabilidad = ws.Cells[Row + i, Col + 22].Value?.ToString()?.Trim() ?? "",
                            tipoIdentificacionComprador = ws.Cells[Row + i, Col + 23].Value?.ToString()?.Trim() ?? "",
                            descripcionesadicionales = ws.Cells[Row + i, Col + 24].Value?.ToString()?.Trim() ?? "",
                            error = ws.Cells[Row + i, Col + 25].Value?.ToString()?.Trim() ?? "",
                        });
                    }
                    else
                    {
                        numeroFilasVacias++;
                        if (numeroFilasVacias == 10) break;
                    }
                }
                return model;
            }
            catch (Exception ex) { return model; }
        }

        public List<ExcelPagoTurnos> ConvertFormatExcelToClassPT(Stream data)
        {
            var model = new List<ExcelPagoTurnos>();
            try
            {
                ExcelFile excelFilePlantilla = ExcelFile.Load(data, LoadOptions.XlsxDefault);
                int Row = 0, Col = 0;

                var ws = excelFilePlantilla.Worksheets[0];
                excelFilePlantilla.Worksheets.ActiveWorksheet = ws;

                int numeroFilasVacias = 0;

                for (int i = 1; i < ws.Rows.Count; i++)
                {
                    var LineNumber = ws.Cells[Row + i, Col].Value?.ToString();

                    if (!string.IsNullOrEmpty(LineNumber))
                    {
                        model.Add(new ExcelPagoTurnos
                        {
                            Contenedor = ws.Cells[Row + i, Col].Value?.ToString()?.Trim() ?? "",
                            BookingBL = ws.Cells[Row + i, Col + 1].Value?.ToString()?.Trim() ?? "",
                            RucDeposito = ws.Cells[Row + i, Col + 2].Value?.ToString()?.Trim() ?? "",
                            NombreDeposito = ws.Cells[Row + i, Col + 3].Value?.ToString()?.Trim() ?? "",
                            NumeroOp = ws.Cells[Row + i, Col + 4].Value?.ToString()?.Trim() ?? "",
                            ValorOp = Convert.ToDecimal(ws.Cells[Row + i, Col + 5].Value?.ToString()?.Trim() ?? "0"),
                            FechaOP = Convert.ToDateTime(ws.Cells[Row + i, Col + 6].Value?.ToString()?.Trim() ?? DateTime.MinValue.ToShortDateString()),
                            Banco = ws.Cells[Row + i, Col + 7].Value?.ToString()?.Trim() ?? "",
                            ArchivoGenerado = true,
                            error = ws.Cells[Row + i, Col + 9].Value?.ToString()?.Trim() ?? "",
                        });
                    }
                    else
                    {
                        numeroFilasVacias++;
                        if (numeroFilasVacias == 10) break;
                    }
                }
                return model;
            }
            catch (Exception ex) { return model; }
        }

        public List<ExcelConsumaVehiculos> ConvertFormatExcelToClassPrTVehiculos(Stream data)
        {
            var model = new List<ExcelConsumaVehiculos>();
            try
            {
                ExcelFile excelFilePlantilla = ExcelFile.Load(data, LoadOptions.XlsxDefault);
                int Row = 0, Col = 0;

                var ws = excelFilePlantilla.Worksheets[0];
                excelFilePlantilla.Worksheets.ActiveWorksheet = ws;

                int numeroFilasVacias = 0;

                for (int i = 1; i < ws.Rows.Count; i++)
                {
                    var LineNumber = ws.Cells[Row + i, Col].Value?.ToString();

                    if (!string.IsNullOrEmpty(LineNumber))
                    {
                        model.Add(new ExcelConsumaVehiculos
                        {
                            CodigoCliente = ws.Cells[Row + i, Col].Value?.ToString()?.Trim() ?? "",
                            RazonSocial = ws.Cells[Row + i, Col + 1].Value?.ToString()?.Trim() ?? "",
                            FechaEmision = ws.Cells[Row + i, Col + 2].Value?.ToString()?.Trim() ?? "",
                            HoraEmision = ws.Cells[Row + i, Col + 3].Value?.ToString()?.Trim() ?? "",
                            FechaContable = ws.Cells[Row + i, Col + 4].Value?.ToString()?.Trim() ?? "",
                            NombreEDS = ws.Cells[Row + i, Col + 5].Value?.ToString()?.Trim() ?? "",
                            Placa = ws.Cells[Row + i, Col + 6].Value?.ToString()?.Trim() ?? "",
                            Modelo = ws.Cells[Row + i, Col + 7].Value?.ToString()?.Trim() ?? "",
                            Unidad = ws.Cells[Row + i, Col + 8].Value?.ToString()?.Trim() ?? "",
                            NombreChofer = ws.Cells[Row + i, Col + 9].Value?.ToString()?.Trim() ?? "",
                            Kilometraje = ws.Cells[Row + i, Col + 10].Value?.ToString()?.Trim() ?? "",
                            Departamento = ws.Cells[Row + i, Col + 11].Value?.ToString()?.Trim() ?? "",
                            Producto = ws.Cells[Row + i, Col + 12].Value?.ToString()?.Trim() ?? "",
                            NotaDespacho = ws.Cells[Row + i, Col + 13].Value?.ToString()?.Trim() ?? "",
                            Cantidad = ws.Cells[Row + i, Col + 14].Value?.ToString()?.Trim() ?? "",
                            MontoCalculado = ws.Cells[Row + i, Col + 15].Value?.ToString()?.Trim() ?? ""
                        });
                    }
                    else
                    {
                        numeroFilasVacias++;
                        if (numeroFilasVacias == 8) break;
                    }
                }
                return model;
            }
            catch (Exception ex) { return model; }
        }

        public List<ExcelProcesoTurnos> ConvertFormatExcelToClassPrT(Stream data)
        {
            var model = new List<ExcelProcesoTurnos>();
            try
            {
                ExcelFile excelFilePlantilla = ExcelFile.Load(data, LoadOptions.XlsxDefault);
                int Row = 0, Col = 0;

                var ws = excelFilePlantilla.Worksheets[0];
                excelFilePlantilla.Worksheets.ActiveWorksheet = ws;

                int numeroFilasVacias = 0;

                for (int i = 1; i < ws.Rows.Count; i++)
                {
                    var LineNumber = ws.Cells[Row + i, Col].Value?.ToString();

                    if (!string.IsNullOrEmpty(LineNumber))
                    {
                        model.Add(new ExcelProcesoTurnos
                        {
                            Factura = ws.Cells[Row + i, Col].Value?.ToString()?.Trim() ?? "",
                            RUC = ws.Cells[Row + i, Col + 1].Value?.ToString()?.Trim() ?? "",
                            Proveedor = ws.Cells[Row + i, Col + 2].Value?.ToString()?.Trim() ?? "",
                            Booking = ws.Cells[Row + i, Col + 3].Value?.ToString()?.Trim() ?? "",
                            Contenedor = ws.Cells[Row + i, Col + 4].Value?.ToString()?.Trim() ?? "",
                            Op = ws.Cells[Row + i, Col + 5].Value?.ToString()?.Trim() ?? "",
                            ArchivoGenerado = true,
                            error = ws.Cells[Row + i, Col + 7].Value?.ToString()?.Trim() ?? "",
                        });
                    }
                    else
                    {
                        numeroFilasVacias++;
                        if (numeroFilasVacias == 8) break;
                    }
                }
                return model;
            }
            catch (Exception ex) { return model; }
        }

        public MemoryStream GenerateExcelByFormatName(string templatePath, object lista)
        {
            //try
            //{
            //    IList collection = (IList)lista;
            //    var keyword = collection[0].GetType().GetProperty("No") != null ? "No" : "ITEM";
            //    ExcelFile excelFilePlantilla = ExcelFile.Load(templatePath);

            //    var SaveOptions = XlsxSaveOptions.XlsxDefault;
            //    var ws = excelFilePlantilla.Worksheets[0];
            //    excelFilePlantilla.Worksheets.ActiveWorksheet = ws;

            //    int Row = 0, Col = 0;
            //    ws.Cells.FindText(keyword, true, true, out Row, out Col);
            //    Row++;

            //    SetCaberaPaita(cabeceraPaita, ref ws);

            //    foreach (var obj in collection)
            //    {
            //        foreach (PropertyInfo prop in obj.GetType().GetProperties())
            //        {
            //            if (!prop.Name.Equals("Id"))  //para que no salga el id en el excel
            //            {
            //                ws.Cells[Row, Col].Value = prop.GetValue(obj, null)?.ToString();
            //                Col++;
            //            }
            //        }
            //        Col = 0;
            //        Row++;
            //    }

            //    return DocumentToMemoryStream(excelFilePlantilla, SaveOptions);
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}

            return null;
        }

        private MemoryStream DocumentToMemoryStream(ExcelFile excelFile, SaveOptions saveOptions)
        {
            var memoryStream = new MemoryStream();
            excelFile.Save(memoryStream, saveOptions);
            return memoryStream;
        }
    }

    public class FormatoExcelSRI
    {
        public string claveAcceso { get; set; }
        public string ambiente { get; set; }

        public string estado
        { get; set; }

        public string numeroAutorizacion { get; set; }
        public string codDoc { get; set; }

        public string NumeroDocumento { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public DateTime fechaAutorizacion { get; set; }
        public string Estado { get; set; }
        public string razonSocialComprador { get; set; }
        public string identificacionComprador { get; set; }
        public string direccionComprador { get; set; }

        public decimal Total { get; set; }
        public bool ArchivoGenerado { get; set; }
        public string estab { get; set; }
        public string ptoEmi { get; set; }

        public string secuencial { get; set; }
        public string dirMatriz { get; set; }
        public DateTime fechaEmision { get; set; }
        public string dirEstablecimiento { get; set; }

        public string contribuyenteEspecial { get; set; }
        public string obligadoContabilidad { get; set; }
        public string tipoIdentificacionComprador { get; set; }
        public string descripcionesadicionales { get; set; }

        public string error
        { get; set; }
    }

    public class ExcelSRI
    {
        public string CodNaviera { get; set; }
        public string Naviera { get; set; }
        public string CodNave { get; set; }
        public string Nave { get; set; }
        public string CodViaje { get; set; }
        public string Viaje { get; set; }
    }

    public class ExcelPagoTurnos
    {
        public string Contenedor { get; set; }
        public string BookingBL { get; set; }
        public string RucDeposito { get; set; }
        public string NombreDeposito { get; set; }
        public string NumeroOp { get; set; }
        public decimal ValorOp { get; set; }
        public DateTime FechaOP { get; set; }
        public string Banco { get; set; }
        public bool ArchivoGenerado { get; set; }
        public string error { get; set; }
    }

    public class ExcelProcesoTurnos
    {
        public string Factura { get; set; }
        public string RUC { get; set; }
        public string Proveedor { get; set; }
        public string Booking { get; set; }
        public string Contenedor { get; set; }
        public string Op { get; set; }
        public bool ArchivoGenerado { get; set; }
        public string error { get; set; }
    }

    public class ExcelConsumaVehiculos
    {
        public string CodigoCliente { get; set; }
        public string RazonSocial { get; set; }
        public string FechaEmision { get; set; }
        public string HoraEmision { get; set; }
        public string FechaContable { get; set; }
        public string NombreEDS { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Unidad { get; set; }
        public string NombreChofer { get; set; }
        public string Kilometraje { get; set; }
        public string Departamento { get; set; }
        public string Producto { get; set; }
        public string NotaDespacho { get; set; }
        public string Cantidad { get; set; }
        public string MontoCalculado { get; set; }
    }

    public class ExcelCreacionReservaMasiva
    {
        public string Buque { get; set; }
        public string Viaje { get; set; }
        public string BookingBL { get; set; }
        public string Categoria { get; set; }
        public string RUC { get; set; }
        public string ExportadorConsignatario { get; set; }
        public int Cantidad { get; set; }
        public string ISO { get; set; }
        public string Producto { get; set; }
        public string Linea { get; set; }
        public string Ruta { get; set; }
        public string Planta { get; set; }
        public string RUCCliente { get; set; }
        public string Cliente { get; set; }
        public string Ecas { get; set; }
        public string DAE { get; set; }
    }

    public class ExcelCreacionReservaMasivaImportacion
    {
        public string Buque { get; set; }
        public string Viaje { get; set; }
        public string BookingBL { get; set; }
        public string Categoria { get; set; }
        public string RUC { get; set; }
        public string ExportadorConsignatario { get; set; }
        public int Cantidad { get; set; }
        public string ISO { get; set; }
        public string Producto { get; set; }
        public string Linea { get; set; }
        public string Ruta { get; set; }
        public string Planta { get; set; }
        public string RUCCliente { get; set; }
        public string Cliente { get; set; }
        public string Ecas { get; set; }
        public string Pedido { get; set; }
        public string Parcial { get; set; }
        public string Puerto { get; set; }
        public string Contenedor { get; set; }
        public string DepositoDevolucion { get; set; }
        public string DAI { get; set; }
    }

    public class ExcelHistoricoLlantas
    {
        public string Termico { get; set; }
        public string IdVehiculo { get; set; }
        public string KmRecorridos { get; set; }
        public string Posicion { get; set; }
        public string Medida { get; set; }
        public string MarcaNueva { get; set; }
        public string Diseño { get; set; }
        public string Tipo { get; set; }
        public string MarcaReencauche { get; set; }
        public string DiseñoReencauche { get; set; }
        public string Eje { get; set; }
        public string MMOriginal { get; set; }
        public string MM1 { get; set; }
        public string MM2 { get; set; }
        public string MM3 { get; set; }
        public string MM4 { get; set; }
        public string MMMin { get; set; }
        public string MMRetiro { get; set; }
        public string PorcentajeDesgaste { get; set; }
        public string FechaInspeccion { get; set; }
        public string Ciudad { get; set; }
        public string MarcaVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public string TipoVehiculo { get; set; }
        public string TipoEstructura { get; set; }
        public string Referencia { get; set; }
        public string Observaciones { get; set; }
    }

    public class ListaRegistrosCargadosExcel
    {
        public string Termico { get; set; }
        public string Placa { get; set; }
        public string FechaInspeccion { get; set; }
        public string Observaciones { get; set; }
    }
}