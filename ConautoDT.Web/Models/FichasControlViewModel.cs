namespace VET_ANIMAL.WEB.Models
{
    public class FichasControl
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

    public class fichasHemoparasitosis
    {

        public long idFichaHemo { get; set; }
        public string codigoFichaHemo { get; set; }
        public long idHistoriaClinica { get; set; }
        public DateTime fecha { get; set; }
        public long idMascota { get; set; }
        public long idEnfermedad { get; set; }
        public long? idContenido { get; set; }
        public string enfermedad { get; set; }
        public string tratamiento { get; set; }
        public string observaciones { get; set; }
        public string nombreMascota { get; set; }
        public string cliente { get; set; }
        public float? peso { get; set; }
        public string raza { get; set; }


    }
    public class FichaSintomaDTO
    {
        public long idFicha { get; set; }
        public string codigoFicha { get; set; }
        public DateTime fecha { get; set; }
        public List<FichaDetalleDTO> FichaDetalles { get; set; }
    }
    public class FichaDetalleDTO
    {
        public long idDetalle { get; set; }
        public long idFicha { get; set; }
        public long idSintoma { get; set; }
        public string sintoma { get; set; }
        public string observacion { get; set; }
    }

    public class ResultadosHemo
    {
        public long idFichaHemo { get; set; }
        public string codigoFichaHemo { get; set; }
        public long idHistoriaClinica { get; set; }
        public DateTime fecha { get; set; }
        public long idMascota { get; set; }
        public long idEnfermedad { get; set; }
        public string enfermedad { get; set; }
        public string tratamiento { get; set; }
        public string observaciones { get; set; }
        public string nombreMascota { get; set; }
        public string cliente { get; set; }
    }

    public class FichasControlViewModel
    {
        public long idHistoriaClinica { get; set; }
        public string codigoHistorial { get; set; }
        public string cedula { get; set; }
        public List<FichaSintomaDTO> fichasSintoma { get; set; }
        public List<FichasControl> fichasControl { get; set; }
        public List<fichasHemoparasitosis> fichasHemoparasitosis { get; set; }

    }


    public class FichasHemoViewModel
    {
        public long idHistoriaClinica { get; set; }
        public string codigoHistorial { get; set; }
        public string cedula { get; set; }
        public List<fichasHemoparasitosis> fichasHemoparasitosis { get; set; }

    }
}

