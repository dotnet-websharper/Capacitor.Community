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

    [<AutoOpen>]
    module Media = 
        let MediaLocation =
            Pattern.Config "MediaLocation" {
                Required = []
                Optional = [
                    "latitude", T<float>
                    "longitude", T<float>
                    "heading", T<float>
                    "altitude", T<float>
                    "speed", T<float>
                ]
            }

        let MediaAsset =
            Pattern.Config "MediaAsset" {
                Required = []
                Optional = [
                    "identifier", T<string>
                    "data", T<string>
                    "creationDate", T<string>
                    "fullWidth", T<int>
                    "fullHeight", T<int>
                    "thumbnailWidth", T<int>
                    "thumbnailHeight", T<int>
                    "location", MediaLocation.Type
                ]
            }

        let MediaResponse =
            Pattern.Config "MediaResponse" {
                Required = []
                Optional = ["medias", !|MediaAsset]
            }

        let MediaPath =
            Pattern.Config "MediaPath" {
                Required = []
                Optional = [
                    "path", T<string>
                    "identifier", T<string>
                ]
            }

        let AlbumsPathResponse =
            Pattern.Config "AlbumsPathResponse" {
                Required = []
                Optional = ["path", T<string>]
            }

        let MediaAlbumType = 
            Pattern.EnumStrings "MediaAlbumType" [
                "smart"; "shared"; "user"
            ]

        let MediaAlbum =
            Pattern.Config "MediaAlbum" {
                Required = []
                Optional = [
                    "identifier", T<string>
                    "name", T<string>
                    "type", MediaAlbumType.Type
                ]
            }

        let MediaAlbumResponse =
            Pattern.Config "MediaAlbumResponse" {
                Required = []
                Optional = ["albums", !|MediaAlbum]
            }

        let MediaAlbumCreate =
            Pattern.Config "MediaAlbumCreate" {
                Required = []
                Optional = ["name", T<string>]
            }

        let MediaSaveOptions =
            Pattern.Config "MediaSaveOptions" {
                Required = []
                Optional = [
                    "path", T<string>
                    "albumIdentifier", T<string>
                    "fileName", T<string>
                ]
            }

        let MediaTypes =    
            Pattern.EnumStrings "MediaTypes" [
                "photos"; "videos"; "all"
            ]

        let MediaSortKeys =    
            Pattern.EnumStrings "MediaSortKeys" [
                "mediaType"
                "mediaSubtypes"
                "sourceType" 
                "pixelWidth"  
                "pixelHeight" 
                "creationDate" 
                "modificationDate" 
                "isFavorite" 
                "burstIdentifier"
            ]

        let MediaSort =
            Pattern.Config "MediaSort" {
                Required = []
                Optional = [
                    "key", MediaSortKeys.Type
                    "ascending", T<bool>
                ]
            }

        let MediaFetchOptions =
            Pattern.Config "MediaFetchOptions" {
                Required = []
                Optional = [
                    "quantity", T<int>
                    "thumbnailWidth", T<int>
                    "thumbnailHeight", T<int>
                    "thumbnailQuality", T<int>
                    "types", MediaTypes.Type
                    "albumIdentifier", T<string>
                    "sort", MediaSortKeys.Type + !| MediaSort
                ]
            }

        let PhotoResponse =
            Pattern.Config "PhotoResponse" {
                Required = []
                Optional = [
                    "filePath", T<string> 
                ]
            }

        let GetMediaByIdentifierOptions = 
            Pattern.Config "GetMediaByIdentifierOptions" {
                Required = []
                Optional = ["identifier", T<string>]
            }

        let MediaPlugin =
            Class "MediaPlugin"
            |+> Instance [
                "getMedias" => !?MediaFetchOptions?options ^-> T<Promise<_>>[MediaResponse]
                "getMediaByIdentifier" => !?GetMediaByIdentifierOptions?options ^-> T<Promise<_>>[MediaPath]
                "getAlbums" => T<unit> ^-> T<Promise<_>>[MediaAlbumResponse]
                "savePhoto" => !?MediaSaveOptions?options ^-> T<Promise<_>>[PhotoResponse]
                "saveVideo" => !?MediaSaveOptions?options ^-> T<Promise<_>>[PhotoResponse]
                "createAlbum" => MediaAlbumCreate?options ^-> T<Promise<unit>>
                "getAlbumsPath" => T<unit> ^-> T<Promise<_>>[AlbumsPathResponse]
            ]

    [<AutoOpen>]
    module AppIcon = 
        let IconOptions =
            Pattern.Config "IconOptions" {
                Required = []
                Optional = [
                    "name", T<string>
                    "suppressNotification", T<bool>
                    "disable", !|T<string>
                ]
            }

        let ResetOptions =
            Pattern.Config "ResetOptions" {
                Required = []
                Optional = [
                    "suppressNotification", T<bool>
                    "disable", !|T<string>
                ]
            }

        let IsSupportedResponse = 
            Pattern.Config "IsSupportedResponse" {
                Required = ["value", T<bool>]
                Optional = []
            }

        let GetNameResponse = 
            Pattern.Config "GetNameResponse" {
                Required = ["value", T<string> + T<unit>]
                Optional = []
            }

        let AppIconPlugin =
            Class "AppIconPlugin"
            |+> Instance [
                "isSupported" => T<unit> ^-> T<Promise<_>>[IsSupportedResponse]
                "getName" => T<unit> ^-> T<Promise<_>>[GetNameResponse]
                "change" => IconOptions?options ^-> T<Promise<unit>>
                "reset" => ResetOptions?options ^-> T<Promise<unit>>
            ]

    [<AutoOpen>]
    module PhotoViewer = 
        let Image =
            Pattern.Config "Image" {
                Required = []
                Optional = [
                    "url", T<string>
                    "title", T<string>
                ]
            }

        let MovieOptions =
            Pattern.Config "MovieOptions" {
                Required = []
                Optional = [
                    "name", T<string>
                    "imagetime", T<int>
                    "mode", T<string>
                    "ratio", T<string>
                ]
            }

        let ViewerOptions =
            Pattern.Config "ViewerOptions" {
                Required = []
                Optional = [
                    "share", T<bool>
                    "title", T<bool>
                    "transformer", T<string>
                    "spancount", T<int>
                    "maxzoomscale", T<int>
                    "compressionquality", T<int>
                    "backgroundcolor", T<string>
                    "divid", T<string>
                    "movieoptions", MovieOptions.Type
                    "customHeaders", T<obj>
                ]
            }

        let CapShowOptions =
            Pattern.Config "CapShowOptions" {
                Required = []
                Optional = [
                    "images", !|Image.Type
                    "options", ViewerOptions.Type
                    "mode", T<string>
                    "startFrom", T<int>
                ]
            }

        let CapEchoOptions =
            Pattern.Config "CapEchoOptions" {
                Required = []
                Optional = ["value", T<string>]
            }

        let CapEchoResult =
            Pattern.Config "CapEchoResult" {
                Required = []
                Optional = ["value", T<string>]
            }

        let CapShowResult =
            Pattern.Config "CapShowResult" {
                Required = []
                Optional = [
                    "result", T<bool>
                    "message", T<string>
                    "imageIndex", T<int>
                ]
            }

        let CapHttpOptions =
            Pattern.Config "CapHttpOptions" {
                Required = []
                Optional = [
                    "url", T<string>
                    "filename", T<string>
                ]
            }

        let CapHttpResult =
            Pattern.Config "CapHttpResult" {
                Required = []
                Optional = [
                    "webPath", T<string>
                    "message", T<string>
                ]
            }

        let CapPaths =
            Pattern.Config "CapPaths" {
                Required = []
                Optional = ["pathList", !|T<string>]
            }

        let PhotoViewerPlugin =
            Class "PhotoViewerPlugin"
            |+> Instance [
                "echo" => CapEchoOptions?options ^-> T<Promise<_>>[CapEchoResult]
                "show" => CapShowOptions?options ^-> T<Promise<_>>[CapShowResult]
                "saveImageFromHttpToInternal" => CapHttpOptions?options ^-> T<Promise<_>>[CapHttpResult]
                "getInternalImagePaths" => T<unit> ^-> T<Promise<_>>[CapPaths]
            ]

    [<AutoOpen>]
    module Intercom = 
        let IntercomPushNotificationData =
            Pattern.Config "IntercomPushNotificationData" {
                Required = []
                Optional = [
                    "conversation_id", T<string>
                    "message", T<string>
                    "body", T<string>
                    "author_name", T<string>
                    "image_url", T<string>
                    "app_name", T<string>
                    "receiver", T<string>
                    "conversation_part_type", T<string>
                    "intercom_push_type", T<string>
                    "uri", T<string>
                    "push_only_conversation_id", T<string>
                    "instance_id", T<string>
                    "title", T<string>
                    "priority", T<int>
                ]
            }

        let IntercomUserUpdateOptions =
            Pattern.Config "IntercomUserUpdateOptions" {
                Required = []
                Optional = [
                    "userId", T<string>
                    "email", T<string>
                    "name", T<string>
                    "phone", T<string>
                    "languageOverride", T<string>
                    "customAttributes", !| T<obj>
                ]
            }

        let LoadWithKeysOptions = 
            Pattern.Config "LoadWithKeysOptions" {
                Required = []
                Optional = [
                    "appId", T<string>
                    "apiKeyIOS", T<string>
                    "apiKeyAndroid", T<string>
                ]
            }

        let RegisterIdentifiedUserOptions = 
            Pattern.Config "RegisterIdentifiedUserOptions" {
                Required = []
                Optional = [
                    "userId", T<string>
                    "email", T<string>
                ]
            }

        let LogEventOptions =
            Pattern.Config "LogEventOptions" {
                Required = []
                Optional = [
                    "name", T<string>
                    "data", T<obj>
                ]
            }

        let IntercomPlugin =
            Class "IntercomPlugin"
            |+> Instance [
                "loadWithKeys" => LoadWithKeysOptions?options ^-> T<Promise<unit>>
                "registerIdentifiedUser" => RegisterIdentifiedUserOptions?options ^-> T<Promise<unit>>
                "registerUnidentifiedUser" => T<unit> ^-> T<Promise<unit>>
                "updateUser" => IntercomUserUpdateOptions?options ^-> T<Promise<unit>>
                "logout" => T<unit> ^-> T<Promise<unit>>
                "logEvent" => LogEventOptions?options ^-> T<Promise<unit>>
                "displayMessenger" => T<unit> ^-> T<Promise<unit>>
                "displayMessageComposer" => T<string>?options ^-> T<Promise<unit>>
                "displayHelpCenter" => T<unit> ^-> T<Promise<unit>>
                "hideMessenger" => T<unit> ^-> T<Promise<unit>>
                "displayLauncher" => T<unit> ^-> T<Promise<unit>>
                "hideLauncher" => T<unit> ^-> T<Promise<unit>>
                "displayInAppMessages" => T<unit> ^-> T<Promise<unit>>
                "hideInAppMessages" => T<unit> ^-> T<Promise<unit>>
                "displayCarousel" => T<string>?options ^-> T<Promise<unit>>
                "setUserHash" => T<string>?options ^-> T<Promise<unit>>
                "setBottomPadding" => T<string>?options ^-> T<Promise<unit>>
                "sendPushTokenToIntercom" => T<string>?options ^-> T<Promise<unit>>
                "receivePush" => IntercomPushNotificationData?notification ^-> T<Promise<unit>>
                "displayArticle" => T<string>?options ^-> T<Promise<unit>>
            ]

    [<AutoOpen>]
    module SpeechRecognition = 
        let PermissionStatus =
            Pattern.Config "PermissionStatus" {
                Required = []
                Optional = ["speechRecognition", PermissionState.Type]
            }

        let UtteranceOptions =
            Pattern.Config "UtteranceOptions" {
                Required = []
                Optional = [
                    "language", T<string>
                    "maxResults", T<int>
                    "prompt", T<string>
                    "popup", T<bool>
                    "partialResults", T<bool>
                ]
            }

        let AvailableResponse =
            Pattern.Config "AvailableResponse" {
                Required = ["available", T<bool>]
                Optional = []
            }

        let MatchesResponse =
            Pattern.Config "MatchesResponse" {
                Required = []
                Optional = ["matches", !|T<string>]
            }

        let ListeningResponse =
            Pattern.Config "ListeningResponse" {
                Required = ["listening", T<bool>]
                Optional = []
            }

        let SupportedLanguagesResponse =
            Pattern.Config "SupportedLanguagesResponse" {
                Required = ["languages", !|T<obj>]
                Optional = []
            }

        let ListeningStateResponse =
            Pattern.Config "ListeningStateResponse" {
                Required = ["status", T<string>]
                Optional = []
            }

        let PartialResultsResponse =
            Pattern.Config "PartialResultsResponse" {
                Required = ["matches", !|T<string>]
                Optional = []
            }

        let SpeechRecognitionPlugin =
            Class "SpeechRecognitionPlugin"
            |+> Instance [
                "available" => T<unit> ^-> T<Promise<_>>[AvailableResponse]
                "start" => UtteranceOptions?options ^-> T<Promise<_>>[MatchesResponse]
                "stop" => T<unit> ^-> T<Promise<unit>>
                "getSupportedLanguages" => T<unit> ^-> T<Promise<_>>[SupportedLanguagesResponse]
                "isListening" => T<unit> ^-> T<Promise<_>>[ListeningResponse]
                "checkPermissions" => T<unit> ^-> T<Promise<_>>[PermissionStatus]
                "requestPermissions" => T<unit> ^-> T<Promise<_>>[PermissionStatus]
                "addListener" => T<string>?eventName * (PartialResultsResponse?data ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen to 'partialResults' event"
                "addListener" => T<string>?eventName * (ListeningStateResponse?data ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
                |> WithComment "Listen to 'listeningState' event"
                "removeAllListeners" => T<unit> ^-> T<Promise<unit>>
            ]

    [<AutoOpen>]
    module CameraPreview = 
        let CameraPreviewOptions =
            Pattern.Config "CameraPreviewOptions" {
                Required = []
                Optional = [
                    "parent", T<string>
                    "className", T<string>
                    "width", T<int>
                    "height", T<int>
                    "x", T<int>
                    "y", T<int>
                    "toBack", T<bool>
                    "paddingBottom", T<int>
                    "rotateWhenOrientationChanged", T<bool>
                    "position", T<string>
                    "storeToFile", T<bool>
                    "disableExifHeaderStripping", T<bool>
                    "enableHighResolution", T<bool>
                    "disableAudio", T<bool>
                    "lockAndroidOrientation", T<bool>
                    "enableOpacity", T<bool>
                    "enableZoom", T<bool>
                ]
            }

        let CameraPreviewPictureOptions =
            Pattern.Config "CameraPreviewPictureOptions" {
                Required = []
                Optional = [
                    "height", T<int>
                    "width", T<int>
                    "quality", T<int>
                ]
            }

        let CameraSampleOptions =
            Pattern.Config "CameraSampleOptions" {
                Required = []
                Optional = [
                    "quality", T<int>
                ]
            }

        let CameraOpacityOptions =
            Pattern.Config "CameraOpacityOptions" {
                Required = []
                Optional = ["opacity", T<int>]
            }

        let CameraPreviewPlugin =
            Class "CameraPreviewPlugin"
            |+> Instance [
                "start" => CameraPreviewOptions?options ^-> T<Promise<_>>
                "startRecordVideo" => CameraPreviewOptions?options ^-> T<Promise<_>>
                "stop" => T<unit> ^-> T<Promise<_>>
                "stopRecordVideo" => T<unit> ^-> T<Promise<_>>
                "capture" => CameraPreviewPictureOptions?options ^-> T<Promise<string>>
                "captureSample" => CameraSampleOptions?options ^-> T<Promise<string>>
                "getSupportedFlashModes" => T<unit> ^-> T<Promise<_>>[!|T<string>]
                "setFlashMode" => T<string>?options ^-> T<Promise<unit>>
                "flip" => T<unit> ^-> T<Promise<unit>>
                "setOpacity" => CameraOpacityOptions?options ^-> T<Promise<_>>
            ]

    [<AutoOpen>]
    module SafeArea = 
        let Config =
            Pattern.Config "Config" {
                Required = []
                Optional = [
                    "customColorsForSystemBars", T<bool>
                    "statusBarColor", T<string>
                    "statusBarContent", T<string>
                    "navigationBarColor", T<string>
                    "navigationBarContent", T<string>
                    "offset", T<int>
                ]
            }

        let SafeAreaPlugin =
            Class "SafeAreaPlugin"
            |+> Instance [
                "enable" => Config?options ^-> T<Promise<unit>>
                "disable" => Config?options ^-> T<Promise<unit>>
            ]
    
        let SafeAreaPluginConfig =
            Pattern.Config "PluginsConfig.SafeArea" {
                Required = []
                Optional = [
                    "enabled", T<bool>
                ]
            }
            |=> Inherits Config
        
    [<AutoOpen>]
    module BluetoothLe = 
        let ScanMode =
            Pattern.EnumInlines "ScanMode" [
                "SCAN_MODE_LOW_POWER", "0"
                "SCAN_MODE_BALANCED", "1"
                "SCAN_MODE_LOW_LATENCY", "2"
            ]

        let ConnectionPriority =
            Pattern.EnumInlines "ConnectionPriority" [
                "CONNECTION_PRIORITY_BALANCED", "0"
                "CONNECTION_PRIORITY_HIGH", "1"
                "CONNECTION_PRIORITY_LOW_POWER", "2"
            ]

        let InitializeOptions =
            Pattern.Config "InitializeOptions" {
                Required = []
                Optional = [
                    "androidNeverForLocation", T<bool>
                ]
            }

        let DisplayStrings =
            Pattern.Config "DisplayStrings" {
                Required = []
                Optional = [
                    "scanning", T<string>
                    "cancel", T<string>
                    "availableDevices", T<string>
                    "noDeviceFound", T<string>
                ]
            }

        let BleDevice =
            Pattern.Config "BleDevice" {
                Required = []
                Optional = [
                    "deviceId", T<string>
                    "name", T<string>
                    "uuids", !| T<string>
                ]
            }

        let RequestBleDeviceOptions =
            Pattern.Config "RequestBleDeviceOptions" {
                Required = []
                Optional = [
                    "services", !| T<string>
                    "name", T<string>
                    "namePrefix", T<string>
                    "optionalServices", !| T<string>
                    "allowDuplicates", T<bool>
                    "scanMode", ScanMode.Type
                ]
            }

        let BleCharacteristicProperties =
            Pattern.Config "BleCharacteristicProperties" {
                Required = []
                Optional = [
                    "broadcast", T<bool>
                    "read", T<bool>
                    "writeWithoutResponse", T<bool>
                    "write", T<bool>
                    "notify", T<bool>
                    "indicate", T<bool>
                    "authenticatedSignedWrites", T<bool>
                    "reliableWrite", T<bool>
                    "writableAuxiliaries", T<bool>
                    "extendedProperties", T<bool>
                    "notifyEncryptionRequired", T<bool>
                    "indicateEncryptionRequired", T<bool>
                ]
            }

        let BleDescriptor = 
            Pattern.Config "BleDescriptor" {
                Required = []
                Optional = [
                    "uuid", T<string>
                ]
            }

        let BleCharacteristic =
            Pattern.Config "BleCharacteristic" {
                Required = []
                Optional = [
                    "uuid", T<string>
                    "properties", BleCharacteristicProperties.Type
                    "descriptors", !| BleDescriptor
                ]
            }

        let BleService =
            Pattern.Config "BleService" {
                Required = []
                Optional = [
                    "uuid", T<string>
                    "characteristics", !| BleCharacteristic
                ]
            }

        let TimeoutOptions =
            Pattern.Config "TimeoutOptions" {
                Required = []
                Optional = ["timeout", T<int>]
            }

        let ScanResult =
            Pattern.Config "ScanResult" {
                Required = []
                Optional = [
                    "device", BleDevice.Type
                    "localName", T<string>
                    "rssi", T<int>
                    "txPower", T<int>
                    "manufacturerData", T<obj> 
                    "serviceData", T<obj> 
                    "uuids", !| T<string>
                    "rawAdvertisement", T<DataView> 
                ]
            }

        let BooleanType = 
            Pattern.Config "BooleanType" {
                Required = []
                Optional = [
                    "value", T<bool>
                ]
            }

        let ScanResultType = 
            Pattern.Config "ScanResultType" {
                Required = []
                Optional = [
                    "result", ScanResult.Type
                ]
            }

        let StringType = 
            Pattern.Config "StringType" {
                Required = []
                Optional = [
                    "deviceId",T<string>
                ]
            }

        let DataViewCallback = 
            Pattern.Config "DataViewCallback" {
                Required = []
                Optional = [
                    "value",T<DataView>
                ]
            }

        let BluetoothLePlugin =
            Class "BluetoothLePlugin"
            |+> Instance [
                "initialize" => !?InitializeOptions?options ^-> T<Promise<unit>>
                "isEnabled" => T<unit> ^-> T<Promise<bool>>
                "requestEnable" => T<unit> ^-> T<Promise<unit>>
                "enable" => T<unit> ^-> T<Promise<unit>>
                "disable" => T<unit> ^-> T<Promise<unit>>
                "startEnabledNotifications" => (BooleanType ^-> T<unit>)?callback ^-> T<Promise<unit>>
                "stopEnabledNotifications" => T<unit> ^-> T<Promise<unit>>
                "isLocationEnabled" => T<unit> ^-> T<Promise<bool>>
                "openLocationSettings" => T<unit> ^-> T<Promise<unit>>
                "openBluetoothSettings" => T<unit> ^-> T<Promise<unit>>
                "openAppSettings" => T<unit> ^-> T<Promise<unit>>
                "setDisplayStrings" => DisplayStrings?displayStrings ^-> T<Promise<unit>>
                "requestDevice" => !?RequestBleDeviceOptions?options ^-> T<Promise<_>>[BleDevice]
                "requestLEScan" => RequestBleDeviceOptions?options * (ScanResultType ^-> T<unit>)?callback ^-> T<Promise<unit>>
                "stopLEScan" => T<unit> ^-> T<Promise<unit>>
                "getDevices" => (!|T<string>)?deviceIds ^-> T<Promise<_>>[!| BleDevice]
                "getConnectedDevices" => (!|T<string>)?services ^-> T<Promise<_>>[!| BleDevice]
                "connect" => T<string>?deviceId * !?(StringType ^-> T<unit>)?onDisconnect * !?TimeoutOptions?options ^-> T<Promise<unit>>
                "createBond" => T<string>?deviceId * !?TimeoutOptions?options ^-> T<Promise<unit>>
                "isBonded" => T<string>?deviceId ^-> T<Promise<bool>>
                "disconnect" => T<string>?deviceId ^-> T<Promise<unit>>
                "getServices" => T<string>?deviceId ^-> T<Promise<_>>[!| BleService] 
                "discoverServices" => T<string>?deviceId ^-> T<Promise<unit>>
                "getMtu" => T<string>?deviceId ^-> T<Promise<int>>
                "requestConnectionPriority" => T<string>?deviceId * ConnectionPriority?connectionPriority ^-> T<Promise<unit>>
                "readRssi" => T<string>?deviceId ^-> T<Promise<int>>
                "read" => T<string>?deviceId * T<string>?service * T<string>?characteristic * !?TimeoutOptions?options ^-> T<Promise<DataView>>
                "write" => T<string>?deviceId * T<string>?service * T<string>?characteristic * T<DataView>?value * !?TimeoutOptions?options ^-> T<Promise<unit>>
                "writeWithoutResponse" => T<string>?deviceId * T<string>?service * T<string>?characteristic * T<DataView>?value * !?TimeoutOptions?options ^-> T<Promise<unit>>
                "readDescriptor" => T<string>?deviceId * T<string>?service * T<string>?characteristic * T<string>?descriptor * !?TimeoutOptions?options ^-> T<Promise<DataView>>
                "writeDescriptor" => T<string>?deviceId * T<string>?service * T<string>?characteristic * T<string>?descriptor * T<DataView>?value * !?TimeoutOptions?options ^-> T<Promise<unit>>
                "startNotifications" => T<string>?deviceId * T<string>?service * T<string>?characteristic * (DataViewCallback ^-> T<unit>)?callback ^-> T<Promise<unit>>
                "stopNotifications" => T<string>?deviceId * T<string>?service * T<string>?characteristic ^-> T<Promise<unit>>
            ]

    [<AutoOpen>]
    module NativeAudio = 
        let ConfigureOptions =
            Pattern.Config "ConfigureOptions" {
                Required = []
                Optional = [
                    "fade", T<bool>
                    "focus", T<bool>
                ]
            }

        let PreloadOptions =
            Pattern.Config "PreloadOptions" {
                Required = []
                Optional = [
                    "assetPath", T<string>
                    "assetId", T<string>
                    "volume", T<float>
                    "audioChannelNum", T<int>
                    "isUrl", T<bool>
                ]
            }

        let PlayOptions =
            Pattern.Config "PlayOptions" {
                Required = []
                Optional = [
                    "assetId", T<string>
                    "time", T<int>
                ]
            }

        let SetVolumeOptions =
            Pattern.Config "SetVolumeOptions" {
                Required = []
                Optional = [
                    "assetId", T<string>
                    "volume", T<int>
                ]
            }

        let NativeAudioPlugin =
            Class "NativeAudioPlugin"
            |+> Instance [
                "configure" => ConfigureOptions?options ^-> T<Promise<unit>>
                "preload" => PreloadOptions?options ^-> T<Promise<unit>>
                "play" => PlayOptions?options ^-> T<Promise<unit>>
                "pause" => T<string>?options ^-> T<Promise<unit>>
                "resume" => T<string>?options ^-> T<Promise<unit>>
                "loop" => T<string>?options ^-> T<Promise<unit>>
                "stop" => T<string>?options ^-> T<Promise<unit>>
                "unload" => T<string>?options ^-> T<Promise<unit>>
                "setVolume" => SetVolumeOptions?options ^-> T<Promise<unit>>
                "getCurrentTime" => T<string>?options ^-> T<Promise<int>>
                "getDuration" => T<string>?options ^-> T<Promise<int>>
                "isPlaying" => T<string>?options ^-> T<Promise<bool>>
                "addListener" => T<string>?eventName * (T<string>?event ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
            ]

    [<AutoOpen>]
    module TextToSpeech = 
        let SpeechSynthesisVoice =
            Pattern.Config "SpeechSynthesisVoice" {
                Required = []
                Optional = [
                    "default", T<bool>
                    "lang", T<string>
                    "localService", T<bool>
                    "name", T<string>
                    "voiceURI", T<string>
                ]
            }

        let TTSOptions =
            Pattern.Config "TTSOptions" {
                Required = []
                Optional = [
                    "text", T<string>
                    "lang", T<string>
                    "rate", T<float>
                    "pitch", T<float>
                    "volume", T<float>
                    "voice", T<int>
                    "category", T<string>
                ]
            }

        let ListenFuncOptions = 
            Pattern.Config "ListenFuncOptions" {
                Required = []
                Optional = [
                    "start", T<int>
                    "end", T<int>
                    "spokenWord", T<string>
                ]
            }

        let TextToSpeechPlugin =
            Class "TextToSpeechPlugin"
            |+> Instance [
                "speak" => TTSOptions?options ^-> T<Promise<unit>>
                "stop" => T<unit> ^-> T<Promise<unit>>
                "getSupportedLanguages" => T<unit> ^-> T<Promise<_>>[!| T<string>]
                "getSupportedVoices" => T<unit> ^-> T<Promise<_>>[!| SpeechSynthesisVoice]
                "isLanguageSupported" => T<string>?options ^-> T<Promise<bool>>
                "openInstall" => T<unit> ^-> T<Promise<unit>>
                "addListener" => (T<string>?eventName * ListenFuncOptions?info ^-> T<unit>)?listenerFunc ^-> T<Promise<_>>[PluginListenerHandle]
            ]

    [<AutoOpen>]
    module ScreenBrightness = 
        let SetBrightnessOptions =
            Pattern.Config "SetBrightnessOptions" {
                Required = []
                Optional = ["brightness", T<float>]
            }

        let GetBrightnessReturnValue =
            Pattern.Config "GetBrightnessReturnValue" {
                Required = []
                Optional = ["brightness", T<float>]
            }

        let ScreenBrightnessPlugin =
            Class "ScreenBrightnessPlugin"
            |+> Instance [
                "setBrightness" => SetBrightnessOptions?options ^-> T<Promise<unit>>
                "getBrightness" => T<unit> ^-> T<Promise<_>>[GetBrightnessReturnValue]
            ]

    [<AutoOpen>]
    module SecurityProvider = 
        let SecurityProviderStatus =
            Pattern.EnumStrings "SecurityProviderStatus" [
                "Success"
                "NotImplemented"
                "GooglePlayServicesRepairableException"
                "GooglePlayServicesNotAvailableException"
            ]

        let InstallIfNeededReturnValue =
            Pattern.Config "InstallIfNeededReturnValue" {
                Required = []
                Optional = ["status", SecurityProviderStatus.Type]
            }

        let SecurityProviderPlugin =
            Class "SecurityProviderPlugin"
            |+> Instance [
                "installIfNeeded" => T<unit> ^-> T<Promise<_>>[InstallIfNeededReturnValue]
            ]

    [<AutoOpen>]
    module DeviceCheck =
        let GenerateTokenResult = 
            Pattern.Config "GenerateTokenResult" {
                Required = []
                Optional = ["token", T<string>]
            }

        let DeviceCheckPlugin =
            Class "DeviceCheckPlugin"
            |+> Instance [
                "generateToken" => T<unit> ^-> T<Promise<_>>[GenerateTokenResult]
            ]

    let CapacitorCommunity = 
        Class "CapacitorCommunity"
        |+> Static [
            "DeviceCheck" =? DeviceCheckPlugin
            |> Import "DeviceCheck" "@capacitor-community/device-check"
            "SecurityProvider" =? SecurityProviderPlugin
            |> Import "SecurityProvider" "@capacitor-community/security-provider"
            "ScreenBrightness" =? ScreenBrightnessPlugin
            |> Import "ScreenBrightness" "@capacitor-community/screen-brightness"
            "TextToSpeech" =? TextToSpeechPlugin
            |> Import "TextToSpeech" "@capacitor-community/text-to-speech"
            "NativeAudio" =? NativeAudioPlugin
            |> Import "NativeAudio" "@capacitor-community/native-audio"
            "BluetoothLe" =? BluetoothLePlugin
            |> Import "BluetoothLe" "@capacitor-community/bluetooth-le"
            "SafeArea" =? SafeAreaPlugin
            |> Import "SafeArea" "@capacitor-community/safe-area"
            "CameraPreview" =? CameraPreviewPlugin
            |> Import "CameraPreview" "@capacitor-community/camera-preview"
            "SpeechRecognition" =? SpeechRecognitionPlugin
            |> Import "SpeechRecognition" "@capacitor-community/speech-recognition"
            "Intercom" =? IntercomPlugin
            |> Import "Intercom" "@capacitor-community/intercom"
            "PhotoViewer" =? PhotoViewerPlugin
            |> Import "PhotoViewer" "@capacitor-community/photoviewer"
            "AppIcon" =? AppIconPlugin
            |> Import "AppIcon" "@capacitor-community/app-icon"
            "Media" =? MediaPlugin
            |> Import "Media" "@capacitor-community/media"
            "FacebookLogin" =? FacebookLoginPlugin
            |> Import "FacebookLogin" "@capacitor-community/facebook-login"
            "Stripe" =? StripePlugin
            |> Import "Stripe" "@capacitor-community/stripe"
            "PrivacyScreen" =? PrivacyScreenPlugin
            |> Import "PrivacyScreen" "@capacitor-community/privacy-screen"
            "KeepAwake" =? KeepAwakePlugin
            |> Import "KeepAwake" "@capacitor-community/keep-awake"
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
            Namespace "WebSharper.Capacitor.Community" [
                CapacitorCommunity
                PermissionState
                PluginListenerHandle

                FacebookLoginPlugin
                StripePlugin
                PrivacyScreenPlugin
                KeepAwakePlugin
                ContactsPlugin
                DatePickerPlugin
                SQLitePlugin
                ImageToTextPlugin
                FileOpenerPlugin
                AppleSignInPlugin
                BackgroundGeolocationPlugin
                VolumeButtonsPlugin
                InAppReviewPlugin
                MediaPlugin
                AppIconPlugin
                PhotoViewerPlugin
                IntercomPlugin
                SpeechRecognitionPlugin
                CameraPreviewPlugin
                SafeAreaPlugin
                BluetoothLePlugin
                NativeAudioPlugin
                TextToSpeechPlugin
                ScreenBrightnessPlugin
                SecurityProviderPlugin
                DeviceCheckPlugin
            ]
            Namespace "WebSharper.Capacitor.Community.FacebookLogin" [
                FacebookConfiguration; LoginOptions; FacebookLoginResponse; FacebookCurrentAccessTokenResponse; ProfileOptions; AccessToken
            ]
            Namespace "WebSharper.Capacitor.Community.Stripe" [
                CapacitorStripeContext; PresentPaymentSheetResult; ConfirmPaymentFlowResult; PresentPaymentFlowResult; PresentGooglePayResult
                StripeURLHandlingOptions; StripeInitializationOptions; CreatePaymentSheetOption; CreatePaymentFlowOption; BillingDetailsCollectionConfiguration
                CreateGooglePayOption; DidSelectShippingContact; ShippingContact; CreateApplePayOption; ShippingContactType; PaymentSummaryType
                PaymentFlowResultInterface; AddressCollectionMode; CollectionMode; GooglePayResultInterface; ApplePayResultInterface; PaymentSheetEventsEnum
                GooglePayEventsEnum; ApplePayEventsEnum; PresentApplePayResult; StyleType; PaymentSheetResultInterface; PaymentFlowEventsEnum
            ]
            Namespace "WebSharper.Capacitor.Community.PrivacyScreen" [
                PluginsConfig; PrivacyScreenConfig; ContentMode
            ]
            Namespace "WebSharper.Capacitor.Community.KeepAwake" [
                IsKeptAwakeResult; IsSupportedResult
            ]
            Namespace "WebSharper.Capacitor.Community.Contacts" [
                PickContactOptions; PickContactResult; DeleteContactOptions; CreateContactOptions; ContactInput; PostalAddressInput; EmailInput
                PhoneInput; BirthdayInput; OrganizationInput; NameInput; CreateContactResult; GetContactsOptions; GetContactsResult; GetContactOptions
                GetContactResult; Projection; ContactPayload; ImagePayload; PostalAddressPayload; EmailPayload; PhonePayload; BirthdayPayload
                OrganizationPayload; NamePayload; PhoneType; EmailType; PostalAddressType; Contacts.PermissionStatus
            ]
            Namespace "WebSharper.Capacitor.Community.DatePicker" [
                DatePickerResult; DatePickerOptions; DatePickerIosOptions; DatePickerBaseOptions
                DatePickerIosStyle; DatePickerTheme; DatePickerMode
            ]
            Namespace "WebSharper.Capacitor.Community.SQLite" [
                capSQLiteRunOptions; capSQLiteSetOptions; capSQLiteSet; capSQLiteExecuteOptions; capNCOptions; capNCConnectionOptions
                capNCDatabasePathOptions; capAllConnectionsOptions; capVersionResult; capSQLiteUrl; capSQLiteChanges; Changes
                capEchoOptions; capEchoResult; capConnectionOptions; capChangeSecretOptions; capSetSecretOptions; capSQLiteResult
                capSQLiteLocalDiskOptions; capSQLiteOptions; capSQLiteHTTPOptions; capSQLiteFromAssetsOptions; capSQLiteSyncDateOptions
                capSQLiteExportOptions; capSQLiteImportOptions; capSQLiteSyncDate; capSQLiteJson; JsonSQLite; JsonView; JsonTable
                JsonIndex; JsonTrigger; JsonColumn; capSQLiteValues; capNCDatabasePathResult; capSQLiteTableOptions; capSQLitePathOptions
                capSQLiteUpgradeOptions; capSQLiteVersionUpgrade; capSQLiteQueryOptions
            ]
            Namespace "WebSharper.Capacitor.Community.ImageToText" [
                TextDetections; TextDetection; DetectTextBase64Options; DetectTextFileOptions; ImageOrientation
            ]
            Namespace "WebSharper.Capacitor.Community.FileOpener" [
                FileOpenerOptions; ChooserPosition
            ]
            Namespace "WebSharper.Capacitor.Community.AppleSignIn" [
                SignInWithAppleOptions; SignInWithAppleResponse; SignInWithAppleResponseData
            ]
            Namespace "WebSharper.Capacitor.Community.BackgroundGeolocation" [
                Location; CallbackError; WatcherOptions; RemoveWatcherOptions; CallbackOptions
            ]
            Namespace "WebSharper.Capacitor.Community.VolumeButtons" [
                Direction; VolumeButtonsResult; GetIsWatchingResult; VolumeButtonsOptions
            ]
            Namespace "WebSharper.Capacitor.Community.Media" [
                GetMediaByIdentifierOptions; PhotoResponse; MediaFetchOptions; MediaSort; MediaSortKeys
                MediaSaveOptions; MediaAlbumCreate; MediaAlbumResponse; MediaAlbum; MediaAlbumType
                MediaPath; MediaResponse; MediaAsset; MediaLocation; MediaTypes; AlbumsPathResponse
            ]
            Namespace "WebSharper.Capacitor.Community.AppIcon" [
                GetNameResponse; IsSupportedResponse; ResetOptions; IconOptions
            ]
            Namespace "WebSharper.Capacitor.Community.PhotoViewer" [
                CapPaths; CapHttpResult; CapHttpOptions; CapShowResult; CapEchoResult
                CapShowOptions; ViewerOptions; MovieOptions; Image; CapEchoOptions
            ]
            Namespace "WebSharper.Capacitor.Community.Intercom" [
                IntercomUserUpdateOptions; IntercomPushNotificationData; LogEventOptions
                RegisterIdentifiedUserOptions; LoadWithKeysOptions
            ]
            Namespace "WebSharper.Capacitor.Community.SpeechRecognition" [
                PartialResultsResponse; ListeningStateResponse; SupportedLanguagesResponse; ListeningResponse
                MatchesResponse; AvailableResponse; UtteranceOptions; PermissionStatus
            ]
            Namespace "WebSharper.Capacitor.Community.CameraPreview" [
                CameraOpacityOptions; CameraSampleOptions; CameraPreviewPictureOptions; CameraPreviewOptions
            ]
            Namespace "WebSharper.Capacitor.Community.SafeArea" [
                Config; SafeAreaPluginConfig
            ]
            Namespace "WebSharper.Capacitor.Community.BluetoothLe" [
                DataViewCallback; StringType; ScanResultType; BooleanType; ScanResult; TimeoutOptions
                BleService; BleCharacteristic; BleDescriptor; BleCharacteristicProperties; BleDevice
                RequestBleDeviceOptions; DisplayStrings; InitializeOptions; ConnectionPriority; ScanMode
            ]
            Namespace "WebSharper.Capacitor.Community.NativeAudio" [
                SetVolumeOptions; PlayOptions; PreloadOptions; ConfigureOptions
            ]
            Namespace "WebSharper.Capacitor.Community.TextToSpeech" [
                SpeechSynthesisVoice; TTSOptions; ListenFuncOptions
            ]
            Namespace "WebSharper.Capacitor.Community.ScreenBrightness" [
                GetBrightnessReturnValue; SetBrightnessOptions
            ]
            Namespace "WebSharper.Capacitor.Community.SecurityProvider" [
                InstallIfNeededReturnValue; SecurityProviderStatus
            ]
            Namespace "WebSharper.Capacitor.Community.DeviceCheck" [
                GenerateTokenResult
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
