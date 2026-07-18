# Travlr-ASP.NET-Core
A repository for the Travlr application, ported to an ASP.NET Core Web API

# Getting Started

## Initialize User Secrets and Save Your Jwt Secret to the Secret Store
dotnet user-secrets init

dotnet user-secrets set "JwtSettings:Secret" "CopyAndPasteYourJwtSecretHere"
