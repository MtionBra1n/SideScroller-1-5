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

    [Header("Run Sounds")] 
    [SerializeField] private AudioClip[] grassRunStepSounds;
    [SerializeField] private AudioClip[] mudRunStepSounds;
    [SerializeField] private AudioClip[] woodRunStepSounds;
    [SerializeField] private AudioClip[] defaultRunStepSounds;
    
    [Header("Jump Start Sounds")] 
    [SerializeField] private AudioClip[] grassJumpStartSounds;
    [SerializeField] private AudioClip[] mudJumpStartSounds;
    [SerializeField] private AudioClip[] woodJumpStartSounds;
    [SerializeField] private AudioClip[] defaultJumpStartSounds;
    
    [Header("Jump Land Sounds")] 
    [SerializeField] private AudioClip[] grassJumpLandSounds;
    [SerializeField] private AudioClip[] mudJumpLandSounds;
    [SerializeField] private AudioClip[] woodJumpLandSounds;
    [SerializeField] private AudioClip[] defaultJumpLandSounds;
    
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

    public void PlayRunStepSound()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastPosition,
            Vector2.down, raycastDistance, groundLayer);

        if (hit.collider != null)
        {
            string groundTag = hit.collider.tag;
            switch (groundTag)
            {
                case "Grass":
                    PlayRandomSound(grassRunStepSounds);
                    break;
                
                case "Mud":
                    PlayRandomSound(mudRunStepSounds);
                    break;
                
                case "Wood":
                    PlayRandomSound(woodRunStepSounds);
                    break;
                
                default:
                    PlayRandomSound(defaultRunStepSounds);
                    break;
            }
        }
    }
    
    public void PlayJumpStartSound()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastPosition,
            Vector2.down, raycastDistance, groundLayer);

        if (hit.collider != null)
        {
            string groundTag = hit.collider.tag;
            switch (groundTag)
            {
                case "Grass":
                    PlayRandomSound(grassJumpStartSounds);
                    break;
                
                case "Mud":
                    PlayRandomSound(mudJumpStartSounds);
                    break;
                
                case "Wood":
                    PlayRandomSound(woodJumpStartSounds);
                    break;
                
                default:
                    PlayRandomSound(defaultJumpStartSounds);
                    break;
            }
        }
    }
    
    public void PlayJumpLandSound()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastPosition,
            Vector2.down, raycastDistance, groundLayer);

        if (hit.collider != null)
        {
            string groundTag = hit.collider.tag;
            switch (groundTag)
            {
                case "Grass":
                    PlayRandomSound(grassJumpLandSounds);
                    break;
                
                case "Mud":
                    PlayRandomSound(mudJumpLandSounds);
                    break;
                
                case "Wood":
                    PlayRandomSound(woodJumpLandSounds);
                    break;
                
                default:
                    PlayRandomSound(defaultJumpLandSounds);
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
