using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyhealth : MonoBehaviour
{
    public int enemymaxhealth = 50;
    public int enemycurrenthealth;

    void Start()
    {
        enemycurrenthealth = enemymaxhealth;
    }

    public void TakeDamage(int damage)
    {
        enemycurrenthealth -= damage;

        if (enemycurrenthealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
