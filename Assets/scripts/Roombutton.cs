using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Roombutton : MonoBehaviour
{
    [SerializeField] private Text RoomName_Txt;
    [SerializeField] private Text Size_Txt;
    private string RoomName;
    private int RoomCount;
    private int PlayerCount;
   public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(RoomName); 
    }

   public void SetRoom(string name,int sizecount,int count)
    {
        RoomName = name;
        RoomCount = sizecount;
        PlayerCount = count;
        RoomName_Txt.text = name;
        Size_Txt.text = count + "/" +sizecount;
    }
}
