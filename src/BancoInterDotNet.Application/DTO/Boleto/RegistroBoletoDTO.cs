using System;

namespace BancoInterDotNet.Application.DTO.Boleto
{
    public class RegistroBoletoDTO
    {
        public String seuNumero { get; set; }
        public Decimal valorNominal { get; set; }
        public Decimal valorAbatimento { get; set; }
        public String dataVencimento { get; set; }
        public Int32 numDiasAgenda { get; set; }
        public PagadorDTO pagador { get; set; }
        public MensagemDTO mensagem { get; set; }
        public DescontoDTO desconto1 { get; set; }
        public DescontoDTO desconto2 { get; set; }
        public DescontoDTO desconto3 { get; set; }
        public MultaDTO multa { get; set; }
        public MoraDTO mora { get; set; }

        public RegistroBoletoDTO()
        {
            pagador = new PagadorDTO();
            mensagem = new MensagemDTO();
            desconto1 = new DescontoDTO();
            desconto2 = new DescontoDTO();
            desconto3 = new DescontoDTO();
            multa = new MultaDTO();
            mora = new MoraDTO();
        }
    }
}
