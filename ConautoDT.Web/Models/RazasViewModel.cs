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

    public class ItemRazas
    {
        public long idRaza { get; set; }
        public string descripcion { get; set; }
    }

    public class ItemVeterinarios
    {
        public long id { get; set; }
        public string nombres { get; set; }
        public string identificacion { get; set; }
        public string sede { get; set; }
        public string rol { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
    }

    public class EliminarRazas
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    //public class ListaAnio
    //{
    //    public List<ItemAnio> ItemAnio { get; set; }
    //}

    public class RazasViewModel
    {
        public List<ItemRazas> ItemRazas { get; set; }
        public long idRaza { get; set; }
        public string descripcion { get; set; }
        //   public string tipoCiudad { get; set; }
    }

    public class VeterinariosViewModel
    {
        public List<ItemVeterinarios> ItemVeterinarios { get; set; }
        public long id { get; set; }
        public string nombres { get; set; }
        public string identificacion { get; set; }
        public string sede { get; set; }
        public string rol { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
    }
    public class RazaCantidadViewModel
    {
        public string descripcionRaza { get; set; }
        public int cantidad { get; set; }
    }
}