using UnityEngine;
using Photon.Pun;

public class Target : MonoBehaviourPunCallbacks
{
    public int maxhealth = 50;

    public int currenthealth;

    public healthbar healthBar;

    void Start()
    {
        currenthealth = maxhealth;

        healthBar.SetMaxHealth(50);
    }

    void Update()
    {
        if (!photonView.IsMine) return;
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;

        healthBar.SetHealth(currenthealth);

        if (currenthealth <= 0f)
        {
            photonView.RPC("Die", RpcTarget.All);
        }
    }

    [PunRPC]
    public void Die()
    {
        transform.position = new Vector3(-27, 95, -6);
        currenthealth = maxhealth;
        Debug.Log("Dead");
    }
}