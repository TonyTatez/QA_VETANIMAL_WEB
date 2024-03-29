using System.ComponentModel.DataAnnotations;

namespace VET_ANIMAL.WEB.Models

{ //public class CrearCiudad
    //{
    //    public string Descripcion { get; set; }

    //}

    //public class EditarCiudad
    //{
    //    public long Id { get; set; }
    //    public Crear
    //
    //    Anio { get; set; }
    //}

    public class ItemDiagnostico
    {
        public long idEnfermedad { get; set; }
        public string nombre { get; set; }
    }
    public class CasoEnfermedad
    {
        public string Enfermedad { get; set; }
        public int Cantidad { get; set; }
    }
    public class EliminarReporte
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    

    //public class ListaAnio
    //{
    //    public List<ItemAnio> ItemAnio { get; set; }
    //}

    public class ReportesViewModel
    {
        public List<ItemDiagnostico> ItemDiagnostico { get; set; }
        public long idEnfermedad { get; set; }
        public string nombre { get; set; }
        //   public string tipoCiudad { get; set; }
    }
    public class DetallesFichas
    {
        public int IdFichaControl { get; set; }
        public int IdFichaHemo { get; set; }
        public string ResultadoAlgoritmo { get; set; }
        public string ResultadoFrotis { get; set; }
        public int IdHistoriaClinica { get; set; }

    }
    public class ResponseDatas
    {
        public List<DetallesFichas> DetallesFichas { get; set; }
        public int DiagnosticoAlgoritmo { get; set; } // Nuevo campo
        public int DiagnosticoFrotis { get; set; } // Nuevo campo
    }
}