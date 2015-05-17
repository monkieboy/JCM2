//// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
//// for more guidance on F# programming.
//
//#load "Library1.fs"
//open JCM2
//#load "../packages/FsLab.0.2.7/FsLab.fsx"
//// Define your library scripting code here
//
//open Foogle
//open Deedle
//open FSharp.Data
//
//let wb = WorldBankData.GetDataContext()
//let cz = wb.Countries.``Czech Republic``.Indicators
//let eu = wb.Countries.``European Union``.Indicators
//
//let czschool = series cz.``School enrollment, tertiary (% gross)``
//let euschool = series eu.``School enrollment, tertiary (% gross)``
//
//abs (czschool - euschool)
//|> Series.sort
//|> Series.rev
//|> Series.take 5
//
//Chart.LineChart
// ([ for y in 1985 .. 2012 ->
//     string y,
//       [ cz.``School enrollment, tertiary (% gross)``.[y]
//         eu.``School enrollment, tertiary (% gross)``.[y] ] ],
//  Labels = ["CZ"; "EU"])
//
//
//  // For the fixture date 2013-08-17 how many goals were scored
//  // and chart it
//

#load "../packages/FsLab.0.2.7/FsLab.fsx"

open Foogle
open Deedle
open FSharp.Data

open System
open System.Net
open System.Text.RegularExpressions

type Fixture = 
    {   Date : DateTime
        ``Team 1`` : string
        ``Team 2`` : string
        FT : (int * int)
        HT : (int * int) }

let getSeasonSeries (seasonLink:string) = 
    let wc = new WebClient()
    let rawData = wc.DownloadString(seasonLink)
    let lines = 
        Regex.Split(rawData, "\r\n") 
        |> Seq.skip(1) 
        |> Seq.filter (fun s -> not (String.IsNullOrWhiteSpace(s)))
        |> Array.ofSeq
    let getScore (text : string) =
        let parts = text.Split('-') |> Array.map int
        (parts.[0], parts.[1])



    let data = 
        lines 
        |> Array.map (fun line -> line.Split(',') )
        |> Array.map (fun parts -> 
        {   Date = DateTime.ParseExact(parts.[0], "yyyy-MM-dd", null)
            ``Team 1`` = parts.[1]
            ``Team 2`` = parts.[2]
            FT = getScore parts.[3]      
            HT = getScore parts.[4] })

    let groupedFixtures =
        data
        |> Seq.groupBy (fun f -> f.Date)

    let result = 
        groupedFixtures
        |> Seq.map (fun group -> ((fst group).ToString("yyyy-MM-dd ddd"), [(snd group) |> Seq.fold (fun s f -> s + (fst f.FT) + (snd f.FT)) 0]) )
        |> Seq.map (fun (d, s) -> (d, s |> Seq.sum))
    result

//Chart.LineChart
// (goalsByDate,
//  Labels = ["Goals"])