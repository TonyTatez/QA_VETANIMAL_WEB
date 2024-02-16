namespace VET_ANIMAL.WEB.Models
{
    public class ItemMascota
    {
        public long idMascota { get; set; }
        public string codigo { get; set; }
        public string nombreMascota { get; set; }
        public string raza { get; set; }
        public string sexo { get; set; }
        public float? peso { get; set; }
        public string cliente { get; set; }

        public DateTime? fechaNacimiento { get; set; }
        public long idCliente { get; set; }
        public long idMotivo { get; set; }
        public string observacion { get; set; }
        public long idHistoriaClinica { get; set; }
        public long idFichaControl { get; set; }
        public string motivo { get; set; }
        public string codigoHistorial { get; set; }
        public string cedula { get; set; }
        
    }

    public class ItemCliente
    {
        public long idCliente { get; set; }
        public long identificacion { get; set; }
        public string codigo { get; set; }
        public string nombres { get; set; }
    }

    public class ListaMascotas
    {
        public List<ItemMascota> ListaMascota { get; set; }
    }

    public class MascotasViewModel
    {
        public long? IdCliente { get; set; }
        public List<ItemMascota> ListaMascota { get; set; }
       
        public List<ItemCliente> ListaClientes { get; set; }
        public FiltroReporteInspeccion Filtro { get; set; }
        public List<HistoricoInspeccionLlantaViewModel> ListaInspeccion { get; set; }

        public long idMascota { get; set; }
        public string codigo { get; set; }
        public string nombreMascota { get; set; }
        public string raza { get; set; }
        public string sexo { get; set; }
        public float? peso { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public long idHistoriaClinica { get; set; }
        public long idMotivo { get; set; }
        public string observacion { get; set; }
        public string motivo { get; set; }
        public string codigoHistorial { get; set; }
        public string cedula { get; set; }

    }

    public class FichaControl
    {
        public long idFichaControl { get; set; }
        public string codigoFichaControl { get; set; }
        public DateTime fecha { get; set; }
        public long idHistoriaClinica { get; set; }
        public long idMotivo { get; set; }
        public string motivo { get; set; }
        public float peso { get; set; }
        public string observacion { get; set; }
    }

    public class HistoricoInspeccionLlantaViewModel
    {
        public long IdFichaControl { get; set; }
        public string CodigoFichaControl { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public long IdHistoriaClinica { get; set; }
        public long IdMotivo { get; set; }
        public long IdMascota { get; set; }
        public string Motivo { get; set; }
        public float? Peso { get; set; }
        public string Observacion { get; set; }
        public string NombreMascota { get; set; }
        public string Raza { get; set; }
        public string Sexo { get; set; }
        public string Cliente { get; set; }
        public string Resultado { get; set; }
        public long idHistoInspeLlanta { get; set; }
        public long idLlanta { get; set; }

        public string termico { get; set; }
        public long idVehiculo { get; set; }

        public string placa { get; set; }
        public long idEstadoLLanta { get; set; }

        public string estadoLlanta { get; set; }
        public long idTipoVehiculo { get; set; }

        public string tipoVehiculo { get; set; }
        public int numeroPosicion { get; set; }

        public string posicion { get; set; }
        public long idLLantaInspe { get; set; }
        public DateTime fechaInspeccion { get; set; }
        public long idMarcaLLanta { get; set; }

        public string marcaLlanta { get; set; }
        public long idMedidaLLanta { get; set; }

        public string medidaLlanta { get; set; }
        public long idDisenioLLanta { get; set; }

        public string disenioLlanta { get; set; }
    }

    public class FiltroReporteInspeccion
    {
        public DateTime? desde { get; set; }
        public DateTime? hasta { get; set; }
        public List<string> enfermedades  { get; set; }
        public List<string> sexo { get; set; }
        public List<string> razas { get; set; }
    }

    public class HistoricoInspeccion
    {
        public long IdFichaControl { get; set; }
        public string CodigoFichaControl { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public long IdHistoriaClinica { get; set; }
        public long IdMotivo { get; set; }
        public long IdMascota { get; set; }
        public string Motivo { get; set; }
        public float? peso { get; set; }
        public string Observacion { get; set; }
        public string nombreMascota { get; set; }
        public string raza { get; set; }
        public string sexo { get; set; }
        public string cliente { get; set; }
        public string resultado { get; set; }

    }

    public class ItemMascotaFichas
    {
        public long idMascota { get; set; }
        public string codigo { get; set; }
        public string nombreMascota { get; set; }
        public string raza { get; set; }
        public string sexo { get; set; }
        public float? peso { get; set; }
        public string cliente { get; set; }

        public DateTime? fechaNacimiento { get; set; }
        public long idCliente { get; set; }
        public long idMotivo { get; set; }
        public long idEnfermedad { get; set; }
        public string enfermedad { get; set; }
        public string observaciones { get; set; }
        public string tratamiento { get; set; }
        public long idHistoriaClinica { get; set; }
        public long idFichaControl { get; set; }
        public string motivo { get; set; }
        public string codigoHistorial { get; set; }
        public string cedula { get; set; }

    }
}