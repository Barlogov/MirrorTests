using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public readonly SyncList<NetworkIdentity> listOfConnectedPlayers = new SyncList<NetworkIdentity>();

    
    public override void OnStartClient()
    {
        base.OnStartClient();
        CmdAddPlayerToTheList();
    }

    // assigned in inspector
    public GameObject cubePrefab;

    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKey(KeyCode.X))
            CmdDropCube();
        if (Input.GetKey(KeyCode.Space))
            CmdShowConnectionList();
    }

    [Command]
    void CmdDropCube()
    {
        if (cubePrefab != null)
        {
            Vector3 spawnPos = transform.position + transform.forward * 2;
            Quaternion spawnRot = transform.rotation;
            GameObject cube = Instantiate(cubePrefab, spawnPos, spawnRot);
            NetworkServer.Spawn(cube);
        }
    }

    [Command]
    void CmdShowConnectionList()
    {
        foreach (NetworkIdentity networkIdentity in listOfConnectedPlayers)
        {
            Debug.Log(networkIdentity);
        }
    }

    [Command]
    void CmdAddPlayerToTheList()
    {
        if (listOfConnectedPlayers.Contains(this.netIdentity))
        {
            Debug.Log($"Net Identity Already Exist: {this.netIdentity}");
        }
        else
        {
            listOfConnectedPlayers.Add(this.netIdentity);
            Debug.Log($"Net Identity Added: {this.netIdentity}");
        }
    }
}
