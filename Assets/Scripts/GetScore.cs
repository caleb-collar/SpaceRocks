using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    [SerializeField] private TMP_Text[] scoreTexts;
    private TMP_Text reference;
    private string score;
    void Start()
    {
        reference = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();
        score = reference.text;
        for (int i=0; i<scoreTexts.Length; i++)
        {
            scoreTexts[i].text = score.ToString();
        }
    }
}
