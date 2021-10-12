using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 10f;


    public Transform player;

	void Start()
    {
        // Add + 1 to player's last known position so bullet appears to float above ground.
        Vector3 playerPosition = new Vector3(player.position.x, player.position.y + 1, player.position.z);
 
        // Aim bullet in player's direction.
        transform.rotation = Quaternion.LookRotation(playerPosition);
    }
 
    // Update is called once per frame
    void Update()
    {
        // Move the projectile forward towards the player's last known direction;
        transform.position += transform.forward * speed * Time.deltaTime;
    }

}
