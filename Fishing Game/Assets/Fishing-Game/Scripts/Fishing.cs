using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class Fishing : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float fishThreshold = 0.2f;
        [Range(0f, 1f)]
        public float spiritThreshold = 0.05f;
        public float escapeWindow = 1.0f;
        public float fishCheckUpdate = 1.0f;
        public float reelSpeed = 1.0f;
        public float reelPowerDelta = 0.2f;

        bool isLookingForFish = false;
        FishingState fishingState = FishingState.None;

        Animator animator;

        float escapeTimer = 0f;
        float catchTimer = 0f;
        float fishCheckTimer = 0f;

        Fish currentFish;
        float reelPower = 0f;
        float reelMin = 0f;
        float reelMax = 0f;
        bool pullEscape = false; // Escaping = false, pulling = true

        enum FishingState
        {
            None,
            Looking,
            Hooking,
            Reeling,
            Escaping,
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
            switch(fishingState)
            {
                // If looking for fish, update our fish check timer, and if it's reached the check time, check for a fish!
                case FishingState.Looking:
                    if (fishCheckTimer < fishCheckUpdate)
                    {
                        fishCheckTimer += Time.deltaTime;
                    }
                    else
                    {
                        CheckForFish();
                    }
                    break;
                // Once we found a fish and notify player, track a window. If the player doesn't press the button in time, the fish escapes!
                case FishingState.Hooking:
                    if (escapeTimer < escapeWindow)
                    {
                        escapeTimer += Time.deltaTime;
                    }
                    else
                    {
                        FishEscaped();
                    }
                    break;
                // Okay, fish is getting reeled in!
                case FishingState.Reeling:
                    AdjustReel(); // Timestep our current reeling power
                    // And our reel power is in the fishy catch zone
                    if (reelMin < reelPower && reelPower < reelMax)
                    {
                        // Add to the catch timer if we haven't reached the catch count
                        if (catchTimer < escapeWindow)
                        {
                            catchTimer += reelSpeed * Time.deltaTime;
                        }
                        // Or if we have: CATCH DAT FISH
                        else
                        {
                            FishCaught();
                        }
                    }
                    // If we aren't in the catch zone...
                    else
                    {
                        fishingState = FishingState.Escaping; // Swap state to fish escaping
                        escapeTimer = 0f;   // Reset our escape timer so it starts escaping again
                    }
                    break;
                // If fish is currently escaping
                case FishingState.Escaping:
                    AdjustReel(); // Timestep our current reeling power
                    // But we got in the catch zone!
                    if (reelMin < reelPower && reelPower < reelMax)
                    {
                        fishingState = FishingState.Reeling; // Swap to catching the fish!
                    }
                    // Otherwise if we haven't reached the end of escape timer...
                    else if (escapeTimer < escapeWindow)
                    {
                        escapeTimer += reelSpeed * Time.deltaTime; // Add to the escape timer
                    }
                    // Otherwise if we have reached the end
                    else
                    {
                        FishEscaped(); // The fish is freeeee
                    }
                    break;
            }
            
            
        }

        // Handle fishing logic character input based on current fishing state
        public void TryFishing()
        {
            switch (fishingState)
            {
                // If we are not currently fishing, start fishing
                case FishingState.None:
                    StartFishing();
                    break;
                // If we're currently looking for a fish, then go ahead and stop fishing
                case FishingState.Looking:
                    StopFishing();
                    break;
                // If we're currently trying to hook the fish, then hook it and start reeling!
                case FishingState.Hooking:
                    ReelFish();
                    break;
                // If we're reeling the fish, add power to your reel power!
                case FishingState.Reeling:
                    AdjustReel(true);
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
            if (fishCheck < fishThreshold)
            {
                Debug.Log("Found a fishy!");
                // Found a fish! Now generate it and if it's actually a spirit, give us a spirit.
                currentFish = FishLibrary.GenerateFish("", "", (fishCheck < spiritThreshold));

                // Generate notification shake, noise, and exclamation

                // Then change state to hooking!
                fishingState = FishingState.Hooking;
                escapeTimer = 0f;
            }
            fishCheckTimer = 0f;
        }

        public void ReelFish()
        {
            fishingState = FishingState.Reeling;
            escapeTimer = 0f;
            catchTimer = 0f;
            reelMin = 0 + currentFish.reelwindow / 2f;
            reelMax = 1 - currentFish.reelwindow / 2f;
            Debug.Log("Reel dat fish in!");
            // Create your fishing UI with range bar for the tapping!
            UIFishBar.instance.SetValue(currentFish.reelwindow);
        }

        public void AdjustReel(bool addPower = false)
        {
            Debug.Log("Reel power is: " + reelPower.ToString());

            // Add power if we're adding power
            if (addPower)
            {
                reelPower = (reelPower < 1f) ? reelPower + reelPowerDelta : 1f;
                return;
            }

            // Subtract from your reel power (but don't go below zero)
            reelPower = (reelPower > 0f) ? reelPower - reelSpeed * Time.deltaTime : 0f;
        }

        public void StopFishing()
        {
            CharacterController.instance.isFishing = false;
            fishingState = FishingState.None;
            Debug.Log("Stopped fishing!");
        }

        public void FishEscaped()
        {
            fishingState = FishingState.Escaped;
            Debug.Log("Fish escaped...");
            StopFishing();
        }

        public void FishCaught()
        {
            fishingState = FishingState.Caught;
            Debug.Log("Caught a fish!");
            StopFishing();
        }
    }
}