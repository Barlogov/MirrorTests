using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine.UI;

public class UI_HUD : NetworkBehaviour
{
    public Text countText;
    [SyncVar] public int count = 0;

    // Эта функция вызывается после нажатия кнопки у пользователя
    public void AddOneToTheCount()
    {
        CmdAddOneToTheCount();
    }

    // Просим сервер добавить 1 к синхронизируемой переменной
    [Command(requiresAuthority = false)]
    void CmdAddOneToTheCount()
    {
        count++;
        RpcUpdateHUDCount();
    }
    
    // Сервер просит пользователей обновить свой ХУД до синхронизированной переменной
    [ClientRpc]
    void RpcUpdateHUDCount()
    {
        countText.text = count.ToString();
    }
}
