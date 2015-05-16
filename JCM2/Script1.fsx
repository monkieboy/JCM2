#load "../packages/FsLab.0.2.7/FsLab.fsx"

open Foogle
open Deedle
open FSharp.Data
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

Chart.LineChart
    (Seasonify.getSeasonSeries(``Season 12 to 13``) ,
    Labels = ["Goals"])
Chart.LineChart
    (Seasonify.getSeasonSeries(``Season 13 to 14``) ,
    Labels = ["Goals"])