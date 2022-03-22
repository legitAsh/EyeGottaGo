using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public PlayerManager playerManager;

    public GameObject Drop;

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.tag == "Player") {
            if(playerManager.rb.velocity.sqrMagnitude > 300) {
                Drop.gameObject.SetActive(true);
                Destroy(this.gameObject, 0.1f);
            }
        }
    }
}
