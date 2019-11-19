using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Networkconnection : MonoBehaviourPunCallbacks
{
    public static Networkconnection networkconnection;
    private void Awake() {
        networkconnection = this;
    }
    private void Start() {

        PhotonNetwork.ConnectUsingSettings();
      
    }
    // Callbacks
    public override void OnConnectedToMaster() {
        print("connect network");
    }
    public override void OnDisconnected(DisconnectCause cause) {
        print("Disconnect network");
    }
}
