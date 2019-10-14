using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class FishingZone : MonoBehaviour
    {
        // When the player enters the fishing zone, tell it that it can fish
        void OnTriggerEnter2D(Collider2D other)
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                controller.canFish = true;
            }
        }

        // When the player exits the fishing zone, tell it you can't fish no more
        private void OnTriggerExit2D(Collider2D other)
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                controller.canFish = false;
            }
        }
    }
}