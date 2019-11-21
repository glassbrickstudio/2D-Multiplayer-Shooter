using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomListing : MonoBehaviourPunCallbacks
{
    public Transform Grid
        ;
    public GameObject RoomNamePrefab;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {

            if (room.RemovedFromList)
            {

                DeleteRoom(room);
            }
            else {
                AddRoom(room);
            }



        }
    }


    void AddRoom(RoomInfo room)
    {
        GameObject obj = Instantiate(RoomNamePrefab, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(Grid.transform, false);
        obj.GetComponentInChildren<Text>().text = room.Name;



    }

    void DeleteRoom(RoomInfo room)
    {
        int roomCounts = Grid.childCount;

        for (int i = 0; i < roomCounts; ++i)
        {
            if (Grid.GetChild(i).gameObject.GetComponentInChildren<Text>().text == room.Name)
            {
                Destroy(Grid.GetChild(i).gameObject);

            }
        }


    }












}
