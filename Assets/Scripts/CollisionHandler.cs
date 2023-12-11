using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LoadDelay = 1f;

    AudioSource audioSource;
    [SerializeField] AudioClip CrashSFX;
    [SerializeField] AudioClip SuccessSFX;

    [SerializeField] ParticleSystem sucPart;
    [SerializeField] ParticleSystem crashPart;

    private bool isTransitioning = false;
    private bool collisionDisable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys(); 
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisable)
            return;

        switch (collision.gameObject.tag)
        {
            
         
            case "Friendly":
                Debug.Log("FRIEND");
                break;

            case "Finish":
                NextLevelSequence();
                break;

            default:
                
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(CrashSFX);
        sucPart.Play();
        
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", LoadDelay);
        
    }

    void NextLevelSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(SuccessSFX);
        crashPart.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", LoadDelay);
    }

    void ReloadScene()
    {
       int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(CurrentSceneIndex);
    }

    void LoadNextLevel()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = CurrentSceneIndex + 1;
        if (NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextSceneIndex = 0;
        }
        SceneManager.LoadScene(NextSceneIndex);
    }
}
