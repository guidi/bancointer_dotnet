using BancoInterDotNet.Application.DTO.Response;
using System.Collections.Generic;

namespace BancoInterDotNet.Application.DTO.ContaCorrente
{
    public class ExtratoResponseDTO : ResponseBase
    {
        public List<TransacoesExtratoDTO> transacoes { get; set; }
    }

    public class TransacoesExtratoDTO
    {
        public string dataEntrada { get; set; }
        public string tipoTransacao { get; set; }
        public string tipoOperacao { get; set; }
        public string valor { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string codigoHistorico { get; set; }

    }
}
