using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        // Read input and drive movement for the owning client only.
    }
}
