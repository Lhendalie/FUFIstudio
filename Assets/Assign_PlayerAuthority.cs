using UnityEngine;
using UnityEngine.Networking;

public class Assign_PlayerAuthority : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
    }
}
