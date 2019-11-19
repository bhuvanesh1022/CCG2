using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomCanvas : MonoBehaviourPunCallbacks
{
    public RoomList RoomListing;
    public CreateRoom CreaterRoom;
    public CreateorJoinRoomCanvas createjoincanvas;

    public void Firstinitialize() {
        CreaterRoom.Firstinitialize(this);
        createjoincanvas.Firstinitialize(this);
    }
}
