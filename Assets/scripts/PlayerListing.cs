using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class PlayerListing : MonoBehaviourPunCallbacks
{
    public Text Plname_txt;
    public Image Icon_img;
   public Player _player;
     void Start()
    {
        Icon_img = GetComponent<Image>();
    }

    public void SetPlayer_Info(Player player) {
        _player = player;
        Plname_txt.text = player.NickName;
    }
}
