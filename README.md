# WebSharper Capacitor Community

This repository provides WebSharper bindings for [Capacitor Community](https://github.com/capacitor-community) plugins, extending the functionality of [WebSharper Capacitor](https://github.com/dotnet-websharper/Capacitor).

## Overview

WebSharper Capacitor Community enables developers to use additional Capacitor plugins maintained by the Capacitor Community within WebSharper projects. These bindings simplify the integration of community plugins, allowing for seamless development of cross-platform applications.

## Currently Bound Plugins

The following Capacitor Community plugins are currently bound in this repository:

1. **DeviceCheck** - [@capacitor-community/device-check](https://www.npmjs.com/package/@capacitor-community/device-check)
2. **SecurityProvider** - [@capacitor-community/security-provider](https://www.npmjs.com/package/@capacitor-community/security-provider)
3. **ScreenBrightness** - [@capacitor-community/screen-brightness](https://www.npmjs.com/package/@capacitor-community/screen-brightness)
4. **TextToSpeech** - [@capacitor-community/text-to-speech](https://www.npmjs.com/package/@capacitor-community/text-to-speech)
5. **NativeAudio** - [@capacitor-community/native-audio](https://www.npmjs.com/package/@capacitor-community/native-audio)
6. **BluetoothLe** - [@capacitor-community/bluetooth-le](https://www.npmjs.com/package/@capacitor-community/bluetooth-le)
7. **SafeArea** - [@capacitor-community/safe-area](https://www.npmjs.com/package/@capacitor-community/safe-area)
8. **CameraPreview** - [@capacitor-community/camera-preview](https://www.npmjs.com/package/@capacitor-community/camera-preview)
9. **SpeechRecognition** - [@capacitor-community/speech-recognition](https://www.npmjs.com/package/@capacitor-community/speech-recognition)
10. **Intercom** - [@capacitor-community/intercom](https://www.npmjs.com/package/@capacitor-community/intercom)
11. **PhotoViewer** - [@capacitor-community/photoviewer](https://www.npmjs.com/package/@capacitor-community/photoviewer)
12. **AppIcon** - [@capacitor-community/app-icon](https://www.npmjs.com/package/@capacitor-community/app-icon)
13. **Media** - [@capacitor-community/media](https://www.npmjs.com/package/@capacitor-community/media)
14. **FacebookLogin** - [@capacitor-community/facebook-login](https://www.npmjs.com/package/@capacitor-community/facebook-login)
15. **Stripe** - [@capacitor-community/stripe](https://www.npmjs.com/package/@capacitor-community/stripe)
16. **PrivacyScreen** - [@capacitor-community/privacy-screen](https://www.npmjs.com/package/@capacitor-community/privacy-screen)
17. **KeepAwake** - [@capacitor-community/keep-awake](https://www.npmjs.com/package/@capacitor-community/keep-awake)
18. **Contacts** - [@capacitor-community/contacts](https://www.npmjs.com/package/@capacitor-community/contacts)
19. **DatePicker** - [@capacitor-community/date-picker](https://www.npmjs.com/package/@capacitor-community/date-picker)
20. **SQLite** - [@capacitor-community/sqlite](https://www.npmjs.com/package/@capacitor-community/sqlite)
21. **ImageToText** - [@capacitor-community/image-to-text](https://www.npmjs.com/package/@capacitor-community/image-to-text)
22. **FileOpener** - [@capacitor-community/file-opener](https://www.npmjs.com/package/@capacitor-community/file-opener)
23. **AppleSignIn** - [@capacitor-community/apple-sign-in](https://www.npmjs.com/package/@capacitor-community/apple-sign-in)
24. **BackgroundGeolocation** - [@capacitor-community/background-geolocation](https://www.npmjs.com/package/@capacitor-community/background-geolocation)
25. **VolumeButtons** - [@capacitor-community/volume-buttons](https://www.npmjs.com/package/@capacitor-community/volume-buttons)
26. **InAppReview** - [@capacitor-community/in-app-review](https://www.npmjs.com/package/@capacitor-community/in-app-review)

## Getting Started

### Prerequisites

Before starting, ensure you have the following:

- Node.js and npm installed on your machine.
- [Capacitor](https://capacitorjs.com/) set up for managing cross-platform apps.
- [WebSharper](https://websharper.com/) for building F#-based web applications.
- A project structure ready to integrate Capacitor and WebSharper.

### Installation

1. Initialize a Capacitor project if not already done:

   ```bash
   npm init                # Initialize a new Node.js project
   npm install             # Install default dependencies
   npm i @capacitor/core   # Install Capacitor core library
   npm i -D @capacitor/cli # Install Capacitor CLI as a dev dependency
   npx cap init "YourApp" com.example.yourapp --web-dir wwwroot/dist # Initialize Capacitor in the project
   ```

2. Add the WebSharper Capacitor NuGet package:

   ```bash
   dotnet add package WebSharper.Capacitor --version 8.0.0.494-beta1
   ```

    This package is required because WebSharper Capacitor Community builds upon the core functionality provided by WebSharper Capacitor. It ensures compatibility and seamless integration of Capacitor plugins into your WebSharper project.

3. Add the WebSharper Capacitor Community NuGet package:

   ```bash
   dotnet add package WebSharper.Capacitor.Community --version 8.0.0.494-beta1
   ```

3. Choose and install the Capacitor Community plugins you want to use. For example, to use the **BluetoothLe** plugin:

   ```bash
   npm i @capacitor-community/bluetooth-le
   ```

4. Build your project:

   ```bash
   npm i vite                # Install Vite for building your web assets
   npx vite build            # Build your web project with Vite
   ```

5. Sync configuration:

   ```bash
   npx cap sync              # Sync Capacitor configuration and plugins
   ```

## Notes

- Ensure your Capacitor project is correctly configured with `capacitor.config.json`.
- The WebSharper Capacitor Community package extends functionality provided by WebSharper Capacitor.
- Check out sample usage of plugins in the `WebSharper.Capacitor.Community.Sample` directory.