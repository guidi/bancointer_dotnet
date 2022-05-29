using BancoInterDotNet.Application.DTO;
using BancoInterDotNet.Application.DTO.Boleto;
using BancoInterDotNet.Application.DTO.ContaCorrente;
using BancoInterDotNet.Application.DTO.Response;
using BancoInterDotNet.Application.Model;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BancoInterDotNet.Application.Service
{
    public static class BancoInterService
    {
        private const String URL_BASE_COBRANCA = "https://cdpj.partners.bancointer.com.br/cobranca/v2/";

        private const String URL_CONSULTAR_EXTRATO = "https://cdpj.partners.bancointer.com.br/banking/v2/extrato";
        private const String URL_EXTRATO_EM_PDF = "https://cdpj.partners.bancointer.com.br/banking/v2/extrato/exportar";
        private const String URL_CONSULTAR_SALDO = "https://cdpj.partners.bancointer.com.br/banking/v2/saldo";

        private const String URL_TOKEN = "https://cdpj.partners.bancointer.com.br/oauth/v2/token";

        private const String ESCOPO_CRIAR_BOLETO = "boleto-cobranca.write";
        private const String ESCOPO_CONSULTAR_BOLETO = "boleto-cobranca.read";
        private const String ESCOPO_CANCELAR_BOLETO = "boleto-cobranca.write";
        private const String ESCOPO_PDF_BOLETO = "boleto-cobranca.read";
        private const String ESCOPO_CONSULTAR_EXTRATO = "extrato.read";
        private const String ESCOPO_CONSULTAR_SALDO = "extrato.read";

        private static ConfiguracaoEmpresa ObterConfiguracaoDaEmpresa()
        {
            return ConfiguracaoEmpresaService.ObterConfiguracao();
        }

        private static HttpClientHandler ObterHttpClientHandler()
        {
            var configuracao = ObterConfiguracaoDaEmpresa();
            var httpClientHandler = new HttpClientHandler();
            //Seta o certificado que irá junto com a requisição
            httpClientHandler.ClientCertificates.Add(new X509Certificate2(configuracao.PathCertificado, configuracao.SenhaCertificado));
            //Define que o certificado foi setado de forma manual e não será obtido do store
            httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            httpClientHandler.SslProtocols = SslProtocols.Tls12;

            return httpClientHandler;
        }

        private static async Task<DefaultTokenDTO> ObterToken(String Escopo)
        {
            DefaultTokenDTO defaultToken = null;
            try
            {
                var conteudoKVP = new List<KeyValuePair<String, String>>
                {
                    new KeyValuePair<String, String>("client_id", ObterConfiguracaoDaEmpresa().ClientId),
                    new KeyValuePair<String, String>("client_secret", ObterConfiguracaoDaEmpresa().ClientSecret),
                    new KeyValuePair<String, String>("grant_type", "client_credentials"),
                    new KeyValuePair<String, String>("scope", Escopo),
                };

                var conteudo = new FormUrlEncodedContent(conteudoKVP);
                var handler = ObterHttpClientHandler();

                using (var httpClient = new HttpClient(handler))
                {
                    var respostaRequisicao = await httpClient.PostAsync(URL_TOKEN, conteudo);

                    if (respostaRequisicao.IsSuccessStatusCode)
                    {
                        var responseString = await respostaRequisicao.Content.ReadAsStringAsync();
                        DefaultTokenDTO obj = JsonConvert.DeserializeObject<DefaultTokenDTO>(responseString);

                        defaultToken = new DefaultTokenDTO();
                        defaultToken.access_token = obj.access_token;
                        defaultToken.token_type = obj.token_type;
                        defaultToken.refresh_token = obj.refresh_token;
                    }
                }
            }
            catch (Exception ex)
            {
                defaultToken = new DefaultTokenDTO();
                defaultToken.TemErro = true;
                defaultToken.Mensagem = ex.Message;
            }

            return defaultToken;
        }

        public static async Task<RegistroBoletoResponseDTO> RegistrarBoleto(RegistroBoletoDTO emissaoBoletoDTO)
        {
            RegistroBoletoResponseDTO response = new RegistroBoletoResponseDTO();

            try
            {
                var objToken = await ObterToken(ESCOPO_CRIAR_BOLETO);

                if (objToken.TemErro)
                {
                    response.TemErro = true;
                    response.Mensagem = objToken.Mensagem;
                    return response;
                }

                var handler = ObterHttpClientHandler();
                //TODO: Mudar para IHttpClientFactory 
                var httpClient = new HttpClient(handler);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objToken.access_token);

                var boletoSerializado = JsonConvert.SerializeObject(emissaoBoletoDTO);
                var conteudo = new StringContent(boletoSerializado, Encoding.UTF8, "application/json");
                var url = String.Concat(URL_BASE_COBRANCA, "boletos");
                var message = await httpClient.PostAsync(url, conteudo);
                var resultadoStr = await message.Content.ReadAsStringAsync();
                message.EnsureSuccessStatusCode();
                response = JsonConvert.DeserializeObject<RegistroBoletoResponseDTO>(resultadoStr);

            }
            catch (Exception ex)
            {
                response.TemErro = true;
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
                //TODO: Mudar para IHttpClientFactory 
                var httpClient = new HttpClient(httpClientHandler);
                var objToken = await ObterToken(ESCOPO_CONSULTAR_BOLETO);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objToken.access_token);
                var url = URL_BASE_COBRANCA + "boletos/" + nossoNumero;
                var message = await httpClient.GetAsync(url);

                var resultadoStr = await message.Content.ReadAsStringAsync();
                message.EnsureSuccessStatusCode();
                response = JsonConvert.DeserializeObject<ConsultaBoletoDetalhadoResponseDTO>(resultadoStr);

            }
            catch (Exception ex)
            {
                response.TemErro = true;
                response.Mensagem = ex.Message;
            }

            return response;
        }

        public static async Task<ConsultaBoletoEmLoteResponseDTO> ConsultarBoletoBancarioEmLote(String dataInicial, String dataFinal)
        {
            ConsultaBoletoEmLoteResponseDTO response = new ConsultaBoletoEmLoteResponseDTO();

            try
            {
                var httpClientHandler = ObterHttpClientHandler();
                //TODO: Mudar para IHttpClientFactory 
                var httpClient = new HttpClient(httpClientHandler);
                var objToken = await ObterToken(ESCOPO_CONSULTAR_BOLETO);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objToken.access_token);
                var url = URL_BASE_COBRANCA + "boletos";

                var parametros = new Dictionary<String, String>
                {
                    { "dataInicial", dataInicial},
                    { "dataFinal", dataFinal }
                };

                url = QueryHelpers.AddQueryString(url, parametros);

                var message = await httpClient.GetAsync(url);
                var resultadoStr = await message.Content.ReadAsStringAsync();
                message.EnsureSuccessStatusCode();

                response = JsonConvert.DeserializeObject<ConsultaBoletoEmLoteResponseDTO>(resultadoStr);

            }
            catch (Exception ex)
            {
                response.TemErro = true;
                response.Mensagem = ex.Message;
            }

            return response;
        }

        public static async Task<RespostaComMensagemDTO> BaixarBoleto(String nossoNumero, String motivoBaixa)
        {
            RespostaComMensagemDTO response = new RespostaComMensagemDTO();

            try
            {
                var objToken = await ObterToken(ESCOPO_CANCELAR_BOLETO);
                var httpClientHandler = ObterHttpClientHandler();
                var httpClient = new HttpClient(httpClientHandler);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objToken.access_token);

                var parametros = new Dictionary<String, String>();
                parametros.Add("motivoCancelamento", motivoBaixa);
                StringContent content = new StringContent(JsonConvert.SerializeObject(parametros), Encoding.UTF8, "application/json");
                var url = String.Format(String.Concat(URL_BASE_COBRANCA, "boletos/{0}/cancelar"), nossoNumero);
                var message = await httpClient.PostAsync(url, content);

                message.EnsureSuccessStatusCode();

                var resultadoStr = await message.Content.ReadAsStringAsync();
                response.TemErro = false;
                return response;

            }
            catch (Exception ex)
            {
                response.TemErro = true;
                response.Mensagem = ex.Message;
            }

            return response;
        }

        public static async Task<RespostaComPDFDTO> ObterPDFBoleto(String nossoNumero)
        {
            RespostaComPDFDTO response = new RespostaComPDFDTO();

            try
            {
                var httpClientHandler = ObterHttpClientHandler();
                //TODO: Mudar para IHttpClientFactory 
                var httpClient = new HttpClient(httpClientHandler);
                var objToken = await ObterToken(ESCOPO_PDF_BOLETO);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objToken.access_token);
                var url = String.Concat(URL_BASE_COBRANCA, String.Format("boletos/{0}/pdf", nossoNumero));
                var message = await httpClient.GetAsync(url);
                var resultadoStr = await message.Content.ReadAsStringAsync();
                message.EnsureSuccessStatusCode();

                var resposta = JsonConvert.DeserializeObject<RespostaComPDFDTO>(resultadoStr);
                response.TemErro = false;
                response.pdf = resposta.pdf;

            }
            catch (Exception ex)
            {
                response.TemErro = true;
                response.Mensagem = ex.Message;
            }

            return response;
        }

        public static async Task<ExtratoResponseDTO> ConsultarExtrato(String dataInicial, String dataFinal)
        {
            ExtratoResponseDTO response = new ExtratoResponseDTO();

            try
            {
                var httpClientHandler = ObterHttpClientHandler();
                //TODO: Mudar para IHttpClientFactory 
                var httpClient = new HttpClient(httpClientHandler);
                var objToken = await ObterToken(ESCOPO_CONSULTAR_EXTRATO);

                if (objToken.TemErro)
                {
                    response.TemErro = true;
                    response.Mensagem = objToken.Mensagem;
                    return response;
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objToken.access_token);
                var parametros = new Dictionary<String, String>
                {
                    { "dataInicio", dataInicial},
                    { "dataFim", dataFinal }
                };

                var url = URL_CONSULTAR_EXTRATO;
                url = QueryHelpers.AddQueryString(url, parametros);

                var message = await httpClient.GetAsync(url);
                var resultadoStr = await message.Content.ReadAsStringAsync();
                message.EnsureSuccessStatusCode();

                response = JsonConvert.DeserializeObject<ExtratoResponseDTO>(resultadoStr);

            }
            catch (Exception ex)
            {
                response.TemErro = true;
                response.Mensagem = ex.Message;
            }

            return response;
        }

        public static async Task<RespostaComPDFDTO> ConsultarExtratoPDF(string dataInicial, string dataFinal)
        {
            RespostaComPDFDTO response = new RespostaComPDFDTO();

            try
            {
                var httpClientHandler = ObterHttpClientHandler();
                //TODO: Mudar para IHttpClientFactory 
                var httpClient = new HttpClient(httpClientHandler);
                var objToken = await ObterToken(ESCOPO_CONSULTAR_EXTRATO);

                if (objToken.TemErro)
                {
                    response.TemErro = true;
                    response.Mensagem = objToken.Mensagem;
                    return response;
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objToken.access_token);
                var parametros = new Dictionary<String, String>
                {
                    { "dataInicio", dataInicial},
                    { "dataFim", dataFinal }
                };

                var url = URL_EXTRATO_EM_PDF;
                url = QueryHelpers.AddQueryString(url, parametros);

                var message = await httpClient.GetAsync(url);
                var resultadoStr = await message.Content.ReadAsStringAsync();
                message.EnsureSuccessStatusCode();

                response = JsonConvert.DeserializeObject<RespostaComPDFDTO>(resultadoStr);
                response.TemErro = false;
                response.pdf = response.pdf;

            }
            catch (Exception ex)
            {
                response.TemErro = true;
                response.Mensagem = ex.Message;
            }

            return response;
        }

        public static async Task<SaldoResponseDTO> ConsultarSaldo()
        {
            SaldoResponseDTO response = new SaldoResponseDTO();

            try
            {
                var httpClientHandler = ObterHttpClientHandler();
                var httpClient = new HttpClient(httpClientHandler);
                var objToken = await ObterToken(ESCOPO_CONSULTAR_SALDO);

                if (objToken.TemErro)
                {
                    response.TemErro = true;
                    response.Mensagem = objToken.Mensagem;
                    return response;
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objToken.access_token);

                var message = await httpClient.GetAsync(URL_CONSULTAR_SALDO);
                var resultadoStr = await message.Content.ReadAsStringAsync();
                message.EnsureSuccessStatusCode();

                response = JsonConvert.DeserializeObject<SaldoResponseDTO>(resultadoStr);

            }
            catch (Exception ex)
            {
                response.TemErro = true;
                response.Mensagem = ex.Message;
            }

            return response;
        }
    }
}
