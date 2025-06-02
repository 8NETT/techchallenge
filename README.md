# FIAP Cloud Games (FCG)

# Introdução

O **FIAP Cloud Games (FCG)** é uma API REST desenvolvida como resposta ao Tech Challenge da FIAP. O objetivo do projeto é implementar um plataforma de venda de jogos digitais e gestão de servidores para partidas online. Este README apresenta o escopo da fase 1 do desafio, descrevendo o propósito, tecnologias, arquitetura, funcionalidades, endpoints, instruções de uso e testes

## Objetivo do Sistema

O sistema FCG disponibiliza uma API REST em **.NET 8** para permitir que usuários se cadastrem, autentiquem e acessem sua biblioteca de jogos, enquanto administradores podem gerenciar usuários, jogos e promoções. A aplicação focará em:

- **Cadastro de usuários** com validação de e-mail e requisitos de senha forte.
- **Autenticação via JWT (JSON Web Token)**, garantindo segurança nas requisições subsequentes.
- **Gestão de jogos** (CRUD) e **promoções** disponível apenas para administradores.
- **Acesso diferenciado** entre usuários comuns (acesso à própria biblioteca de jogos) e administradores (poder de gerência completo).

![image](https://github.com/user-attachments/assets/1da905d9-936e-4555-8951-ef1efcd6b55e)

## Tecnologias Utilizadas

- **.NET 8**: Plataforma da Microsoft para desenvolvimento de aplicações web e APIs (LTS).
- **Entity Framework Core**: ORM para acesso a dados em .NET, facilitando mapeamento objeto-relacional e migrações de esquema.
- **JWT (JSON Web Token)**: Padrão aberto para emissão de tokens de autenticação, permitindo segurança stateless na API.
- **Swagger / OpenAPI**: Ferramentas para documentação e teste interativo da API REST, descrevendo endpoints e operações disponíveis.
- **xUnit**: Framework open source de testes unitários para .NET.
- **Moq**: Biblioteca popular de *mocking* para .NET, usada em testes para simular dependências.

## Estrutura do Projeto

O projeto adota uma **arquitetura monolítica**, centralizando todos os componentes em uma única aplicação executável. A estrutura segue uma abordagem baseada em **camadas**, com separação clara entre as responsabilidades: aplicação, domínio, infraestrutura e interface (API). Essa abordagem facilita a organização do código, promove a reutilização de componentes e permite testes mais isolados e eficientes.

## Funcionalidades Principais

- **Cadastro de Usuário**: permite criar conta nova com validação de e-mail (formato válido) e senha forte (critérios mínimos de segurança).
- **Autenticação JWT**: usuário cadastrado pode fazer login e receber um token JWT para autorizar acessos subsequentes.
- **Níveis de Acesso**: define *usuário* comum e *administrador*. Usuários comuns têm acesso à sua biblioteca de jogos; administradores podem gerenciar usuários, jogos e promoções.
- **Gerenciamento de Jogos (CRUD)**: administradores podem cadastrar, consultar, alterar e remover jogos do sistema.
- **Gerenciamento de Promoções**: administradores podem criar e editar promoções vinculadas a jogos.
- **Biblioteca do Usuário**: cada usuário autenticado pode visualizar e gerenciar sua própria coleção de jogos.

## Endpoints Disponíveis

A API expõe os seguintes endpoints principais:

- **POST `/api/Account/login`**: Autentica credenciais (e-mail/senha) e retorna o JWT para acesso às demais rotas.
- **GET `/api/Account/biblioteca`**: Retorna a lista de jogos da biblioteca do usuário logado (requer token válido).
- **GET `/Jogo`**: Lista todos os jogos cadastrados.
- **GET `/Jogo/{id}`**: Obtém detalhes de um jogo pelo seu ID.
- **POST `/Jogo`**: Cadastra um novo jogo (admin).
- **PUT `/Jogo/{id}`**: Atualiza dados de um jogo existente (admin).
- **DELETE `/Jogo/{id}`**: Remove um jogo do sistema (admin).
- **GET `/Usuario`**: Lista todos os usuários cadastrados (admin).
- **GET `/Usuario/{id}`**: Obtém detalhes de um usuário pelo ID (admin).
- **POST `/Usuario`**: Cadastra um novo usuário.
- **PUT `/Usuario/{id}`**: Atualiza dados de um usuário (admin).
- **DELETE `/Usuario/{id}`**: Exclui um usuário do sistema (admin).

## Instruções de Uso

### Pré-requisitos

- .NET 8 SDK instalado na máquina (ver [Microsoft .NET](https://dotnet.microsoft.com/) para download).
- SQL Server ou outro servidor de banco de dados compatível (configurável via *ConnectionString*).
- (Opcional) Git para clonar o repositório.

### Execução Local

Para executar o projeto localmente, siga estes passos:

1. Clone o repositório e acesse a pasta do projeto:
    
    ```bash
    https://github.com/8NETT/techchallenge.git
    cd techchallenge
    
    ```
    
2. Restaure as dependências e construa o projeto:
    
    ```bash
    dotnet restore
    dotnet build
    
    ```
    
3. Crie e aplique as migrations no banco de dados (exemplo usando SQL Server):
    
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    
    ```
    
4. Inicie a aplicação:
    
    ```bash
    dotnet run
    
    ```
    
    A API estará disponível em `https://localhost:7241`
    

### Autenticação e Swagger

- Para autenticar, use o endpoint **`POST /api/Account/login`** informando e-mail/senha cadastrados. O servidor retornará um JWT (token) de autenticação.
- Na interface do **Swagger UI** (`https://localhost:7241/swagger`), clique em **“Authorize”** e cole o token JWT (sem incluir a palavra `Bearer`) no campo de autorização. Conforme especificação OpenAPI, após este passo todas as requisições subsequentes incluirão automaticamente o cabeçalho `Authorization: Bearer <token>`.
- Em seguida, você pode testar todos os endpoints diretamente pelo Swagger: selecione um endpoint, clique em “Try it out” e veja as respostas da API.

## Testes

O projeto inclui testes automatizados para garantir a qualidade do código:

- **Framework:** xUnit foi utilizado para criar testes unitários em C#.
- **Mocking:** A biblioteca Moq foi empregada para simular dependências (por exemplo, repositórios) durante os testes.
- **Cobertura:** Foram implementados testes para operações CRUD e para os controllers de Usuário, assegurando cenários de sucesso e falha (e-mails inválidos, dados faltando etc.).
    
    Para executar os testes, rode:
    

```bash
dotnet test

```

## Documentação

- **Swagger / OpenAPI:** Toda a API está documentada em Swagger, disponível em `/swagger`. A especificação OpenAPI lista os endpoints, modelos de dados e métodos suportados, permitindo explorar e testar cada rota interativamente.
- **Documentação de Domínio (DDD):** Mapas de contexto, fluxos de casos de uso e diagramas de entidade-relacionamento foram elaborados no Miro. Eles estão disponíveis no repositório (pasta `docs/`) ou podem ser acessados via link compartilhado nos materiais do projeto.

## Créditos

- Emerson Coutinho
- Rodrigo Zanetta Slaski Suchorzewski
- Rayan Pereira de Lima
- Gabriel Costa Marques
- Washington De Almeida Lopes

Cada integrante contribuiu para o desenvolvimento da aplicação, seguindo as especificações da fase 1 do Tech Challenge.
