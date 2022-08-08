# Dockable Panel Bug

As reported in: https://github.com/specklesystems/speckle-sharp/issues/1469

### Steps to reproduce the bug

This sample project currently targets Revit 2022.

1. Rebuild the project
2. Copy the built `DockableDialog.dll` and `DockableDialog.addin` to a Revit addin folder eg `%appdata%/Autodesk/Revit/Addins/2022`
3. Set the right start up action AND add a command line argument pointing to a file, so it will be opened automatically, **this is essential to reproduce the bug**

![image](https://user-images.githubusercontent.com/2679513/183346801-45ffcb15-0bf6-4dd1-9433-b2dbe105b65a.png)

4. Click debug and after the file opens > AEC LABS > Show Dockable Panel and the error is shown

![image](https://user-images.githubusercontent.com/2679513/183350555-2cdc8427-7cd4-4ae6-a4dd-7a4780a4b639.png)

5. Also, no events like `DocumentOpened`, or `ViewActivated` are triggered under these circumstances.

NOTE: remember to manually copy over the project dll if any change is made
NOTE2: removing the command line argument pointing to the file, the panel is registered correctly