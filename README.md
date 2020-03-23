# estudo-abp-io-modularity-microservices

#Nuget packages:

- **Domain**: 
> - **Volo.Abp.Ddd.Domain**
Onde vamos ter as classes e interfaces de repositorios, domain services, aggregate, entity. 

- **Application**: 
>  - **Volo.Abp.Ddd.Application**
Vamos ter a camada com a classe abstrata de ApplicationService, na qual já implementa: IApplicationService, IAvoidDuplicateCrossCuttingConcerns, IValidationEnabled, IUnitOfWorkEnabled,  IAuditingEnabled, ITransientDependency e outras classes abstratas derivadas da mesma como:  CrudAppService.
>  1. **Volo.Abp.AutoMapper**
 

- **Application Contracts**: 
> - **Volo.Abp.Ddd.Application.Contracts**
Vamos ter as interfaces (IApplicationService, ICrudAppService) das classes de abstração do pacote Volo.Abp.Ddd.Application.


- **Shareds**: 
> - **Volo.Abp.Core** (Para criar os modulos)
Pacote amplo que contem o core do framework, como extensões, modularidade e etc. Precisamos dele para criar os modulos. Normalmente ele já vem nas dependências de outros pacotes como por exemplo: *Volo.Abp.Ddd.Domain*, assim não sendo necessário baixar ele separadamente em todos as layers de projetos na solução.


- **EntityFramework (Infra.Data)**
> - ** Volo.Abp.EntityFrameworkCore **
Pacote que cria a abstração para o entity framework core, criando o contexto, mapeamento e repositorios. Ele não depende de selecionar um banco, isso será na camada final de host. 

- ** Host/Microserviço **
 Esse é o projeto no qual acaba tendo mais dependências, mas vou destacar as principais, quando se trata de uma api. 
>  - **Volo.Abp.EntityFrameworkCore.BANCODEDADOS** 
Você deve escolher um banco de dados e implementar nesse projeto. 
>  - **Serilog** 
 Para logs, você deve adicionar algumas extensões dele de acordo com a sua necessidade, como por exemplo: *Serilog.AspNetCore*, *Serilog.Extensions.Logging*, *Serilog.Skins.Console*, *Serilog.Skins.File*. 
>  - **Swashbuckle.AspNetCore** 
Para documentação e teste da api. 
>  - **Volo.Abp.AutoFac** 
Para as injeções de dependência do framework funcionar no asp net core.
