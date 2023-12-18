using System.ComponentModel.DataAnnotations;

namespace VET_ANIMAL.WEB.Models
{
    public class CuentaViewModel
    {
        public CuentaViewModel()
        {
            ListaRoles = new List<ItemRolesViewModel>();
            Modal = new CreateRolesViewModel();
        }

        public List<ItemRolesViewModel> ListaRoles { get; set; }
        public CreateRolesViewModel Modal { get; set; }
        public EditRolesViewModel ModalEdit { get; set; }
    }

    public class LoginCuentaViewModel
    {
        [Required]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }

    public class LogedDataViewModel
    {
        public string token { get; set; }
        public DateTime expiracion { get; set; }
    }

    public class CreateRolesViewModel
    {
        [Required]
        public string Rol { get; set; }

        [Required]
        public string Descripcion { get; set; }
    }

    public class EditRolesViewModel : CreateRolesViewModel
    {
        public long IdRol { get; set; }
    }

    public class ItemRolesViewModel : EditRolesViewModel
    {
    }
}