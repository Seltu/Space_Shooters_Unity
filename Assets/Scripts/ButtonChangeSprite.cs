using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonChangeSprite : MonoBehaviour
{
    public Sprite changedSprite;
    public Button self;

    public void Clicked()
    {
        self.image.sprite = changedSprite;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
