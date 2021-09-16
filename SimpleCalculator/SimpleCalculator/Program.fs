namespace SimpleCalculator.Program

open System

type SimpleCalculator() =     

    let SplitInput (input: string) (delimitersArray: string array) : string array =
       match input with
       | "" -> [||]
       | _ -> input.Split(delimitersArray, StringSplitOptions.RemoveEmptyEntries)

    let GetDelimitersString (input: string) : string = 
        match input with
        | "" -> ""
        | _ when input.StartsWith "//[" && input.Contains "]\n"-> input.Substring(2, input.IndexOf "]\n" - 1)
        | _ when input.StartsWith "//" && input.Contains "\n" ->
            Char.ToString input.[2]
        | _ -> ""

    let GetCleanInput (input: string) : string = 
        match input with
        | "" -> ""
        | _ when input.StartsWith "//" && input.Contains "\n" ->
            input.Substring(input.IndexOf "\n", (input.Length - input.IndexOf "\n"))
        | _ -> input

    member simpleCalculator.Add (input: string) : int =

        let delimiterString = GetDelimitersString input

        let cleanInput = GetCleanInput input

        let fullDelimitersArray = Array.append (delimiterString.Split([| "[" ; "]" |], StringSplitOptions.RemoveEmptyEntries)) [| "\n" ; "," |]

        let numbers = [ for number in (SplitInput cleanInput fullDelimitersArray) -> Int32.Parse number ]      

        let negativeValues, positiveValues = numbers |> List.partition (fun number -> number < 0)

        let limitedPositiveValues = positiveValues |> List.filter (fun number -> number <= 1000 ) 

        match input with
        | "" -> 0
        | _ -> 
            if negativeValues.Length > 0 then raise (new Exception(sprintf "negatives not allowed %A" negativeValues))
            List.sum limitedPositiveValues
                
