using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject UserNameScreen, ConnectScreen;

    [SerializeField]
    private GameObject CreateUserNameButton;

    [SerializeField]
    private InputField UserNameInput, CreateRoomInput, JoinRoomInput;

    [SerializeField]
    private Text status;


    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        
    }




    //connection test to master and if success join lobby
    public override void OnConnectedToMaster()
    {
        status.text = "Connected";
        status.color = Color.green;
         
        Debug.Log("Connected to master!!!");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
            
     }


    //connection test to lobby and if success join room
    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby");
        UserNameScreen.SetActive(true);


    }

    public override void OnJoinedRoom()
    {
        //Assign teams
        AssignTeam();
        //play game scene
        PhotonNetwork.LoadLevel(1);
    }







    #region UIMethods


    void AssignTeam()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        int size = PhotonNetwork.PlayerList.GetLength(0);

        if (size % 2 == 0)
        {
            hash.Add("Team", 0);

        }
        else {
            hash.Add("Team", 1);
        }

        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }









    public void OnClick_CreateNameBtn()
    {
        
        PhotonNetwork.NickName = UserNameInput.text;
        UserNameScreen.SetActive(false);
        ConnectScreen.SetActive(true);
    }


    public void OnNameField_Changed()
    {
        if (UserNameInput.text.Length >= 2)
        {
            CreateUserNameButton.SetActive(true);

        }
        else {
            CreateUserNameButton.SetActive(false);
        }
    }



    public void Onclick_JoinRoom()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(JoinRoomInput.text, ro, TypedLobby.Default);

    }


    public void Onclick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateRoomInput.text, new RoomOptions { MaxPlayers = 4 },null);
    }






    #endregion

}
