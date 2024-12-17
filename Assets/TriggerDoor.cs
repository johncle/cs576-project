using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    private Animator _doorAnimator;
    private AudioSource _audioSource; // AudioSource for playing the sound
    [SerializeField] private BoxCollider targetBoxCollider;
    [SerializeField] private string[] requiredKeycardIDs; // Array of required keycard IDs
    [SerializeField] private AudioClip doorOpenSound; // Sound clip to play when the door opens

    void Start()
    {
        _doorAnimator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if (targetBoxCollider == null)
        {
            Debug.LogError("Target BoxCollider is not assigned.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the GameObject.");
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

                if (doorOpenSound != null && _audioSource != null)
                {
                    _audioSource.PlayOneShot(doorOpenSound);
                }
                else if (doorOpenSound == null)
                {
                    Debug.LogWarning("Door open sound is not assigned.");
                }

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
