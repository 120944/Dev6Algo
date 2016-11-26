module Sorting
    open Microsoft.Xna.Framework
    open System

    let rec split_on = function 
        | (n, []) -> ([],[])
        | (1, x::xs) -> (x::[],xs)
        | (n, x::xs) ->
            let (l, r) = split_on (n-1, xs)
            x::l, r

    let split = fun (list : 'a list) -> 
        split_on (list.Length/2, list)

    let rec mergeList = fun (selector:Func<'a,'b>) l r ->
        match (l, r) with
        | [], l -> l
        | l, [] -> l
        | x::xs, y::ys ->
            if (selector.Invoke(x)) <= (selector.Invoke(y)) then
                x :: (mergeList selector xs (y::ys))
            else
                y :: (mergeList selector (x::xs) ys)

    let rec mergeSort = fun (selector:Func<'a,'b>) (list : 'a list) ->
        match list with
        | [] -> [] // no elements or end of list
        | [x] -> [x] // one element or last element
        | xs ->
            let (l, r) = split xs
            let l_sorted = mergeSort selector l
            let r_sorted = mergeSort selector r
            mergeList selector l_sorted r_sorted