using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioManager : MonoBehaviour
{   
        
    protected AudioSource audioSource;
    [SerializeField] protected float pitchRandomness = 0.03f;
    protected float basePitch;
        

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        basePitch = audioSource.pitch;
    }

    protected void PlayClipWithRandomPitch(AudioClip clip)
    {
        var randomPitch = Random.Range(-pitchRandomness, pitchRandomness);
        audioSource.pitch = basePitch + randomPitch;
        PlayClip(clip);
    }

    protected void PlayClip(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    protected void PlayOneShotAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

      
}