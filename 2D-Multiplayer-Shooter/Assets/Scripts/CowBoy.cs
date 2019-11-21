using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.UI;

public class CowBoy : MonoBehaviourPun
{


    public GameObject playerCam;

    public float MoveSpeed = 5f;

    public SpriteRenderer sprite;

    public PhotonView photonview;

    public Animator anim;

    private bool AllowMoving = true;

    public GameObject BulletPrefab;

    public Transform BulletSpawnPointRight;
    public Transform BulletSpawnPointLeft;
    public bool isGrounded = false;
    public Text PlayerName;
    public bool DisableInputs = false;
    private Rigidbody2D rb;

    public float JumpForce;

    public string MyName;


    // Start is called before the first frame update
    void Awake()
    {
        /////////switch camera if player is local
        if (photonView.IsMine)
        {
            GameManager.instance.LocalPlayer = this.gameObject;
            playerCam.SetActive(true);
            playerCam.transform.SetParent(null, false);

            PlayerName.text = PhotonNetwork.NickName;
            PlayerName.color = Color.green;
            MyName = PhotonNetwork.NickName;
        }
        else {
            PlayerName.text = photonview.Owner.NickName;
            PlayerName.color = Color.yellow;
        }



    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine && !DisableInputs)
        {
            checkInputs();

        }


    }





    private void checkInputs()
    {
        /////////////////can move
        if (AllowMoving)
        {
            var movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
            transform.position += movement * MoveSpeed * Time.deltaTime;
        }


        /////////////////shooting key
        if (Input.GetKeyDown(KeyCode.RightControl) && anim.GetBool("IsMove") == false)
        {
            shot();

        } else if (Input.GetKeyUp(KeyCode.RightControl))
        {
            anim.SetBool("IsShot", false);
            AllowMoving = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }


        /////////////////// moving left 
        if (Input.GetKeyDown(KeyCode.D) && anim.GetBool("IsShot") == false)
        {
            anim.SetBool("IsMove", true);

            photonView.RPC("FlipSprite_Right", RpcTarget.AllBuffered);
            playerCam.GetComponent<Camera2DFollow>().offset = new Vector3(1.3f,1.53f,0);


        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("IsMove", false);
        }


        ////////////////////moving right
        if (Input.GetKeyDown(KeyCode.A) && anim.GetBool("IsShot") == false)
        {
            anim.SetBool("IsMove", true);
            photonView.RPC("FlipSprite_Left", RpcTarget.AllBuffered);
            playerCam.GetComponent<Camera2DFollow>().offset = new Vector3(-1.3f, 1.53f, 0);



        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("IsMove", false);
        }


    }
    private void shot()
        {
        
        if (sprite.flipX == false)
        {
            GameObject bullet = PhotonNetwork.Instantiate(BulletPrefab.name, new Vector2(BulletSpawnPointRight.position.x, BulletSpawnPointRight.position.y), Quaternion.identity, 0);

            bullet.GetComponent<Bullet>().localPlayerObj = this.gameObject;
        }


        if (sprite.flipX == true)
        {
            GameObject bullet = PhotonNetwork.Instantiate(BulletPrefab.name, new Vector2(BulletSpawnPointLeft.position.x, BulletSpawnPointLeft.position.y), Quaternion.identity, 0);
            bullet.GetComponent<Bullet>().localPlayerObj = this.gameObject;
            bullet.GetComponent<PhotonView>().RPC("ChangeDirection", RpcTarget.AllBuffered);
        }
       


        anim.SetBool("isShot", true);
            AllowMoving = false;
        }








    [PunRPC]
    private void FlipSprite_Right()
    {
        sprite.flipX = false;
    }

    [PunRPC]
    private void FlipSprite_Left()
    {
        sprite.flipX = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            isGrounded = false;
        }
    }


    void Jump()
    {
        rb.AddForce(new Vector2(0, JumpForce* Time.deltaTime));
        Debug.Log("jumping");
    }

}











