using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [SerializeField]
    Text collectedText;
    [SerializeField]
    Text levelText;
    [SerializeField]
    Text missedText;
    [SerializeField]
    Text scoreText;
    public int score;
    public int missed;
    public int collected;
    public int level;

    //Custom delegate with no parameters
    public delegate void MyEventHandlerEmpty();

    //The event belonging to the custom delegate
    public event MyEventHandlerEmpty LevelUnlockedEvent;
    // Start is called before the first frame update
    void Awake()
    {
        collected = 0;
        level = 1;
        missed = 0;
        Player.StarCollected += OnStarCollected;
        Star.StarMissed += OnStarMissed;
        LevelUnlockedEvent += OnLevelUnlocked;
        Star.StarCollectedWithValue += OnStarCollectedWithValue;
    }
    void Start()
    {
        UpdateUI();
    }
    void OnStarCollectedWithValue(object sender, StarInfo e)
    {
        score += e.value;
        UpdateUI();
    }
    void OnStarCollected(object sender, EventArgs e)
    {
        collected += 1;
        if(collected%5 == 0){
            LevelUnlockedEvent?.Invoke();
        }
        UpdateUI();
    }
    void OnLevelUnlocked(){
        level += 1;
        UpdateUI();
    }
    void OnStarMissed()
    {
        missed += 1;
        UpdateUI();
    }
    void UpdateUI()
    {
        collectedText.text = collected.ToString();
        levelText.text = level.ToString();
        missedText.text = missed.ToString();
        scoreText.text = score.ToString();
    }
    void OnDestroy()
    {
        Player.StarCollected -= OnStarCollected;
        Star.StarMissed -= OnStarMissed;
        LevelUnlockedEvent -= OnLevelUnlocked;
        Star.StarCollectedWithValue -= OnStarCollectedWithValue;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
