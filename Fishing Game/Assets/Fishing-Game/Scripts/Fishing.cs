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
        public float reelPowerDelta = 1.0f;

        bool isLookingForFish = false;
        FishingState fishingState = FishingState.None;

        Animator animator;

        float escapeTimer = 0f;
        float catchTimer = 0f;
        float fishCheckTimer = 0f;

        Fish currentFish;
        float reelPower = 0f;
        bool pullEscape = false; // Escaping = false, pulling = true

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
            if (fishingState == FishingState.Looking)
            {
                if (fishCheckTimer < fishCheckUpdate)
                {
                    fishCheckTimer += Time.deltaTime;
                }
                else
                {
                    CheckForFish();
                }

            }
            else if (fishingState == FishingState.Hooking)
            {
                if (escapeTimer < escapeWindow)
                {
                    escapeTimer += Time.deltaTime;
                }
                else
                {
                    FishEscaped();
                }
            }
            else if (fishingState == FishingState.Reeling)
            {
                reelPower -= reelSpeed * Time.deltaTime;
                if (pullEscape)
                {
                    if (currentFish.reelmin < reelPower && reelPower < currentFish.reelmax)
                    {
                        if (catchTimer < escapeWindow)
                        {
                            catchTimer += reelSpeed * Time.deltaTime;
                        }
                        else
                        {
                            FishCaught();
                        }
                    }
                    else
                    {
                        pullEscape = false;
                        escapeTimer = 0f;
                    }
                }
                else
                {
                    if (currentFish.reelmin < reelPower && reelPower < currentFish.reelmax)
                    {
                        pullEscape = true;
                    }
                    else if (escapeTimer < escapeWindow)
                    {
                        escapeTimer += reelSpeed * Time.deltaTime;
                    }
                    else
                    {
                        FishEscaped();
                    }
                }
            }
        }

        public void TryFishing()
        {
            switch (fishingState)
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
            Debug.Log("Reel dat fish in!");
            // Create your fishing UI with range bar for the tapping!
        }

        public void AdjustReel()
        {
            reelPower += reelPowerDelta;
            Debug.Log("Reel power is: " + reelPower.ToString());
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