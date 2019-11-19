using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviour
{
    public Text _text;
    public RoomInfo Info;
    public RoomCanvas canvases;

   
    void Firstinitialize(RoomCanvas roomcanvases) {
        canvases = roomcanvases;
    }

    public void SetRommInfo(RoomInfo roomInfo)
    {
        Info = roomInfo;
        _text.text = roomInfo.MaxPlayers + "" +roomInfo.Name;

    }
    public void OnclickButton() {
        PhotonNetwork.JoinRoom(Info.Name);
    }
}
