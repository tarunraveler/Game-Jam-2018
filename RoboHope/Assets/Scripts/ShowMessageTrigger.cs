using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMessageTrigger : MonoBehaviour
{

    public int messageNumber;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            DialogueManager.instance.Show(messageNumber);
            gameObject.SetActive(false);
        }
    }
}
