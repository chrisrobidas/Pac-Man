using UnityEngine;

public class PacManMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.4f;
    
    private Vector2 destination = Vector2.zero;
    
    private void Awake()
    {
        destination = transform.position;
    }

    private void FixedUpdate()
    {
        // Move closer to Destination
        Vector2 position = Vector2.MoveTowards(transform.position, destination, speed);
        GetComponent<Rigidbody2D>().MovePosition(position);
        
        // Check for Input if not moving
        //if ((Vector2)transform.position == destination) {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && ValidateMovement(Vector2.up))
                destination = (Vector2)transform.position + Vector2.up;
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && ValidateMovement(Vector2.right))
                destination = (Vector2)transform.position + Vector2.right;
            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && ValidateMovement(Vector2.down))
                destination = (Vector2)transform.position + Vector2.down;
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && ValidateMovement(Vector2.left))
                destination = (Vector2)transform.position + Vector2.left;
        //}
        
        // Animation Parameters
        Vector2 movement = destination - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("MovementX", movement.x);
        GetComponent<Animator>().SetFloat("MovementY", movement.y);
    }
    
    private bool ValidateMovement(Vector2 direction) {
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
        Vector2 position = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(position + direction, position);
        return (hit.collider == GetComponent<Collider2D>());
    }
}
