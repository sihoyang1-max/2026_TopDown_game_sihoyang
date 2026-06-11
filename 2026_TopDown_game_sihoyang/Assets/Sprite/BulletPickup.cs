using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    public int amount = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player =
                other.GetComponent<PlayerController>();

            player.bulletCount += amount;
            player.UpdateBulletUI();

            Destroy(gameObject);
        }
    }
}
