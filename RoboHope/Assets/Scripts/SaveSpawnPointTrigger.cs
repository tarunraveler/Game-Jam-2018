using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSpawnPointTrigger : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.ChangeSpawnPoint(gameObject.transform.position);
        }
    }
}
