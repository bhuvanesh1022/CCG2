using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class PlayerController : MonoBehaviour {
    public static PlayerController playerController;
    public GameObject playeref;
    public List<Transform> playerposition = new List<Transform>();
   public List<GameObject> playerlist = new List<GameObject>();
    public GameObject Reload_btn, Close_btn;
    public bool GameStart,Visual_enable;
    public GameObject Visual_Txt;
    public List<string> PlayerNames = new List<string>();
    private GameObject PlayerObject;

    // Cards Details
    public int Row, coloumns;
    public Vector2 gridsize;
    public Vector2 gridoffset;
    public GameObject CardObj;
    public Vector2 cellscale;
    public Vector2 cellsize;
    public List<Sprite> cardsprite = new List<Sprite>();
    public Vector3 newpos;
    public List<Transform> cardspawnpos = new List<Transform>();
    public List<Transform> cardplaceposition = new List<Transform>();
    private GameObject g;
    public int cardcount;
    public List<GameObject> cardlist = new List<GameObject>();
    public List<GameObject> placedcardlist = new List<GameObject>();
    public int numberofcard;
    public List<Sprite> coveredsprite = new List<Sprite>();
    public List<GameObject> cardchipbet = new List<GameObject>();

    // wages
    public GameObject WagesObject;
    public GameObject Incbtn, DecBtn;
    public int WagesValue, MaxWagesValue;

    public int betadjust;
    public TextMeshProUGUI wagevaluetext, bettedtext;
    public TextMeshProUGUI ChipText;
    public int HealthCnt;
    public string WinName;

    public GameObject _OpponentBetText;

    public bool _NextTurn;
    // public bool CardShowVal;
    // public int playerenteredindex;

    //speacial button
    public GameObject _SpecialBtn, _OpenentSpecialBtn;
    public List<GameObject> _Specials = new List<GameObject>();
    public int OpponentSpecial_Num,MaxSpecialCount;

    private void Start() {
        Reload_btn.GetComponent<Button>().onClick.AddListener(Onclick_ReloadFun);
        playerInst();
        CardClone();
    }
    // reload fun
    void Onclick_ReloadFun() {
        if (!PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(0);
        }
        else {
            PhotonNetwork.LoadLevel(0);
        }
    }

    void playerInst() {
        PlayerObject = PhotonNetwork.Instantiate(playeref.name,new Vector3(0,0,0),Quaternion.identity);

    }
    // card spawn
    void CardClone() {
        cellsize = cardsprite[Mastermanager._gamesettings.playerenteredindex].bounds.size;
       // Debug.Log("cellsize-------"+ cellsize);
        Vector2 newcellsize = new Vector2(gridsize.x / (float)coloumns, gridsize.y / (float)Row);

        cellscale = new Vector2(newcellsize.x / cellsize.x, newcellsize.y / cellsize.y);
        cellsize = newcellsize;

        Debug.Log(CardObj.transform.position + "cpos");

        CardObj.transform.localScale = new Vector2(cellscale.x, cellscale.y);

        for (int i = 0; i < Row; i++) {
            for (int j = 0; j < coloumns; j++) {


                newpos = new Vector2(j * cellsize.x + gridoffset.x, i * cellsize.y + gridoffset.y);

                g = PhotonNetwork.Instantiate(CardObj.name, newpos + cardspawnpos[0].transform.position, Quaternion.identity);




                g.transform.localScale = new Vector2(cellscale.x, cellscale.y);

            }

        }
    }
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, gridsize);
    }
}
