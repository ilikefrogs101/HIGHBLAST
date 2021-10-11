using UnityEngine;
using UnityEngine.SceneManagement;

public class bosshealth : MonoBehaviour
{
    public bossbar Bossbar;

    public int bossmaxhealth = 750;

    public int bosscurrenthealth;

    void Start()
    {
        Bossbar.SetMaxHealth(bossmaxhealth);

        bosscurrenthealth = bossmaxhealth;
    }

    public void TakeDamage(int damage)
    {
        bosscurrenthealth -= damage;

        Bossbar.SetHealth(bosscurrenthealth);

        if (bosscurrenthealth <= 0f)
        {
            Die(); 
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
