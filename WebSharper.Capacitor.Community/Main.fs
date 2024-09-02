namespace WebSharper.Capacitor.Community

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =
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

    let CapacitorCommunity = 
        Class "Capacitor.Community"
        |+> Static [
            "FacebookLogin" =? FacebookLoginPlugin
            |> Import "FacebookLogin" "@capacitor-community/facebook-login"
        ]

    let Assembly =
        Assembly [
            Namespace "Websharper.Capacitor.Community" [
                FacebookLoginPlugin
            ]
            Namespace "Websharper.Capacitor.Community.FacebookLogin" [
                FacebookConfiguration; LoginOptions; FacebookLoginResponse; FacebookCurrentAccessTokenResponse; ProfileOptions; AccessToken
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
