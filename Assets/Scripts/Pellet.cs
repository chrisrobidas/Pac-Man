using UnityEngine;

public class Pellet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "Pac-Man")
            Destroy(gameObject);
    }
}
