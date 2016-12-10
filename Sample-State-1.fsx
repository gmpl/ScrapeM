﻿#load "ScrapeM.fsx"

open System.Collections.Generic
open FSharp.Data
open FSharpPlus
open ScrapeM

// Simple Stateful query with a single result

type News = {User : string; Title : string; IntroText : string}

let q = linq {
    let! credential = result ("stopthebug", "stopthebug1")
    let!  _  = request "https://secure.moddb.com/members/login" None                        
    let!  _  = request "" (Some [KeyValuePair ("username", fst credential); KeyValuePair ("password", snd credential)])                                                       
    let! htm = request ("http://www.moddb.com/members/" + fst credential) None
    return htm |> parse |> cssSelect "p" |> Seq.head |> fun x -> x.InnerText() }

let bio = State.eval q Context.Empty