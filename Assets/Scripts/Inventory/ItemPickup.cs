using System.Collections;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] public InventoryItem item;
    [SerializeField] public int quantity = 1;
    private Animator animator;
    private bool isCollected = false;

    void Start()
    {
        if (item.itemType != ItemType.WEAPON)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (item.itemType != ItemType.WEAPON)
            {
                OnCollect();
            }

            InventoryManager.Instance.AddItem(item, quantity);
            StartCoroutine(WaitForAnimationDestroy());
        }
    }

    public void OnCollect()
    {
        if (isCollected)
        {
            return; 
        }

        isCollected = true;
        if (animator != null && item.itemType != ItemType.WEAPON)
        {
            animator.SetBool("IsCollected", true);
        }
        
    }

    private IEnumerator WaitForAnimationDestroy()
    {
        if (animator != null)
        {
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

            yield return new WaitForSeconds(animationLength);
        }
        
        Destroy(gameObject);
    }
    
}
