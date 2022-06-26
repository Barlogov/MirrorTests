using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Mirror;
using UnityEngine;

public class CubePositionManager : NetworkBehaviour
{
    // Сингалтон
    public static CubePositionManager instance;
    private void Awake()
    {
        instance = this;
    }

    


    public readonly SyncDictionary<int, Vector3> cubesDictionary = new SyncDictionary<int, Vector3>();
    
    // Проверка на какой платформе запущена программа
    public void CheckPlatform()
    {
       Debug.Log(Application.platform);
    }
    
    // Запрос сервера отрисовывать фигуры в нужных точках
    public void CreateObjects()
    {
        
    }

    // При загрузке сервера будут созданы 3 записи в SyncDictionary
    [Server]
    private void Start()
    {
        cubesDictionary.Add(0, new Vector3(0,2, 0)); // Верхний
        cubesDictionary.Add(1, new Vector3(-2,-1, 0)); // Левый нижний
        cubesDictionary.Add(2, new Vector3(2,-1, 0)); // Правый нижний
        Debug.Log("Dictionary Created");
    }

    [Command(requiresAuthority = false)]
    public void CmdChangeTheStateOfCubes()
    {
        for (int i = 0; i < cubesDictionary.Count; i++)
        {
            cubesDictionary[i] += Vector3.up;
        }
    }
    
    
    
}


