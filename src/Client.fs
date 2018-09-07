namespace Samples

open WebSharper
open WebSharper.UI
open WebSharper.UI.Html
open WebSharper.UI.Client
open WebSharper.Moment

[<JavaScript>]
module HelloWorld =

    [<SPAEntryPoint>]
    let Main =
        let locales = 
            [
                "en"
                "fr"
                "es"
                "it"
                "de"
            ]
            
        let zones =
            [
                "Cairo", "Africa/Cairo"
                "New York", "America/New_York"
                "Tokyo", "Asia/Tokyo"  
            ]
        
        locales
        |> List.map (fun l ->
            let now = Moment().Locale(l)
            let diff = Duration(-4, "hours").Add(-47, "minutes")
            let ago = now.Clone().Add(diff)
            div [] [
                h1 [] [text l]
                p [] [
                    text ("Local " + now.Format("LLL"))
                    br [] []
                    text (ago.From(now) + ": " + ago.Format("LLL"))
                ]
                zones
                |> List.map (fun (t, z) ->
                    p [] [
                        text (t + ": " + now.Clone().Tz(z).Format("LLL"))
                        br [] []
                        text (ago.From(now) + ": " + ago.Tz(z).Format("LLL"))
                    ])
                |> Doc.Concat
            ])
        |> Doc.Concat
        |> Doc.RunById "main"
