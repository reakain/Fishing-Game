using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish 
{
    bool isSpirit;
    string type;
    string[] seasons;
    string[] weather;
    public float reelMax { get; private set; }
    public float reelMin { get; private set; }


    Fish(bool isspirit = false)
    {
        
    }

    public static Fish GenerateFish(string season, string weather, bool isspirit = false)
    {
        return new Fish();
    }
}
