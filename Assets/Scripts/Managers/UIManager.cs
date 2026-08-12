using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text scoreText;

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
