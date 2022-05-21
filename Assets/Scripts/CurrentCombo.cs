using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentCombo : MonoBehaviour
{
    private int currentCombo = 0;
    public Text currentComboText;

    // Start is called before the first frame update
    void Start()
    {
        currentComboText.text = "COMBO " + currentCombo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        currentCombo = player.currentCombo;
        currentComboText.text = "COMBO " + currentCombo.ToString();
    }
}
