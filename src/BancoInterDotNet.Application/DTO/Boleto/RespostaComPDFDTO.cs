using BancoInterDotNet.Application.DTO.Response;
using System;

namespace BancoInterDotNet.Application.DTO.Boleto
{
    public class RespostaComPDFDTO : ResponseBase
    {
        public String pdf { get; set; }
    }
}
