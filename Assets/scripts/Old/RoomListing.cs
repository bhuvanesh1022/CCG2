using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform Content;
    [SerializeField]
    private RoomList roomlisting;
    public List<RoomList> rmlist=new List<RoomList>();
    private RoomCanvas roomCanvas;

    public void FirstInitialize(RoomCanvas canvas) {
        roomCanvas = canvas;
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList) {

        foreach (RoomInfo room in roomList) {

        }
    }

}
