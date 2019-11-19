using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datacontroller : MonoBehaviour
{
    public static Datacontroller datacontroller;
    public string Name;
    public string character;
    public int ID;
    void Awake()
    {
        if (datacontroller == null) {
            datacontroller = this;
        }
        else {
            if (datacontroller != null) {
                Destroy(datacontroller.gameObject);
                datacontroller = this;
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    
}
