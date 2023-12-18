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


    public class FichasControlViewModel
    {
        public long idHistoriaClinica { get; set; }
        public string codigoHistorial { get; set; }
        public string cedula { get; set; }
        public List<FichaSintomaDTO> fichasSintoma { get; set; }
        public List<FichasControl> fichasControl { get; set; }

    }
}
