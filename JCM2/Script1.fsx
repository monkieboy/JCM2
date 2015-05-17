#load "../packages/FsLab.0.2.7/FsLab.fsx"
#load "../packages/FSharp.Charting.0.90.10/FSharp.Charting.fsx"

open Foogle
open Deedle
open FSharp.Data
open FSharp.Charting
#load "seasonify.fsx"

let ``Season 12 to 13`` = "https://raw.githubusercontent.com/footballcsv/en-england/master/2010s/2012-13/1-premierleague.csv"
let ``Season 13 to 14`` = "https://raw.githubusercontent.com/footballcsv/en-england/master/2010s/2013-14/1-premierleague.csv"

//let season1 = Seasonify.getSeasonSeries ``Season 12 to 13``
//let season2 = Seasonify.getSeasonSeries ``Season 13 to 14``
//
//let seasonsCombined =
//    (season1,season2)
//    ||> Seq.map2 (fun (a, b)(_) ->
//
//Chart.LineChart
// ( zippedInputs,
//  Labels = ["Goals"])

//Chart.LineChart
//    (Seasonify.getSeasonSeries(``Season 12 to 13``) ,
//    Labels = ["Goals"])

let Season2012 = Seasonify.getSeasonSeries(``Season 12 to 13``)
let Season2013 = Seasonify.getSeasonSeries(``Season 13 to 14``)

let days = [1..365] |> Seq.map(fun d -> "fixture " + d.ToString()) |> List.ofSeq

Chart.Combine(
    [ Chart.Line (Season2012, Name = "Season 2012/2013", Labels = days @ ["Goals"])
      Chart.Line (Season2013, Name = "Season 2013/2014", Labels = days @ ["Goals"]) ])
   