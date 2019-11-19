using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class CreateRoom : MonoBehaviourPunCallbacks
{
    public Text Roomname;
    public RoomCanvas roomCanvas;
    public void OnClick_createRoom() {
        if (!PhotonNetwork.IsConnected) {
            return;
        }
        RoomOptions option = new RoomOptions();
        option.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(Roomname.text,option,TypedLobby.Default);

    }
    public override void OnConnectedToMaster() {
        print("connect network");

    }

    public override void OnJoinedRoom() {
        print("roomname"+Roomname.text);

    }
    public void Firstinitialize(RoomCanvas RCanvas) {

        roomCanvas = RCanvas;
    }
    //callback
    public override void OnCreateRoomFailed(short returnCode, string message) {
        print("failed--");
    }

}
