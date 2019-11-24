using UnityEngine;
using UnityEngine.Networking;

public class Player_Slides : NetworkBehaviour
{
    private int currentSlideNumber = 0;

    private void OnTriggerStay(Collider other)
    {
        if (isServer)
        {
            if (other.CompareTag("slides"))
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    RpcShowNextSlide(currentSlideNumber);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    RpcShowPreviousSlide(currentSlideNumber);
                }
            }
        }
        else if  (isLocalPlayer)
        {            
            if (other.CompareTag("slides"))
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    CmdShowNextSlide(currentSlideNumber);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    CmdShowPreviousSlide(currentSlideNumber);
                }
            }
        }        
    }

    [ClientRpc]
    private void RpcShowPreviousSlide(int givenNumber)
    {
        currentSlideNumber--;

        if (givenNumber <= 1)
        {
            currentSlideNumber = 1;
        }
        else if (givenNumber > 23)
        {
            currentSlideNumber = 1;
        }

        for (int i = 1; i < 24; i++)
        {
            GameObject.Find($"{i}").GetComponent<Renderer>().enabled = false;
        }

        GameObject slide = GameObject.Find($"{currentSlideNumber}");
        slide.GetComponent<Renderer>().enabled = true;
    }

    [Command]
    private void CmdShowPreviousSlide(int givenNumber)
    {
        RpcShowPreviousSlide(givenNumber);
    }

    [ClientRpc]
    void RpcShowNextSlide(int givenNumber)
    {
        currentSlideNumber++;

        if (givenNumber <= 0)
        {
            currentSlideNumber = 1;
        }
        else if (givenNumber > 22)
        {
            currentSlideNumber = 1;
        }

        for (int i = 1; i < 24; i++)
        {
            GameObject.Find($"{i}").GetComponent<Renderer>().enabled = false;
        }

        GameObject slide = GameObject.Find($"{currentSlideNumber}");
        slide.GetComponent<Renderer>().enabled = true;
    }

    [Command]
    private void CmdShowNextSlide(int givenNumber)
    {
        RpcShowNextSlide(givenNumber);
    }
}
