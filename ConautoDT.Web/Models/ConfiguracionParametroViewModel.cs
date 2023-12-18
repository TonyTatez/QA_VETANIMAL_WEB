namespace VET_ANIMAL.WEB.Models
{
    //public class CrearConfiguracionParametros
    //{
    //    public string Descripcion { get; set; }

    //}

    //public class EditarConfiguracionParametros
    //{
    //    public long Id { get; set; }
    //    public CrearConfiguracionParametros ConfiguracionParametros { get; set; }
    //}

    public class ItemConfiguracionParametros
    {
        public long idConfigParam { get; set; }
        public string codigoParam { get; set; }
        public string descripcionParam { get; set; }
        public string valor { get; set; }
    }

    public class EliminarConfiguracionParametros
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    //public class ListaConfiguracionParametros
    //{
    //    public List<ItemConfiguracionParametros> ItemConfiguracionParametros { get; set; }
    //}

    public class ConfiguracionParametroViewModel
    {
        public List<ItemConfiguracionParametros> ItemConfiguracionParametros { get; set; }
        public long idConfigParam { get; set; }
        public string codigoParam { get; set; }
        public string descripcionParam { get; set; }
        public string valor { get; set; }
        //   public string tipoConfiguracionParametros { get; set; }
    }
}