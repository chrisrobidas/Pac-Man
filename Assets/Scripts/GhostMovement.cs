using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private Transform[] waypoints;
    
    private int currentWaypoint;

    private void FixedUpdate()
    {
        if (Vector2.SqrMagnitude(waypoints[currentWaypoint].position - transform.position) > 0.0001)
        {
            Vector2 position = Vector2.MoveTowards(transform.position,
                waypoints[currentWaypoint].position,
                speed);
            GetComponent<Rigidbody2D>().MovePosition(position);
        }
        else currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        
        // Animation
        Vector2 direction = waypoints[currentWaypoint].position - transform.position;
        GetComponent<Animator>().SetFloat("MovementX", direction.x);
        GetComponent<Animator>().SetFloat("MovementY", direction.y);
    }
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //if (otherCollider.name == "Pac-Man")
            //Destroy(otherCollider.gameObject);
    }
}
