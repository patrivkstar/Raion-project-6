using UnityEngine;

public class GentongAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip soundJatuh;
    public float thresholdKecepatan = 2f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > thresholdKecepatan)
        {
            audioSource.PlayOneShot(soundJatuh);
        }
    }
}