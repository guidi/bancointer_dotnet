using BancoInterDotNet.Application.Enum;
using System;

namespace BancoInterDotNet.Application.DTO.Boleto
{
    public class EmissaoBoletoDTO
    {
        public String seuNumero { get; set; }
        public String cnpjCPFBeneficiario { get; set; }
        public Decimal valorNominal { get; set; }
        public Decimal valorAbatimento { get; set; }
        public String dataEmissao { get; set; }
        public String dataVencimento { get; set; }
        public Agenda Agenda { get; set; }
        private String DiasAgenda { get; set; }
        public PagadorDTO pagador { get; set; }
        public MensagemDTO mensagem { get; set; }
        public DescontoDTO desconto1 { get; set; }
        public DescontoDTO desconto2 { get; set; }
        public DescontoDTO desconto3 { get; set; }
        public MultaDTO multa { get; set; }
        public MoraDTO mora { get; set; }

        public EmissaoBoletoDTO()
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
