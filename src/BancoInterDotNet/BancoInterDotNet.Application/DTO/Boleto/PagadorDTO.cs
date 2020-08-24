using BancoInterDotNet.Application.Enum;
using System;

namespace BancoInterDotNet.Application.DTO.Boleto
{
    public class PagadorDTO
    {
        public TipoPessoa TipoPessoa { get; set; }
        private String tipoPessoa { get; set; }
        public String nome { get; set; }
        public String endereco { get; set; }
        public String numero { get; set; }
        public String complemento { get; set; }
        public String bairro { get; set; }
        public String cidade { get; set; }
        public String uf { get; set; }
        public String cep { get; set; }
        public String cnpjCpf { get; set; }
        public String email { get; set; }
        public String ddd { get; set; }
        public String telefone { get; set; }
    }
}
