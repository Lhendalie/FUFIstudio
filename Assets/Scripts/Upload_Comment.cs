using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Upload_Comment : NetworkBehaviour
{
    public static List<string> comments = new List<string>();

    [SyncVar]
    public string allComments;

}
