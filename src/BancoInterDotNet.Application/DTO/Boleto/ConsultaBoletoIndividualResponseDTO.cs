using System;

namespace BancoInterDotNet.Application.DTO.Boleto
{
    public class ConsultaBoletoIndividualResponseDTO
    {
        public string nomeBeneficiario { get; set; }
        public string cnpjCpfBeneficiario { get; set; }
        public string tipoPessoaBeneficiario { get; set; }
        public string contaCorrente { get; set; }
        public string nossoNumero { get; set; }
        public string seuNumero { get; set; }
        public PagadorDTO pagador { get; set; }
        public string situacao { get; set; }
        public string dataHoraSituacao { get; set; }
        public string dataVencimento { get; set; }
        public double valorNominal { get; set; }
        public string dataEmissao { get; set; }
        public string dataLimite { get; set; }
        public string codigoEspecie { get; set; }
        public string codigoBarras { get; set; }
        public string linhaDigitavel { get; set; }
        public string origem { get; set; }
        public MensagemDTO mensagem { get; set; }
        public DescontoDTO desconto1 { get; set; }
        public DescontoDTO desconto2 { get; set; }
        public DescontoDTO desconto3 { get; set; }
        public MultaDTO multa { get; set; }
        public MoraDTO mora { get; set; }
        public Decimal valorAbatimento { get; set; }
    }
}
