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
        ]

    let Assembly =
        Assembly [
            Namespace "Websharper.Capacitor.Community" [
                PluginListenerHandle
                FacebookLoginPlugin
                StripePlugin
                PrivacyScreenPlugin
                KeepAwakePlugin
                GoogleMapsPlugin
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
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
