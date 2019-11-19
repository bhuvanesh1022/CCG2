using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateorJoinRoomCanvas : MonoBehaviour
{
    public RoomCanvas roomCanvas;
    public CreateRoom createRoom;

    public void Firstinitialize(RoomCanvas canvases) {
        roomCanvas = canvases;
        createRoom.Firstinitialize(canvases);
    }
}
