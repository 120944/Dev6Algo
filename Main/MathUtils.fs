module MathUtils
    open System
    open Microsoft.Xna.Framework

    let euclosure (root:Vector2) : (Vector2 -> float) =
        let euclidian_distance (target : Vector2) = Math.Pow(Math.Pow(float(root.X - target.X), 2.0) + Math.Pow(float (root.Y - target.Y), 2.0), 0.5)
        
        euclidian_distance
        
     