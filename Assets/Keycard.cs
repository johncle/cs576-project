using UnityEngine;

public class Keycard : MonoBehaviour
{
    [SerializeField] private string keycardID;
    [SerializeField] private AudioClip pickupSound;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the GameObject.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKeycard(keycardID);

                if (pickupSound != null && _audioSource != null)
                {
                    _audioSource.PlayOneShot(pickupSound);
                }
                else if (pickupSound == null)
                {
                    Debug.LogWarning("Pickup sound is not assigned.");
                }

                // ensure the sound plays before destroying the object
                Destroy(gameObject, pickupSound != null ? pickupSound.length : 0f);
            }
        }
    }
}
