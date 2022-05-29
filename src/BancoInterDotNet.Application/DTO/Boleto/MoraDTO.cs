using System;

namespace BancoInterDotNet.Application.DTO.Boleto
{
    public class MoraDTO
    {
        public String codigoMora { get; set; }
        public String data { get; set; }
        public Double taxa { get; set; }
        public Decimal valor { get; set; }
    }
}
