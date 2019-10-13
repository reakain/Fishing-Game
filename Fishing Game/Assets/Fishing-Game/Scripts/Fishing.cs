using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFishing()
    {
        CharacterController.instance.isFishing = true;
        // Generate fishing pole
        // Cast fishing line
        CastLine();
    }

    public void CastLine()
    {

    }

    public void CheckForFish()
    {

    }

    public void ReelFish()
    {

    }

    public void StopFishing()
    {
        CharacterController.instance.isFishing = false;
    }
}
