﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class AlterarJogoDTO : CadastrarJogoDTO
    {
        [Required(ErrorMessage = "O campo {0} deve ser preenchido.")]
        public required int Id { get; set; }
    }
}
