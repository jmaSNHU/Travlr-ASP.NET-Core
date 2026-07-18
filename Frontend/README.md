# cs465-fullstack
CS-465 Full Stack Development with MEAN


## Architecture

- The Travlr App implements two frontends: a customer facing website and an administrative app that allows Travlr staff to make content changes (e.g., adding or modifying trip destinations). The customer facing frontend uses traditional server side rendering (SSR) with Node.js and Express, using Handlebars templates. When a user navigates to different pages (routes), JavaScript controllers retrieve data from the backend API and inject it into the Handlebars templates before the HTML content is rendered and served to the user’s browser. The Admin App is a Single Page Application (SPA) built with the Angular framework. Instead of routing between separate pages, the SPA uses its router to render different components on a single page, eliminating page reloads and providing a fast, streamlined user experience.

- A NoSQL MongoDB database fulfills the application’s data persistence needs, allowing users and administrators to create, modify, and remove data dynamically. Without a database, the website would be limited to rendering static content or storing temporary in memory data that would be lost when the application restarts. MongoDB differs from traditional relational databases by providing a flexible, document based structure that does not enforce a strict schema, allowing developers to modify data structures without updating a database schema directly. The Mongoose ODM is used to manage collection schemas and provides the interface between JavaScript models in the codebase and collections in MongoDB.

## Functionality

-	JavaScript Object Notation (JSON), named for its resemblance to JavaScript syntax, is a standard format for representing objects as key value pairs. It is widely used for transmitting data between applications via application programming interfaces (APIs). Travlr’s app_api is a Representational State Transfer (REST) API with endpoints that allow both the customer facing and admin frontends to create, read, and update trip data by sending and receiving request objects in JSON format.

-	Code refactoring was performed continuously throughout each iteration of the full stack development process. As features were added during each sprint, existing code often required refactoring to prevent duplication and support new functionality. For example, multiple Angular components were created to extract reusable UI elements so they could be shared across components rather than duplicated.

## Testing

-	Each API endpoint must be tested for both pass and fail cases to verify correct behavior. Ideally, unit tests should be written to uncover defects within controller methods. At the integration level, REST clients like Postman make it easy to perform end to end testing of backend API endpoints, including authentication. This helps identify defects before moving on to full system testing involving both the frontend and backend.

# Reflection

-	During CS 465, I gained valuable full stack web development experience with MongoDB, Node.js & Express, and the Angular framework. Databases, programming languages, and frameworks are tools for software development, and being fluent in as many as possible makes me a more flexible and adaptable programmer. Having prior experience with .NET Core, a MySQL database, and a Vue.js frontend, it has been especially valuable to learn how to implement a simple web application using the MEAN stack.


