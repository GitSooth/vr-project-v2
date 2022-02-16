using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AdvisorManager : MonoBehaviour
{

    [SerializeField]
    private ShowCountryStats selectedCountryGetter;

    private bool advisorOnCD = false;
    private float advisorTimer = 0;
    private float advisorTimerMax = 30;

    [SerializeField]
    private TextMeshProUGUI advisorTimerText, adviseText0, adviseText, tvText;

    [SerializeField]
    private Image tvImage;

    public GameObject[] beforeChoice;
    public GameObject[] postChoice;

    public GameObject button1, button2;

    public int offerID, advisorID;
    private int beforeObjects, postObjects;

    private string tvString;

    public Sprite[] advisorImages;

    // Start is called before the first frame update
    void Start()
    {
        beforeObjects = beforeChoice.Length;
        postObjects = postChoice.Length;

        for (int i = 0; i < postObjects; i++)
        {
            postChoice[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (advisorOnCD)
        {
            advisorTimer += Time.deltaTime;
            float textTimeout = advisorTimerMax - advisorTimer;
            advisorTimerText.SetText(Mathf.Round(textTimeout).ToString());

            for (int i = 0; i < beforeObjects; i++)
            {
                beforeChoice[i].SetActive(false);
            }

            for (int i = 0; i < postObjects; i++)
            {
                postChoice[i].SetActive(true);
            }

        }

        if (advisorTimer >= advisorTimerMax)
        {
            advisorOnCD = false;
            advisorTimer = 0;
            advisorTimerText.SetText("Council Ready");

            for (int i = 0; i < beforeObjects; i++)
            {
                beforeChoice[i].SetActive(true);
            }

            for (int i = 0; i < postObjects; i++)
            {
                postChoice[i].SetActive(false);
            }
        }
    }

    public void Advise(int advisorOrigin)
    {

        tvString = "";

        if (selectedCountryGetter.selectedCountry != null)
        {
            switch (advisorOrigin)
            {
                case 1: //Economical

                    adviseText0.SetText("You believe " + selectedCountryGetter.selectedCountry.name + " is in need of economical support? Here's my offer.");
                    CalculateOffer(1);
                    advisorID = 1;
                    tvString = "Breaking News: "+ selectedCountryGetter.selectedCountry.name +" has adopted a new economical policy!";
                    break;

                case 2: //Ecological
                    adviseText0.SetText("I see " + selectedCountryGetter.selectedCountry.name + " is in need of ecological support, here's my offer.");
                    CalculateOffer(2);
                    advisorID = 2;
                    tvString = "Breaking News: " + selectedCountryGetter.selectedCountry.name + " has adopted a new ecological policy!";
                    break;

                case 3: //Social
                    adviseText0.SetText("If the people of " + selectedCountryGetter.selectedCountry.name + " are in need, here's my offer.");
                    CalculateOffer(3);
                    advisorID = 3;
                    tvString = "Breaking News: " + selectedCountryGetter.selectedCountry.name + " has adopted a new social policy!";
                    break;
            }

            advisorOnCD = true;
        }
        else
        {
            Debug.Log("Select a country first!");
        }
    }


    private void CalculateOffer(int offerTypeID)
    {
        switch (offerTypeID)
        {
            case 1:

                if (selectedCountryGetter.selectedCountry.economic > 0)  // Rolls for a "weak offer"
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            adviseText.SetText("I'll increase the economy of this continent slightly by a fixed amount.");
                            offerID = 10;
                            break;

                        case 1:
                            adviseText.SetText("I'll increase the economy of this continent slightly by a random amount.");
                            offerID = 11;
                            break;
                    }
                }
                else if (selectedCountryGetter.selectedCountry.economic < 0 && selectedCountryGetter.selectedCountry.economic > -5)  // Rolls for a "mid offer"
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            adviseText.SetText("I'll increase the economy stat moderatly, although another random stat will take damage.");
                            offerID = 20;
                            break;

                        case 1:
                            adviseText.SetText("I'll increase the economy stat moderatly, although another two stats will take slight damage.");
                            offerID = 21;
                            break;
                    }
                }
                else if (selectedCountryGetter.selectedCountry.economic < -5) // Rolls for a "gambit"
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            adviseText.SetText("I'll invert the value of all of your stats.");
                            offerID = 30;
                            break;

                        case 1:
                            adviseText.SetText("I'll fix the economy for you, although all your other stats will suffer heavy damage.");
                            offerID = 31;
                            break;
                    }
                }

                break;
            case 2:
                if (selectedCountryGetter.selectedCountry.ecologic > 0)  // Rolls for a "weak offer"
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            adviseText.SetText("We'll run a campaign, it'll increase your ecological stat by a fixed amount.");
                            offerID = 10;
                            break;

                        case 1:
                            adviseText.SetText("This innitiative might not connect with everyone, but it'll increase your ecological stat by a random amount.");
                            offerID = 11;
                            break;
                    }
                }
                else if (selectedCountryGetter.selectedCountry.ecologic < 0 && selectedCountryGetter.selectedCountry.ecologic > -5)  // Rolls for a "mid offer"
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            adviseText.SetText("I can help you heal your ecological stat, but another stat will suffer from it.");
                            offerID = 20;
                            break;

                        case 1:
                            adviseText.SetText("I'll moderatly heal the ecological sector, but all other sectors will be damaged.");
                            offerID = 21;
                            break;
                    }
                }
                else if (selectedCountryGetter.selectedCountry.ecologic < -5) // Rolls for a "gambit"
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            adviseText.SetText("As a last resort I can try to invert all your stats to fix your ecological sector.");
                            offerID = 30;
                            break;

                        case 1:
                            adviseText.SetText("I'll fix the ecological sector of this continent, but the other sectors will take massive damage if you choose to accept.");
                            offerID = 31;
                            break;
                    }
                }
                break;
            case 3:
                if (selectedCountryGetter.selectedCountry.social > 0)  // Rolls for a "weak offer"
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            adviseText.SetText("Your people are united, though with this offer we can make them even closer.");
                            offerID = 10;
                            break;

                        case 1:
                            adviseText.SetText("Since your people are already happy the best I can do is try to randomly make them better.");
                            offerID = 11;
                            break;
                    }
                }
                else if (selectedCountryGetter.selectedCountry.social < 0 && selectedCountryGetter.selectedCountry.social > -5)  // Rolls for a "mid offer"
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            adviseText.SetText("The people are everything, I'll increase your social stat at the cost of damaging another random one.");
                            offerID = 20;
                            break;

                        case 1:
                            adviseText.SetText("Your people will become happier, but both your other sectors will suffer from it.");
                            offerID = 21;
                            break;
                    }
                }
                else if (selectedCountryGetter.selectedCountry.social < -5) // Rolls for a "gambit"
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            adviseText.SetText("Your nation is in dire shape, I'll invert all your stats as a final gambit to try and fix it.");
                            offerID = 30;
                            break;

                        case 1:
                            adviseText.SetText("This populace cries for chance, I can give them what they what, but your other sectors will suffer from it.");
                            offerID = 31;
                            break;
                    }
                }
                break;
        }
    }

    public void AcceptOffer()
    {
        switch (offerID)
        {
            case 10: //Increases the stat by a fixed amount
                if (advisorID == 1)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("economic",2);
                }
                else if (advisorID == 2)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("ecologic", 2);
                }
                else if (advisorID == 3)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("social", 2);
                }
                break;
            case 11: //Increases the stat randomly
                if (advisorID == 1)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("economic", Random.Range(1, 5));
                }
                else if (advisorID == 2)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("ecologic", Random.Range(1, 5));
                }
                else if (advisorID == 3)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("social", Random.Range(1, 5));
                }
                break;
            case 20:  // Increases one decreases another random one
                if (advisorID == 1)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("economic", 7);

                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            selectedCountryGetter.selectedCountry.TakeDamage("ecologic", -3);
                            break;
                        case 1:
                            selectedCountryGetter.selectedCountry.TakeDamage("social", -3);
                            break;
                    }
                }
                else if (advisorID == 2)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("ecologic", 7);

                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            selectedCountryGetter.selectedCountry.TakeDamage("economic", -3);
                            break;
                        case 1:
                            selectedCountryGetter.selectedCountry.TakeDamage("social", -3);
                            break;
                    }
                }
                else if (advisorID == 3)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("social", 7);

                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            selectedCountryGetter.selectedCountry.TakeDamage("ecologic", -3);
                            break;
                        case 1:
                            selectedCountryGetter.selectedCountry.TakeDamage("economic", -3);
                            break;
                    }
                }
                break;
            case 21: //Increases a stat, reduces all other randomly;
                if (advisorID == 1)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("economic", 7);
                    selectedCountryGetter.selectedCountry.TakeDamage("ecologic", -Random.Range(1, 3));
                    selectedCountryGetter.selectedCountry.TakeDamage("social", -Random.Range(1, 3));
                }
                else if (advisorID == 2)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("ecologic", 7);
                    selectedCountryGetter.selectedCountry.TakeDamage("economic", -Random.Range(1, 3));
                    selectedCountryGetter.selectedCountry.TakeDamage("social", -Random.Range(1, 3));
                }
                else if (advisorID == 3)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("social", 7);
                    selectedCountryGetter.selectedCountry.TakeDamage("economic", -Random.Range(1, 3));
                    selectedCountryGetter.selectedCountry.TakeDamage("ecologic", -Random.Range(1, 3));
                }
                break;
            case 30: // Inverts all stats
                selectedCountryGetter.selectedCountry.InvertStats();
                break;
            case 31: // Heals a stat by a ton damages all other stats by a ton as well
                if (advisorID == 1)
                {

                    selectedCountryGetter.selectedCountry.TakeDamage("economic", 15);
                    selectedCountryGetter.selectedCountry.TakeDamage("ecologic", -Random.Range(4, 9));
                    selectedCountryGetter.selectedCountry.TakeDamage("social", -Random.Range(4, 9));
                }
                else if (advisorID == 2)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("ecologic", 15);
                    selectedCountryGetter.selectedCountry.TakeDamage("economic", -Random.Range(4, 9));
                    selectedCountryGetter.selectedCountry.TakeDamage("social", -Random.Range(4, 9));
                }
                else if (advisorID == 3)
                {
                    selectedCountryGetter.selectedCountry.TakeDamage("social", 15);
                    selectedCountryGetter.selectedCountry.TakeDamage("ecologic", -Random.Range(4, 9));
                    selectedCountryGetter.selectedCountry.TakeDamage("economic", -Random.Range(4, 9));
                }
                break;
        }

        button1.SetActive(false);
        button2.SetActive(false);

        adviseText0.SetText("The deal is done.");
        adviseText.SetText("");
        advisorTimer += advisorTimerMax - 5;
        tvText.SetText(tvString);
        tvImage.sprite = advisorImages[advisorID];
    }

    public void RejectOffer()
    {
        adviseText0.SetText("Perhaps another time.");
        adviseText.SetText("");

        button1.SetActive(false);
        button2.SetActive(false);


        advisorTimer += advisorTimerMax - 5;
    }

}
