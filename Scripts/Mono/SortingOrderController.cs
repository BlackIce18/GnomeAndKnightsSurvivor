using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortingOrderController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // „ем ниже по Y - тем больше Order in Layer
        _spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }
}
