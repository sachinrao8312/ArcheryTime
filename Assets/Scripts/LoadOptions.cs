using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOptions : MonoBehaviour
{
    new public GameObject gameObject;


    public void LoadOption(){
        gameObject.SetActive(true);
    }


    public void UnloadOption(){
        gameObject.SetActive(false);
    }

    
}
