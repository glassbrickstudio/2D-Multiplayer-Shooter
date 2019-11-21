using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ConnectedPlayers : MonoBehaviour
{



    public GameObject CurrentPlayer_PREFAB;

    public GameObject CurrentPlayers_GRID;


    public void AddLocalPlayer()
    {

        GameObject obj = Instantiate(CurrentPlayer_PREFAB, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(CurrentPlayers_GRID.transform, false);
        obj.GetComponentInChildren<Text>().text = "You: " + PhotonNetwork.NickName;
        obj.GetComponentInChildren<Text>().color = Color.green;


    }




    //called from gamemanager
    [PunRPC]
    public void UpdatePlayerList(string name)
    {
        GameObject obj = Instantiate(CurrentPlayer_PREFAB, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(CurrentPlayers_GRID.transform, false);
        obj.GetComponentInChildren<Text>().text = name;

    }


    //called from gamemanager
    public void RemovePlayerList(string name)
    {
        foreach (Text playerName in CurrentPlayers_GRID.GetComponentsInChildren<Text>())
        {
            if (name == playerName.text)
                Destroy(playerName.transform.parent.gameObject);

        }

    }




}
