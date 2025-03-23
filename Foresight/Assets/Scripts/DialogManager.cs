using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public string[] sentences;
    public float typingSpeed;
    private int index;
    public GameObject continueButton;
    public GameObject startGameButton;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }


    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray()) 
        {
            displayText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            displayText.text = "";
            StartCoroutine(Type());
        }
        else
        {
            displayText.text = "";
            continueButton.SetActive(false);
        }

    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // Update is called once per frame
    void Update()
    {
        if(displayText.text == sentences[index] && index != sentences.Length-1)
        {
            continueButton.SetActive(true);
        }
        else if(displayText.text == sentences[index] && index == sentences.Length - 1)
        {
            startGameButton.SetActive(true);
        }
    }
}
