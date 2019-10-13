using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [Range(0f,1f)]
    public float fishThreshold = 0.2f;
    [Range(0f, 1f)]
    public float spiritThreshold = 0.05f;
    public float hookWindow = 1.0f;
    public float fishCheckUpdate = 1.0f;
    public float reelPowerDelta = 1.0f;

    bool isLookingForFish = false;
    FishingState fishingState = FishingState.None;

    Animator animator;

    float hookTimer = 0f;
    float fishCheckTimer = 0f;

    Fish currentFish;
    float reelPower = 0f;

    enum FishingState
    {
        None,
        Looking,
        Hooking,
        Reeling,
        Caught,
        Escaped
    }

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fishingState == FishingState.Looking)
        {
            if(fishCheckTimer < fishCheckUpdate)
            {
                fishCheckTimer += Time.deltaTime;
            }
            else
            {
                CheckForFish();
            }
            
        }
        else if(fishingState == FishingState.Hooking)
        {
            if(hookTimer < hookWindow)
            {
                hookTimer += Time.deltaTime;
            }
            else
            {
                fishingState = FishingState.Looking;
            }
        }
        else if(fishingState == FishingState.Reeling)
        {

        }
    }

    public void TryFishing()
    {
        switch(fishingState)
        {
            case FishingState.None:
                StartFishing();
                break;
            case FishingState.Looking:
                StopFishing();
                break;
            case FishingState.Hooking:
                ReelFish();
                break;
            case FishingState.Reeling:
                AdjustReel();
                break;
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
        // animator.SetBool("Fishing", true);
        // Change your animation state to the fishing state.
        // Start checking for fish on a ratio!
        //isLookingForFish = true;
        fishingState = FishingState.Looking;
    }

    public void CheckForFish()
    {
        var fishCheck = Random.value;
        // Check if you found a fish! 
        if(fishCheck < fishThreshold )
        {
            Debug.Log("Found a fishy!");
            // Found a fish! Now generate it and if it's actually a spirit, give us a spirit.
            currentFish = Fish.GenerateFish("", "", (fishCheck < spiritThreshold));

            // Generate notification shake, noise, and exclamation

            // Then change state to hooking!
            fishingState = FishingState.Hooking;
            hookTimer = 0f;
        }
        fishCheckTimer = 0f;
    }

    public void ReelFish()
    {
        fishingState = FishingState.Reeling;
        Debug.Log("Reel dat fish in!");
        // Create your fishing UI with range bar for the tapping!
    }

    public void AdjustReel()
    {
        reelPower += reelPowerDelta;
    }

    public void StopFishing()
    {
        CharacterController.instance.isFishing = false;
        fishingState = FishingState.None;
        Debug.Log("Stopped fishing!");
    }
}
