using BancoInterDotNet.Application.DTO.Response;
using System;

namespace BancoInterDotNet.Application.DTO
{
    public class DefaultTokenDTO : ResponseBase
    {
        public String access_token { get; set; }
        public String refresh_token { get; set; }
        public String token_type { get; set; }
    }
}
