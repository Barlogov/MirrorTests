using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    
    public readonly List<GameObject> listOfCubes = new List<GameObject>();
    public GameObject cubePrefab;

    
    public override void OnStartClient()
    {
        if (!isLocalPlayer) return;
        
        base.OnStartClient();
        //CubePositionManager.instance.CheckPlatform();
        
        foreach (KeyValuePair<int,Vector3> cubeRef in CubePositionManager.instance.cubesDictionary)
        {
            Vector3 posV3 = cubeRef.Value;
            listOfCubes.Add(Instantiate(cubePrefab, posV3, Quaternion.identity)); 
        }
        
    }


    void Update()
    {
        if (!isLocalPlayer) return;

        // if (Input.GetKeyDown(KeyCode.Space))
        //     CmdCreateCube();

        foreach (KeyValuePair<int,Vector3> cubeRef in CubePositionManager.instance.cubesDictionary)
        {
            Vector3 posV3 = cubeRef.Value;
            int num = cubeRef.Key;
            listOfCubes[num].transform.position = posV3;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
                Debug.Log("Windows");
                CubePositionManager.instance.CmdChangeTheStateOfCubes();
            }
        }

    }
    
    


    // [Command]
    // void CmdCreateCube()
    // {
    //     if (cubePrefab != null)
    //     {
    //         Vector3 spawnPos = transform.position + transform.forward * 2;
    //         Quaternion spawnRot = transform.rotation;
    //         GameObject cube = Instantiate(cubePrefab, spawnPos, spawnRot);
    //         NetworkServer.Spawn(cube);
    //     }
    // }



}
