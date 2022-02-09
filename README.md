# ShowInfo
An API that accesses TV show information.

## Dependencies

- dotnet 6

## Build

`dotnet build`

## Test

`dotnet test`

## Notes on development

- I have deliberately developed this on the assumption that it will be more than a test
project; this is implemented as I would implement a production-ready application, so it
will seem over-engineered for what it is. It is definitely not the "simplest possible thing"
I could have done, and I'm happy to explain some of the choices I've made.

- I have not implemented out-of-process persistent storage; all storage is done against
a singleton in-memory store, however I have left open an interface (`IStoreRepository`)
which can be implemented to allow persistence to external stores, e.g. SQL, Redis, MongoDb
and other dbs etc.

- I've used a number of third party packages.
  - MediatR: a command-pattern implementation framework
  - FluentValidation: for a nice FluentApi to describe validation rules
  - Scrutor: for assembly-scanning type registration
  - Polly: to easily apply retrial policies for HTTP requests

- One area for improvement is the logging which at the moment is rather sparse; with
more time I would have provided more logging.

Matt Jones, Feb 2022