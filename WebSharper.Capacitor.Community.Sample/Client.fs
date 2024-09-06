namespace WebSharper.Capacitor.Community.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.Capacitor.Community

[<JavaScript>]
module Client =
    // The templates are loaded from the DOM, so you just can edit index.html
    // and refresh your browser, no need to recompile unless you add or remove holes.
    type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

    let People =
        ListModel.FromSeq [
            "John"
            "Paul"
        ]

    let TextToSpeech () = promise {
        let textToSpeech = TextToSpeech.TTSOptions(
            Text = "This is a sample text.",
            Lang = "en-US",
            Rate = 1.0,
            Pitch = 1.0,
            Volume = 1.0,
            Category = "ambient"
        )

        let! speech = CapacitorCommunity.TextToSpeech.Speak(textToSpeech)

        return speech
    }     

    [<SPAEntryPoint>]
    let Main () =
        let newName = Var.Create ""

        IndexTemplate.Main()
            .ListContainer(
                People.View.DocSeqCached(fun (name: string) ->
                    IndexTemplate.ListItem().Name(name).Doc()
                )
            )
            .Name(newName)
            .Add(fun _ ->
                People.Add(newName.Value)
                newName.Value <- ""
            )
            .TextToSpeech(fun _ -> 
                async {
                    return! TextToSpeech().Then(fun _ -> printfn "Speak Started").AsAsync()
                }
                |> Async.Start
            )
            .Doc()
        |> Doc.RunById "main"
