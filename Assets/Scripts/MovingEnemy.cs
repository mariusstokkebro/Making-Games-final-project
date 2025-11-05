using UnityEngine;

public class MovingEnemy : BaseEnemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Transform player = FindPlayer();

        if (player != null)
        {
            TurnTowardsPlayer();
            MoveTowardsTarget(player.position);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerScript player = collider.gameObject.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log("player hit)");
            }
        }
    }
}
