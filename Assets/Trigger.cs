using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{
    public int eventIdx;
    
    void Start()
    {
        if (eventIdx == 0)
        {
            gameObject.SetActive(true);
        }
        else if (eventIdx == 1)
        {
            gameObject.SetActive(false);
        }
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(eventIdx == 0)
        {
            GameManager.instance.Stage3(other.gameObject);
            gameObject.SetActive(false);
        }
        else if(eventIdx == 1)
        {
            GameManager.instance.Scene("SecondFloor");
            gameObject.SetActive(false);
        }
    }
}
