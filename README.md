# Metas: 
- ~~Implementar modulos sem utilizar templates ou o cli, realizar na unha.~~ 
- ~~Utilizar o provider do MySql e migrations.~~ 
- ~~Analizar o event bus distributed com rabbitmq do framework~~
- ~~Analizar como implementaria um CQRS~~ 

# Próximas issues:
1. <a href="https://github.com/muriloxk/estudo-abp-io-modularity-microservices/issues/1" >  Criar um gateway bff mobile e web</a>
1. <a href="https://github.com/muriloxk/estudo-abp-io-modularity-microservices/issues/2" > Criar autenticação via o gateway mobile (Hybrid grantype)</a>
1. <a href="https://github.com/muriloxk/estudo-abp-io-modularity-microservices/issues/7">Criar autenticação Implicit grant type para web</a>
1. <a href="https://github.com/muriloxk/estudo-abp-io-modularity-microservices/issues/3" > Implementar multitenancy compartilhando os bancos.</a>
1. <a href="https://github.com/muriloxk/estudo-abp-io-modularity-microservices/issues/4" > Autorização de papeis entre as funcionalidades.</a>
1. <a href="https://github.com/muriloxk/estudo-abp-io-modularity-microservices/issues/5" > Serilog com elasticsearch</a>
1. <a href="https://github.com/muriloxk/estudo-abp-io-modularity-microservices/issues/6" > Implementar cache com redis de acordo com os inquilinos. </a>


# Anotações e observações sobre o estudo sobre o framework abp

# Nuget packages:
Pacotes que devem ser utilizados por camada em um modulo.

- **Domain**: 
> - **Volo.Abp.Ddd.Domain**
Onde vamos ter as classes e interfaces de repositorios, domain services, aggregate, entity. 

- **Application**: 
>  - **Volo.Abp.Ddd.Application**
Vamos ter a camada com a classe abstrata de ApplicationService, na qual já implementa: IApplicationService, IAvoidDuplicateCrossCuttingConcerns, IValidationEnabled, IUnitOfWorkEnabled,  IAuditingEnabled, ITransientDependency e outras classes abstratas derivadas da mesma como:  CrudAppService.
> - **Volo.Abp.AutoMapper**
 

- **Application Contracts**: 
> - **Volo.Abp.Ddd.Application.Contracts**
Vamos ter as interfaces (IApplicationService, ICrudAppService) das classes de abstração do pacote Volo.Abp.Ddd.Application.

- **Shareds**: 
> - **Volo.Abp.Core** (Para criar os modulos)
Pacote amplo que contem o core do framework, como extensões, modularidade e etc. Precisamos dele para criar os modulos. Normalmente ele já vem nas dependências de outros pacotes como por exemplo: *Volo.Abp.Ddd.Domain*, assim não sendo necessário baixar ele separadamente em todos as layers de projetos na solução.

- **EntityFramework (Infra.Data)**
> - **Volo.Abp.EntityFrameworkCore**
Pacote que cria a abstração para o entity framework core, criando o contexto, mapeamento e repositorios. Ele não depende de selecionar um banco, isso será na camada final de host. 

- **Host/Microserviço**
 Esse é o projeto no qual acaba tendo mais dependências, mas vou destacar as principais, quando se trata de uma api. 
>  - **Volo.Abp.EntityFrameworkCore.BANCODEDADOS** 
Você deve escolher um banco de dados e implementar nesse projeto. 
>  - **Serilog** 
 Para logs, você deve adicionar algumas extensões dele de acordo com a sua necessidade, como por exemplo: *Serilog.AspNetCore*, *Serilog.Extensions.Logging*, *Serilog.Skins.Console*, *Serilog.Skins.File*. 
>  - **Swashbuckle.AspNetCore** 
Para documentação e teste da api. 
>  - **Volo.Abp.AutoFac** 
Para as injeções de dependência do framework funcionar no asp net core.


# Dependências de camadas/submódulo (AbpModule) 

![Arquitetura do modulo](https://raw.githubusercontent.com/abpframework/abp/dev/docs/en/images/module-layers-and-packages.jpg)

*Observações*:

1. Embora como na imagem acima diga que a camada de api não deve depender da camada de aplicação e sim apenas a de contratos, porem os "controllers automáticos" funciona apenas com a camada de aplicação. Como foi discutido nessa issue aqui: https://github.com/abpframework/abp/issues/1731 

1. Não achei necessário a camada HttpApi no modulo como na imagem da documentação ou criado pelo template do cli, quando possuo um microserviço/host. Logo, não criei ele. 


# Rabbit MQ 

Observações:

   - A classe implementada no RabbitMqDistributedEventBus é fraca e acoplada a um fluxo de exchange, que aparentemente começaram apenas há 2 meses e o correto seria criar um utilizando a biblioteca do proprio framework,  
   https://github.com/abpframework/abp/tree/dev/framework/src/Volo.Abp.RabbitMQ/Volo/Abp/RabbitMQ ou as vezes até melhorar a classe RabbitMqDistributedEventBus e contribuir.

- Ainda não há uma implementação com MediaTR o que ajudaria muito nos handlers de comando chamados por serviços em CQRS, como nos meus estudos (https://github.com/muriloxk/estudo-microservices-rabbitmq): Há uma issue com prioridade alta aberta no github: CQRS infrastructure #57: https://github.com/abpframework/abp/issues/57 *(Talvez a classe na qual comentei no item acima, seja o começo de um fruto dessa discussão)*


