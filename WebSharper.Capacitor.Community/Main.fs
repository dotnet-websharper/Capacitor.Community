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
        ]

    let Assembly =
        Assembly [
            Namespace "Websharper.Capacitor.Community" [
                PluginListenerHandle
                FacebookLoginPlugin
                StripePlugin
                PrivacyScreenPlugin
                KeepAwakePlugin
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
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
