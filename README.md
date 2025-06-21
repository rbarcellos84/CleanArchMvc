Clean Architecture com ASP.NET Core: Um Guia Prático
🚀 Visão Geral do Projeto
Este projeto é uma demonstração abrangente de como implementar uma aplicação robusta utilizando ASP.NET Core e seguindo os princípios da Clean Architecture. O principal objetivo é fornecer um modelo prático e detalhado para construir sistemas que sejam modulares, escaláveis, testáveis e fáceis de manter, garantindo que a lógica de negócio principal seja independente de frameworks, bancos de dados ou interfaces de usuário.

Iniciamos com uma abordagem ASP.NET Core MVC e evoluímos para uma API RESTful, demonstrando a flexibilidade e a resiliência da Clean Architecture em se adaptar a diferentes cenários de apresentação.

✨ Tecnologias e Conceitos Desenvolvidos
Este projeto explora e aplica as seguintes tecnologias e conceitos fundamentais:

ASP.NET Core (.NET 8): A plataforma de desenvolvimento mais recente da Microsoft, escolhida por sua alta performance, compatibilidade multiplataforma e ecossistema rico para construção de aplicações web modernas.
ASP.NET Core MVC: Utilizado para a interface inicial do usuário, demonstrando a construção de aplicações web com o padrão Model-View-Controller.
ASP.NET Core Web API: A evolução natural da camada de apresentação, focando na construção de serviços RESTful para consumo por diferentes clientes (SPAs, mobile, outros serviços).
Clean Architecture: Um padrão de arquitetura focado na separação de preocupações e na independência do domínio de negócio em relação a detalhes técnicos externos. Seus pilares são:
Separação de Preocupações: O código é organizado em camadas concêntricas, onde as camadas internas contêm as regras de negócio e as camadas externas lidam com detalhes de implementação.
Inversão de Dependência (DIP): Dependências fluem para o centro. As camadas internas nunca dependem das externas. Interfaces são definidas nas camadas internas e implementadas nas externas.
Testabilidade: O isolamento das regras de negócio permite testar cada componente de forma unitária e eficiente.
Entity Framework Core: Um ORM (Object-Relational Mapper) robusto para .NET, utilizado para interagir com o banco de dados de forma orientada a objetos. Ele abstrai complexidades do SQL, facilitando operações de CRUD e gerenciamento de esquemas (via Migrações).
C#: A linguagem de programação principal, aproveitando seus recursos modernos e tipagem forte.
Injeção de Dependência (DI): Um princípio fundamental que permite o desacoplamento de componentes, facilitando a testabilidade e a manutenção do código. Utilizamos o container de DI integrado do .NET Core.
JSON Web Tokens (JWT): Implementado para segurança da API, proporcionando um mecanismo padrão e seguro para autenticação e autorização de requisições.
Domain-Driven Design (DDD): Foco na modelagem de um domínio rico, com entidades, objetos de valor e agregados que representam o negócio de forma precisa e encapsulam suas regras.
Padrões de Projeto: Aplicação de padrões como Repositório e Unit of Work para abstrair o acesso a dados e gerenciar transações, além de Services/Handlers para a lógica da camada de aplicação.
CQRS (Command Query Responsibility Segregation) - (Mencione se você implementou): Separação das operações de leitura (Queries) e escrita (Commands) para otimizar desempenho e escalabilidade. MediatR é comumente usado aqui para orquestração.
📐 A Arquitetura do Projeto em Detalhes
A estrutura do projeto segue rigorosamente os princípios da Clean Architecture, organizada em camadas que garantem o baixo acoplamento e a alta coesão.

Este diagrama ilustra as quatro camadas principais do projeto e como as dependências fluem de fora para dentro, garantindo que o domínio de negócio seja o núcleo e independente de detalhes de infraestrutura ou interface.

Detalhamento das Camadas:

