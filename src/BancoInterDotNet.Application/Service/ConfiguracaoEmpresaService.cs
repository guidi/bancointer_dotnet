using BancoInterDotNet.Application.Model;

namespace BancoInterDotNet.Application.Service
{
    public static class ConfiguracaoEmpresaService
    {
        public static ConfiguracaoEmpresa ObterConfiguracao()
        {
            ConfiguracaoEmpresa empresaConfiguracao = new ConfiguracaoEmpresa();
            empresaConfiguracao.ContaCorrente = "12234141";
            empresaConfiguracao.PathCertificado = @"C:\temp\cert\guidihost.pfx";
            empresaConfiguracao.SenhaCertificado = "Guidi@cert";

            return empresaConfiguracao;
        }
    }
}
