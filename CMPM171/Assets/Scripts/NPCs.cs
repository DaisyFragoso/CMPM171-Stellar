using UnityEngine;

public class NPCs : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] points;
    
    private int i;
    private SpriteRenderer spriteRenderer;
    public Vector2 respawnPoint;

    void Start()
    {
        respawnPoint = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, points[i].position) < 0.75f)
        {
            i++;
            if(i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        spriteRenderer.flipX = (transform.position.x - points[i].position.x) < 0;

        if (transform.position.y < -25f)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint;
    }
}
