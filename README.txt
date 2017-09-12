This project interfaces an arduino bot (2 single axis servos) with a bot in a game created in Unity3D.
Set up the Unity project by copying all assets and C# assembly into a Unity project. Open default scene testScene.
Set up Arduino project:
* Include the SerialCommand folder in the Arduino libraries
* Run bluetooth master on your master Arduino connected to the serial port
* Have another slave Arduino on the robot