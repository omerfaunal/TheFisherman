using UnityEngine;

public class PlaySoundWithDistance : MonoBehaviour
{
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure the player has a tag "Player"
        {
            audioSource.Play();  // Start playing the sound when the player enters the trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Stop();  // Stop playing the sound when the player exits the trigger
        }
    }
}
