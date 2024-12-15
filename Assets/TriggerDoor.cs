using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    private Animator _doorAnimator;
    [SerializeField] private BoxCollider targetBoxCollider;
    [SerializeField] private string[] requiredKeycardIDs; // Array of required keycard IDs

    void Start()
    {
        _doorAnimator = GetComponent<Animator>();

        if (targetBoxCollider == null)
        {
            Debug.LogError("Target BoxCollider is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null && HasAllRequiredKeycards(inventory))
            {
                _doorAnimator.SetTrigger("Open");

                if (targetBoxCollider != null)
                {
                    targetBoxCollider.enabled = false;
                }
            }
        }
    }

    private bool HasAllRequiredKeycards(PlayerInventory inventory)
    {
        foreach (string keycardID in requiredKeycardIDs)
        {
            if (!inventory.HasKeycard(keycardID))
            {
                return false;
            }
        }
        return true;
    }
}
