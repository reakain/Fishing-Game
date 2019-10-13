using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingZone : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.canFish = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.canFish = false;
        }
    }

}
