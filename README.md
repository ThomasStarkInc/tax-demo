# Clean architecture implementation demoing simple api calls using CQRS
This is the first time using the Clean Architecture template by Milan Jovanovic.  I chose to use this template to gain some experience with this flavor of clean architecture and the use of MediatR and CQRS.
The task was to create a small app that can return municipality and scheduled taxes for given dates.  Solving the task is fairly straight forward, but it has given me the opportunity to try this temlate, which I have been meaning to do for a while.
CQRS requires some setup, but it is very explicit.  It takes getting used to I suppose.

Mssing in this repo:
- [ ] Needs more logging
- [ ] Needs more exception handling
- [ ] Needs more validation and sanitization
- [ ] Need to add abstraction for EF.  Perhaps with a Repository.

# Clean Architecture Template

What's included in the template?

- SharedKernel project with common Domain-Driven Design abstractions.
- Domain layer with sample entities.
- Application layer with abstractions for:
  - CQRS
  - Example use cases
  - Cross-cutting concerns (logging, validation)
- Infrastructure layer with:
  - Authentication
  - Permission authorization
  - EF Core, PostgreSQL
  - Serilog
- Seq for searching and analyzing structured logs
  - Seq is available at http://localhost:8081 by default
- Testing projects
  - Architecture testing

I'm open to hearing your feedback about the template and what you'd like to see in future iterations.

If you're ready to learn more, check out [**Pragmatic Clean Architecture**](https://www.milanjovanovic.tech/pragmatic-clean-architecture?utm_source=ca-template):

- Domain-Driven Design
- Role-based authorization
- Permission-based authorization
- Distributed caching with Redis
- OpenTelemetry
- Outbox pattern
- API Versioning
- Unit testing
- Functional testing
- Integration testing

Stay awesome!
