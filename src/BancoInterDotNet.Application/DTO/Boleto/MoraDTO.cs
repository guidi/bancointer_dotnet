using System;

namespace BancoInterDotNet.Application.DTO.Boleto
{
    public class MoraDTO
    {
        public String TipoMora { get; set; }
        public String codigoMora { get; set; }
        public DateTime? data { get; set; }
        public Double taxa { get; set; }
        public Decimal valor { get; set; }
    }
}
