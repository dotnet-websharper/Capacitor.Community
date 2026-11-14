namespace WebSharper.Capacitor.Community.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.Capacitor.Community

[<JavaScript>]
module Helpers =

    // Probe the Capacitor bridge without depending on the core binding. `?` is
    // safe for reading properties; calling needs an inline helper.
    [<Inline "$0 && $0.isNativePlatform ? $0.isNativePlatform() : false">]
    let private platformIsNative (cap: obj) = X<bool>

    [<Inline "$0 && $0.getPlatform ? $0.getPlatform() : 'web'">]
    let private platformName (cap: obj) = X<string>

    let IsNative () = platformIsNative (JS.Global?Capacitor)

    let Platform () = platformName (JS.Global?Capacitor)

[<JavaScript>]
module Client =
    // The template is loaded from the DOM, so editing index.html and refreshing
    // the browser is enough; a rebuild is only needed when holes change.
    type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

    // The storage key is shared with the bootstrap script in index.html.
    let themeStorageKey = "websharper-capacitor-community-theme"
    let theme = Var.Create "light"
    let themeLabel = Var.Create "🌙 Dark mode"

    let applyTheme () =
        JS.Document.DocumentElement.SetAttribute("data-theme", theme.Value)
        themeLabel.Value <- if theme.Value = "dark" then "☀️ Light mode" else "🌙 Dark mode"

    let toggleTheme () =
        theme.Value <- (if theme.Value = "dark" then "light" else "dark")
        try JS.Window.LocalStorage.SetItem(themeStorageKey, theme.Value) with _ -> ()
        applyTheme ()

    // ---------------------------------------------------------------------
    // Plugin demos
    // ---------------------------------------------------------------------

    type Demo =
        {
            Name: string
            Group: string
            Description: string
            Run: unit -> Promise<string>
        }

    let demos : Demo list =
        [
            // Display
            {
                Name = "SafeArea"
                Group = "Display"
                Description = "Style and show/hide the status and navigation bars."
                Run = fun () ->
                    promise {
                        do! CapacitorCommunity.SafeArea.SetSystemBarsStyle(
                                SafeArea.SystemBarsStyleOptions(SafeArea.SystemBarsStyle.Dark))
                        do! CapacitorCommunity.SafeArea.ShowSystemBars(SafeArea.SystemBarsVisibilityOptions())
                        return "bars styled DARK and shown"
                    }
            }
            {
                Name = "ScreenBrightness"
                Group = "Display"
                Description = "Read and set the screen brightness (0–1)."
                Run = fun () ->
                    promise {
                        let! current = CapacitorCommunity.ScreenBrightness.GetBrightness()
                        do! CapacitorCommunity.ScreenBrightness.SetBrightness(
                                ScreenBrightness.SetBrightnessOptions(Brightness = 1.0))
                        return sprintf "brightness %.2f, set to 1.00" current.Brightness
                    }
            }
            {
                Name = "KeepAwake"
                Group = "Display"
                Description = "Prevent the screen from dimming or locking."
                Run = fun () ->
                    promise {
                        let! supported = CapacitorCommunity.KeepAwake.IsSupported()
                        do! CapacitorCommunity.KeepAwake.KeepAwake()
                        let! kept = CapacitorCommunity.KeepAwake.IsKeptAwake()
                        return sprintf "supported=%b, kept awake=%b" supported.IsSupported kept.IsKeptAwake
                    }
            }
            {
                Name = "PrivacyScreen"
                Group = "Display"
                Description = "Protect the app from screenshots and screen recording."
                Run = fun () ->
                    promise {
                        do! CapacitorCommunity.PrivacyScreen.Enable()
                        return "privacy screen enabled"
                    }
            }
            {
                Name = "AppIcon"
                Group = "Display"
                Description = "Switch the home-screen icon at runtime (iOS)."
                Run = fun () ->
                    promise {
                        let! supported = CapacitorCommunity.AppIcon.IsSupported()
                        return sprintf "icon switching supported=%b" supported.Value
                    }
            }
            {
                Name = "DatePicker"
                Group = "Display"
                Description = "Native date and time picker."
                Run = fun () ->
                    promise {
                        let! _ = CapacitorCommunity.DatePicker.Present(DatePicker.DatePickerOptions())
                        return "picker closed"
                    }
            }
            // Audio & speech
            {
                Name = "TextToSpeech"
                Group = "Audio & speech"
                Description = "Synthesize speech; enumerate languages and voices."
                Run = fun () ->
                    promise {
                        let! languages = CapacitorCommunity.TextToSpeech.GetSupportedLanguages()
                        let! voices = CapacitorCommunity.TextToSpeech.GetSupportedVoices()
                        let! supported =
                            CapacitorCommunity.TextToSpeech.IsLanguageSupported(
                                TextToSpeech.IsLanguageSupportedOptions("en-US"))
                        do! CapacitorCommunity.TextToSpeech.Speak(
                                TextToSpeech.TTSOptions(Text = "Hello from WebSharper", Lang = "en-US", Rate = 1.0))
                        return sprintf "%d languages, %d voices, en-US supported=%b"
                            languages.Length voices.Length supported.Supported
                    }
            }
            {
                Name = "NativeAudio"
                Group = "Audio & speech"
                Description = "Low-latency playback of preloaded audio assets."
                Run = fun () ->
                    promise {
                        do! CapacitorCommunity.NativeAudio.Configure(
                                NativeAudio.ConfigureOptions(Fade = false, Focus = false))
                        do! CapacitorCommunity.NativeAudio.SetVolume(
                                NativeAudio.SetVolumeOptions(AssetId = "demo", Volume = 0.5))
                        let! duration =
                            CapacitorCommunity.NativeAudio.GetDuration(NativeAudio.AssetIdOptions("demo"))
                        return sprintf "configured, duration=%.2fs" duration.Duration
                    }
            }
            {
                Name = "SpeechRecognition"
                Group = "Audio & speech"
                Description = "On-device speech to text."
                Run = fun () ->
                    promise {
                        let! available = CapacitorCommunity.SpeechRecognition.Available()
                        let! _ = CapacitorCommunity.SpeechRecognition.CheckPermissions()
                        return sprintf "available=%b" available.Available
                    }
            }
            // Media
            {
                Name = "CameraPreview"
                Group = "Media"
                Description = "Render a live camera feed behind the web view."
                Run = fun () ->
                    promise {
                        let! started = CapacitorCommunity.CameraPreview.IsCameraStarted()
                        let! modes = CapacitorCommunity.CameraPreview.GetSupportedFlashModes()
                        return sprintf "started=%b, %d flash modes" started.Value modes.Result.Length
                    }
            }
            {
                Name = "Media"
                Group = "Media"
                Description = "Save photos and videos to the gallery and manage albums."
                Run = fun () ->
                    promise {
                        let! albums = CapacitorCommunity.Media.GetAlbums()
                        return sprintf "%d albums" albums.Albums.Length
                    }
            }
            {
                Name = "PhotoViewer"
                Group = "Media"
                Description = "Full-screen image viewer with zoom, swipe and share."
                Run = fun () ->
                    promise {
                        let! paths = CapacitorCommunity.PhotoViewer.GetInternalImagePaths()
                        return sprintf "%d internal images" paths.PathList.Length
                    }
            }
            {
                Name = "ImageToText"
                Group = "Media"
                Description = "OCR: detect text in an image."
                Run = fun () ->
                    promise {
                        let! result =
                            CapacitorCommunity.ImageToText.DetectText(
                                ImageToText.DetectTextBase64Options(Base64 = ""))
                        return sprintf "%d text blocks" result.TextDetections.Length
                    }
            }
            // Device
            {
                Name = "BluetoothLe"
                Group = "Device"
                Description = "Scan, connect and read/write BLE characteristics."
                Run = fun () ->
                    promise {
                        do! CapacitorCommunity.BluetoothLe.Initialize(
                                BluetoothLe.InitializeOptions(AndroidNeverForLocation = false))
                        let! enabled = CapacitorCommunity.BluetoothLe.IsEnabled()
                        let! bonded = CapacitorCommunity.BluetoothLe.GetBondedDevices()
                        return sprintf "enabled=%b, %d bonded devices" enabled.Value bonded.Devices.Length
                    }
            }
            {
                Name = "VolumeButtons"
                Group = "Device"
                Description = "Listen for hardware volume button presses."
                Run = fun () ->
                    promise {
                        let! watching = CapacitorCommunity.VolumeButtons.IsWatching()
                        do! CapacitorCommunity.VolumeButtons.ClearWatch()
                        return sprintf "watching=%b, watch cleared" watching.Value
                    }
            }
            // Security
            {
                Name = "DeviceCheck"
                Group = "Security"
                Description = "Apple DeviceCheck token generation."
                Run = fun () ->
                    promise {
                        let! token = CapacitorCommunity.DeviceCheck.GenerateToken()
                        return sprintf "token received (%d chars)" token.Token.Length
                    }
            }
            {
                Name = "SecurityProvider"
                Group = "Security"
                Description = "Check and install the Android security provider."
                Run = fun () ->
                    promise {
                        let! result = CapacitorCommunity.SecurityProvider.InstallIfNeeded()
                        return "installIfNeeded completed"
                    }
            }
            // Data & files
            {
                Name = "SQLite"
                Group = "Data & files"
                Description = "Native SQLite with encryption, JSON import/export and sync."
                Run = fun () ->
                    promise {
                        let! echo = CapacitorCommunity.SQLite.Echo(SQLite.CapEchoOptions(Value = "hello"))
                        return sprintf "echo=%s" echo.Value
                    }
            }
            {
                Name = "FileOpener"
                Group = "Data & files"
                Description = "Hand a file to the OS so another app can open it."
                Run = fun () ->
                    promise {
                        do! CapacitorCommunity.FileOpener.Open(
                                FileOpener.FileOpenerOptions(FilePath = "file:///demo.txt", ContentType = "text/plain"))
                        return "handed the file to the OS"
                    }
            }
            {
                Name = "Contacts"
                Group = "Data & files"
                Description = "Read, create and pick contacts."
                Run = fun () ->
                    promise {
                        let! _ = CapacitorCommunity.Contacts.CheckPermissions()
                        let! contacts = CapacitorCommunity.Contacts.GetContacts(Contacts.GetContactsOptions())
                        return sprintf "%d contacts" contacts.Contacts.Length
                    }
            }
            {
                Name = "BackgroundGeolocation"
                Group = "Data & files"
                Description = "Continuous location updates while the app is backgrounded."
                Run = fun () ->
                    promise {
                        do! CapacitorCommunity.BackgroundGeolocation.OpenSettings()
                        return "system settings opened"
                    }
            }
            // Auth & commerce
            {
                Name = "AppleSignIn"
                Group = "Auth & commerce"
                Description = "Sign in with Apple."
                Run = fun () ->
                    promise {
                        let! response =
                            CapacitorCommunity.AppleSignIn.Authorize(
                                AppleSignIn.SignInWithAppleOptions(
                                    ClientId = "com.example.app",
                                    RedirectURI = "https://example.com/callback",
                                    Scopes = "email,name"))
                        return "authorization completed"
                    }
            }
            {
                Name = "FacebookLogin"
                Group = "Auth & commerce"
                Description = "Facebook OAuth login."
                Run = fun () ->
                    promise {
                        let! _ = CapacitorCommunity.FacebookLogin.GetCurrentAccessToken()
                        return "current access token checked"
                    }
            }
            {
                Name = "Intercom"
                Group = "Auth & commerce"
                Description = "Intercom messenger and in-app messages."
                Run = fun () ->
                    promise {
                        do! CapacitorCommunity.Intercom.RegisterUnidentifiedUser()
                        return "registered an unidentified user"
                    }
            }
            {
                Name = "Stripe"
                Group = "Auth & commerce"
                Description = "Payment Sheet, Payment Flow, Apple Pay and Google Pay."
                Run = fun () ->
                    promise {
                        do! CapacitorCommunity.Stripe.Initialize(
                                Stripe.StripeInitializationOptions(PublishableKey = "pk_test_placeholder"))
                        do! CapacitorCommunity.Stripe.IsApplePayAvailable()
                        do! CapacitorCommunity.Stripe.IsGooglePayAvailable()
                        return "initialized; Apple Pay and Google Pay availability checked"
                    }
            }
            {
                Name = "InAppReview"
                Group = "Auth & commerce"
                Description = "Ask the user to rate the app with the native dialog."
                Run = fun () ->
                    promise {
                        do! CapacitorCommunity.InAppReview.RequestReview()
                        return "native review dialog requested"
                    }
            }
        ]

    let output = Var.Create "Ready — press Run on any plugin card."

    let run (demo: Demo) () =
        output.Value <- output.Value + "\n… " + demo.Name
        async {
            try
                let! result = Promise.AsAsync (demo.Run())
                output.Value <- output.Value + "\n✓ " + demo.Name + ": " + result
            with ex ->
                output.Value <- output.Value + "\n✗ " + demo.Name + ": " + ex.Message
        }
        |> Async.Start

    [<SPAEntryPoint>]
    let Main () =
        // Restore a previously chosen theme (light is the default).
        let savedTheme =
            try JS.Window.LocalStorage.GetItem(themeStorageKey) with _ -> null
        if savedTheme = "dark" || savedTheme = "light" then
            theme.Value <- savedTheme
        applyTheme ()

        let hint =
            if Helpers.IsNative () then
                sprintf "Running natively on %s — plugin calls are dispatched to the native bridge."
                    (Helpers.Platform())
            else
                "Running in a web browser. Most of these plugins are native-only, so their calls will report an error instead of crashing the page."

        let pluginList =
            demos
            |> List.map (fun (demo: Demo) ->
                IndexTemplate.PluginItem()
                    .name(demo.Name)
                    .group(demo.Group)
                    .description(demo.Description)
                    .run(fun _ -> run demo ())
                    .Doc())
            |> Doc.Concat

        // `.Bind()` renders the whole document body.
        IndexTemplate()
            .platform(Helpers.Platform())
            .themeLabel(themeLabel.V)
            .count(string demos.Length)
            .hint(hint)
            .output(output.V)
            .PluginList(pluginList)
            .ToggleTheme(fun _ -> toggleTheme ())
            .Bind()