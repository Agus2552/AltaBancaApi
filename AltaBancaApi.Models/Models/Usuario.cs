using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS8618
namespace AltaBancaApi.Models.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(150)]
        public string Apellido { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [StringLength(50)]
        public string Cuit { get; set; }
        [Required]
        [StringLength(150)]
        public string Domicilio { get; set; }
        [Required]
        [StringLength(50)]
        public string Celular { get; set; }
        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
