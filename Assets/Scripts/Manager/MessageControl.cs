using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageControl : MonoBehaviour {
    public string[] scenarios;
    [SerializeField]
    Text uiText;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;

    private string currentText = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    private int currentLine = 0;
    private int lastUpdateCharacter = -1;
	private bool counter;
	private float time;
	private int line;

    public int i = 0;
    public int j = 0;
    public GameObject message;
    public GameObject[] lantern;

    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Start()
    {
        SetNextLine();
    }

    void Update()
    {
        if (IsCompleteDisplayText)
        {
            if (currentLine < scenarios.Length && Input.GetMouseButtonDown(1))
            {
                Invoke("SetNextLine",1f);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                timeUntilDisplay = 0;
            }
        }

        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
        if (displayCharacterCount != lastUpdateCharacter)
        {
            uiText.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }

		if ( counter ) {
			time += Time.deltaTime;
			if ( time >= 0.5f ) {
				switch ( line ) {
				case 0:
					SetNextTitle( );
					break;
				case 1:
					SetNextTitle1( );
					break;
				}
				counter = false;
				time = 0;
			}
		}
    }

    void SetNextLine()
    {
        AudioManager.Instance.PlaySE("Kaigyou");
        if (currentLine < i)
        {
            currentText = scenarios[currentLine];
            timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
            timeElapsed = Time.time;
            currentLine++;
            lastUpdateCharacter = -1;
        }
    }

    public void SetNextTitle()
    {
        AudioManager.Instance.PlaySE("Kaigyou2");
        if (currentLine < i)
        {
            currentText = scenarios[currentLine];
            timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
            timeElapsed = Time.time;
            currentLine++;
            lastUpdateCharacter = -1;
            if (currentLine >= j)
            {
                lantern[0].SetActive(true);
            }
        } else {
            lantern[0].SetActive(false);
            message.SetActive(false);
            if ( j == 1)
            {
                lantern[2].SetActive(true);
            }
            lantern[1].SetActive(true);

        }
    }


    public void SetNextTitle1()
    {
        AudioManager.Instance.PlaySE("Kaigyou2");
        if (currentLine < i)
        {
            currentText = scenarios[currentLine];
            timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
            timeElapsed = Time.time;
            currentLine++;
            lastUpdateCharacter = -1;
            if (currentLine >= 0)
            {
                lantern[2].SetActive(true);
            }
        } else
        {
            message.SetActive(false);
            lantern[2].SetActive(false);
            lantern[0].SetActive(true);
            lantern[1].SetActive(true);
        }
    }

	public void enterline1Pointer(){
		counter = true;
		line = 0;
	}

	public void enterline2Pointer(){
		counter = true;
		line = 1;
	}

	public void exitlinePointer(){
		counter = false;
	}
}