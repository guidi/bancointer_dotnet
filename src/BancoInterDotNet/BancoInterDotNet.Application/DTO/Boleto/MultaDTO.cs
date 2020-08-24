using BancoInterDotNet.Application.Enum;
using System;

namespace BancoInterDotNet.Application.DTO.Boleto
{
    public class MultaDTO
    {
        private TipoMulta TipoMulta { get; set; }
        private String codigoMulta { get; set; }
        public DateTime? data { get; set; }
        public Double taxa { get; set; }
        public Decimal valor { get; set; }
    }
}
