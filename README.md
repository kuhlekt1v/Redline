# Redline

SWEG5302-The Redline Medical System aims to create an efficient, low-cost emergency medical contact sytsem for Android and iOS devices using the Xamarin framework.

## Local Setup

1. Clone repository to local enviroment.
2. If necessary install an Android Device (Pixel 2 Pie 9.0 - API 28, or similar, at a minimum.)
   - Tools -> Android -> Android Device Manager.. -> + New
3. If necessary install Andround 10.0 - Q (Android SDK Platform 29)
   - Tools -> Android -> Android SDK Manager
     <br>

## Version History

| Date     | Description of Work performed                                                                                                                                                                                                                                             | Author(s)                     |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------- |
| 03/01/21 | Created project template including intial NuGet packages, code style conventions, and configured framework versions.                                                                                                                                                      | Cody Sheridan                 |
| 03/22/21 | Completed reusable behvaviors for form entry validation. Completed UI pages include Login, Register, Medical Info, Preconditions, Prescription, Profile, and Contact. Completed functionality includes login/logout, registering a new user, and navigation via a navbar. | Cody Sheridan Daniel Mansilla |
| 04/01/21 | Added ability to send text messages using.                                                                                                                                                                                                                                | Amaris Sneed                  |
| 04/15/21 | All core functions operating as expected. Added support for Google Map/API, Geolocation, and email services.                                                                                                                                                              | Cody Sheridan Daniel Mansilla |

<br>

## Android Emulator Setup (Visual Studio 2019)

![alt text](https://github.com/kuhlekt1v/redline/blob/revised-map/OpenAndroidSDKManager.png?raw=true)

1. Open the Android SDK Manager. _Circled in red above._
   <br>

![alt text](https://github.com/kuhlekt1v/redline/blob/revised-map/SDKManager.png?raw=true) 2. Ensure both Google Play (Intel x86 Atom & Intel x86 Atom_64) and Google API system images are installed for the device emulator you will use. _Highlighted in red above._
<br>
