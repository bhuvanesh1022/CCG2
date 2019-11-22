using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class PlayerController : MonoBehaviour {
    public static PlayerController playerController;
    public GameObject playeref;
    public List<Transform> playerposition = new List<Transform>();
   public List<GameObject> playerlist = new List<GameObject>();
    public GameObject Reload_btn, Close_btn;
    public bool GameStart,Visual_enable;

    public GameObject Visual_Txt;

    public List<string> PlayerNames = new List<string>();

    
}
