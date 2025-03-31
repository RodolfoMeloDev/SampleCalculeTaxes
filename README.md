# SampleCalculeTaxes
O projeto simula o calculo do imposto de uma compra. O cálculo está preparado com uma FeatureFlag para passar a responder a nova regra da Reforma Tributária

## Executar Aplicação
Há um arquivo docker-compose na raiz do projeto que cria uma imagem do projeto e também do redis.  

A aplicação irá subir como **http://localhost:8080**  
A imagem foi criada com a variavel de ambiente como **Development** para poder visualizar o swagger **http://localhost:8080/swagger/index.html**

Também tem na raiz do projeto um arquivo json para importação no postman

**A aplicação está configurada para cálculo com a formula atual, caso queira mudar para a Reforma Tributária deve atualizar a FeatureFlag já cadastrada, deve mudar seu status para Active=True**

**Caso queira atualizar o imposto de pedidos já criados deve utilizar o endpoint:  http://localhost:8080/api/Order/RecalculateTaxes/:id**

### Banco de Dados
Foi utilizado o banco de dados InMemory para a aplicação. Ao iniciar o projeto o mesmo já realiza a inclusão de 10 Clientes, 10 Produtos e 10 Pedidos

### Logs
Foi utilizado a biblioteca indicada (Serilog), o mesmo está logando em console as informações. Foi definido nível de Log Information para a aplicação.

Os logs possuem um template padrão para as mensages. Exemplo:  
**[INF] - 31-03-2025-16:31:24 - Mensagem de log**

### Testes Unitários
Foi aplicação com foco na camada de Serviço 
