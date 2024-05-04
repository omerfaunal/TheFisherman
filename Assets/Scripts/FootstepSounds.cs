using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] footstepSounds; // Array to hold footstep sounds
    public float stepRate = 0.5f; // Time between steps in seconds
    private float stepCooldown; // Cooldown timer to manage step sound rate

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stepCooldown = 0f; // Initialize cooldown timer
    }

    void Update()
    {
        // Reduce the cooldown timer
        if (stepCooldown > 0)
        {
            stepCooldown -= Time.deltaTime;
            return;
        }

        // Check if any of the WASD keys is pressed and if the cooldown timer has elapsed
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            // Play a random footstep sound
            audioSource.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.Play();
            stepCooldown = stepRate; // Reset cooldown timer
        } else {
            // Stop the audio source if no keys are pressed
            audioSource.Stop();
        }
    }
}
