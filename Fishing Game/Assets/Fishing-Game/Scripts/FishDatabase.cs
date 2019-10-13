/* 
Copyright (c) 2019 Reakain & Bandit Bots LLC

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE
OR OTHER DEALINGS IN THE SOFTWARE.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    #region FishDatabase
    [System.Serializable]
    public static class FishLibrary
    {
        public static FishDatabase fishLibrary = FishDatabase.CreateFromJSON(Resources.Load<TextAsset>("Fish/FishDatabase").text);

        public static Fish GenerateFish(string season, string weather, bool isspirit = false)
    {
        var possibleFish = System.Array.FindAll(fishLibrary.fish, f => f.isSpirit == isspirit);
        return possibleFish[Random.Range(0,possibleFish.Length - 1)];
    }

    }

    [System.Serializable]
    public class FishDatabase
    {
        public Fish[] fish;
        public static FishDatabase CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<FishDatabase>(jsonString);
        }
    }

    [System.Serializable]
    public class Fish 
{
        public string type = "";
    public bool isSpirit = false;
public string source = "";
    
    public float reelmin = 0f;
    public float reelmax = 0f;
    //public string[] seasons;
    //public string[] weather;


    Fish(bool isspirit = false)
    {
        
    }

    
}
    #endregion

}