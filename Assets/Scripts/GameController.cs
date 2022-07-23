using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private GameObject gameOverCanvas;
    
    [Header ("Game Score Point")] 
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI totalObjectText;
    [SerializeField] private TextMeshProUGUI object2XText;
    [SerializeField] private TextMeshProUGUI object3XText;
    [SerializeField] private TextMeshProUGUI object5XText;

    [HideInInspector][SerializeField] private int totalObject = 0;
    [HideInInspector][SerializeField] private int totalScore = 0;

    [Header ("Objects Count")] 
    [HideInInspector] public List<int> smallCircleCount;
    [HideInInspector] public List<int> midCircleCount;
    [HideInInspector] public List<int> bigCircleCount;
    
    private const int FactorSmall = 5;
    private const int FactorMid = 3;
    private const int FactorBig = 2;
    
    [SerializeField] private List<GameObject> objectPrefab;
    private int _randomPrefab = 0;
    
    #region Singleton Class: GameController

    public static GameController Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        Time.timeScale = 1f;
    }
    
    #endregion

    private void Start()
    {
        backgroundMusic = gameObject.GetComponent<AudioSource>();
        backgroundMusic.Play();
        StartCoroutine(CreateObject());
    }

    private void Update()
    {
        if (CountdownTimer.Instance.currentTime <= 0)
        {
            Time.timeScale = 0f;
            backgroundMusic.Stop();
            gameOverCanvas.SetActive(true);
        }
        
        totalObject = smallCircleCount.Count + midCircleCount.Count + bigCircleCount.Count;
        totalScore = (smallCircleCount.Count * FactorSmall) +
                     (midCircleCount.Count * FactorMid) +
                     (bigCircleCount.Count * FactorBig);
        
        TextUpdate();

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(sceneBuildIndex: 0);

        if (Input.GetKey(KeyCode.Q))
            Application.Quit();
    }

    private void TextUpdate()
    {
        totalObjectText.text = "Objects: " + totalObject.ToString();
        scoreText.text = "Score: " + totalScore.ToString();
        object2XText.text = "2X Objects: " + bigCircleCount.Count;
        object3XText.text = "3X Objects: " + midCircleCount.Count;
        object5XText.text = "5X Objects: " + smallCircleCount.Count;
    }
    
    IEnumerator CreateObject()
    {
        while (true)
        {
            _randomPrefab = Random.Range(0, 3);
            Instantiate(
                objectPrefab[_randomPrefab], 
                new Vector3(Random.Range(-3, 4), 3.0f, Random.Range(-13, -7)),
                Quaternion.identity
            );
            yield return new WaitForSeconds(8f);
        }
    }
}
