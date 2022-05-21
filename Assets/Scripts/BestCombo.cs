using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestCombo : MonoBehaviour
{
    private int bestCombo = 0;
    public Text bestComboText;

    // Start is called before the first frame update
    void Start()
    {
        bestComboText.text = "BEST COMBO " + bestCombo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        bestCombo = player.highestCombo;
        bestComboText.text = "BEST COMBO " + bestCombo.ToString();
    }
}
