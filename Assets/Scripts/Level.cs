using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    [Range(.1f,3f)][SerializeField] private float gameSpeed = 1.0f;
    [SerializeField] private int pointsPerBlock = 12;
    [SerializeField] private TMP_Text[] scoreTexts;
    
    private int numBlocks = 0;
    public int score = 0;
    private int levelCount;

    private void Awake()
    {
        levelCount++;
        if (levelCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        ScoreUpdate();
    }

    private void ScoreUpdate()
    {
        for (int i=0; i<scoreTexts.Length; i++)
        {
            scoreTexts[i].text = score.ToString();
        }
    }
    private void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddBlocks()
    {
        numBlocks++;
    }

    public void RemoveBlock()
    {
        numBlocks--;
        if (numBlocks <= 0)
        {
            FindObjectOfType<SceneLoader>().LoadNextScene();
        }

        score += pointsPerBlock;
        ScoreUpdate();
    }

    public void DestroyLevel()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
