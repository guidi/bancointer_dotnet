using System;

namespace BancoInterDotNet.Application.DTO.Boleto
{
    public class PagadorDTO
    {
        public String cpfCnpj { get; set; }
        public String tipoPessoa { get; set; }
        public String nome { get; set; }
        public String endereco { get; set; }
        public String numero { get; set; }
        public String complemento { get; set; }
        public String bairro { get; set; }
        public String cidade { get; set; }
        public String uf { get; set; }
        public String cep { get; set; }
        public String email { get; set; }
        public String ddd { get; set; }
        public String telefone { get; set; }
    }
}
