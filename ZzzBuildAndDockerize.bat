

docker build -f scaffolding\docker\WorkerServiceExampleOneConsoleApp.docker -t myworkerserviceexampleone/entryapp .

REM docker run -i --rm myworkerserviceexampleone/entryapp
REM OR
REM docker run --env ASPNETCORE_ENVIRONMENT=Development  -i --rm myworkerserviceexampleone/entryapp

REM No "port forwarding" with a console app
REM  -p 55555:52400