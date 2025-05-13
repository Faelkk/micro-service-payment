## 💳 Micro-Service-Payment

O Micro-Service-Payment é um sistema baseado em microserviços desenvolvido em .NET, que simula um fluxo completo de pagamentos. O projeto é composto por três serviços principais:

- Serviço de Produtos

- Serviço de Pedidos

- Serviço de Pagamentos

- API Gateway (usando Ocelot)

Esses serviços são integrados e contêinerizados com Docker, utilizando um banco de dados relacional MySQL para persistência de dados.

## 🧱 Arquitetura

A arquitetura do sistema é baseada em microserviços independentes que se comunicam entre si. Cada serviço é contêinerizado usando Docker, o que permite escalabilidade e facilita o deploy. A arquitetura inclui os seguintes componentes:

- Serviços: Produtos, Pedidos, Pagamentos

- API Gateway: Gerencia as requisições de entrada e roteia para os serviços corretos

- Banco de Dados: MySQL, utilizado para armazenar informações persistentes dos serviços

## 🛠️ Tecnologias Utilizadas

- [.NET](https://dotnet.microsoft.com/pt-br/)
- [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-9.0&WT.mc_id=dotnet-35129-website)
- [SqlServer](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [Ocelot](https://learn.microsoft.com/pt-br/dotnet/architecture/microservices/multi-container-microservice-net-applications/implement-api-gateways-with-ocelot)

🔋 **Controle de versão e deploy**

- [Git](https://git-scm.com)
- [Docker](https://www.docker.com/)

⚙️ **Como Rodar o Projeto**

Para rodar o projeto em seu ambiente local, siga os passos abaixo:

1.  Clonar o Repositório
    Primeiramente, clone o repositório do GitHub para sua máquina local:

    $ git clone https://github.com/Faelkk/micro-service-payment

2.  Instalar as Dependências
    Acesse o diretório do projeto e instale as dependências:

        $ dotnet restore

3.  Configurar o Docker Compose
    Antes de rodar o projeto, configure as variáveis do docker-compose de acordo com as suas necessidades. Certifique-se de que o Docker e o Docker Compose estão instalados corretamente em sua máquina.

4.  Construir o Projeto com Docker
    Crie as imagens do Docker para o projeto:

        $ docker compose build

5.  Subir o Projeto
    Finalmente, suba o projeto utilizando Docker Compose:

        $ docker compose up -d

<br>

**Como me ajudar nesse projeto?**

- Você ira me ajudar muito me seguindo aqui no GitHub
- Dando uma estrela no projeto
- Conectando-se comigo no LinkedIn para fazer parte da minha rede.

<br>

**Feito por**
[Rafael Achtenberg](linkedin.com/in/rafael-achtenberg-7a4b12284/)
