module ListHelper
    open System
    
    let rec map (f:'a->'b) = function 
        | [] -> []
        | x::xs -> (f x)::(map f xs)

    let rec mapC (f:Func<'a,'b>) = function 
        | [] -> []
        | x::xs -> (f.Invoke(x))::(mapC f xs)

    let rec filter (p:'a->bool) = function
        | [] -> []
        | x::xs ->
            if p x then
                x::(filter p xs)
            else
                filter p xs

    let rec filterC (p:Predicate<'a>) = function
        | [] -> []
        | x::xs ->
            if p.Invoke(x) then
                x::(filterC p xs)
            else
                filterC p xs

    let rec get_at_index (index : int) (offset : int) = function
        | [] -> null
        | x::xs ->
            if offset = index then
                x
            else 
                get_at_index index (offset + 1) xs

    let getValueAtIndex = fun (index : int) (list: 'a list) -> get_at_index index 0 list

    let rec length = function
        | [] -> 0
        | x::xs -> 1 + length xs

