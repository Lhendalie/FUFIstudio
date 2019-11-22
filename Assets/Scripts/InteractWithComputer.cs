using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InteractWithComputer : NetworkBehaviour
{
    GameObject codePanel;
    public static Text userCode;
    // Start is called before the first frame update
    void Start()
    {
        GameObject parent = GameObject.Find("Parent");
        codePanel = parent.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isServer)
            return;

        if (other.CompareTag("computer"))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                codePanel.SetActive(true);

                userCode = GameObject.Find("UserCode").GetComponent<Text>();

                Debug.Log(userCode.text);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                codePanel.SetActive(false);
            }
        }       
    }

    [ClientRpc]
    private void RpcGetInput()
    {
        
    }

    [ClientRpc]
    void RpcShowInput()
    {

    }
}
