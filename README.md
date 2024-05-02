Projeto LAB-APP

Este projeto é uma aplicação backend (API) chamada LAB-APP, que oferece operações CRUD (Create, Read, Update, Delete) para gerenciar informações sobre escolas. A API foi desenvolvida para fornecer um serviço robusto para criar, ler, atualizar e excluir informações de escolas, permitindo que os usuários executem diversas operações de gerenciamento de dados.



Arquitetura e Tecnologias Utilizadas

Linguagem de Programação: C#
Framework: ASP.NET Core
Banco de Dados: SQLite
Documentação da API: Swagger
Contêinerização: Docker



Estrutura de Arquivos

Controllers: Contém os controladores da API, responsáveis por receber as solicitações HTTP e acionar a lógica de negócios apropriada.
Models: Define os modelos de dados da aplicação, representando entidades como escolas.
Enums: Contém os enums utilizados na aplicação, como o enum de províncias.
Services: Implementa a lógica de negócios da aplicação, separando a camada de controle da camada de acesso a dados.
Repository: Fornece acesso aos dados, interagindo com o banco de dados SQLite.
Migrations: Mantém as migrações do banco de dados, garantindo a consistência entre o modelo de dados e o esquema do banco de dados.




Funcionalidades

CRUD de Escolas: Permite criar, ler, atualizar e excluir informações sobre escolas.
Validação de Provincias: Valida se a província fornecida está dentro do conjunto predefinido de províncias utilizando enums.
Documentação da API: Utiliza o Swagger para documentar e testar os endpoints da API de forma interativa.



Como Executar o Projeto

Clone este repositório em sua máquina local.
Abra o projeto em sua IDE preferida.
Execute o aplicativo.



Documentação da API

Acesse a documentação interativa da API através do Swagger:
