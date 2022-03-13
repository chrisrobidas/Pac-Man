using System.Collections;
using UnityEngine;

public class PacManMovement : MonoBehaviour
{
    private enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
    
    public Vector2 Destination = Vector2.zero;
    
    [SerializeField] private float speed = 5;

    private Direction currentDirection = Direction.Left;
    private Direction nextDirection = Direction.Left;

    private void Awake()
    {
        // We want Pac-Man to go a little bit to the left at the beginning
        Destination = transform.position - new Vector3(0.5f, 0, 0);
    }

    private void FixedUpdate()
    {
        // Move closer to Destination
        StartCoroutine(MoveOverSpeed(Destination));
        
        // Check for Input if not moving
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            nextDirection = Direction.Up;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            nextDirection = Direction.Right;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            nextDirection = nextDirection = Direction.Down;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            nextDirection = Direction.Left;
        
        if (Vector2.SqrMagnitude(Destination - (Vector2)transform.position) < 0.0001)
        {
            if ((nextDirection == Direction.Up || currentDirection == Direction.Up) && ValidateMovement(Vector2.up))
            {
                Destination = (Vector2)transform.position + Vector2.up;
                currentDirection = Direction.Up;
            }
            if ((nextDirection == Direction.Right || currentDirection == Direction.Right) && ValidateMovement(Vector2.right))
            {
                Destination = (Vector2)transform.position + Vector2.right;
                currentDirection = Direction.Right;
            }
            if ((nextDirection == Direction.Down || currentDirection == Direction.Down) && ValidateMovement(Vector2.down))
            {
                Destination = (Vector2)transform.position + Vector2.down;
                currentDirection = Direction.Down;
            }
            if ((nextDirection == Direction.Left || currentDirection == Direction.Left) && ValidateMovement(Vector2.left))
            {
                Destination = (Vector2)transform.position + Vector2.left;
                currentDirection = Direction.Left;
            }
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        Vector2 movement = Destination - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("MovementX", movement.x);
        GetComponent<Animator>().SetFloat("MovementY", movement.y);
    }
    
    private IEnumerator MoveOverSpeed(Vector3 end)
    {
        while (transform.position != end)
        {
            Vector2 position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(position);
            yield return new WaitForEndOfFrame();
        }
        GetComponent<Rigidbody2D>().MovePosition(Destination);
    }
    
    private bool ValidateMovement(Vector2 direction)
    {
        Vector2 direction2 = direction;

        if (direction.x == 0)
        {
            direction.x = 0.1f;
            direction2.x = -0.1f;
        }
        else
        {
            direction.y = 0.1f;
            direction2.y = -0.1f;
        }
        
        Vector2 position = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(position + direction * 1.1f, position);
        RaycastHit2D hit2 = Physics2D.Linecast(position + direction2 * 1.1f, position);
        return hit.collider.gameObject.name != "Map" && hit2.collider.gameObject.name != "Map";
    }
}
