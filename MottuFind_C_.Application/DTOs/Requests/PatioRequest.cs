﻿using Sprint1_C_.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sprint1_C_.Application.DTOs.Requests
{
    public class PatioRequest
    {

        [Required(ErrorMessage = "O nome do pátio é obrigatório.")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        [MaxLength(40, ErrorMessage = "O nome deve ter no máximo 40 caracteres.")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ0-9\s\-]+$",
        ErrorMessage = "O nome do pátio deve conter apenas letras, números, espaços e traços.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O ID da filial é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "O ID da filial deve ser positivo.")]
        public int FilialId { get; set; }
    }
}
