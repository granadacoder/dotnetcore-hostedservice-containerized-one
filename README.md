

The code:

\src\Solutions\MyCompany.MyExamples.WorkerServiceExampleOne.sln

Items of interest:

\src\BusinessLayer\HostedServices\TimedHostedService.cs

This file fires every 5(?) seconds.  (Hard coded).   It writes some logs, and creates files in a /tmp directory.
  
    See \src\BusinessLayer\IO\TempFileHelpers.cs for the logic of where the tmp files are written, especially on Linux.  ( /tmp/MyEnvironmentVariableUserName is probably where files are written )

Containerization:

\scaffolding\docker\WorkerServiceExampleOneConsoleApp.docker


On Windows machine, you can just run
    .\ZzzBuildAndDockerize.bat
	
On Linux/Mac, you'll have to open up above .bat file and mimic the 2 commands.


ZzzBuildAndDockerize.bat will build the docker container.
Open up ZzzBuildAndDockerize.bat in notepad and see the 2 "docker run" commands.



