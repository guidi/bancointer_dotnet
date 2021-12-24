using System;

namespace BancoInterDotNet.Application.DTO.Response
{
    public class EmissaoBoletoResponseDTO : ResponseBase
    {
        public string seuNumero { get; set; }
        public string nossoNumero { get; set; }
        public string codigoBarras { get; set; }
        public string linhaDigitavel { get; set; }

    }
}
