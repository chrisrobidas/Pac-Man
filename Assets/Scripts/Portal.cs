using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform teleportLocation;
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        otherCollider.transform.position = teleportLocation.position;
        
        if (otherCollider.name == "Pac-Man")
            otherCollider.gameObject.GetComponent<PacManMovement>().Destination = teleportLocation.position;
    }
}
