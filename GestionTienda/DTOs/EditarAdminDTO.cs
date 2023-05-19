using System;
using System.ComponentModel.DataAnnotations;

namespace GestionTienda.DTOs
{
	public class EditarAdminDTO
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

