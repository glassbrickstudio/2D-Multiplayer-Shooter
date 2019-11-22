using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    public GameObject playerPrefab;

    public GameObject canvas;
    public GameObject cpCanvas;
    public GameObject sceneCam;
    private float TimeAmount = 5f;
    private bool startRespawn;
    public Text pingRate;

    public Text RespawnTextTimer;
    public GameObject respawnUI;
    [HideInInspector]
    public GameObject LocalPlayer;
    public static GameManager instance = null;

    public GameObject LeaveScreen;

    public GameObject feedBox;
    public GameObject feedText_Prefab;

    public GameObject killFeedBox;

    public ConnectedPlayers cp;
    public GameObject MobileInput;
   




    void Awake()
    {
       // PhotonNetwork.OfflineMode = true;
        instance = this;  //singleton
        canvas.SetActive(true);
    }

    private void Start()
    {
        cp.AddLocalPlayer();
        cp.GetComponent<PhotonView>().RPC("UpdatePlayerList", RpcTarget.OthersBuffered, PhotonNetwork.NickName);


    }


    private void Update()
    {
        if (startRespawn)
        {
            StartRespawn();
        }
        pingRate.text = "Ping Rate : " + PhotonNetwork.GetPing();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleLeaveScreen();
        }


        if (Input.GetKey(KeyCode.Tab))
        {
            cpCanvas.SetActive(true);

        }
        else
        {
            cpCanvas.SetActive(false);
        }

    }


    public void StartRespawn()
    {

        TimeAmount -= Time.deltaTime;
        RespawnTextTimer.text = "Respawn in: " + TimeAmount.ToString("F0");

        if (TimeAmount <= 0)
        {
            respawnUI.SetActive(false);
            startRespawn = false;
            PlayerRelocation();
            LocalPlayer.GetComponent<Health>().EnableInputs();
            LocalPlayer.GetComponent<PhotonView>().RPC("Revive", RpcTarget.AllBuffered);
        }
    }


    public void ToggleLeaveScreen()
    {
        if (LeaveScreen.activeSelf)
        {
            LeaveScreen.SetActive(false);
        }
        else {
            LeaveScreen.SetActive(true);
        }

    }



    public override void OnPlayerEnteredRoom(Player player)
    {
       GameObject go = Instantiate(feedText_Prefab, new Vector2(0, 0), Quaternion.identity);
        go.transform.SetParent(feedBox.transform);
        go.GetComponent<Text>().text = player.NickName + " has joined the game";
        Destroy(go, 3);
    }


    public override void OnPlayerLeftRoom(Player player)
    {
        cp.RemovePlayerList(player.NickName);
        GameObject go = Instantiate(feedText_Prefab, new Vector2(0, 0), Quaternion.identity);
        go.transform.SetParent(feedBox.transform);
        go.GetComponent<Text>().text = player.NickName + " has left the game";
        Destroy(go, 3);
    }

















    public void LeaveGame()
    {

        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

    public void PlayerRelocation()
    {
        float randomPosition = Random.Range(-5, 5);
        LocalPlayer.transform.localPosition = new Vector2(randomPosition, 1);

    }




   public void EnableRespawn()
    {
        TimeAmount = 5;
        startRespawn = true;
        respawnUI.SetActive(true);
    }



    public void SpawnPlayer()
    {
        float randomValue = Random.Range(-5, 5);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(playerPrefab.transform.position.x * randomValue, playerPrefab.transform.position.y), Quaternion.identity,0);
        canvas.SetActive(false);
        sceneCam.SetActive(false);
        MobileInput.SetActive(true);
    }




}
