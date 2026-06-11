using Unity.VisualScripting;
using UnityEngine;
public class EnemyAI : MonoBehaviour
{
    public bool canMove = true;
    private bool stunned = false;
    private float stunTimer = 0f;
    public Transform player;

    public float moveSpeed = 3f;
    public float chaseRange = 6f;
    public float gridSize = 0.5f;
    private Camera mainCamera;
    public LayerMask wallLayer;

    private bool isMoving = false;
    private Vector3 targetPosition;


    private void Start()
    {
        targetPosition = transform.position;

        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!canMove)
            return;
        if (stunned)
        {
            stunTimer -= Time.deltaTime;

            if (stunTimer <= 0)
            {
                stunned = false;
            }

            return;
        }
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }

            return;
        }

        if (player == null)
            return;

        float distance =
            Vector2.Distance(transform.position, player.position);

        if (distance > chaseRange)
            return;

        MoveOneStep();
    }

    private void MoveOneStep()
    {
        Vector2 diff = player.position - transform.position;

        Vector3 horizontalPos =
            transform.position +
            Vector3.right * Mathf.Sign(diff.x) * gridSize;

        Vector3 verticalPos =
            transform.position +
            Vector3.up * Mathf.Sign(diff.y) * gridSize;

        bool horizontalBlocked =
            Physics2D.OverlapCircle(horizontalPos, 0.2f, wallLayer);

        bool verticalBlocked =
            Physics2D.OverlapCircle(verticalPos, 0.2f, wallLayer);

        // 더 먼 축을 우선 이동
        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            if (!horizontalBlocked)
            {
                targetPosition = ClampToCamera(horizontalPos);
                isMoving = true;
            }
            else if (!verticalBlocked)
            {
                targetPosition = ClampToCamera(verticalPos);
                isMoving = true;
            }
        }
        else
        {
            if (!verticalBlocked)
            {
                targetPosition = ClampToCamera(verticalPos);
                isMoving = true;
            }
            else if (!horizontalBlocked)
            {
                targetPosition = ClampToCamera(horizontalPos);
                isMoving = true;
            }
        }
    }
    private void GameOver()
    {
        Debug.Log("게임 오버!");

        Time.timeScale = 0f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameOver();
        }
    }
    public void Stun(float duration)
    {
        stunned = true;
        stunTimer = duration;
    }
    private Vector3 ClampToCamera(Vector3 pos)
    {
        Vector3 min =
            mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        Vector3 max =
            mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        return pos;
    }
}