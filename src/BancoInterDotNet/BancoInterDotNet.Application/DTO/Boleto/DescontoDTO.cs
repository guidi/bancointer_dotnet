using BancoInterDotNet.Application.Enum;
using System;

namespace BancoInterDotNet.Application.DTO.Boleto
{
    public class DescontoDTO
    {
        private TipoDesconto TipoDesconto { get; set; }
        private String codigoDesconto { get; set; }
        public DateTime? data { get; set; }
        public Double taxa { get; set; }
        public Decimal valor { get; set; }
    }
}
