using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class TimeOut : MonoBehaviourPun
{




    private float idleTime = 50f;
    private float timer = 20;

    public GameObject TimeOutUI;
    public Text TimeOuUI_Text;

    private bool TimeOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
            if (!TimeOver)
            {

                if (Input.anyKey)
                {
                    idleTime = 50f;

                }
                idleTime -= Time.deltaTime;


                if (idleTime <= 0) ////////// if idle time finish activate time out screen
                {
                    playerNotMoving();
                }

                if (TimeOutUI.activeSelf)
                {
                    timer -= Time.deltaTime;
                    TimeOuUI_Text.text = "Disconnecting in : " + timer.ToString("F0");
                    if (timer <= 0)
                    {
                        TimeOver = true;
                    }
                    else if (timer > 0 && Input.anyKey) //interupt while counting
                    {
                        idleTime = 50;
                        timer = 20;
                        TimeOutUI.SetActive(false);
                    }
                }


            }
            else
            {
                LeaveGame();
            }

       
    }//update


    public void playerNotMoving()
    {
        TimeOutUI.SetActive(true);

    }



    void LeaveGame()
    {

        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }


}
