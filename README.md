# News Score

Repository for Aidn technical assessment focusing on calculating the NEWS Score of a patient based on vital signs.

## Prerequisites
- Dotnet 10 SDK
- IDE with SLNX support (e.g., Visual Studio, JetBrains Rider)

## Assumptions 
- The 'contracts' cannot be changed, so the request has to come in via a collection of vital signs.
- There can only be 3 vital signs per request.
- No duplicate vital signs
- No missing vital signs
- The request and response has to be in JSON format, assuming the example is taken from JS where object literals are used.

## Nice to haves in future
- Add logging / OTEL support
- Add CI/CD pipeline
- Containerization for integration tests

Reviewer notes:
- All  the problem details functionality is boilerplate code that I have pulled from other projects as i have not had time to package it up.