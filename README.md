## Structure Overview

The project follows the "Modular Monolith" architecture, offering a well-organized and scalable structure.

### Bootstrapper

At the core is the Bootstrapper, a robust Web API serving as the system's primary entry point.

### Modules

Modules encapsulate business rules, infrastructure, and application logic, ensuring a modular and maintainable codebase.

- **Jobs Module**:Dedicated to the job portal, empowering the HR department to seamlessly manage job openings, track progress, and initiate employee onboarding with a single button click.

### Shared

Shared projects enhance cohesion across modules.

#### Hyre.Shared.Abstractions

This project houses code safely shareable with the domain layer, fostering consistency.

- **Exceptions**: Defines base exceptions inherited by module-specific custom exceptions.
- **Kernel**: Includes shared files relevant to the domain project:
- **Logging**: Just an interface designed to facilitate message logging testing.
- **Repositories**: Base interfaces for database communication, ensuring a standardized approach. Direct access is restricted.

#### Hyre.Shared.Infrastructure

This project hosts infrastructure code, contributing to the system's robust foundation.

- **Logging**: Implements the interface defined in the abstraction folder, ensuring consistent logging behavior.
- **Repositories**: Provides implementations for contracts outlined in abstractions, promoting code reusability and maintainability.
