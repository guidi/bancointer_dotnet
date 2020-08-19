# bancointer_api_dotnet
API para comunicação com o banco Inter (Boletos)


Para cadastrar uma API na sua conta, siga o passo a passo abaixo: 

1 - Acesse sua conta pelo Internet Banking; \
2 - Depois selecione o espaço de Conta Digital;\
3 - Em seguida procure por API e clique em Criar Aplicação; \
4 - Depois leia a mensagem na tela e clique em Criar Aplicação;\
5 - Antes de assinar o certificado, será necessário criar um nome e definir um comentário a aplicação, da forma que desejar, pois é apenas a título de controle gerencial sobre as aplicações que assina com o Inter. Em seguida, clique em Continuar;\
6 - Agora para completar a integração será necessário clicar em Assinar Certificado ou Assinar Depois \
7 - Se você optar em "Assinar Depois" o certificado vai ficar pendente de assinatura. Depois basta acessar “Aplicações” (no menu da Conta Digital) e clicar em "Assinar Certificado"; \
8 - Caso você escolha "Assinar Certificado", será necessário incluir uma chave gerada pelo certificado .CSR, em caso de dúvidas você pode clicar na tooltip “?” e obter mais informações quanto ao passo a passo para a autenticação da aplicação;\
Pronto! Com a autenticação, Sua aplicação foi ativada com sucesso. \
IMPORTANTE: \

Assinar certificado é autenticar a operação com a chave gerada por você no arquivo  .CSR.\

O código .CSR (Certificate Signing Request) é um arquivo de texto, que contém as informações da sua solicitação de certificado junto ao Inter, usada para gerar uma chave e assinar digitalmente o certificado.\
