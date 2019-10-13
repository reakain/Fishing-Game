using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [Range(0f,1f)]
    public float fishThreshold = 0.2f;

    bool isLookingForFish = false;
    FishingState fishingState = FishingState.None;

    enum FishingState
    {
        None,
        Looking,
        Hooking,
        Reeling,
        Caught
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fishingState == FishingState.Looking)
        {
            CheckForFish();
        }
        else if(fishingState == FishingState.Hooking)
        {

        }
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
        // Run your line cast animation
        // Change your animation state to the fishing state.
        // Start checking for fish on a ratio!
        //isLookingForFish = true;
        fishingState = FishingState.Looking;
    }

    public void CheckForFish()
    {
        // Check if you found a fish! 
        if(Random.value < fishThreshold )
        {
            // Found a fish!
            // Generate notification shake, noise, and exclamation
            // Then change state to hooking!
            fishingState = FishingState.Hooking;
        }
    }

    public void ReelFish()
    {

    }

    public void StopFishing()
    {
        CharacterController.instance.isFishing = false;
    }
}
