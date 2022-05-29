# bancointer_dotnet
Biblioteca em .NET para comunicação com o banco Inter (Boletos e Conta Corrente)


- Cadastre a sua aplicação na sua conta PJ do banco inter e com isso você vai poder fazer o download do certificado.  
- O certificado será composto por dois arquivos, um arquivo .crt e um arquivo .key
- Use o openssl para gerar um arquivo .pfx desses dois arquivos .crt e .key  
- supondo que você tenha os arquivos certificado.crt e certificado.key, execute o comando no CMD ou Bash (se estiver usando linux ou WSL) <b>openssl pkcs12 -export -out certificado.pfx -inkey "certificado.key" -in "certificado.crt"</b>
- Será necessário criar um senha para o certificado, após informar a senha será gerado o arquivo certificado.pfx 
- Abra a solution  e informe seu ClientId, ClientSecret, caminho do certificado e senha do certificado no método  <b>ObterConfiguracao</b> da  classe ConfiguracaoEmpresaService 
