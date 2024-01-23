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
        public string motivo { get; set; }
        public string codigoHistorial { get; set; }
        public string cedula { get; set; }

    }
}