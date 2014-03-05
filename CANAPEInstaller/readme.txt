You need the Wix toolkit from http://wixtoolset.org/ installed and in your path for the batch files to work. 

To build first compile the canape project and generate the help file using the sandcastle help file builder. Once that is done run the copyfiles.bat file to copy the project data into the installer directory. This should also sign files as well if you have the Context key.

Finally run the build.bat file to build the MSI and sign it. 

If you need to add new files to the distribution run copyfiles.bat and delete all files which should not be present from the distribution. Now run the harvert.bat file to capture all the files you need. This will regenerate canape_files.wxs. You can now follow the original build process.