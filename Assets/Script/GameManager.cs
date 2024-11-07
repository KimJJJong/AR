using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<GameManager>();
                    singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";

                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    //Csv颇老 包府
    public DialogueBehaviorTree behaviorTree;


    //ARChracter包府
    public bool canSpawn;
    public ECharacterState characterState;

    void Start()
    {
        behaviorTree = GameObject.Find("BehaviorTree").GetComponent<DialogueBehaviorTree>();
        canSpawn = true;
        characterState = ECharacterState.Idle;
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    
}
