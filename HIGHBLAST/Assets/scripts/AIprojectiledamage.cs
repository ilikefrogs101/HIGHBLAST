using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIprojectiledamage : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.TryGetComponent(out singleplayertarget attack))
        {
            attack.TakeDamage(damage);
            Debug.Log("Hit!");
        }

    }
}
