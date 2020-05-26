using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Position
{
    public int x, y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Position operator +(Position a, Position b)
    {
        return new Position
        {
            x = a.x + b.x,
            y = a.y + b.y
        };
    }

    public override string ToString()
    {
        return string.Format("(x = {0} | y = {1}", x, y);
    }
}
