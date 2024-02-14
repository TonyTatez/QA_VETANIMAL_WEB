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
}