using BancoInterDotNet.Application.DTO.Boleto;
using BancoInterDotNet.Application.DTO.Response;
using BancoInterDotNet.Application.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BancoInterDotNet.Application.Service
{
    public static class BancoInterService
    {
        private const String URL_CRIAR_BOLETO = "https://apis.bancointer.com.br/openbanking/v1/certificado/boletos";
        private const String URL_CONSULTAR_BOLETO_DETALHADO = "https://apis.bancointer.com.br/openbanking/v1/certificado/boletos/";
        private const String URL_BAIXAR_BOLETO = "https://apis.bancointer.com.br/openbanking/v1/certificado/boletos/{0}/baixas";


        private static ConfiguracaoEmpresa ObterConfiguracaoDaEmpresa()
        {
            return ConfiguracaoEmpresaService.ObterConfiguracao();
        }

        private static HttpClientHandler ObterHttpClientHandler()
        {
            var configuracao = ObterConfiguracaoDaEmpresa();
            var httpClientHandler = new HttpClientHandler();
            //Seta os certificado que irá junto com a requisição
            httpClientHandler.ClientCertificates.Add(new X509Certificate2(configuracao.PathCertificado, configuracao.SenhaCertificado));
            //Define que o certificado foi setado de forma manual e não será obtido do store
            httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            httpClientHandler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;

            return httpClientHandler;
        }

        public static async Task<EmissaoBoletoResponseDTO> IncluirBoleto(EmissaoBoletoDTO emissaoBoletoDTO)
        {
            EmissaoBoletoResponseDTO response = new EmissaoBoletoResponseDTO();

            try
            {
                var httpClientHandler = ObterHttpClientHandler();
                var httpClient = new HttpClient(httpClientHandler);

                httpClient.DefaultRequestHeaders.Add("accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("x-inter-conta-corrente", ObterConfiguracaoDaEmpresa().ContaCorrente);

                //TODO: Mudar para IHttpClientFactory 
                var message = await httpClient.PostAsync(URL_CRIAR_BOLETO, new StringContent(JsonConvert.SerializeObject(emissaoBoletoDTO),
                                                        Encoding.UTF8, "application/json"));

                message.EnsureSuccessStatusCode();

                var resultadoStr = await message.Content.ReadAsStringAsync();
                EmissaoBoletoResponseDTO responseDTO = JsonConvert.DeserializeObject<EmissaoBoletoResponseDTO>(resultadoStr);

                return responseDTO;

            }
            catch (Exception ex)
            {
                response.Erro = true;
                response.Mensagem = ex.Message;
            }

            return response;
        }

        public static async Task<ConsultaBoletoDetalhadoResponseDTO> ConsultarBoletoDetalhado(String nossoNumero)
        {
            ConsultaBoletoDetalhadoResponseDTO response = new ConsultaBoletoDetalhadoResponseDTO();

            try
            {
                var httpClientHandler = ObterHttpClientHandler();
                var httpClient = new HttpClient(httpClientHandler);

                httpClient.DefaultRequestHeaders.Add("accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("x-inter-conta-corrente", ObterConfiguracaoDaEmpresa().ContaCorrente);

                //TODO: Mudar para IHttpClientFactory 
                var message = await httpClient.GetAsync(URL_CONSULTAR_BOLETO_DETALHADO + nossoNumero);

                message.EnsureSuccessStatusCode();

                var resultadoStr = await message.Content.ReadAsStringAsync();
                ConsultaBoletoDetalhadoResponseDTO responseDTO = JsonConvert.DeserializeObject<ConsultaBoletoDetalhadoResponseDTO>(resultadoStr);

                return responseDTO;

            }
            catch (Exception ex)
            {
                response.Erro = true;
                response.Mensagem = ex.Message;
            }

            return response;
        }

        public static async Task<RespostaComMensagemDTO> BaixarBoleto(String nossoNumero, String codigoBaixa)
        {
            RespostaComMensagemDTO response = new RespostaComMensagemDTO();

            try
            {
                var httpClientHandler = ObterHttpClientHandler();
                var httpClient = new HttpClient(httpClientHandler);

                httpClient.DefaultRequestHeaders.Add("accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("x-inter-conta-corrente", ObterConfiguracaoDaEmpresa().ContaCorrente);

                var parametros = new Dictionary<String, String>();
                parametros.Add("nossoNumero", nossoNumero);
                parametros.Add("codigoBaixa", codigoBaixa);
                StringContent content = new StringContent(JsonConvert.SerializeObject(parametros), Encoding.UTF8, "application/json");
                //TODO: Mudar para IHttpClientFactory 
                var message = await httpClient.PostAsync(String.Format(URL_BAIXAR_BOLETO, nossoNumero), content); 

                message.EnsureSuccessStatusCode();

                var resultadoStr = await message.Content.ReadAsStringAsync();
                RespostaComMensagemDTO responseDTO = JsonConvert.DeserializeObject<RespostaComMensagemDTO>(resultadoStr);

                return responseDTO;

            }
            catch (Exception ex)
            {
                response.Erro = true;
                response.Mensagem = ex.Message;
            }

            return response;
        }
    }
}
