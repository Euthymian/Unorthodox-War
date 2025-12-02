using System;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private DefenderStructure targetStructure;
    private DefenderStructure homeStructure;

    [SerializeField] private float speed = 2f;
    private Vector2 moveDirection;

    [SerializeField] private LayerMask structureLayer;
    [SerializeField] private float searchRadius = 5f;
    [SerializeField] private float searchInterval = 1f;
    [SerializeField] private float targetReachedDistance = 0.5f;
    private float searchTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        homeStructure = FindAnyObjectByType<Home>();
    }

    private void OnEnable()
    {
        searchTimer = searchInterval;
        SearchForTargetStructure();
    }

    private void Update()
    {
        searchTimer -= Time.deltaTime;

        if (searchTimer <= 0)
        {
            SearchForTargetStructure();
            searchTimer = searchInterval;
        }

        // Validate target still exists
        if (targetStructure != null)
            SearchForTargetStructure();
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private void UpdateMoveDirection()
    {
        Vector2 directionToTarget = (Vector2)targetStructure.transform.position - (Vector2)transform.position;
        float distanceToTarget = directionToTarget.magnitude;

        if (distanceToTarget > targetReachedDistance)
        {
            moveDirection = directionToTarget.normalized;
        }
        else
        {
            moveDirection = Vector2.zero;
            OnReachedTarget();
        }
    }

    private void SearchForTargetStructure()
    {
        Collider2D[] allInRangeStructure = Physics2D.OverlapCircleAll(transform.position, searchRadius, structureLayer);
        DefenderStructure highestPriorityStructure = null;

        foreach (var structureCollider in allInRangeStructure)
        {
            DefenderStructure structure = structureCollider.GetComponent<DefenderStructure>();

            if (structure != null && structure.gameObject.activeInHierarchy)
            {
                if (highestPriorityStructure == null ||
                    structure.structureImportance > highestPriorityStructure.structureImportance)
                {
                    highestPriorityStructure = structure;
                }
            }
        }

        SetTargetStructure(highestPriorityStructure);
    }

    private void SetTargetStructure(DefenderStructure target)
    {
        if (target == null)
            target = homeStructure;

        targetStructure = target;

        UpdateMoveDirection();
    }

    private void OnReachedTarget()
    {
        // Handle what happens when enemy reaches structure
        // (attack, damage, etc.)
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);

        if (targetStructure != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, targetStructure.transform.position);
        }
    }
}