using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class Card : MonoBehaviourPunCallbacks,IPunObservable {
    public Vector2 startpos;
    public string CardName;
    public List<string> CardAttributes = new List<string>();
    public bool CardShowvalues;
    public int Whichplayer, CardValue;
    public TextMeshProUGUI Cardattribute_name;

    public bool iscollided;
    public bool initBool;
   // public bool IsshowText;
    public bool IsplaceCard;// hit card
    void Start() {
        initBool = true;
        startpos = transform.position;
        LocalScale();
        CardNameDis();
        Attributes();
       
        photonView.RPC("Addcard", RpcTarget.AllBuffered, null);
    }
    private void Update() {
       // gamestart();
        CardShow();
        CardVisible();
    }
    void Script_Refresh() {
        if (PlayerController.playerController == null) {
            PlayerController.playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        }

    }
    void gamestart() {
        if (PlayerController.playerController.placedcardlist.Count == 2) {
            CardShowvalues = true;
        }
    }

    // set scale
    void LocalScale() {
        if (!photonView.IsMine) {
            transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }
    //
    void CardShow() {
        Script_Refresh();
        if (!photonView.IsMine && IsplaceCard) {
            transform.localScale = PlayerController.playerController.cellscale;
            transform.position = PlayerController.playerController.cardplaceposition[1].transform.position;


        }
        else if (!photonView.IsMine && !IsplaceCard) {
            transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }
    void CardVisible() {
        Script_Refresh();

        if (!photonView.IsMine) {
            if (CardShowvalues) {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = PlayerController.playerController.coveredsprite[0];
                gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
            }

        }
        else {
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = PlayerController.playerController.coveredsprite[1];
            gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        }

    }
    // card name
    void CardNameDis() {
        if (photonView.IsMine) {
            Whichplayer = Mastermanager._gamesettings.playerenteredindex;
            PlayerController.playerController.cardcount++;
            CardValue = PlayerController.playerController.cardcount;
            print("CardValue----" + CardValue);
            CardName = Mastermanager._gamesettings.name;
            this.gameObject.name = CardName + "'s" + "card" + CardValue;
        }
    }
    // 
    void Attributes()
        {
        if (photonView.IsMine && initBool)  {
            Cardattribute_name.text = CardAttributes[CardValue - 1];
            initBool = false;
        }
      
        }

    [PunRPC]
    public void Addcard() {
        Script_Refresh();
        if (PlayerController.playerController.cardlist.Contains(gameObject)) {
            return;
        }
        PlayerController.playerController.cardlist.Add(gameObject);
    }
    [PunRPC]
    public void Addplacedcard() {
        Script_Refresh();
        if (PlayerController.playerController.placedcardlist.Contains(gameObject)) {
            return;
        }
        PlayerController.playerController.placedcardlist.Add(gameObject);

    }
    //triggering it
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Table") {
            iscollided = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Table") {
            iscollided = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Table") {
            iscollided = false;
        }
    }

    //-------Card drag------
    private void OnMouseDrag() {
       // Script_Refresh();
        if (photonView.IsMine ) {
        print("drag-------");
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = pos;
            gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
       }
    }
    private void OnMouseDown() {
      //  Script_Refresh();
       if (photonView.IsMine) {
        print("hjuf-------");
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = pos;
            gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        }

    }
    private void OnMouseUp() {
       // Script_Refresh();
       
            transform.position = startpos;
            IsplaceCard = false;
            gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 0;

            if (iscollided) {          
            gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
            if (photonView.IsMine) {
                transform.position= PlayerController.playerController.cardplaceposition[0].transform.position;
            }
            IsplaceCard = true;
            photonView.RPC("Addplacedcard", RpcTarget.AllBuffered, null);

        }


    }

   


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(Whichplayer);
            stream.SendNext(iscollided);
            stream.SendNext(CardValue);
            stream.SendNext(this.gameObject.name);
            stream.SendNext(IsplaceCard);
            stream.SendNext(CardShowvalues);
            stream.SendNext(initBool);

            if (iscollided && CardShowvalues) {
                stream.SendNext(Cardattribute_name.text);//dis
            }
        }
        else if (stream.IsReading) {
            Whichplayer = (int)stream.ReceiveNext();
            iscollided = (bool)stream.ReceiveNext();
            CardValue = (int)stream.ReceiveNext();
            this.gameObject.name = (string)stream.ReceiveNext();
            IsplaceCard = (bool)stream.ReceiveNext();
            CardShowvalues = (bool)stream.ReceiveNext();
            initBool = (bool)stream.ReceiveNext();

            if (iscollided && CardShowvalues) {
                Cardattribute_name.text = (string)stream.ReceiveNext();//dis
            }
        }
    }
}
