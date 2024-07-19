using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCounter : MonoBehaviour
{
    public int GemTotal = 0;
    public TextMeshProUGUI GemText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGemText();
    }

    // UpdateGemText
    private void UpdateGemText()
    {
        GemText.text = GemTotal.ToString();
    }

    // when the player collects a gem
    public void CollectGem()
    {
        GemTotal++;
        UpdateGemText();
    }
}
