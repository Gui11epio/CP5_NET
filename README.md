# CP 05

### 👥 Nome e RM dos Integrantes

- Guilherme Camasmie Laiber de Jesus – RM554894

- Fernando Fernandes Prado – RM557982

- Pedro Manzo Yokoo – RM556115

### 📌 Descrição do Projeto

Este projeto consiste em uma API RESTful desenvolvida com ASP.NET Core, tenta representar uma solução de monitoramento de motos por meio de Rfid, com objetivo de gerenciar entidades como Motos, Filiais, Pátios, Usuários, LeitorRFID e LeituraRFID. 

A TagRfid é criada automaticamente quando uma moto é cadastrada.

### 📌 Arquitetura do Projeto

A aplicação implementa operações básicas de CRUD (Create, Read, Update, Delete), segue uma arquitetura em camadas (Controllers, Application, Domain, Infrastructure), segue os príncipios de DDD e Clean Code.

Com o objetivo de deixar a aplicação mais organizada e destribuir as responsabilidades

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

> Os outros controllers (`FilialController`, `PatioController`, `UsuarioController`, `LeitorRFIDController` e `LeituraRFIDController`) seguem estrutura semelhante com operações básicas de CRUD.

## 🛠️ Tecnologias Utilizadas

- [.NET 6 / ASP.NET Core](https://dotnet.microsoft.com/)
- C#
- Entity Framework Core
- Swagger (OpenAPI) para documentação
- Visual Studio 2022
- MongoDB
- AutoMapper
- Migrations
- DataAnnotations
- Pagination
- HATEOAS
- Health Check
- Versionamento API

## ▶️ Instruções de Execução

1. **Clone o repositório:**
   ```bash
   https://github.com/Gui11epio/MottuFind_C.git
   

2. **Vá até "appsettings.json"**
   
   <img width="299" height="260" alt="image" src="https://github.com/user-attachments/assets/5766cd58-42b3-4030-bd39-4a0ca3c37963" />

   
- Nota: Clique com o botão direito em cima de **MottuFind_C_.API** e defina ele como projeto de inicialização, se ainda não estiver 


3. **Coloque coloque a senha do MongoDB**
   <img width="1459" height="106" alt="image" src="https://github.com/user-attachments/assets/2e3aeaa3-5d2a-4854-9d73-41a5c519e46d" />

   Senha do Banco:
   ````bash
   Re5z3B3DK2rEBGDM
   ````

4. **Após tudo isso, rode o programa e o Swagger abrirá sozinho**
   ```bash
   https://localhost:7117/swagger

5. **EndPoints do HealthCheck**
   ```bash
    https://localhost:7117/health
   ````
   
   ````bash
   https://localhost:7117/health/ready
   ````

   ````bash
   https://localhost:7117/health/live
   ````


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
  "senha": "roB123@!"
}
```
🔤 Setores deve conter:

- Setores: "MECANICA" ou "GARAGEM"


#

- LeitorRFID
```bash
{
  "localizacao": "Portão Principal A",
  "ipDispositivo": "192.168.1.100",
  "patioId": 1
}
```

#

- LeituraRFID
```bash
{
  
  "dataHora": "2025-01-15T14:30:00",
  "leitorId": 1,
  "tagRfidId": 1
}

```




  



   
