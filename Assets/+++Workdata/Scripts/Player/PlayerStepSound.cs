using UnityEngine;

public class PlayerStepSound : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastDistance = 0.2f;
    [SerializeField] private Vector3 raycastPosition;

    [SerializeField] private AudioSource audioSource;

    [Header("Walk Sounds")] 
    [SerializeField] private AudioClip[] grassWalkStepSounds;
    [SerializeField] private AudioClip[] mudWalkStepSounds;
    [SerializeField] private AudioClip[] woodWalkStepSounds;
    [SerializeField] private AudioClip[] defaultWalkStepSounds;

    public void PlayWalkStepSound()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastPosition,
            Vector2.down, raycastDistance, groundLayer);

        if (hit.collider != null)
        {
            string groundTag = hit.collider.tag;
            switch (groundTag)
            {
                case "Grass":
                    PlayRandomSound(grassWalkStepSounds);
                    break;
                
                case "Mud":
                    PlayRandomSound(mudWalkStepSounds);
                    break;
                
                case "Wood":
                    PlayRandomSound(woodWalkStepSounds);
                    break;
                
                default:
                    PlayRandomSound(defaultWalkStepSounds);
                    break;
            }
        }
    }

    public void PlayRandomSound(AudioClip[] audioClips)
    {
        int randomNumber = Random.Range(0, audioClips.Length); 
        audioSource.PlayOneShot(audioClips[randomNumber]);
    }
}