1. CleanArchMvc.Domain (A Camada Mais Interna - O Coração do Negócio)
Propósito: Contém as regras de negócio essenciais, entidades do domínio, objetos de valor, agregados, e interfaces de repositório. Esta camada é completamente independente de qualquer framework ou tecnologia externa.
Implementação:
Entidades: Classes que representam os conceitos de negócio (e.g., Product, Category). Elas possuem identidade e comportamentos de domínio.
Interfaces de Repositório: Definições contratuais de como os dados do domínio podem ser acessados e persistidos (e.g., IProductRepository, ICategoryRepository). As implementações dessas interfaces estão na camada de Infraestrutura.
Eventos de Domínio (Opcional): Representam algo significativo que aconteceu no domínio.
Exceções de Domínio: Exceções específicas que representam violações de regras de negócio.
Vantagens: Garante que a lógica de negócio seja reusável e possa ser utilizada em diferentes tipos de aplicações (web, desktop, mobile, etc.) sem modificações. É a parte mais estável do sistema.

2. CleanArchMvc.Application (A Camada de Orquestração)
Propósito: Orquestra a lógica de negócio para casos de uso específicos da aplicação. Contém DTOs (Data Transfer Objects), interfaces de serviços de aplicação e, se aplicável, handlers de comandos/queries (CQRS).
Implementação:
Serviços de Aplicação/Handlers: Classes que recebem requisições de usuários (diretamente ou via um framework de mensageria como MediatR), orquestram as entidades de domínio, interagem com os repositórios (definidos na camada de domínio e implementados na infraestrutura), e retornam DTOs para a camada de apresentação.
DTOs: Objetos simples que transferem dados entre as camadas da aplicação, desacoplando o modelo de domínio do modelo de apresentação.
Interfaces de Serviços Externos: Se a aplicação precisar interagir com serviços externos, suas interfaces podem ser definidas aqui e implementadas na camada de infraestrutura.
Vantagens: Encapsula os casos de uso da aplicação, mantendo a camada de Domínio focada apenas nas regras de negócio e a camada de Apresentação limpa de lógica complexa.

3. CleanArchMvc.Infra.Data (A Camada de Infraestrutura de Dados)
Propósito: Responsável pelos detalhes de persistência de dados. Contém as implementações concretas das interfaces de repositório definidas na camada de Domínio.
Implementação:
DbContext do Entity Framework Core: A classe principal para interagir com o banco de dados.
Configurações de Entidades: Mapeamento das entidades de domínio para o esquema do banco de dados.
Migrações: Gerenciamento do esquema do banco de dados através do EF Core Migrations.
Implementações de Repositório: Classes que implementam as interfaces de repositório definidas no Domínio, usando o DbContext para realizar operações de CRUD.
Vantagens: Isola a lógica de acesso a dados do resto da aplicação, permitindo que o tipo de banco de dados (SQL Server, PostgreSQL, MySQL) e a tecnologia de acesso a dados (EF Core, Dapper) sejam alterados sem afetar as camadas superiores.

4. CleanArchMvc.Infra.IoC (Configuração de Injeção de Dependência)
Propósito: Este projeto contém as configurações de Injeção de Dependência para registrar todas as interfaces e suas implementações concretas, bem como os serviços do Entity Framework Core. Embora não seja uma camada de "arquitetura" no sentido puro, é um ponto central para configurar o container de DI.
Implementação: Métodos de extensão para IServiceCollection que configuram o Entity Framework Core e mapeiam interfaces para suas implementações concretas.
Vantagens: Centraliza a configuração de dependências, tornando o processo de inicialização da aplicação claro e permitindo que as camadas dependam de abstrações, não de implementações concretas.

5. CleanArchMvc.WebUI (A Camada de Apresentação)
Propósito: É a porta de entrada da aplicação, responsável por receber as requisições do usuário e apresentar os resultados. É a camada mais externa e depende de todas as outras.
Implementação:
Controladores ASP.NET Core MVC: Recebem requisições HTTP e as encaminham para os serviços da camada de Aplicação. Utilizam DTOs para comunicação.
Views Razor: Para renderização da interface do usuário no padrão MVC.
Controladores ASP.NET Core Web API: Endpoints RESTful que expõem os casos de uso da aplicação para consumo externo (e.g., por um frontend React/Angular, aplicações móveis).
Middleware de Autenticação/Autorização: Configuração do JWT para proteger os endpoints da API.
Mapeamentos (Ex: AutoMapper): Mapeamento de DTOs para entidades de domínio e vice-versa, quando necessário, para desacoplar as camadas.
Vantagens: O ideal é que esta camada seja "burra", contendo o mínimo de lógica possível, focando apenas na apresentação e na delegação de responsabilidades. Isso a torna facilmente substituível (e.g., de MVC para uma API pura + SPA).

