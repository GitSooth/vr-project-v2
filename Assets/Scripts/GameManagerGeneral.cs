using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerGeneral : MonoBehaviour
{
    bool done = false;
    public float timeBetweenTicks = 2f;

    [Header("Country Vars")]
    public Country_M[] countries;

    [SerializeField]
    private TextMeshProUGUI advisorText;

    public int maxMultiplier = 0, maxID, minID;

    [Header("Control Vars")]
    [SerializeField]
    private Slider worldSlider;

    private float globalTimer, globalTimeout;
    public float graceTimer;
    public int globalMultiplier;

    [SerializeField]
    private AudioManager audioManager;
    //private Image globalTimerBarImage;

    public Gradient globalTimerGradient;

    private int randomEventInterval = 5, graceTimerMax = 15;

    public bool eventCanOccur = false, graceTimerIsOn = false;

    [SerializeField]
    private bool gameHasStarted = false, gameHasEnded = false;

    [SerializeField]
    private Animator fadeAnimator;

    [Header("Event Vars")]
    public int randomEventChance;
    public int randomRange;

    public int eventslists;

    public float hpCounter = 0;

    public Image fadeImage;
    public Material mat;

    public Image newsImage;

    public Sprite[] eventImages;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        CalculateGlobalMultiplier();
        globalTimeout = 60000;
        worldSlider.maxValue = globalTimeout;

        randomEventChance = 0;
        randomRange = 0;

        graceTimerIsOn = true;
    }

    void Update()
    {
        if (!done)
            StartCoroutine("TickTime");

        hpCounter += -1 * (globalMultiplier / 10);

        worldSlider.value = hpCounter;

        if (worldSlider.value == globalTimeout && !gameHasEnded)
        {
            gameHasEnded = true;
            fadeAnimator.Play("FadeToBlack");
        }

        if (fadeImage.color.a == 1 && gameHasEnded)
        {
            SceneManager.LoadScene("M_TestScene");
        }


        /*if (gameHasStarted && !gameHasEnded)
        {
            globalTimer += Time.deltaTime * (-1 * globalMultiplier);

            worldSlider.value = globalTimer;

            //globalTimerBarImage.color = globalTimerGradient.Evaluate(globalTimer / globalTimeout);

            if (globalTimer >= globalTimeout)
            {
                Debug.Log("ah gottem' ggs");
                gameHasEnded = true;
            }


            if (Mathf.RoundToInt(globalTimer) % randomEventInterval == 0 && eventCanOccur)
            {
                // GenerateRandomEvent();
                eventCanOccur = false;
            }
    }

        if (graceTimerIsOn == true)
            graceTimer += Time.deltaTime;

        if (graceTimer > graceTimerMax)
        {
            gameHasStarted = true;
            eventCanOccur = true;
            graceTimerIsOn = false;
            graceTimer = 0;
        }*/
    }

    private void CalculateGlobalMultiplier()
    {
        for (int i = 0; i < countries.Length; i++)
        {
            globalMultiplier += countries[i].ecologic + countries[i].economic + countries[i].social;
        }

        globalMultiplier += -5;
    }

    public void GenerateRandomEvent()
    {

        audioManager.Play("EventAlert");


        switch (Random.Range(0, 11))
        {
            case 0:
                newsImage.sprite = eventImages[0];
                maxID = FindStrongestCountry();
                advisorText.SetText("War ravages through " + countries[maxID].countryName +
                " all sectors suffer heavy damage from this tragedy.");

                StartCoroutine(ChangeToRed());

                countries[maxID].ecologic -= 5;
                countries[maxID].economic -= 5;
                countries[maxID].social -= 5;
                break;

            case 1:
                newsImage.sprite = eventImages[1];
                minID = FindWeakestCountry();
                advisorText.SetText("Even in the direst of times the continent of " + countries[minID].countryName +
                " has found a way to gather it's forces, all stats are recovered significantly!");
                mat.color = Color.red;

                StartCoroutine(ChangeToGreen());

                countries[minID].ecologic += 5;
                countries[minID].economic += 5;
                countries[minID].social += 5;

                break;

            case 2:
                newsImage.sprite = eventImages[2];
                int tempRand = Random.Range(0, 6);

                advisorText.SetText(countries[tempRand].countryName +
                " had an unexpected windfall, all stats are increased moderatly!");

                StartCoroutine(ChangeToGreen());

                countries[tempRand].ecologic += 5;
                countries[tempRand].economic += 5;
                countries[tempRand].social += 5;

                break;

            case 3:
                newsImage.sprite = eventImages[3];
                tempRand = Random.Range(0, 6);

                advisorText.SetText("The turtles in the coasts of " + countries[tempRand].countryName +
                " have evolved and revolted against the people, the ecological sector is thriving but all other sectors have suffered damage.");

                StartCoroutine(ChangeToRed());

                countries[tempRand].ecologic += 10;
                countries[tempRand].economic -= 5;
                countries[tempRand].social -= 5;

                break;

            case 4:
                newsImage.sprite = eventImages[4];
                tempRand = Random.Range(0, 6);

                advisorText.SetText("For the people! The masses of " + countries[tempRand].countryName +
                " rise against all other sectors to fight for humanitarian rights, sacrificing the health of other sectors in the process.");

                StartCoroutine(ChangeToRed());

                countries[tempRand].ecologic -= 5;
                countries[tempRand].economic -= 5;
                countries[tempRand].social += 10;

                break;

            case 5:
                newsImage.sprite = eventImages[5];
                tempRand = Random.Range(0, 6);

                advisorText.SetText("The wealthy aristocrats of " + countries[tempRand].countryName +
                " are in control and will stop at nothing to make sure their sector thrives, even if it costs the wealth of all other sectors...");

                StartCoroutine(ChangeToRed());

                countries[tempRand].ecologic -= 5;
                countries[tempRand].economic += 10;
                countries[tempRand].social -= 5;

                break;

            case 6:
                newsImage.sprite = eventImages[6];
                tempRand = Random.Range(0, 6);

                advisorText.SetText("Pure Chaos! The sectors of " + countries[tempRand].countryName +
                " are all randomly affected in either a positive or negative way.");

                StartCoroutine(ChangeToGreen());

                countries[tempRand].ecologic += Random.Range(-5, 5);
                countries[tempRand].economic += Random.Range(-5, 5);
                countries[tempRand].social += Random.Range(-5, 5);

                break;

            case 7:
                newsImage.sprite = eventImages[7];
                tempRand = Random.Range(0, 6);

                advisorText.SetText("Pure Luck! The sectors of " + countries[tempRand].countryName +
                " are all randomly affected in a positive way.");

                StartCoroutine(ChangeToGreen());

                countries[tempRand].ecologic += Random.Range(1, 10);
                countries[tempRand].economic += Random.Range(1, 10);
                countries[tempRand].social += Random.Range(1, 10);

                break;

            case 8:
                newsImage.sprite = eventImages[8];
                tempRand = Random.Range(0, 6);

                advisorText.SetText("Pure Missfortune! The sectors of " + countries[tempRand].countryName +
                " are all randomly affected in a negative way.");

                StartCoroutine(ChangeToRed());

                countries[tempRand].ecologic -= Random.Range(1, 10);
                countries[tempRand].economic -= Random.Range(1, 10);
                countries[tempRand].social -= Random.Range(1, 10);

                break;

            case 9:
                newsImage.sprite = eventImages[9];
                maxID = FindStrongestCountry();

                advisorText.SetText("Prosperity breeds prosperity." + countries[maxID].countryName +
                " has gained a significant increase to all stats.");

                StartCoroutine(ChangeToGreen());

                countries[maxID].ecologic += Random.Range(3, 10);
                countries[maxID].economic += Random.Range(3, 10);
                countries[maxID].social += Random.Range(3, 10);

                break;

            case 10:
                newsImage.sprite = eventImages[10];
                maxID = FindWeakestCountry();

                advisorText.SetText("Misery breeds Misery." + countries[minID].countryName +
                " has taken significant damage to all stats.");

                StartCoroutine(ChangeToRed());

                countries[maxID].ecologic -= Random.Range(3, 10);
                countries[maxID].economic -= Random.Range(3, 10);
                countries[maxID].social -= Random.Range(3, 10);

                break;
        }


        graceTimerIsOn = true;
    }

    private int FindStrongestCountry()
    {
        Country_M country = null;

        foreach (Country_M c in countries)
        {
            int val = (int)c.chanceToGetHit;

            if (c.chanceToGetHit >= val)
                country = c;
        }

        return country.id;
    }

    private int FindWeakestCountry()
    {
        Country_M country = null;

        foreach (Country_M c in countries)
        {
            int val = (int)c.chanceToGetHit;

            if (c.chanceToGetHit <= val)
                country = c;
        }

        return country.id;
    }

    private int CalculateCountryMultiplier(int countryID)
    {
        return countries[countryID].ecologic + countries[countryID].economic + countries[countryID].social;
    }

    #region Event Controller

    IEnumerator TickTime()
    {
        randomRange = Random.Range(0, 100);

        if (randomEventChance >= randomRange)
        {
            GenerateRandomEvent();
            randomEventChance = 0;
        }
        else
            randomEventChance += 10;

        ChangeStats();

        CalculateGlobalMultiplier();


        done = true;
        yield return new WaitForSeconds(timeBetweenTicks);
        done = false;
    }

    public void ChangeStats()
    {
        foreach (Country_M c in countries)
        {
            c.social += c.socialGrowth;
            c.ecologic += c.ecoGrowth;
            c.economic += c.econGrowth;
        }
    }

    IEnumerator ChangeToRed()
    {
        mat.color = Color.red;

        yield return new WaitForSeconds(1.5f);

        mat.color = Color.white;
    }

    IEnumerator ChangeToGreen()
    {
        mat.color = Color.green;

        yield return new WaitForSeconds(1.5f);

        mat.color = Color.white;
    }

    #endregion
}