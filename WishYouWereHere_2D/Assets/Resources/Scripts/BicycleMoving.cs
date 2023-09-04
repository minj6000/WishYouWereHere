using UnityEngine;

public class BicycleMoving : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 targetPosition;
    private bool isMoving = false;
    public float collisionForce = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z; // 유지하고자 하는 Z 좌표 설정
            StartMoving(mousePosition);
        }

        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                StopMoving();
            }
        }
    }

    private void StartMoving(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        isMoving = true;
    }

    private void StopMoving()
    {
        isMoving = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D otherRb = collision.collider.GetComponent<Rigidbody2D>();

        if (otherRb != null)
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            otherRb.AddForce(direction * collisionForce, ForceMode2D.Impulse);
        }
    }
}
