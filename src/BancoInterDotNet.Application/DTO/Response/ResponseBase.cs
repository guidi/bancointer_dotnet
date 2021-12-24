using System;

namespace BancoInterDotNet.Application.DTO.Response
{
    public class ResponseBase
    {
        public Boolean Erro { get; set; }
        public String Mensagem { get; set; }
    }
}
