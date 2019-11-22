using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputScript : MonoBehaviour
{

    public GameObject localPlayer;



    // Start is called before the first frame update
    void Start()
    {



        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {

            if (player.GetComponent<PhotonView>().IsMine)
            {
                localPlayer = player;
                break;

            }


        }


    }



    public void On_RightMove()
    {
        localPlayer.GetComponent<CowBoy>().On_RightMove();

    }

    public void On_LeftMove()
    {
        localPlayer.GetComponent<CowBoy>().On_LeftMove();
    }

    public void On_PointerExit()
    {
        localPlayer.GetComponent<CowBoy>().On_PointerExit();

    }


    public void Jump()
    {

        localPlayer.GetComponent<CowBoy>().Jump();

    }


    public void ShotPressed()
    {

        localPlayer.GetComponent<CowBoy>().shot();
    }

    public void ShotReleased()
    {
        localPlayer.GetComponent<CowBoy>().shotUP();

    }

  









}
