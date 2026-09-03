using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        // Tantangan
        Suspicious,
        Alert
    }

    [Header("Target")]
    [SerializeField]
    private Transform player;

    [Header("AI Parameters")]

    // Tantangan
    [SerializeField]
    [Min(0f)]
    private float suspiciousRadius = 8f;

    [SerializeField]
    [Min(0f)]
    private float alertRadius = 4f;

    [Header("Visual")]
    [SerializeField]
    private Color idleColor = Color.blue;

    // Tantangan
    [SerializeField]
    private Color suspiciousColor = Color.yellow;

    [SerializeField]
    private Color alertColor = Color.red;

    [Header("Debug")]
    [SerializeField]
    private EnemyState currentState;

    [SerializeField]
    private float currentDistance;

    private Renderer enemyRenderer;

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();

        currentState = EnemyState.Alert;
        SetState(EnemyState.Idle);
    }

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        if (player == null)
            return;

        currentDistance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (currentDistance <= alertRadius)
        {
            SetState(EnemyState.Alert);
        }
        // Tantangan
        else if (currentDistance <= suspiciousRadius)
        {
            SetState(EnemyState.Suspicious);
        }
        else
        {
            SetState(EnemyState.Idle);
        }
    }

    void SetState(EnemyState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;

        Debug.Log(
            "Enemy State → " + currentState
        );

        if (enemyRenderer == null)
            return;

        if (currentState == EnemyState.Alert)
        {
            enemyRenderer.material.color = alertColor;
        }
        // Tantangan
        else if (currentState == EnemyState.Suspicious)
        {
            enemyRenderer.material.color = suspiciousColor;
        }
        else
        {
            enemyRenderer.material.color = idleColor;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            suspiciousRadius
        );

        Gizmos.DrawWireSphere(
            transform.position,
            alertRadius
        );
    }
}