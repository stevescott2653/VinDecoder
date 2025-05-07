# VinDecoder

VinDecoderSpike
VinDecoderSpike is a .NET 8 application designed to validate and decode Vehicle Identification Numbers (VINs). It provides a robust API for VIN validation and integrates with the National Highway Traffic Safety Administration (NHTSA) API to fetch detailed vehicle information.
Features
•	VIN Validation: Ensures VINs meet industry standards (e.g., length, valid characters, and exclusion of invalid letters like 'I', 'O', and 'Q').
•	VIN Decoding: Fetches detailed vehicle information using the NHTSA API.
•	Error Handling: Provides structured error responses for invalid VINs or API failures.
•	API Documentation: Includes Swagger/OpenAPI for easy exploration of endpoints.
Technologies
•	C# 12.0
•	.NET 8
•	ASP.NET Core Minimal APIs
•	Newtonsoft.Json for JSON deserialization
•	Microsoft.Extensions.Logging for logging
Getting Started
Prerequisites
•	.NET 8 SDK installed
•	Visual Studio 2022 or any compatible IDE
•	Internet connection for accessing the NHTSA API
