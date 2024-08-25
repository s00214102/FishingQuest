using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

// on key press
// - can no longer move 
public class PlayerFishing : MonoBehaviour
{
    private AdvancedWalkerController walkerController;

    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // context area
        if (other.CompareTag("ContextArea"))
        {
            Debug.Log("Player has entered a context area.");

            // switch player state

        }
    }
}
