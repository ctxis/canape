CANAPE Network Testing Tool
Copyright (C) 2014 Context Information Security
Originally developed by James Forshaw

This is the main code for the CANAPE network testing tool. 

It is licensed under GPLv3. Note some other parts of the solution are
licensed under different licenses, see other_licenses.txt for details.

Building:

The project has been tested in Visual Studio 2012 and 2013, it should just
load up and you can build directly from the GUI

If you want to build the help file you need to install Sandcastle Help
File Builder from https://shfb.codeplex.com/ and build the canape_doc.shfbproj
project.

The installer can be built if necessary from CANAPEInstaller with the WiX 
toolkit.

Security Warning:

The projects CANAPE creates can contain dangerous data such as full privileged
scripts which can be executed by just opening a project. Do not open projects
from untrusted sources, ever! I've modified the project loader to do it's 
best to prevent serialization issues but you should consider the file format
to be executable because of the script code.

Thanks to Graham Sutherland for making me actually bother to warn people and 
at least attempt to fix it.
