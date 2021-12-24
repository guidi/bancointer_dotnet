using BancoInterDotNet.Application.DTO.Boleto;
using System;

namespace BancoInterDotNet.Application.DTO.Response
{
    public class ConsultaBoletoDetalhadoResponseDTO: ResponseBase
    {
        public String nomeBeneficiario { get; set; }
        public String cnpjCpfBeneficiario { get; set; }
        public String tipoPessoaBeneficiario { get; set; }
        public String dataHoraSituacao { get; set; }
        public String codigoBarras { get; set; }
        public String linhaDigitavel { get; set; }
        public String dataVencimento { get; set; }
        public String dataEmissao { get; set; }
        public String seuNumero { get; set; }
        public Decimal valorNominal { get; set; }
        public String nomePagador { get; set; }
        public String emailPagador { get; set; }
        public String dddPagador { get; set; }
        public String telefonePagador { get; set; }
        public String tipoPessoaPagador { get; set; }
        public String cnpjCpfPagador { get; set; }
        public String codigoEspecie { get; set; }
        public String dataLimitePagamento { get; set; }
        public Decimal valorAbatimento { get; set; }
        public String situacaoPagamento { get; set; }
        public String situacao { get; set; }
        public Decimal valorTotalRecebimento { get; set; }
        public MensagemDTO mensagem { get; set; }
        public DescontoDTO desconto1 { get; set; }
        public DescontoDTO desconto2 { get; set; }
        public DescontoDTO desconto3 { get; set; }
        public MultaDTO multa { get; set; }
        public MoraDTO mora { get; set; }
    }
}
