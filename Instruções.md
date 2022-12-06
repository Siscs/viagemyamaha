# Instruções Rota de Viagem Yamaha #

## Tecnologias utilizadas ##
- .Net 6.0
- Nodejs v14.20.0 (com npm 6.14.17)
- Angular 15.0.2 (Frontend web)
- Ide Visual studio 2022 (projetos backend)
- Ide Visual studio Code (projeto angular front-end)

## Estrutura dos arquivos / projeto ##

Db - pasta onde está o arquivo rotas.csv
Postman - collection de teste da api

src - pasta com os projetos
src\ViagemYamaha.API - Projeto API Rest
src\ViagemYamaha.Console.UI - Projeto UI Console 
src\ViagemYamaha.Core - Projeto core que será utilizado pelos projetos
src\ViagemYamaha.Web.UI - Projeto UI Web (Angular 15)

test - pasta com o projeto de teste de unidade
test\ViagemYamaha.Core.Test
test\coverlet.bat - script para geração de relatório de cobertura de código

ViagemYamaha.sln - arquivo de solução do projeto


## Como executar a aplicação ##

- Descompacte o zip em uma pasta de preferência

** Executar API
- Abrir a solution ViagemYamaha.sln com o visual studio 2022
- No projeto ViagemYamaha.API configure o local do arquivo rotas.csv no appsettings.development
- Compilar projetos
- Definir projeto como startup
- Executar aplicação. (irá iniciar em https://localhost:7049)
- Ao executar a aplicação será apresentada o swagger para testes da api ou pode-se utilizar a collection do postman para testes da api.

** Executar Console
- Abrir a solution ViagemYamaha.sln com o visual studio 2022
- Definir projeto ViagemYamaha.Console como startup
- compilar aplicação
- será gerado o executavél na pasta  src\ViagemYamaha.Console.UI\bin\Debug\net6.0\ViagemYamaha.Console.exe
- abrir terminal e executar passando como parâmetro o local do arquivo rotas.csv
  (caso queira pode-se gerar um publish deste projeto para uma pasta especifica)

** Executar aplicação front-end web
- este projeto depende da API rest estar executando
- Certificar se nodejs e angular instalados
- Alterar a url de apontamento para a api no arquivo ViagemYamaha.Web.UI\src\environments\ViagemYamaha.Web.UI\src\environments (parâmetro apiUrl)
- na pasta src\ViagemYamaha.Web.UI executar:
  - npm install  (para instalação dos pacotes)
  - ng serve -o (será aberto no browser padrão)
  
## Decisões de design adotadas ##
- Criado camada de aplicação (projeto core onde será utilizado por todos os fronts) e concentrando o negócio
- Utilizado alguns design patterns como: Repository, IOC, 
- Alguns padrões solid com SRP,ISP, DSP	

## Api Rest ##
- Pasta Configurations - Onde configuramos middleware de erros e Injeção de dependencias via extensions methods
- Pasta Controllers
  - BaseController - Controller base que poderá ser utilizado por vários controllers
  - RotaController - Controller responsável pelos endpoints de serviços de rota.

- Endpointpara consultar melhor rota
	- GET https://localhost:7049/api/Rotas?Origem=GUA&Destino=SCL
	- Será retornado objeto com string melhor rota
	{
		"data": "GUA - VCF - DFE ao custo de R$ 31"
	}
	
- Endpoint para consultar melhor rota
	- POST https://localhost:7049/api/Rotas
	-- exemplo de body a ser enviado em JSON
	{
	  "origem": "JDI",
	  "destino": "MEX",
	  "escalas": [
		  "PAN",
		  "HAI"
	  ],
	  "valor": 50
	}