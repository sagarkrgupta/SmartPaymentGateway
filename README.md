# Payment API

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

The **Payment API** is a RESTful web service built using **ASP.NET Core 9**. It allows clients to process payments, retrieve transaction histories, and manage payment-related data. This API is designed to be secure, scalable, and easy to integrate with front-end applications like Angular or React.

---

## Table of Contents

1. [Features](#features)
2. [Prerequisites](#prerequisites)
3. [Getting Started](#getting-started)
   - [Installation](#installation)
4. [Deployment](#deployment)
5. [Contributing](#contributing)
6. [License](#license)

---

## Features

- **Payment Processing:** Accepts payment details (e.g., amount, currency, card number) and simulates payment processing.
- **Transaction History:** Fetches a list of past transactions with details such as transaction ID, user ID, amount, currency, status, and timestamp.
- **Validation:** Ensures all required fields are validated before processing requests.
- **Scalability:** Built on ASP.NET Core 9, ensuring high performance and scalability.
- **Security:** Implements best practices for securing APIs, including HTTPS, JWT authentication, and input validation.

---

## Prerequisites

Before running the API, ensure you have the following installed:

- [.NET SDK 9+](https://dotnet.microsoft.com/download)
- A code editor (e.g., [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/))
- [Postman](https://www.postman.com/) or any other API testing tool (optional)
- [rabbitmq:management]
---

## Getting Started

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/sagarkrgupta/payment-api.git
   cd payment-api
   ```

2. Restore dependencies:
  ```bash
  dotnet restore
   ```
3. Build the project:
  ```bash
  dotnet build
   ```

4. Setup DB and Apply migrations:
  ```bash
  cd SmartPaymentGateway.Migrator
  SmartPaymentGateway.Migrator> dotnet run
   ```

5. Run the application:
  ```bash
  dotnet run
   ```

   
### Deployment
Contributing
We welcome contributions! To contribute:

1. Fork the repository.
 ```bash
  dotnet publish -c Release -o ./publish
   ```
2. Create a new branch (git checkout -b feature/YourFeatureName).
3. Commit your changes (git commit -m 'Add some feature').
4. Push to the branch (git push origin feature/YourFeatureName).
5. Open a pull request.

Please ensure your code adheres to the project's coding standards and includes appropriate tests.


### License
This project is licensed under the MIT License . See the LICENSE file for details.