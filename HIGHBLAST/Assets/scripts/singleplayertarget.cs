using UnityEngine;
using UnityEngine.SceneManagement;

public class singleplayertarget : MonoBehaviour
{
    public GameObject DeadMenu;

    public healthbar healthBar;

    public int maxhealth = 50;

    public int currenthealth;

    void Start()
    {
        healthBar.SetMaxHealth(50);

        currenthealth = maxhealth;
    }

    public void TakeDamage(int damage)
    {

        currenthealth -= damage;

        healthBar.SetHealth(currenthealth);

        if (currenthealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        DeadMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
