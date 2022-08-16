using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishSound;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;

    AudioSource aS;
    int currentSceneIndex;

    bool isTransitioning = false;
    //bool collisions = true;
    bool collisionDisabled = false;
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        aS = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKey(KeyCode.C))
        {
            // collisions = false;
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning || collisionDisabled) return;
        //if (!collisions) return;
        switch (collision.gameObject.tag)
        {

            //Use blasters as in guns
            /*case "Fuel":
                Debug.Log("You get more fuel");
                break;*/
            case "Finish":
                FinishSequence();
                break;
            case "Friendly":
                Debug.Log("Nothing happens");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        crashParticles.Play();
        isTransitioning = true;
        aS.Stop();
        if (isTransitioning) aS.PlayOneShot(crashSound);
        GetComponent<Rocket>().enabled = false;
        Invoke("ReloadLevel", delay);
    }

    void FinishSequence()
    {
        finishParticles.Play();
        isTransitioning = true;
        aS.Stop();
        if (isTransitioning) aS.PlayOneShot(finishSound);
        GetComponent<Rocket>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }


}
