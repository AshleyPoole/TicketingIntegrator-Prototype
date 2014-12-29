**TicketingIntegrator-Prototype**
=============================

**What is this project?**

This project is a very rough around the edges prototype that I threw together to demo integration between ManageEngine's ServiceDesk and FogCreek's FogBugz.

The prototype is written as a C# console application which consumes JSON as it's input that is provided by ServiceDesks external action interface. This JSON is then mapped into a FogBugz XML API request who's response is then converted back into JSON for ServiceDesk to consume.


**Installation**
- Create a directory at C:\TicketingIntegrator
- Build the solution and copy the output to C:\TicketingIntegrator
- Update C:\TicketingIntegrator\TicketingIntegrator.exe.config with your FogBugz API url and FogBugz API key
- Copy ScriptActionConfig.xml and Request_ActionMenu.xml to [ServiceDesk Install Location]ManageEngineServiceDeskserverdefaultconf
- Copy ScriptExecution.jar to [ServiceDesk Install Location]ManageEngineServiceDeskserverdefaultlib
- Restart ServiceDesk
- Now go to a request in ServiceDesk and under the actions menu, you should now see "Send To FogBugz"


** Notes **
http://www.ashleypoole.co.uk/2014/manageengine-servicedesk-to-fogcreek-fogbugz-prototype/

http://www.ashleypoole.co.uk/2013/servicedesk-and-fogbugz-integration/


**Questions?**

Tweet me - @ashleypoole92

Contact Form - http://www.ashleypoole.co.uk/contact/



