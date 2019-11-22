using UnityEngine;
using UnityEngine.UI;

public class ShowCode : MonoBehaviour
{
    Text screenText;
    // Start is called before the first frame update
    void Start()
    {
        screenText = gameObject.GetComponent<Text>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (InteractWithComputer.userCode != null)
        {
            screenText.text = InteractWithComputer.userCode.text;
        }
    }
}
