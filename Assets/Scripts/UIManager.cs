using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnFireButtonClick()
    {
        //todo
        scoreText.text = "fire";
    }
    public void OnWaterButtonClick()
    {
        //todo
        scoreText.text = "water";
    }
    public void OnEarthButtonClick()
    {
        //todo
        scoreText.text = "earth";
    }
    public void OnLightningButtonClick()
    {
        //todo
        scoreText.text = "lightning";
    }
}
