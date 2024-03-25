# URL Shortener Service

## Introduction
This URL Shortener Service is a robust and efficient web application for shortening URLs. It's built using the CQRS and MediatR patterns, ensuring a clean and scalable architecture.

## Features
- **URL Shortening**: Quickly shorten any long URL.
- **CQRS and MediatR Implementation**: Uses Command Query Responsibility Segregation (CQRS) and MediatR for enhanced separation of concerns and better scalability.
- **User Authentication**: Secure user authentication using JSON Web Tokens (JWT).

## Technology Stack
- **.NET Core**: For building high-performance web applications.
- **ASP.NET Core**: Framework for building web APIs.
- **Entity Framework Core**: ORM for .NET.
- **JWT**: For securing APIs.
- *MediatR* For using Meadiator pattern

## Setup and Installation
1. Clone the repository.
2. Navigate to the project directory and restore the dependencies: `dotnet restore`.
3. Update the database: `dotnet ef database update`.
4. Start the application: `docker compose up --build`.

## How to Use
1. Register as a new user and log in to receive an authentication token.
2. Use the token to authenticate your requests to the API.
3. Send a POST request to `/link/create` with the original URL to receive the shortened URL.

## API Endpoints
- `POST /user/registration`: Register a new user.
- `POST /user/login`: Authenticate a user.
- `POST /link/create`: Shorten a URL.
- `GET /{shortened_url}`: Redirect to the original URL.

## Contributing
Contributions are welcome. Please fork the repository and submit a pull request with your changes.

