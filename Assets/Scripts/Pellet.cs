using UnityEngine;

public class Pellet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.name == "Pac-Man")
            Destroy(gameObject);
    }
}
