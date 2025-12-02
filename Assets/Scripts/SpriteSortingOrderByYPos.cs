using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortingOrderByYPos : MonoBehaviour
{
    [SerializeField] private bool runOnlyOnce;
    [SerializeField] private float posOffsetY;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        float precisionMultiplier = 5f;
        spriteRenderer.sortingOrder = (int)(-(transform.position.y + posOffsetY) * precisionMultiplier);

        if (runOnlyOnce)
        {
            Destroy(this);
        }
    }
}
