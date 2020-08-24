namespace BancoInterDotNet.Application.Enum
{
    public enum TipoDesconto
    {
        NaoTemDesconto, //Não tem desconto.
        ValorFixoDataInformada, // Valor fixo até data informada.
        PercentualDataInformada, //  Percentual até data informada.
        ValorAntecipacaoDiaCorrido, // Valor por antecipação (dia corrido).
        ValorAntecipacaoDiaUtil, //Valor por antecipação (dia útil).
        PercentualValorNominalDiaCorrido, //Percentual sobre o valor nominal por dia corrido.
        PercentualValorNominalDiaUtil //Percentual sobre o valor nominal por dia útil.
    }
}
