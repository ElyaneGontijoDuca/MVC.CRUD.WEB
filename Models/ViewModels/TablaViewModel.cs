using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCRUDweb.Models.ViewModels
{
    public class TablaViewModel
    {
        public int Id { get; set; }
        [Required]//DataNotacions para fazer validaciones para determinado campo
        [StringLength(50)]
        [Display(Name = "Nombre")] //o que vai mostrar 
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)] //Maximo que o campo permite
        [EmailAddress] //Valida se é um e-mail
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime Fecha_Nacimiento { get; set; }
        
    }
}