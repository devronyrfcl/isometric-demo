using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAndDisable : MonoBehaviour
{
    
    [SerializeField] GameObject[] objectsToEnable;
    [SerializeField] GameObject[] objectsToDisable;


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach (var gameObject in objectsToEnable)
            {
                gameObject.SetActive(true);
            }

            foreach (var gameObject in objectsToDisable)
            {
                gameObject.SetActive(false);
            }
        }
    }
    

}
