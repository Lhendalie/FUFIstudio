using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class Player_Animation : NetworkBehaviour
{
    public Animator playerAnimator;
   
    // Update is called once per frame
    void Update()
    {
        CheckForPlayerInput();
    }

    void CheckForPlayerInput()
    {
        if (!isLocalPlayer)  //  only for current player
        {
            return;
        }

        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0 ||
            Mathf.Abs(Input.GetAxis("Horizontal")) > 0)  // Get the user input and plays the animation for it
        {
            playerAnimator.SetBool("Moving", true);
        }

        else
        {
            playerAnimator.SetBool("Moving", false);  // Stop Animation
        }
    }
}