🔒 Segurança com JWT
Para garantir a segurança dos endpoints da API, este projeto implementa autenticação e autorização via JSON Web Tokens (JWT).

Autenticação: Usuários enviam credenciais (username/password) para um endpoint de login. Após validação, um JWT é gerado e retornado ao cliente.
Autorização: Em requisições subsequentes para endpoints protegidos, o cliente envia o JWT no cabeçalho Authorization (formato Bearer token). O servidor valida o token (assinatura, expiração) e extrai as informações do usuário e suas permissões para autorizar o acesso ao recurso.
Configuração no ASP.NET Core: Utiliza os pacotes Microsoft.AspNetCore.Authentication.JwtBearer para configurar e processar os tokens JWT de forma eficiente.

🛠️ Como Executar o Projeto
Siga estes passos para configurar e executar o projeto em sua máquina local:

Pré-requisitos
.NET 8 SDK
Visual Studio (2022 ou superior) ou Visual Studio Code com as extensões C# e .NET
SQL Server (ou outro banco de dados compatível com EF Core, como SQL Server LocalDB ou SQLite para desenvolvimento/testes).
Configuração
Clone o Repositório:

Bash

git clone https://github.com/rbarcellos84/CleanArchMvc.git
cd CleanArchMvc
Configurar o Banco de Dados:

Abra o arquivo appsettings.json na pasta CleanArchMvc.WebUI.
Atualize a DefaultConnection string de conexão para apontar para o seu SQL Server.
<!-- end list -->

JSON

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CleanArchMvcDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
Aplicar Migrações do Entity Framework Core:
Abra o Console do Gerenciador de Pacotes (Package Manager Console) no Visual Studio (selecionando CleanArchMvc.Infra.Data como projeto padrão) ou um terminal na raiz do projeto (CleanArchMvc.sln) e execute os seguintes comandos:

Bash

dotnet ef database update --project CleanArchMvc.Infra.Data --startup-project CleanArchMvc.WebUI
Isso criará ou atualizará o banco de dados conforme o modelo definido pelo Entity Framework Core na camada de infraestrutura.

Execução
Via Visual Studio:

Abra a solução CleanArchMvc.sln.
Defina CleanArchMvc.WebUI como projeto de inicialização (StartUp Project).
Pressione F5 ou o botão "Start" para executar a aplicação.
Via Linha de Comando:
Navegue até a pasta CleanArchMvc.WebUI no terminal e execute:

Bash

dotnet run
A aplicação estará disponível em https://localhost:XXXX (a porta será exibida no terminal).

🚀 Funcionalidades Demonstradas
CRUD Básico de Entidades: Exemplo de como realizar operações de criação, leitura, atualização e exclusão para entidades como Produtos e Categorias.
Autenticação e Autorização: Implementação de JWT para proteger as rotas da API.
Exemplo de API RESTful: Demonstração de endpoints para consumo por aplicações cliente.
(Adicione aqui outras funcionalidades específicas do seu projeto, se houver.)

🤝 Contribuições
Contribuições são muito bem-vindas! Se você tiver sugestões, encontrar bugs ou quiser adicionar novas funcionalidades, sinta-se à vontade para:

Abrir uma Issue descrevendo o problema ou a sugestão.
Criar um Pull Request com suas alterações.

📄 Licença
Este projeto está licenciado sob a Licença MIT. Veja o arquivo LICENSE para mais detalhes.

✉️ Contato
Se você tiver alguma dúvida sobre o projeto ou a implementação da Clean Architecture, sinta-se à vontade para entrar em contato:

Seu Nome/GitHub: rbarcellos84
Email (Opcional): rbarcellos84@gmail.com

