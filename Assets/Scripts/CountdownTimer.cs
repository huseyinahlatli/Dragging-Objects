using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    
    [Header("Timer UI")]
    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float duration;
    [HideInInspector] public float currentTime;
    
    #region Singleton Class: CountdownTimer

    public static CountdownTimer Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    private void Start()
    {
        currentTime = duration;
        timerText.text = currentTime.ToString();

        StartCoroutine(UpdateTimer());

    }
    
    private IEnumerator UpdateTimer()
    {
        while (currentTime >= 0)
        {
            timerImage.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
            timerText.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        yield return null;
    }
}
