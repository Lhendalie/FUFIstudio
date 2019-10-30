using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class Player_Network : NetworkBehaviour
{
    public GameObject firstPersonCharacter;
    public GameObject[] characterModel;
    public bl_ChatManager myChat;

    public override void OnStartLocalPlayer() // This will run for the specific online character
    {
        GetComponent<FirstPersonController>().enabled = true;
        firstPersonCharacter.SetActive(true);

        foreach(GameObject go in characterModel)
        {
            go.SetActive(false); // Disable ethan model
        }

        myChat = FindObjectOfType<bl_ChatManager>();
        myChat.SetupClient();
        myChat.SetPlayerName("PlayerNameHere", true);
    }

}

