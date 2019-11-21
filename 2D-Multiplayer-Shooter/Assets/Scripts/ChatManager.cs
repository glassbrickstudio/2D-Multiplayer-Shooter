using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class ChatManager : MonoBehaviourPun, IPunObservable
{
    public PhotonView photonview;
    public GameObject BubbleSpeech;
    public Text ChatText;

    InputField ChatInput;
    private bool DisabledSend;
    public CowBoy player;

    private void Awake()
    {
        ChatInput = GameObject.Find("ChatInputField").GetComponent<InputField>();
    }



    private void Update()
    {
        if (photonView.IsMine)
        {
            if (ChatInput.isFocused)
            {
                player.DisableInputs = true;

            }
            else
            {
                player.DisableInputs = false;
            }



            if (!DisabledSend && ChatInput.isFocused)// if can chat
            {
                if (ChatInput.text != "" && ChatInput.text.Length > 1 && Input.GetKeyDown(KeyCode.Space))
                {

                    photonview.RPC("SendMsg", RpcTarget.AllBuffered, ChatInput.text);
                    BubbleSpeech.SetActive(true);
                    ChatInput.text = "";
                    DisabledSend = true;
                }
            }
        }


                              
    }


    [PunRPC]
    void SendMsg(string msg)
    {
        ChatText.text = msg;
        StartCoroutine(hideBubbleSpeech());
    }




    IEnumerator hideBubbleSpeech()
    {
        yield return new WaitForSeconds(3);

        BubbleSpeech.SetActive(false);
        DisabledSend = false;

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(BubbleSpeech.activeSelf);
        }
        else if (stream.IsReading)
        {
            BubbleSpeech.SetActive( (bool)stream.ReceiveNext());
        }
    }
}
