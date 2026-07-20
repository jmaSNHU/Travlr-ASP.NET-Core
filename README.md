# Travlr-ASP.NET-Core
A repository for the Travlr application, ported to an ASP.NET Core Web API

# Getting Started

## Install MongoDB 7.0 or higher

## Initialize User Secrets and Save Your Jwt Secret to the Secret Store
```dotnet user-secrets init```

```dotnet user-secrets set "JwtSettings:Secret" "CopyAndPasteYourJwtSecretHere"```
## Install Node.js v20.20.2

```nvm install 20.20.2```

```nvm use 20.20.2```

## Install Angular v17.3.17
```npm install -g @angular/cli@17.3.17```

### CD into Frontend directory and install packages for Node.js project
```npm install```

### CD into Frontend/app_admin directory and install packages for Angular project
```npm install```

# Running the Projects

## API .NET Core WebApi
Run or deploy from Visual Studio

## Node.js Customer-Facing Frontend
CD into the Frontend project directory:

```set DEBUG=Frontend:*```

```npm start```

## Angular App Admin
CD into the Frontend/app_admin directory:
```ng serve```
