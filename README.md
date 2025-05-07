VinDecoderSpike
VinDecoderSpike is a modern .NET 8 application designed to validate and decode Vehicle Identification Numbers (VINs). It offers a clean, extensible, and real-world example of working with external APIs, data modeling, and Minimal API design in ASP.NET Core.

🚗 Overview
A Vehicle Identification Number (VIN) is a unique 17-character code used to identify individual motor vehicles. This project simplifies VIN validation and decoding through a RESTful API interface and integration with the NHTSA API.

🧩 Key Features
VIN Validation

Ensures VIN is exactly 17 characters

Alphanumeric only, excluding letters I, O, Q

Implemented in VinValidator

VIN Decoding

Fetches structured vehicle data: make, model, year, etc.

Uses HttpClient to call NHTSA API via VinService

Error Handling

Graceful exception management and clear error messages

Full logging support using Microsoft.Extensions.Logging

Minimal API

Clean endpoint: GET /vin/{vin}

Swagger UI for API testing and documentation

Extensibility

Includes ExternalVinService for additional integrations

Modular design for future expansion

🛠️ Technologies Used
.NET 8, ASP.NET Core Minimal APIs

C# 12

HttpClient, Newtonsoft.Json

Swagger/OpenAPI

Microsoft.Extensions.Logging

Visual Studio 2022

VinDecoderSpike/
├── Program.cs                     # Configures services, routes, and Swagger
├── Services/
│   ├── VinValidator.cs           # Validates VIN format
│   ├── VinService.cs             # Calls NHTSA API and maps responses
│   └── ExternalVinService.cs     # Optional external integration
├── Models/
│   ├── Vin.cs                    # VIN entity with basic fields
│   ├── Root.cs / Result.cs       # Response models from NHTSA API

📡 How It Works
VinValidator checks if a VIN is valid.

If valid, VinService makes a request to the NHTSA API.

The response is parsed into a structured format (Root, Result).

A REST endpoint GET /vin/{vin} returns the data or error.

🧠 Skills Demonstrated
C# 12 / .NET 8

Minimal API design

Real-world API consumption

JSON mapping and error handling

Modular and extensible backend architecture

Logging and diagnostics


