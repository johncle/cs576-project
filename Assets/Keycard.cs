using UnityEngine;

public class Keycard : MonoBehaviour
{
    [SerializeField] private string keycardID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKeycard(keycardID);
                Destroy(gameObject);
            }
        }
    }
}
