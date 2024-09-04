namespace WebSharper.Capacitor.Community

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =
    [<AutoOpen>]
    module Core = 
        let PluginListenerHandle =
            Class "PluginListenerHandle"
            |+> Instance ["remove" => T<unit> ^-> T<Promise<unit>>]

        let PermissionState = 
            Pattern.EnumStrings "PermissionState" [
                "prompt"
                "prompt-with-rationale"
                "granted"
                "denied"
            ]

    [<AutoOpen>]
    module FacebookLogin = 
        let AccessToken =
            Pattern.Config "AccessToken" {
                Required = []
                Optional = [
                    "applicationId", T<string>
                    "declinedPermissions", !| T<string>
                    "expires", T<string>
                    "isExpired", T<bool>
                    "lastRefresh", T<string>
                    "permissions", !| T<string>
                    "token", T<string>
                    "userId", T<string>
                ]
            }

        let FacebookLoginResponse =
            Pattern.Config "FacebookLoginResponse" {
                Required = []
                Optional = [
                    "accessToken", AccessToken.Type + T<unit>
                    "recentlyGrantedPermissions", !| T<string>
                    "recentlyDeniedPermissions", !| T<string>
                ]
            }

        let FacebookCurrentAccessTokenResponse =
            Pattern.Config "FacebookCurrentAccessTokenResponse" {
                Required = []
                Optional = [
                    "accessToken", AccessToken.Type + T<unit>
                ]
            }

        let FacebookConfiguration = 
            Pattern.Config "FacebookConfiguration" {
                Required = []
                Optional = [
                    "appId", T<string>
                    "autoLogAppEvents", T<bool>
                    "xfbml", T<bool>
                    "version", T<string>
                    "locale", T<string>
                ]
            }

        let LoginOptions = 
            Pattern.Config "LoginOptions" {
                Required = []
                Optional = ["permissions", !| T<string>]
            }

        let ProfileOptions = 
            Pattern.Config "ProfileOptions" {
                Required = []
                Optional = [
                    "fields", !| T<string>
                ]
            }

        let FacebookLoginPlugin =
            Class "FacebookLoginPlugin"
            |+> Instance [  
                "initialize" => FacebookConfiguration?options ^-> T<Promise<unit>>
                "login" => LoginOptions?options ^-> T<Promise<_>>[FacebookLoginResponse]
                "logout" => T<unit> ^-> T<Promise<unit>>
                "reauthorize" => T<unit> ^-> T<Promise<_>>[FacebookLoginResponse]
                "getCurrentAccessToken" => T<unit> ^-> T<Promise<_>>[FacebookCurrentAccessTokenResponse]
                Generic - fun t ->
                    "getProfile" => ProfileOptions?options ^-> T<Promise<_>>[t]
                "logEvent" => T<string>?options ^-> T<Promise<unit>>
                "setAutoLogAppEventsEnabled" => T<bool>?options ^-> T<Promise<unit>>
                "setAdvertiserTrackingEnabled" => T<bool>?options ^-> T<Promise<unit>>
                "setAdvertiserIDCollectionEnabled" => T<bool>?options ^-> T<Promise<unit>>
            ]

    [<AutoOpen>]
    module Stripe = 
        let ApplePayEventsEnum =
            Pattern.EnumInlines "ApplePayEventsEnum" [
                "Loaded", "applePayLoaded"
                "FailedToLoad", "applePayFailedToLoad"
                "Completed", "applePayCompleted"
                "Canceled", "applePayCanceled"
                "Failed", "applePayFailed"
                "DidSelectShippingContact", "applePayDidSelectShippingContact"
                "DidCreatePaymentMethod", "applePayDidCreatePaymentMethod"
            ]

        let GooglePayEventsEnum =
            Pattern.EnumInlines "GooglePayEventsEnum" [
                "Loaded", "googlePayLoaded"
                "FailedToLoad", "googlePayFailedToLoad"
                "Completed", "googlePayCompleted"
                "Canceled", "googlePayCanceled"
                "Failed", "googlePayFailed"
            ]

        let PaymentFlowEventsEnum =
            Pattern.EnumInlines "PaymentFlowEventsEnum" [
                "Loaded", "paymentFlowLoaded"
                "FailedToLoad", "paymentFlowFailedToLoad"
                "Opened", "paymentFlowOpened"
                "Created", "paymentFlowCreated"
                "Completed", "paymentFlowCompleted"
                "Canceled", "paymentFlowCanceled"
                "Failed", "paymentFlowFailed"
            ]

        let PaymentSheetEventsEnum =
            Pattern.EnumInlines "PaymentSheetEventsEnum" [
                "Loaded", "paymentSheetLoaded"
                "FailedToLoad", "paymentSheetFailedToLoad"
                "Completed", "paymentSheetCompleted"
                "Canceled", "paymentSheetCanceled"
                "Failed", "paymentSheetFailed"
            ]

        let ApplePayResultInterface = 
            Pattern.EnumInlines "ApplePayResultInterface" [
                "Completed", "applePayCompleted"
                "Canceled", "applePayCanceled"
                "Failed", "applePayFailed"
                "DidSelectShippingContact", "applePayDidSelectShippingContact"
                "DidCreatePaymentMethod", "applePayDidCreatePaymentMethod"
            ]

        let GooglePayResultInterface = 
            Pattern.EnumInlines "GooglePayResultInterface" [
                "Completed", "googlePayCompleted"
                "Canceled", "googlePayCanceled"
                "Failed", "googlePayFailed"
            ]

        let CollectionMode = 
            Pattern.EnumStrings "CollectionMode" [
                "automatic"
                "always"
            ]

        let AddressCollectionMode = 
            Pattern.EnumStrings "AddressCollectionMode" [
                "automatic"
                "full"
            ]

        let PaymentFlowResultInterface = 
            Pattern.EnumInlines "PaymentFlowResultInterface" [
                "Completed", "paymentFlowCompleted"
                "Canceled", "paymentFlowCanceled"
                "Failed", "paymentFlowFailed"
            ]

        let PaymentSheetResultInterface = 
            Pattern.EnumInlines "PaymentSheetResultInterface" [
                "Completed", "paymentSheetCompleted"
                "Canceled", "paymentSheetCanceled"
                "Failed", "paymentSheetFailed"
            ]

        let PaymentSummaryType = 
            Pattern.Config "PaymentSummaryType" {
                Required = []
                Optional = [
                    "label", T<string>
                    "amount", T<int>
                ]
            }

        let ShippingContactType = 
            Pattern.EnumStrings "ShippingContactType" [
                "postalAddress"
                "phoneNumber"
                "emailAddress"
                "name"
            ]

        let CreateApplePayOption = 
            Pattern.Config "CreateApplePayOption" {
                Required = []
                Optional = [
                    "paymentIntentClientSecret", T<string>
                    "paymentSummaryItems", !| PaymentSummaryType
                    "merchantIdentifier", T<string>
                    "countryCode", T<string>
                    "currency", T<string>
                    "requiredShippingContactFields", !| ShippingContactType
                    "allowedCountries", !| T<string>
                    "allowedCountriesErrorDescription", T<string>
                ]
            }

        let ShippingContact = 
            Pattern.Config "ShippingContact" {
                Required = []
                Optional = [
                    "givenName", T<string>
                    "familyName", T<string>
                    "middleName", T<string>
                    "namePrefix", T<string>
                    "nameSuffix", T<string>
                    "nameFormatted", T<string>
                    "phoneNumber", T<string>
                    "nickname", T<string>
                    "street", T<string>
                    "city", T<string>
                    "state", T<string>
                    "postalCode", T<string>
                    "country", T<string>
                    "isoCountryCode", T<string>
                    "subAdministrativeArea", T<string>
                    "subLocality", T<string>
                ]
            }

        let DidSelectShippingContact = 
            Pattern.Config "DidSelectShippingContact" {
                Required = []
                Optional = [
                    "contact", ShippingContact.Type
                ]
            }

        let CreateGooglePayOption = 
            Pattern.Config "CreateGooglePayOption" {
                Required = []
                Optional = [
                    "paymentIntentClientSecret", T<string>
                    "paymentSummaryItems", PaymentSummaryType.Type
                    "merchantIdentifier", T<string>
                    "countryCode", T<string>
                    "currency", T<string>
                ]
            }

        let StyleType = 
            Pattern.EnumStrings "StyleType" [
                "alwaysLight"
                "alwaysDark"
            ]

        let BillingDetailsCollectionConfiguration = 
            Pattern.Config "BillingDetailsCollectionConfiguration" {
                Required = []
                Optional = [
                    "email", CollectionMode.Type
                    "name", CollectionMode.Type
                    "phone", CollectionMode.Type
                    "address", AddressCollectionMode.Type
                ]
            }

        let CreatePaymentFlowOption = 
            Pattern.Config "CreatePaymentFlowOption" {
                Required = []
                Optional = [
                    "paymentIntentClientSecret", T<string>
                    "setupIntentClientSecret", T<string>
                    "billingDetailsCollectionConfiguration", BillingDetailsCollectionConfiguration.Type
                    "customerEphemeralKeySecret", T<string>
                    "customerId", T<string>
                    "enableApplePay", T<bool>
                    "applePayMerchantId", T<string>
                    "enableGooglePay", T<bool>
                    "GooglePayIsTesting", T<bool>
                    "countryCode", T<string>
                    "merchantDisplayName", T<string>
                    "returnURL", T<string>
                    "style", StyleType.Type
                    "withZipCode", T<bool>
                ]
            }

        let CreatePaymentSheetOption = 
            Pattern.Config "CreatePaymentSheetOption" {
                Required = []
                Optional = [
                    "paymentIntentClientSecret", T<string>
                    "setupIntentClientSecret", T<string>
                    "billingDetailsCollectionConfiguration", BillingDetailsCollectionConfiguration.Type
                    "customerEphemeralKeySecret", T<string>
                    "customerId", T<string>
                    "enableApplePay", T<bool>
                    "applePayMerchantId", T<string>
                    "enableGooglePay", T<bool>
                    "GooglePayIsTesting", T<bool>
                    "countryCode", T<string>
                    "merchantDisplayName", T<string>
                    "returnURL", T<string>
                    "style", StyleType.Type
                    "withZipCode", T<bool>
                ]
            }

        let StripeInitializationOptions = 
            Pattern.Config "StripeInitializationOptions" {
                Required = []
                Optional = [
                    "publishableKey", T<string>
                    "stripeAccount", T<string>
                ]
            }

        let StripeURLHandlingOptions = 
            Pattern.Config "StripeURLHandlingOptions" {
                Required = []
                Optional = [
                    "url", T<string>
                ]
            }

        let PresentApplePayResult = 
            Pattern.Config "PresentApplePayResult" {
                    Required = [
                        "paymentResult", ApplePayResultInterface.Type
                    ]
                    Optional = []
                }

        let PresentGooglePayResult = 
            Pattern.Config "PresentGooglePayResult" {
                Required = [
                    "paymentResult", GooglePayResultInterface.Type
                ]
                Optional = []
            }

        let PresentPaymentFlowResult = 
            Pattern.Config "PresentPaymentFlowResult" {
                Required = [
                    "cardNumber", T<string>
                ]
                Optional = []
            }

        let ConfirmPaymentFlowResult = 
            Pattern.Config "ConfirmPaymentFlowResult" {
                Required = [
                    "paymentResult", PaymentFlowResultInterface.Type
                ]
                Optional = []
        }

        let PresentPaymentSheetResult = 
            Pattern.Config "PresentPaymentSheetResult" {
                Required = [
                    "paymentResult", PaymentSheetResultInterface.Type
                ]
                Optional = []
            }

        let StripePlugin = 
            Class "StripePlugin" 
            |+> Instance [
                "initialize" => StripeInitializationOptions?opts ^-> T<Promise<unit>>
                "handleURLCallback" => StripeURLHandlingOptions?opts ^-> T<Promise<unit>>
                "isApplePayAvailable" => T<unit> ^-> T<Promise<unit>>
                "createApplePay" => CreateApplePayOption?options ^-> T<Promise<unit>>
                "presentApplePay" => T<unit> ^-> T<Promise<_>>[PresentApplePayResult]
                "addListener" => ApplePayEventsEnum?eventName * (T<unit> ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen 'applePayLoaded', 'applePayCompleted' and 'applePayCanceled' events"
                "addListener" => ApplePayEventsEnum?eventName * (T<string> ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen 'applePayFailedToLoad' and 'applePayFailed' events"
                "addListener" => ApplePayEventsEnum?eventName * (DidSelectShippingContact ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen 'applePayDidSelectShippingContact' and 'applePayDidCreatePaymentMethod' events"

                "isGooglePayAvailable" => T<unit> ^-> T<Promise<unit>>
                "createGooglePay" => CreateGooglePayOption?options ^-> T<Promise<unit>>
                "presentGooglePay" => T<unit> ^-> T<Promise<_>>[PresentGooglePayResult]
                "addListener" => GooglePayEventsEnum?eventName * (T<unit> ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen 'googlePayLoaded', 'googlePayCompleted' and 'googlePayCanceled' events"
                "addListener" => GooglePayEventsEnum?eventName * (T<string> ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen 'googlePayFailedToLoad' and 'googlePayFailed' events"

                "createPaymentFlow" => CreatePaymentFlowOption?options ^-> T<Promise<unit>>
                "presentPaymentFlow" => T<unit> ^-> T<Promise<_>>[PresentPaymentFlowResult]
                "confirmPaymentFlow" => T<unit> ^-> T<Promise<_>>[ConfirmPaymentFlowResult]
                "addListener" => PaymentFlowEventsEnum?eventName * (T<unit> ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen 'paymentFlowLoaded', 'paymentFlowOpened', 'paymentFlowCompleted', and 'paymentFlowCanceled' events"
                "addListener" => PaymentFlowEventsEnum?eventName * (T<string> ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen 'paymentFlowFailedToLoad' and 'paymentFlowFailed' events"
                "addListener" => PaymentFlowEventsEnum?eventName * (PresentPaymentFlowResult ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen 'paymentFlowCreated' events"

                "createPaymentSheet" => CreatePaymentSheetOption?options ^-> T<Promise<unit>>
                "presentPaymentSheet" => T<unit> ^-> T<Promise<_>>[PresentPaymentSheetResult]
                "addListener" => PaymentSheetEventsEnum?eventName * (T<unit> ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen 'paymentSheetLoaded', 'paymentSheetCompleted', and 'paymentSheetCanceled' events"
                "addListener" => PaymentSheetEventsEnum?eventName * (T<string> ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen 'paymentSheetFailedToLoad' and 'paymentSheetFailed' events"
            ]

        let CapacitorStripeContext = 
            Pattern.Config "CapacitorStripeContext" {
                Required = []
                Optional = [
                    "stripe", StripePlugin.Type
                    "isApplePayAvailable", T<bool>
                    "isGooglePayAvailable", T<bool>
                ]
            }

    [<AutoOpen>]
    module PrivacyScreen = 
        let ContentMode = 
            Pattern.EnumStrings "ContentMode" [
                "center"
                "scaleToFill"
                "scaleAspectFit"
                "scaleAspectFill"
            ]

        let PrivacyScreenConfig = 
            Pattern.Config "PrivacyScreenConfig" {
                Required = []
                Optional = [
                    "enable", T<bool> 
                    "imageName", T<string>
                    "contentMode", ContentMode.Type
                    "preventScreenshots", T<bool>
                ]
            }

        let PrivacyScreenPlugin =
            Class "PrivacyScreenPlugin"
            |+> Instance [
                "enable" => T<unit> ^-> T<Promise<unit>>
                "disable" => T<unit> ^-> T<Promise<unit>>
                "addListener" => T<string>?eventName * (T<unit> ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen to 'screenRecordingStarted', 'screenRecordingStopped', 'screenshotTaken' events"
                "removeAllListeners" => T<unit> ^-> T<Promise<unit>>
            ]

        let PluginsConfig =
            Pattern.Config "PluginsConfig" {
                Required = []
                Optional = [
                    "privacyScreen", PrivacyScreenConfig.Type
                ]
            }

    [<AutoOpen>]
    module KeepAwake = 
        let IsSupportedResult =
            Pattern.Config "IsSupportedResult" {
                Required = []
                Optional = ["isSupported", T<bool>]
            }

        let IsKeptAwakeResult =
            Pattern.Config "IsKeptAwakeResult" {
                Required = []
                Optional = ["isKeptAwake", T<bool>]
            }

        let KeepAwakePlugin =
            Class "KeepAwakePlugin"
            |+> Instance [
                "keepAwake" => T<unit> ^-> T<Promise<unit>>
                "allowSleep" => T<unit> ^-> T<Promise<unit>>
                "isSupported" => T<unit> ^-> T<Promise<_>>[IsSupportedResult]
                "isKeptAwake" => T<unit> ^-> T<Promise<_>>[IsKeptAwakeResult]
            ]

    [<AutoOpen>]
    module GoogleMaps = 
        let CameraMovementReason = 
            Pattern.EnumInlines "CameraMovementReason" [
                "Gesture", "1"
                "Other", "2"
            ]

        let MapType = 
            Pattern.EnumInlines "MapType" [
                "None", "0"
                "Normal", "1"
                "Satellite", "2"
                "Terrain", "3"
                "Hybrid", "4"
            ]

        let CallbackID = T<string> 

        let InitializeOptions = 
            Pattern.Config "InitializeOptions" {
                Required = []
                Optional = [
                    "devicePixelRatio", T<int>
                    "key", T<string>
                ]
            }

        let LatLng = 
            Pattern.Config "LatLng" {
                Required = []
                Optional = [
                    "latitude", T<int>
                    "longitude", T<int>
                ]
            }

        let CameraPosition = 
            Pattern.Config "CameraPosition" {
                Required = []
                Optional = [
                    "target", LatLng.Type
                    "bearing", T<int>
                    "tilt", T<int>
                    "zoom", T<int>
                ]
            }

        let MapGestures = 
            Pattern.Config "MapGestures" {
                Required = []
                Optional = [
                    "isRotateAllowed", T<bool>
                    "isScrollAllowed", T<bool>
                    "isScrollAllowedDuringRotateOrZoom", T<bool>
                    "isTiltAllowed", T<bool>
                    "isZoomAllowed", T<bool>
                ]
            }

        let MapControls = 
            Pattern.Config "MapControls" {
                Required = []
                Optional = [
                    "isCompassButtonEnabled", T<bool>
                    "isMapToolbarEnabled", T<bool>
                    "isMyLocationButtonEnabled", T<bool>
                    "isZoomButtonsEnabled", T<bool>
                ]
            }

        let MapAppearance = 
            Pattern.Config "MapAppearance" {
                Required = []
                Optional = [
                    "type", MapType.Type
                    "style", T<string>
                    "isBuildingsShown", T<bool>
                    "isIndoorShown", T<bool>
                    "isMyLocationDotShown", T<bool>
                    "isTrafficShown", T<bool>
                ]
            }

        let MapPreferences = 
            Pattern.Config "MapPreferences" {
                Required = []
                Optional = [
                    "gestures", MapGestures.Type
                    "controls", MapControls.Type
                    "appearance", MapAppearance.Type
                    "maxZoom", T<int>
                    "minZoom", T<int>
                    "padding", T<obj>
                    "liteMode", T<bool>
                ]
            }

        let GoogleMap =     
            Pattern.Config "GoogleMap" {
                Required = []
                Optional = [
                    "mapId", T<string>
                    "cameraPosition", CameraPosition.Type
                    "preferences", MapPreferences.Type
                ]
            }

        let BoundingRect = 
            Pattern.Config "BoundingRect" {
                Required = []
                Optional = [
                    "width", T<int>
                    "height", T<int>
                    "x", T<int>
                    "y", T<int>
                ]
            }

        let CreateMapOptions = 
            Pattern.Config "CreateMapOptions" {
                Required = []
                Optional = [
                    "element", T<HTMLElement>
                    "boundingRect", BoundingRect.Type
                    "cameraPosition", CameraPosition.Type
                    "preferences", MapPreferences.Type
                ]
            }

        let CreateMapResult = 
            Pattern.Config "CreateMapResult" {
                Required = []
                Optional = [
                    "googleMap", GoogleMap.Type
                ]
            }

        let UpdateMapOptions = 
            Pattern.Config "UpdateMapOptions" {
                Required = []
                Optional = [
                    "mapId", T<string>
                    "element", T<HTMLElement>
                    "boundingRect", BoundingRect.Type
                    "preferences", MapPreferences.Type
                ]
            }

        let UpdateMapResult = 
            Pattern.Config "UpdateMapResult" {
                Required = []
                Optional = [
                    "googleMap", GoogleMap.Type
                ]
            }

        let RemoveMapOptions = 
            Pattern.Config "RemoveMapOptions" {
                Required = []
                Optional = [
                    "mapId", T<string>
                ]
            }

        let ClearMapOptions = 
            Pattern.Config "ClearMapOptions" {
                Required = []
                Optional = [
                    "mapId", T<string>
                ]
            }

        let MoveCameraOptions = 
            Pattern.Config "MoveCameraOptions" {
                Required = []
                Optional = [
                    "mapId", T<string>
                    "cameraPosition", CameraPosition.Type
                    "duration", T<int>
                    "useCurrentCameraPositionAsBase", T<bool>
                ]
            }

        let Anchor = 
            Pattern.Config "Anchor" {
                Required = []
                Optional = [
                    "x", T<int>
                    "y", T<int>
                ]
            }

        let MarkerIconSize = 
            Pattern.Config "MarkerIconSize" {
                Required = []
                Optional = [
                    "width", T<int>
                    "height", T<int>
                ]
            }

        let MarkerIcon = 
            Pattern.Config "MarkerIcon" {
                Required = []
                Optional = [
                    "url", T<int>
                    "size", MarkerIconSize.Type
                ]
            }

        let MarkerPreferences = 
            Pattern.Config "MarkerPreferences" {
                Required = []
                Optional = [
                    "title", T<string>
                    "snippet", T<string>
                    "opacity", T<int>
                    "isFlat", T<bool>
                    "isDraggable", T<bool>
                    "zIndex", T<int>
                    "anchor", Anchor.Type
                    "icon", MarkerIcon.Type
                    "metadata", T<obj>
                ]
            }

        let AddMarkerOptions = 
            Pattern.Config "AddMarkerOptions" {
                Required = []
                Optional = [
                    "mapId", T<string>
                    "position", LatLng.Type
                    "preferences", MarkerPreferences.Type
                ]
            }

        let Marker = 
            Pattern.Config "Marker" {
                Required = []
                Optional = [
                    "mapId", T<string>
                    "markerId", T<string>
                    "position", LatLng.Type
                    "preferences", MarkerPreferences.Type
                ]
            }

        let AddMarkerResult = 
            Pattern.Config "AddMarkerResult" {
                Required = []
                Optional = [
                    "marker", Marker.Type
                ]
            }

        let MarkerOutputEntry = 
            Pattern.Config "MarkerOutputEntry" {
                Required = []
                Optional = [
                    "markerId", T<string>
                    "position", LatLng.Type
                    "preferences", MarkerPreferences.Type
                ]
            }

        let AddMarkersResult = 
            Pattern.Config "AddMarkersResult" {
                Required = []
                Optional = [
                    "mapId", T<string>
                    "markers", !| MarkerOutputEntry
                ]
            }

        let MarkerInputEntry = 
            Pattern.Config "MarkerInputEntry" {
                Required = []
                Optional = [
                    "position", LatLng.Type
                    "preferences", MarkerPreferences.Type
                ]
            }

        let AddMarkersOptions = 
            Pattern.Config "AddMarkersOptions" {
                Required = []
                Optional = [
                    "mapId", T<string>
                    "markers", !| MarkerInputEntry
                ]
            }

        let RemoveMarkerOptions = 
            Pattern.Config "RemoveMarkerOptions" {
                Required = []
                Optional = [
                    "mapId", T<string>
                    "markerId", T<string>
                ]
            }

        let DefaultEventOptions = 
            Pattern.Config "DefaultEventOptions" {
                Required = []
                Optional = ["mapId", T<string>]
            }

        let DefaultEventWithPreventDefaultOptions = 
            Pattern.Config "DefaultEventWithPreventDefaultOptions" {
                Required = []
                Optional = [
                    "preventDefault", T<bool>
                    "mapId", T<string>
                ]
            }

        let Point = 
            Pattern.Config "Point" {
                Required = []
                Optional = ["x", T<int>; "y", T<int>]
            }

        let DidRequestElementFromPointResult = 
            Pattern.Config "DidRequestElementFromPointResult" {
                Required = []
                Optional = [
                    "point", Point.Type
                    "eventChainId", T<string>
                ]
            }

        let DidTapInfoWindowResult = 
            Pattern.Config "DidTapInfoWindowResult" {
                Required = []
                Optional = [
                    "marker", Marker.Type
                ]
            }

        let DidCloseInfoWindowResult = 
            Pattern.Config "DidCloseInfoWindowResult" {
                Required = []
                Optional = [
                    "marker", Marker.Type
                ]
            }

        let DidTapMapResult = 
            Pattern.Config "DidTapMapResult" {
                Required = []
                Optional = [
                    "position", LatLng.Type
                ]
            }

        let DidLongPressMapResult = 
            Pattern.Config "DidLongPressMapResult" {
                Required = []
                Optional = [
                    "position", LatLng.Type
                ]
            }

        let DidTapMarkerResult = 
            Pattern.Config "DidTapMarkerResult" {
                Required = []
                Optional = [
                    "marker", Marker.Type
                ]
            }

        let DidBeginDraggingMarkerResult = 
            Pattern.Config "DidBeginDraggingMarkerResult" {
                Required = []
                Optional = [
                    "marker", Marker.Type
                ]
            }

        let DidDragMarkerResult = 
            Pattern.Config "DidDragMarkerResult" {
                Required = []
                Optional = [
                    "marker", Marker.Type
                ]
            }

        let DidEndDraggingMarkerResult = 
            Pattern.Config "DidEndDraggingMarkerResult" {
                Required = []
                Optional = [
                    "marker", Marker.Type
                ]
            }

        let DidTapMyLocationDotResult = 
            Pattern.Config "DidTapMyLocationDotResult" {
                Required = []
                Optional = [
                    "position", LatLng.Type
                ]
            }

        let PointOfInterest = 
            Pattern.Config "PointOfInterest" {
                Required = []
                Optional = [
                    "name", T<String>
                    "placeId", T<String>
                    "position", LatLng.Type
                ]
            }

        let DidTapPoiResult = 
            Pattern.Config "DidTapPoiResult" {
                Required = []
                Optional = [
                    "poi", PointOfInterest.Type
                ]
            }

        let DidBeginMovingCameraResult = 
            Pattern.Config "DidBeginMovingCameraResult" {
                Required = []
                Optional = [
                    "reason", CameraMovementReason.Type
                ]
            }

        let DidEndMovingCameraResult = 
            Pattern.Config "DidEndMovingCameraResult" {
                Required = []
                Optional = [
                    "cameraPosition", CameraPosition.Type
                ]
            }
    
        let DidTapInfoWindowCallback = DidTapInfoWindowResult?result * !?T<obj>?err ^-> T<unit>

        let DidCloseInfoWindowCallback = DidCloseInfoWindowResult?result * !?T<obj>?err ^-> T<unit>

        let DidTapMapCallback = DidTapMapResult?result * !?T<obj>?err ^-> T<unit>

        let DidLongPressMapCallback = DidLongPressMapResult?result * !?T<obj>?err ^-> T<unit>

        let DidTapMarkerCallback = DidTapMarkerResult?result * !?T<obj>?err ^-> T<unit>

        let DidBeginDraggingMarkerCallback = DidBeginDraggingMarkerResult?result * !?T<obj>?err ^-> T<unit>

        let DidDragMarkerCallback = DidDragMarkerResult?result * !?T<obj>?err ^-> T<unit>

        let DidEndDraggingMarkerCallback = DidEndDraggingMarkerResult?result * !?T<obj>?err ^-> T<unit>

        let DidTapMyLocationButtonCallback = T<unit>?result * !?T<obj>?err ^-> T<unit>

        let DidTapMyLocationDotCallback = DidTapMyLocationDotResult?result * !?T<obj>?err ^-> T<unit>

        let DidTapPoiCallback = DidTapPoiResult?result * !?T<obj>?err ^-> T<unit>

        let DidBeginMovingCameraCallback = DidBeginMovingCameraResult?result * !?T<obj>?err ^-> T<unit>

        let DidMoveCameraCallback = T<unit>?result * !?T<obj>?err ^-> T<unit>

        let DidEndMovingCameraCallback = DidEndMovingCameraResult?result * !?T<obj>?err ^-> T<unit>

        let ElementFromPointResultOptions = 
            Pattern.Config "ElementFromPointResultOptions" {
                Required = []
                Optional = [
                    "eventChainId", T<string>
                    "mapId", T<string>
                    "isSameNode", T<string>
                ]
            }

        let GoogleMapsPlugin =
            Class "GoogleMapsPlugin"
            |+> Instance [
                "initialize" => InitializeOptions?options ^-> T<Promise<unit>>
                "createMap" => CreateMapOptions?options ^-> T<Promise<_>>[CreateMapResult]
                "updateMap" => UpdateMapOptions?options ^-> T<Promise<_>>[UpdateMapResult]
                "removeMap" => RemoveMapOptions?options ^-> T<Promise<unit>>
                "clearMap" => ClearMapOptions?options ^-> T<Promise<unit>>
                "moveCamera" => MoveCameraOptions?options ^-> T<Promise<unit>>
                "addMarker" => AddMarkerOptions?options ^-> T<Promise<_>>[AddMarkerResult]
                "addMarkers" => AddMarkersOptions?options ^-> T<Promise<_>>[AddMarkersResult]
                "removeMarker" => RemoveMarkerOptions?options ^-> T<Promise<unit>>
                "didTapInfoWindow" => DefaultEventOptions?options * DidTapInfoWindowCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didCloseInfoWindow" => DefaultEventOptions?options * DidCloseInfoWindowCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didTapMap" => DefaultEventOptions?options * DidTapMapCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didLongPressMap" => DefaultEventOptions?options * DidLongPressMapCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didTapMarker" => DefaultEventWithPreventDefaultOptions?options * DidTapMarkerCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didBeginDraggingMarker" => DefaultEventOptions?options * DidBeginDraggingMarkerCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didDragMarker" => DefaultEventOptions?options * DidDragMarkerCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didEndDraggingMarker" => DefaultEventOptions?options * DidEndDraggingMarkerCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didTapMyLocationButton" => DefaultEventWithPreventDefaultOptions?options * DidTapMyLocationButtonCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didTapMyLocationDot" => DefaultEventOptions?options * DidTapMyLocationDotCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didTapPoi" => DefaultEventOptions?options * DidTapPoiCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didBeginMovingCamera" => DefaultEventOptions?options * DidBeginMovingCameraCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didMoveCamera" => DefaultEventOptions?options * DidMoveCameraCallback?callback ^-> T<Promise<_>>[CallbackID]
                "didEndMovingCamera" => DefaultEventOptions?options * DidEndMovingCameraCallback?callback ^-> T<Promise<_>>[CallbackID]
                "elementFromPointResult" => ElementFromPointResultOptions?options ^-> T<Promise<unit>>
                "addListener" => T<string>?eventName * (DidRequestElementFromPointResult ^-> T<unit>)?listenerFunc ^-> PluginListenerHandle
                |> WithComment "Listen to 'didRequestElementFromPoint' event"
                "addListener" => T<string>?eventName * (!| T<obj> ^-> T<obj>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                "removeAllListener" => T<unit> ^-> T<Promise<unit>>
            ]

    [<AutoOpen>]
    module Contacts = 
        let PhoneType =
            Pattern.EnumInlines "PhoneType" [
                "Home", "home"
                "Work", "work"
                "Other", "other"
                "Custom", "custom"
                "Mobile", "mobile"
                "FaxWork", "fax_work"
                "FaxHome", "fax_home"
                "Pager", "pager"
                "Callback", "callback"
                "Car", "car"
                "CompanyMain", "company_main"
                "Isdn", "isdn"
                "Main", "main"
                "OtherFax", "other_fax"
                "Radio", "radio"
                "Telex", "telex"
                "TtyTdd", "tty_tdd"
                "WorkMobile", "work_mobile"
                "WorkPager", "work_pager"
                "Assistant", "assistant"
                "Mms", "mms"
            ]

        let EmailType =
            Pattern.EnumInlines "EmailType" [
                "Home", "home"
                "Work", "work"
                "Other", "other"
                "Custom", "custom"
                "Mobile", "mobile"
            ]

        let PostalAddressType =
            Pattern.EnumInlines "PostalAddressType" [
                "Home", "home"
                "Work", "work"
                "Other", "other"
                "Custom", "custom"
            ]

        let PermissionStatus = 
            Pattern.Config "PermissionStatus" {
                Required = []
                Optional = [
                    "contacts", PermissionState.Type
                ]
            }

        let NamePayload = 
            Pattern.Config "NamePayload" {
                Required = []
                Optional = [
                    "display", T<string>
                    "given", T<string>
                    "middle", T<string>
                    "family", T<string>
                    "prefix", T<string>
                    "suffix", T<string>
                ]
            }

        let OrganizationPayload = 
            Pattern.Config "OrganizationPayload" {
                Required = []
                Optional = [
                    "company", T<string>
                    "jobTitle", T<string>
                    "department", T<string>
                ]
            }

        let BirthdayPayload = 
            Pattern.Config "irthdayPayload" {
                Required = []
                Optional = [
                    "day", T<int>
                    "month", T<int>
                    "year", T<int>
                ]
            }

        let PhonePayload = 
            Pattern.Config "PhonePayload" {
                Required = []
                Optional = [
                    "type", PhoneType.Type
                    "label", T<string>
                    "isPrimary", T<bool>
                    "number", T<string>
                ]
            }

        let EmailPayload = 
            Pattern.Config "EmailPayload" {
                Required = []
                Optional = [
                    "type", EmailType.Type
                    "label", T<string>
                    "isPrimary", T<bool>
                    "address", T<string>
                ]
            }

        let PostalAddressPayload = 
            Pattern.Config "PostalAddressPayload" {
                Required = []
                Optional = [
                    "type", PostalAddressType.Type
                    "label", T<string>
                    "isPrimary", T<bool>
                    "street", T<string>
                    "neighborhood", T<string>
                    "city", T<string>
                    "region", T<string>
                    "postcode", T<string>
                    "country", T<string>
                ]
            }

        let ImagePayload = 
            Pattern.Config "ImagePayload" {
                Required = []
                Optional = [
                    "base64String", T<string>
                ]
            }

        let ContactPayload = 
            Pattern.Config "ContactPayload" {
                Required = []
                Optional = [
                    "contactId", T<string>
                    "name", NamePayload.Type
                    "organization", OrganizationPayload.Type
                    "birthday", BirthdayPayload.Type
                    "note", T<string>
                    "phones", !| PhonePayload
                    "emails", !| EmailPayload
                    "urls", !| T<string>
                    "postalAddresses", !| PostalAddressPayload
                    "image", ImagePayload.Type
                ]
            }

        let Projection = 
            Pattern.Config "Projection" {
                Required = []
                Optional = [
                    "name", T<bool>
                    "organization", T<bool>
                    "birthday", T<bool>
                    "note", T<bool>
                    "phones", T<bool>
                    "emails", T<bool>
                    "urls", T<bool>
                    "postalAddresses", T<bool>
                    "image", T<bool>
                ]
            }

        let GetContactResult = 
            Pattern.Config "GetContactResult" {
                Required = []
                Optional = [
                    "contact", ContactPayload.Type
                ]
            }

        let GetContactOptions = 
            Pattern.Config "GetContactOptions" {
                Required = []
                Optional = [
                    "contactId", T<string>
                    "projection", Projection.Type
                ]
            }

        let GetContactsResult = 
            Pattern.Config "GetContactsResult" {
                Required = []
                Optional = [
                    "contact", !| ContactPayload
                ]
            }

        let GetContactsOptions = 
            Pattern.Config "GetContactsOptions" {
                Required = []
                Optional = [
                    "projection", Projection.Type
                ]
            }

        let CreateContactResult = 
            Pattern.Config "CreateContactResult" {
                Required = []
                Optional = [
                    "contactId", T<string>
                ]
            }

        let NameInput = 
            Pattern.Config "NameInput" {
                Required = []
                Optional = [
                    "given", T<string>
                    "middle", T<string>
                    "family", T<string>
                    "prefix", T<string>
                    "suffix", T<string>
                ]
            }

        let OrganizationInput = 
            Pattern.Config "OrganizationInput" {
                Required = []
                Optional = [
                    "company", T<string>
                    "jobTitle", T<string>
                    "department", T<string>
                ]
            }

        let BirthdayInput = 
            Pattern.Config "BirthdayInput" {
                Required = []
                Optional = [
                    "day", T<int>
                    "month", T<int>
                    "year", T<int>
                ]
            }

        let PhoneInput = 
            Pattern.Config "PhoneInput" {
                Required = []
                Optional = [
                    "type", PhoneType.Type
                    "label", T<string>
                    "isPrimary", T<bool>
                    "number", T<string>
                ]
            }

        let EmailInput = 
            Pattern.Config "EmailInput" {
                Required = []
                Optional = [
                    "type", EmailType.Type
                    "label", T<string>
                    "isPrimary", T<bool>
                    "address", T<string>
                ]
            }

        let PostalAddressInput = 
            Pattern.Config "PostalAddressInput" {
                Required = []
                Optional = [
                    "type", PostalAddressType.Type
                    "label", T<string>
                    "isPrimary", T<bool>
                    "street", T<string>
                    "neighborhood", T<string>
                    "city", T<string>
                    "region", T<string>
                    "postcode", T<string>
                    "country", T<string>
                ]
            }

        let ContactInput = 
            Pattern.Config "ContactInput" {
                Required = []
                Optional = [
                    "name", NameInput.Type
                    "organization", OrganizationInput.Type
                    "birthday", BirthdayInput.Type
                    "note", T<string>
                    "phones", !| PhoneInput
                    "coemailsntactId", !| EmailInput
                    "urls", !| T<string>
                    "postalAddresses", !| PostalAddressInput
                ]
            }

        let CreateContactOptions = 
            Pattern.Config "CreateContactOptions" {
                Required = []
                Optional = [
                    "contact", ContactInput.Type
                ]
            }

        let DeleteContactOptions = 
            Pattern.Config "DeleteContactOptions" {
                Required = []
                Optional = [
                    "contactId", T<string> 
                ]
            }

        let PickContactResult = 
            Pattern.Config "PickContactResult" {
                Required = []
                Optional = [
                    "contact", ContactPayload.Type
                ]
            }

        let PickContactOptions = 
            Pattern.Config "PickContactOptions" {
                Required = []
                Optional = [
                    "projection", Projection.Type
                ]
            }

        let ContactsPlugin =
            Class "ContactsPlugin"
            |+> Instance [
                "checkPermissions" => T<unit> ^-> T<Promise<_>>[PermissionStatus]
                "requestPermissions" => T<unit> ^-> T<Promise<_>>[PermissionStatus]
                "getContact" => GetContactOptions?options ^-> T<Promise<_>>[GetContactResult]
                "getContacts" => GetContactsOptions?options ^-> T<Promise<_>>[GetContactsResult]
                "createContact" => CreateContactOptions?options ^-> T<Promise<_>>[CreateContactResult]
                "deleteContact" => DeleteContactOptions?options ^-> T<Promise<unit>>
                "pickContact" => PickContactOptions?options ^-> T<Promise<_>>[PickContactResult]
            ]

    [<AutoOpen>]
    module DatePicker = 
        let DatePickerMode =
            Pattern.EnumStrings "DatePickerMode" [
                "time"
                "date"
                "dateAndTime"
                "countDownTimer"
            ]

        let DatePickerTheme =
            Pattern.EnumStrings "DatePickerTheme" [
                "light"
                "dark"
            ]

        let DatePickerIosStyle =
            Pattern.EnumStrings "DatePickerIosStyle" [
                "wheels"
                "inline"
            ]

        let DatePickerBaseOptions = 
            Pattern.Config "DatePickerBaseOptions" {
                Required = []
                Optional = [
                    "format", T<string>
                    "locale", T<string>
                    "mode", DatePickerMode.Type
                    "theme", DatePickerTheme.Type
                    "timezone", T<string>
                    "doneText", T<string>
                    "cancelText", T<string>
                    "is24h", T<bool>
                ]
            }

        let DatePickerIosOptions = 
            Pattern.Config "DatePickerIosOptions" {
                Required = []
                Optional = [
                    "style", DatePickerIosStyle.Type
                    "titleFontColor", T<string>
                    "titleBgColor", T<string>
                    "bgColor", T<string>
                    "fontColor", T<string>
                    "buttonBgColor", T<string>
                    "buttonFontColor", T<string>
                    "mergedDateAndTime", T<bool>
                ]
            }
            |=> Inherits DatePickerBaseOptions

        let DatePickerOptions = 
            Pattern.Config "DatePickerOptions" {
                Required = []
                Optional = [
                    "min", T<string>
                    "max", T<string>
                    "date", T<string>
                    "ios", DatePickerIosOptions.Type
                    "android", DatePickerBaseOptions.Type
                ]
            }
            |=> Inherits DatePickerBaseOptions
            

        let DatePickerResult = 
            Pattern.Config "DatePickerResult" {
                Required = []
                Optional = ["value", T<string>]
            }

        let DatePickerPlugin =
            Class "DatePickerPlugin"
            |+> Instance [
                "present" => DatePickerOptions?options ^-> T<Promise<_>>[DatePickerResult]
            ]

    [<AutoOpen>]
    module SQLite = 
        let capSQLiteOptions = 
            Pattern.Config "capSQLiteOptions" {
                Required = []
                Optional = [
                    "database", T<string>
                    "readonly", T<bool>
                ]
            }

        let capSQLiteLocalDiskOptions = 
            Pattern.Config "capSQLiteLocalDiskOptions" {
                Required = []
                Optional = [
                    "overwrite", T<bool>
                ]
            }

        let capSQLiteResult = 
            Pattern.Config "capSQLiteResult" {
                Required = []
                Optional = ["result", T<bool>]
            }

        let capSetSecretOptions = 
            Pattern.Config "capSetSecretOptions" {
                Required = []
                Optional = ["passphrase", T<string>]
            }

        let capChangeSecretOptions = 
            Pattern.Config "capChangeSecretOptions" {
                Required = []
                Optional = [
                    "passphrase", T<string>
                    "oldpassphrase", T<string>
                ]
            }

        let capConnectionOptions = 
            Pattern.Config "capConnectionOptions" {
                Required = []
                Optional = [
                    "database", T<string>
                    "version", T<int>
                    "encrypted", T<bool>
                    "mode", T<string>
                    "readonly", T<bool>
                ]
            }

        let capEchoResult = 
            Pattern.Config "capEchoResult" {
                Required = []
                Optional = ["value", T<string>]
            }

        let capEchoOptions = 
            Pattern.Config "capEchoOptions" {
                Required = []
                Optional = ["value", T<string>]
            }

        let Changes = 
            Pattern.Config "Changes" {
                Required = []
                Optional = [
                    "changes", T<int>
                    "lastId", T<int>
                    "values", !|T<obj>
                ]
            }

        let capSQLiteChanges = 
            Pattern.Config "capSQLiteChanges" {
                Required = []
                Optional = [
                    "changes", Changes.Type
                ]
            }

        let capSQLiteUrl = 
            Pattern.Config "capSQLiteUrl" {
                Required = []
                Optional = ["url", T<string>]
            }

        let capVersionResult = 
            Pattern.Config "capVersionResult" {
                Required = []
                Optional = ["version", T<int>]
            }     

        let capAllConnectionsOptions = 
            Pattern.Config "capAllConnectionsOptions" {
                Required = []
                Optional = [
                    "dbNames", !|T<string>
                    "openModes", !|T<string>
                ]
            }

        let capNCDatabasePathOptions = 
            Pattern.Config "capNCDatabasePathOptions" {
                Required = []
                Optional = [
                    "path", T<string>
                    "database", T<string>
                ]
            }

        let capNCConnectionOptions = 
            Pattern.Config "capNCConnectionOptions" {
                Required = []
                Optional = [
                    "databasePath", T<string>
                    "version", T<int>
                ]
            }

        let capNCOptions = 
            Pattern.Config "capNCOptions" {
                Required = []
                Optional = ["databasePath", T<string>]
            }

        let capSQLiteExecuteOptions = 
            Pattern.Config "capSQLiteExecuteOptions" {
                Required = []
                Optional = [
                    "database", T<string>
                    "statements", T<string>
                    "transaction", T<bool>
                    "readonly", T<bool>
                    "isSQL92", T<bool>
                ]
            }

        let capSQLiteSet = 
            Pattern.Config "capSQLiteSet" {
                Required = []
                Optional = [
                    "statements", T<string>
                    "values", !| T<obj>
                ]
            }

        let capSQLiteSetOptions = 
            Pattern.Config "capSQLiteSetOptions" {
                Required = []
                Optional = [
                    "database", T<string>
                    "set", !| capSQLiteSet
                    "transaction", T<bool>
                    "readonly", T<bool>
                    "returnMode", T<string>
                    "isSQL92", T<bool>
                ]
            }

        let capSQLiteRunOptions = 
            Pattern.Config "capSQLiteRunOptions" {
                Required = []
                Optional = [
                    "database", T<string>
                    "statement", T<string>
                    "values", !|T<obj>
                    "transaction", T<bool>
                    "readonly", T<bool>
                    "returnMode", T<string>
                    "isSQL92", T<bool>
                ]
            }

        let capSQLiteQueryOptions = 
            Pattern.Config "capSQLiteQueryOptions" {
                Required = []
                Optional = [
                    "database", T<string>
                    "statement", T<string>
                    "values", !|T<obj>
                    "readonly", T<bool>
                    "isSQL92", T<bool>
                ]
            }

        let capSQLiteVersionUpgrade = 
            Pattern.Config "capSQLiteVersionUpgrade" {
                Required = []
                Optional = [
                    "toVersion", T<int>
                    "statements", !|T<string>
                ]
            }

        let capSQLiteUpgradeOptions = 
            Pattern.Config "capSQLiteUpgradeOptions" {
                Required = []
                Optional = [
                    "database", T<string>
                    "upgrade", !| capSQLiteVersionUpgrade
                ]
            }

        let capSQLitePathOptions = 
            Pattern.Config "capSQLitePathOptions" {
                Required = []
                Optional = [
                    "folderPath", T<string>
                    "dbNameList", !|T<string>
                ]
            }

        let capSQLiteTableOptions = 
            Pattern.Config "capSQLiteTableOptions" {
                Required = []
                Optional = [
                    "database", T<string>
                    "table", T<string>
                    "readonly", T<bool>
                ]
            }

        let capNCDatabasePathResult = 
            Pattern.Config "capNCDatabasePathResult" {
                Required = []
                Optional = ["path", T<string>]
            }

        let capSQLiteValues = 
            Pattern.Config "capSQLiteValues" {
                Required = []
                Optional = ["values", !|T<obj>]
            }

        let JsonColumn = 
            Pattern.Config "JsonColumn" {
                Required = []
                Optional = [
                    "column", T<string>
                    "value", T<string>
                    "foreignkey", T<string>
                    "constraint", T<string>
                ]
            }

        let JsonTrigger = 
            Pattern.Config "JsonTrigger" {
                Required = []
                Optional = [
                    "condition", T<string>
                    "name", T<string>
                    "timeevent", T<string>
                    "logic", T<string>
                ]
            }

        let JsonIndex = 
            Pattern.Config "JsonIndex" {
                Required = []
                Optional = [
                    "mode", T<string>
                    "name", T<string>
                    "value", T<string>
                ]
            }

        let JsonTable = 
            Pattern.Config "JsonTable" {
                Required = []
                Optional = [
                    "name", T<string>
                    "schema", !| JsonColumn 
                    "indexes", !| JsonIndex 
                    "triggers", !| JsonTrigger 
                    "values", !| !| T<obj>
                ]
            }

        let JsonView = 
            Pattern.Config "JsonView" {
                Required = []
                Optional = [
                    "name", T<string>
                    "value", T<string>
                ]
            }

        let JsonSQLite = 
            Pattern.Config "JsonSQLite" {
                Required = []
                Optional = [
                    "database", T<string>
                    "version", T<int>
                    "overwrite", T<bool>
                    "encrypted", T<bool>
                    "mode", T<string>
                    "tables", !| JsonTable
                    "views", !| JsonView
                ]
            }

        let capSQLiteJson = 
            Pattern.Config "capSQLiteJson" {
                Required = []
                Optional = ["export", JsonSQLite.Type] 
            }

        let capSQLiteSyncDate = 
            Pattern.Config "capSQLiteSyncDate" {
                Required = []
                Optional = ["syncDate", T<int>]
            }

        let capSQLiteImportOptions = 
            Pattern.Config "capSQLiteImportOptions" {
                Required = []
                Optional = [
                    "jsonstring", T<string>
                ]
            }

        let capSQLiteExportOptions = 
            Pattern.Config "capSQLiteExportOptions" {
                Required = []
                Optional = [
                    "database", T<string>
                    "jsonexportmode", T<string>
                    "readonly", T<bool>
                    "encrypted", T<bool>
                ]
            }

        let capSQLiteSyncDateOptions = 
            Pattern.Config "capSQLiteSyncDateOptions" {
                Required = []
                Optional = [
                    "database", T<string>
                    "syncdate", T<string>
                    "readonly", T<bool>
                ]
            }

        let capSQLiteFromAssetsOptions = 
            Pattern.Config "capSQLiteFromAssetsOptions" {
                Required = []
                Optional = [
                    "overwrite", T<bool>
                ]
            }

        let capSQLiteHTTPOptions = 
            Pattern.Config "capSQLiteHTTPOptions" {
                Required = []
                Optional = [
                    "url", T<string>
                    "overwrite", T<bool>
                ]
            }

        let SQLitePlugin =
            Class "SQLitePlugin"
            |+> Instance [
                "initWebStore" => T<unit> ^-> T<Promise<unit>>
                "saveToStore" => capSQLiteOptions?options ^-> T<Promise<unit>>
                "getFromLocalDiskToStore" => capSQLiteLocalDiskOptions?options ^-> T<Promise<unit>>
                "saveToLocalDisk" => capSQLiteOptions?options ^-> T<Promise<unit>>
                "isSecretStored" => T<unit> ^-> T<Promise<_>>[capSQLiteResult]
                "setEncryptionSecret" => capSetSecretOptions?options ^-> T<Promise<unit>>
                "changeEncryptionSecret" => capChangeSecretOptions?options ^-> T<Promise<unit>>
                "clearEncryptionSecret" => T<unit> ^-> T<Promise<unit>>
                "checkEncryptionSecret" => capSetSecretOptions?options ^-> T<Promise<_>>[capSQLiteResult]
                "createConnection" => capConnectionOptions?options ^-> T<Promise<unit>>
                "closeConnection" => capSQLiteOptions?options ^-> T<Promise<unit>>
                "echo" => capEchoOptions?options ^-> T<Promise<_>>[capEchoResult]
                "open" => capSQLiteOptions?options ^-> T<Promise<unit>>
                "close" => capSQLiteOptions?options ^-> T<Promise<unit>>
                "beginTransaction" => capSQLiteOptions?options ^-> T<Promise<_>>[capSQLiteChanges]
                "commitTransaction" => capSQLiteOptions?options ^-> T<Promise<_>>[capSQLiteChanges]
                "rollbackTransaction" => capSQLiteOptions?options ^-> T<Promise<_>>[capSQLiteChanges]
                "isTransactionActive" => capSQLiteOptions?options ^-> T<Promise<_>>[capSQLiteResult]
                "getUrl" => capSQLiteOptions?options ^-> T<Promise<_>>[capSQLiteUrl]
                "getVersion" => capSQLiteOptions?options ^-> T<Promise<_>>[capVersionResult]
                "execute" => capSQLiteExecuteOptions?options ^-> T<Promise<_>>[capSQLiteChanges]
                "executeSet" => capSQLiteSetOptions?options ^-> T<Promise<_>>[capSQLiteChanges]
                "run" => capSQLiteRunOptions?options ^-> T<Promise<_>>[capSQLiteChanges]
                "query" => capSQLiteQueryOptions?options ^-> T<Promise<_>>[capSQLiteValues]
                "isDBExists" => capSQLiteOptions?options ^-> T<Promise<_>>[capSQLiteResult]
                "isDBOpen" => capSQLiteOptions?options ^-> T<Promise<_>>[capSQLiteResult]
                "isDatabaseEncrypted" => capSQLiteOptions?options ^-> T<Promise<_>>[capSQLiteResult]
                "isInConfigEncryption" => T<unit> ^-> T<Promise<_>>[capSQLiteResult]
                "isInConfigBiometricAuth" => T<unit> ^-> T<Promise<_>>[capSQLiteResult]
                "isDatabase" => capSQLiteOptions?options ^-> T<Promise<_>>[capSQLiteResult]
                "isTableExists" => capSQLiteTableOptions?options ^-> T<Promise<_>>[capSQLiteResult]
                "deleteDatabase" => capSQLiteOptions?options ^-> T<Promise<unit>>
                "isJsonValid" => capSQLiteImportOptions?options ^-> T<Promise<_>>[capSQLiteResult]
                "importFromJson" => capSQLiteImportOptions?options ^-> T<Promise<_>>[capSQLiteChanges]
                "exportToJson" => capSQLiteExportOptions?options ^-> T<Promise<_>>[capSQLiteJson]
                "createSyncTable" => capSQLiteOptions?options ^-> T<Promise<_>>[capSQLiteChanges]
                "setSyncDate" => capSQLiteSyncDateOptions?options ^-> T<Promise<unit>>
                "getSyncDate" => capSQLiteOptions?options ^-> T<Promise<_>>[capSQLiteSyncDate]
                "deleteExportedRows" => capSQLiteOptions?options ^-> T<Promise<unit>>
                "addUpgradeStatement" => capSQLiteUpgradeOptions?options ^-> T<Promise<unit>>
                "copyFromAssets" => capSQLiteFromAssetsOptions?options ^-> T<Promise<unit>>
                "getFromHTTPRequest" => capSQLiteHTTPOptions?options ^-> T<Promise<unit>>
                "getDatabaseList" => T<unit> ^-> T<Promise<_>>[capSQLiteValues]
                "getTableList" => capSQLiteOptions?options ^-> T<Promise<_>>[capSQLiteValues]
                "getMigratableDbList" => capSQLitePathOptions?options ^-> T<Promise<_>>[capSQLiteValues]
                "addSQLiteSuffix" => capSQLitePathOptions?options ^-> T<Promise<unit>>
                "deleteOldDatabases" => capSQLitePathOptions?options ^-> T<Promise<unit>>
                "moveDatabasesAndAddSuffix" => capSQLitePathOptions?options ^-> T<Promise<unit>>
                "checkConnectionsConsistency" => capAllConnectionsOptions?options ^-> T<Promise<_>>[capSQLiteResult]
                "getNCDatabasePath" => capNCDatabasePathOptions?options ^-> T<Promise<_>>[capNCDatabasePathResult]
                "createNCConnection" => capNCConnectionOptions?options ^-> T<Promise<unit>>
                "closeNCConnection" => capNCOptions?options ^-> T<Promise<unit>>
                "isNCDatabase" => capNCOptions?options ^-> T<Promise<_>>[capSQLiteResult]
            ]

    [<AutoOpen>]
    module ImageToText = 
        let ImageOrientation =
            Pattern.EnumStrings "ImageOrientation" [
                "UP"
                "DOWN"
                "LEFT"
                "RIGHT"
            ]

        let DetectTextFileOptions =
            Pattern.Config "DetectTextFileOptions" {
                Required = []
                Optional = [
                    "filename", T<string>
                    "orientation", ImageOrientation.Type
                ]
            }

        let DetectTextBase64Options =
            Pattern.Config "DetectTextBase64Options" {
                Required = []
                Optional = [
                    "base64", T<string>
                    "orientation", ImageOrientation.Type
                ]
            }

        let TextDetection =
            Pattern.Config "TextDetection" {
                Required = []
                Optional = [
                    "bottomLeft", !| (T<int> * T<int>) 
                    "bottomRight", !| (T<int> * T<int>) 
                    "topLeft", !| (T<int> * T<int>) 
                    "topRight", !| (T<int> * T<int>) 
                    "text", T<string>
                ]
            }

        let TextDetections =
            Pattern.Config "TextDetections" {
                Required = [] 
                Optional = ["textDetections", !|TextDetection] 
            }

        let ImageToTextPlugin =
            Class "ImageToTextPlugin"
            |+> Instance [
                "detectText" => (DetectTextFileOptions + DetectTextBase64Options)?options ^-> T<Promise<_>>[TextDetections]
            ]

    [<AutoOpen>]
    module FileOpener = 
        let ChooserPosition =
            Pattern.Config "ChooserPosition" {
                Required = []
                Optional = [
                    "x", T<int>
                    "y", T<int>
                ]
            }

        let FileOpenerOptions =
            Pattern.Config "FileOpenerOptions" {
                Required = []
                Optional = [
                    "filePath", T<string>
                    "contentType", T<string>
                    "openWithDefault", T<bool>
                    "chooserPosition", ChooserPosition.Type
                ]
            }

        let FileOpenerPlugin =
            Class "FileOpenerPlugin"
            |+> Instance [
                "open" => FileOpenerOptions?options ^-> T<Promise<unit>>
            ]

    [<AutoOpen>]
    module AppleSignIn = 
        let SignInWithAppleResponseData = 
            Pattern.Config "SignInWithAppleResponseData" {
                Required = []
                Optional = [
                    "user", T<string> + T<unit> 
                    "email", T<string> + T<unit>
                    "givenName", T<string> + T<unit>
                    "familyName", T<string> + T<unit>
                    "identityToken", T<string>
                    "authorizationCode", T<string>
                ]
            }
        
        let SignInWithAppleResponse =
            Pattern.Config "SignInWithAppleResponse" {
                Required = []
                Optional = [
                    "response", SignInWithAppleResponseData.Type
                ]
            }

        let SignInWithAppleOptions =
            Pattern.Config "SignInWithAppleOptions" {
                Required = []
                Optional = [
                    "clientId", T<string>
                    "redirectURI", T<string>
                    "scopes", T<string>
                    "state", T<string>
                    "nonce", T<string>
                ]
            }

        let AppleSignInPlugin =
            Class "AppleSignInPlugin"
            |+> Instance [
                "authorize" => !?SignInWithAppleOptions?options ^-> T<Promise<_>>[SignInWithAppleResponse]
            ]

    [<AutoOpen>]
    module BackgroundGeolocation = 
        let Location =
            Pattern.Config "Location" {
                Required = []
                Optional = [
                    "latitude", T<float>
                    "longitude", T<float>
                    "accuracy", T<float>
                    "simulated", T<bool>
                    "altitude", T<float> + T<unit> 
                    "altitudeAccuracy", T<float> + T<unit>
                    "bearing", T<float> + T<unit>
                    "speed", T<float> + T<unit>
                    "time", T<float> + T<unit>
                ]
            }

        let CallbackError =
            Pattern.Config "CallbackError" {
                Required = []
                Optional = [
                    "code", T<string>
                ]
            }
            |=> Inherits T<Error>

        let WatcherOptions =
            Pattern.Config "WatcherOptions" {
                Required = []
                Optional = [
                    "backgroundMessage", T<string>
                    "backgroundTitle", T<string>
                    "requestPermissions", T<bool>
                    "stale", T<bool>
                    "distanceFilter", T<float>
                ]
            }

        let RemoveWatcherOptions = 
            Pattern.Config "RemoveWatcherOptions" {
                Required = []
                Optional = ["id", T<string>]
            }

        let CallbackOptions = 
            Pattern.Config "CallbackOptions" {
                Required = []
                Optional = [
                    "position", Location.Type
                    "error", CallbackError.Type
                ]
            }

        let CallbackFunc = CallbackOptions ^-> T<unit>

        let BackgroundGeolocationPlugin =
            Class "BackgroundGeolocationPlugin"
            |+> Instance [
                "addWatcher" => WatcherOptions?options * CallbackFunc?callback ^-> T<Promise<string>>
                "removeWatcher" => RemoveWatcherOptions?options ^-> T<Promise<unit>>
                "openSettings" => T<unit> ^-> T<Promise<unit>>
            ]

    [<AutoOpen>]
    module VolumeButtons = 
        let Direction = 
            Pattern.EnumStrings "Direction" ["up"; "down"]

        let VolumeButtonsResult =
            Pattern.Config "VolumeButtonsResult" {
                Required = []
                Optional = [
                    "direction", Direction.Type
                ]
            }

        let GetIsWatchingResult =
            Pattern.Config "GetIsWatchingResult" {
                Required = []
                Optional = ["value", T<bool>]
            }

        let VolumeButtonsOptions =
            Pattern.Config "VolumeButtonsOptions" {
                Required = []
                Optional = [
                    "disableSystemVolumeHandler", T<bool>
                    "suppressVolumeIndicator", T<bool>
                ]
            }

        let VolumeButtonsCallback  = VolumeButtonsResult?result * T<obj>?err ^-> T<unit>

        let VolumeButtonsPlugin =
            Class "VolumeButtonsPlugin"
            |+> Instance [
                "isWatching" => T<unit> ^-> T<Promise<_>>[GetIsWatchingResult]
                "watchVolume" => VolumeButtonsOptions?options * VolumeButtonsCallback?callback ^-> T<Promise<string>>
                "clearWatch" => T<unit> ^-> T<Promise<unit>>
            ]

    [<AutoOpen>]
    module InAppReview = 
        let InAppReviewPlugin =
            Class "InAppReviewPlugin"
            |+> Instance [
                "requestReview" => T<unit> ^-> T<Promise<unit>>
            ]

    let CapacitorCommunity = 
        Class "Capacitor.Community"
        |+> Static [
            "FacebookLogin" =? FacebookLoginPlugin
            |> Import "FacebookLogin" "@capacitor-community/facebook-login"
            "Stripe" =? StripePlugin
            |> Import "Stripe" "@capacitor-community/stripe"
            "PrivacyScreen" =? PrivacyScreenPlugin
            |> Import "PrivacyScreen" "@capacitor-community/privacy-screen"
            "KeepAwake" =? KeepAwakePlugin
            |> Import "KeepAwake" "@capacitor-community/keep-awake"
            "GoogleMaps" =? GoogleMapsPlugin
            |> Import "GoogleMaps" "@capacitor-community/google-maps"
            "Contacts" =? ContactsPlugin
            |> Import "Contacts" "@capacitor-community/contacts"
            "DatePicker" =? DatePickerPlugin
            |> Import "DatePicker" "@capacitor-community/date-picker"
            "SQLite" =? SQLitePlugin
            |> Import "SQLite" "@capacitor-community/sqlite"
            "ImageToText" =? ImageToTextPlugin
            |> Import "ImageToText" "@capacitor-community/image-to-text"
            "FileOpener" =? FileOpenerPlugin
            |> Import "FileOpener" "@capacitor-community/file-opener"
            "AppleSignIn" =? AppleSignInPlugin
            |> Import "AppleSignIn" "@capacitor-community/apple-sign-in"
            "BackgroundGeolocation" =? BackgroundGeolocationPlugin
            |> Import "BackgroundGeolocation" "@capacitor-community/background-geolocation"
            "VolumeButtons" =? VolumeButtonsPlugin
            |> Import "VolumeButtons" "@capacitor-community/volume-buttons"
            "InAppReview" =? InAppReviewPlugin
            |> Import "InAppReview" "@capacitor-community/in-app-review"
        ]

    let Assembly =
        Assembly [
            Namespace "Websharper.Capacitor.Community" [
                PermissionState
                PluginListenerHandle
                FacebookLoginPlugin
                StripePlugin
                PrivacyScreenPlugin
                KeepAwakePlugin
                GoogleMapsPlugin
                ContactsPlugin
                DatePickerPlugin
                SQLitePlugin
                ImageToTextPlugin
                FileOpenerPlugin
                AppleSignInPlugin
                BackgroundGeolocationPlugin
                VolumeButtonsPlugin
                InAppReviewPlugin
            ]
            Namespace "Websharper.Capacitor.Community.FacebookLogin" [
                FacebookConfiguration; LoginOptions; FacebookLoginResponse; FacebookCurrentAccessTokenResponse; ProfileOptions; AccessToken
            ]
            Namespace "Websharper.Capacitor.Community.Stripe" [
                CapacitorStripeContext; PresentPaymentSheetResult; ConfirmPaymentFlowResult; PresentPaymentFlowResult; PresentGooglePayResult
                StripeURLHandlingOptions; StripeInitializationOptions; CreatePaymentSheetOption; CreatePaymentFlowOption; BillingDetailsCollectionConfiguration
                CreateGooglePayOption; DidSelectShippingContact; ShippingContact; CreateApplePayOption; ShippingContactType; PaymentSummaryType
                PaymentFlowResultInterface; AddressCollectionMode; CollectionMode; GooglePayResultInterface; ApplePayResultInterface; PaymentSheetEventsEnum
                GooglePayEventsEnum; ApplePayEventsEnum; PresentApplePayResult; StyleType; PaymentSheetResultInterface; PaymentFlowEventsEnum
            ]
            Namespace "Websharper.Capacitor.Community.PrivacyScreen" [
                PluginsConfig; PrivacyScreenConfig; ContentMode
            ]
            Namespace "Websharper.Capacitor.Community.KeepAwake" [
                IsKeptAwakeResult; IsSupportedResult
            ]
            Namespace "Websharper.Capacitor.Community.GoogleMaps" [
                MarkerOutputEntry; AddMarkerResult; Marker; AddMarkerOptions; MarkerPreferences; MarkerIcon; MarkerIconSize; Anchor; MoveCameraOptions
                ClearMapOptions; RemoveMapOptions; UpdateMapResult; UpdateMapOptions; CreateMapResult; CreateMapOptions; BoundingRect; GoogleMap
                MapPreferences; MapAppearance; MapControls; MapGestures; CameraPosition; LatLng; InitializeOptions; MapType; CameraMovementReason
                DidEndMovingCameraResult; DidBeginMovingCameraResult; DidTapPoiResult; PointOfInterest; DidTapMyLocationDotResult; 
                DidDragMarkerResult; DidBeginDraggingMarkerResult; DidTapMarkerResult; DidLongPressMapResult; DidTapMapResult; DidCloseInfoWindowResult
                DidTapInfoWindowResult; DidRequestElementFromPointResult; Point; DefaultEventWithPreventDefaultOptions; DefaultEventOptions; 
                ElementFromPointResultOptions; AddMarkersResult; AddMarkersOptions; MarkerInputEntry; RemoveMarkerOptions; DidEndDraggingMarkerResult
            ]
            Namespace "Websharper.Capacitor.Community.Contacts" [
                PickContactOptions; PickContactResult; DeleteContactOptions; CreateContactOptions; ContactInput; PostalAddressInput; EmailInput
                PhoneInput; BirthdayInput; OrganizationInput; NameInput; CreateContactResult; GetContactsOptions; GetContactsResult; GetContactOptions
                GetContactResult; Projection; ContactPayload; ImagePayload; PostalAddressPayload; EmailPayload; PhonePayload; BirthdayPayload
                OrganizationPayload; NamePayload; PhoneType; EmailType; PostalAddressType; PermissionStatus
            ]
            Namespace "Websharper.Capacitor.Community.DatePicker" [
                DatePickerResult; DatePickerOptions; DatePickerIosOptions; DatePickerBaseOptions
                DatePickerIosStyle; DatePickerTheme; DatePickerMode
            ]
            Namespace "Websharper.Capacitor.Community.SQLite" [
                capSQLiteRunOptions; capSQLiteSetOptions; capSQLiteSet; capSQLiteExecuteOptions; capNCOptions; capNCConnectionOptions
                capNCDatabasePathOptions; capAllConnectionsOptions; capVersionResult; capSQLiteUrl; capSQLiteChanges; Changes
                capEchoOptions; capEchoResult; capConnectionOptions; capChangeSecretOptions; capSetSecretOptions; capSQLiteResult
                capSQLiteLocalDiskOptions; capSQLiteOptions; capSQLiteHTTPOptions; capSQLiteFromAssetsOptions; capSQLiteSyncDateOptions
                capSQLiteExportOptions; capSQLiteImportOptions; capSQLiteSyncDate; capSQLiteJson; JsonSQLite; JsonView; JsonTable
                JsonIndex; JsonTrigger; JsonColumn; capSQLiteValues; capNCDatabasePathResult; capSQLiteTableOptions; capSQLitePathOptions
                capSQLiteUpgradeOptions; capSQLiteVersionUpgrade; capSQLiteQueryOptions
            ]
            Namespace "Websharper.Capacitor.Community.ImageToText" [
                TextDetections; TextDetection; DetectTextBase64Options; DetectTextFileOptions; ImageOrientation
            ]
            Namespace "Websharper.Capacitor.Community.FileOpener" [
                FileOpenerOptions; ChooserPosition
            ]
            Namespace "Websharper.Capacitor.Community.AppleSignIn" [
                SignInWithAppleOptions; SignInWithAppleResponse; SignInWithAppleResponseData
            ]
            Namespace "Websharper.Capacitor.Community.BackgroundGeolocation" [
                Location; CallbackError; WatcherOptions; RemoveWatcherOptions; CallbackOptions
            ]
            Namespace "Websharper.Capacitor.Community.VolumeButtons" [
                Direction; VolumeButtonsResult; GetIsWatchingResult; VolumeButtonsOptions
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
