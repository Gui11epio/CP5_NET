# Sprint 3

📌 Descrição do Projeto

Este projeto consiste em uma API RESTful desenvolvida com ASP.NET Core, tenta representar uma solução de monitoramento de motos por meio de Rfid, com objetivo de gerenciar entidades como Motos, Filiais e Pátios.

Também tem usuários, mas não está com as funcionalidades completas. 

A aplicação implementa operações básicas de CRUD (Create, Read, Update, Delete), segue uma arquitetura em camadas (Controllers, Application, Domain, Infrastructure), segue os príncipios de DDD e Clean Code.

A TagRfid é criada automaticamente quando uma moto é cadastrada.

## 👥 Nome e RM dos Integrantes

- Guilherme Camasmie Laiber de Jesus – RM554894

- Fernando Fernandes Prado – RM557982

- Pedro Manzo Yokoo – RM556115

## 🚀 Rotas Disponíveis

### 📍 MotoController - `/api/Moto`
- `GET /api/Moto`  
  Retorna todas as motos cadastradas.

- `GET /api/Moto/placa`  
  Retorna uma moto específica pela placa.

- `GET /api/Moto/pagina`  
  Retorna motos por meio de páginas.

- `POST /api/Moto`  
  Cria uma nova moto. Requer um corpo com os dados da moto.

- `PUT /api/Moto/placa`  
  Atualiza os dados de uma moto pela placa.

- `DELETE /api/Moto/placa`  
  Deleta os dados de uma moto pela placa.

> Os outros controllers (`FilialController`, `PatioController` e `UsuarioController`) seguem estrutura semelhante com operações básicas de CRUD.

## 🛠️ Tecnologias Utilizadas

- [.NET 6 / ASP.NET Core](https://dotnet.microsoft.com/)
- C#
- Entity Framework Core
- Swagger (OpenAPI) para documentação
- Visual Studio 2022
- Oracle DataBase
- AutoMapper
- Migrations
- DataAnnotations

## ▶️ Instruções de Execução

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/Gui11epio/CP_C--MottuFind.git
   

2. **Vá até "lauchSettings.json"**
   
   <img width="412" height="167" alt="image" src="https://github.com/user-attachments/assets/5f3c5fa2-cff7-4fa2-9300-9a0e745c5a24" />
   
- Nota: Clique com o botão direito em cima de **MottuFind_C_.API** e defina ele como projeto de inicialização, se ainda não estiver 


3. **Coloque suas informações do Banco de Dados Oracle**

   <img width="995" height="251" alt="image" src="https://github.com/user-attachments/assets/3815d7d0-6038-48f9-84e4-5b16fc378e18" />


4. **Abra a terminal na raiz do projeto e coloque as mesmas informações do Oracle**
   ```bash
   $env:DEFAULT_CONNECTION = "User Id=xxxxxxx;Password=xxxxxx;Data Source=xxxxxxxxxxxx:1521/ORCL"

5. **Ainda na terminal, rode este comando para criar as tabelas em seu banco de dados:**

   - Para criar uma migration
   ```bash
   dotnet ef migrations add ClassesNovas  --project .\MottuFind_C_.Infrastructure\MottuFind_C_.Infrastructure.csproj  --startup-project .\MottuFind\MottuFind_C_.API.csproj  --context AppDbContext
   ```
   - Para poder criar as tabelas
   ```bash
   dotnet ef database update --project .\MottuFind_C_.Infrastructure\MottuFind_C_.Infrastructure.csproj --startup-project .\MottuFind\MottuFind_C_.API.csproj --context AppDbContext
   ```

7. **Após tudo isso, rode o programa e o Swagger abrirá sozinho**
   ```bash
   https://localhost:7117/swagger


## 📬JSON de Teste

- Filial:
  
```bash
{
  "cidade": "São Paulo",
  "pais": "Brasil"
}
```

#

- Pátio:
  
```bash
{
  "nome": "Pátio A1",
  "filialId": 1
}
```

#

- Moto
  
```bash
{
  "placa": "ABC1D23",
  "modelo": "POP",
  "marca": "Yamaha",
  "status": "MANUTENCAO",
  "patioId": 1
}
```
🔤 A placa da Moto deve ser única, não deve repetir

🔤 Modelo e Status devem conter valores válidos dos enums MotoModelo e MotoStatus, como:

- MotoModelo: "POP", "SPORT", "ELETRICA"
  
- MotoStatus: "LIGADO", "DESLIGADO", "MANUTENCAO", "DISPONIVEL"

#

- Usuário
```bash
{
  "setores": "MECANICA",
  "nomeUsuario": "Roberto",
  "email": "roberto@gmail.com",
  "senha": "rob123@!"
}
```
🔤 Setores deve conter:

- Setores: "MECANICA" ou "GARAGEM"



  



   
