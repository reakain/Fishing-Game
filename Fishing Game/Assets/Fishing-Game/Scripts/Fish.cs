using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish 
{
    bool isSpirit;
    string type;
    string[] seasons;
    string[] weather;


    Fish(bool isspirit = false)
    {
        
    }

    public static Fish GenerateFish(string season, string weather, bool isspirit = false)
    {
        return new Fish();
    }
}
