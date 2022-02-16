using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Skill
{
    economy,
    social,
    ecology
}

public class Country_M : MonoBehaviour
{
    public int id;
    public string countryName;

    [Range(-10, 10)]
    public int economic, ecologic, social;
    public int econGrowth, ecoGrowth, socialGrowth;

    public int actualEcon, actualEco, actualSocial;

    public int min, max;
    public Skill skill;

    public float chanceToGetHit;

    private GameManagerGeneral worldGameManager;

    public int skillID;

    void Start()
    {
        worldGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerGeneral>();

        SetInitialStats();
    }

    // public void CallUpdateStatsPanel()
    // {
    //     worldGameManager.SetStatsPanel(id);
    // }

    public void Update()
    {
        CalculateChanceToGetHit();
        LimitStats();

        if(social<-5 && skillID == 2)
        {
            social = 1;
        }

        if (economic < -5 && skillID == 0)
        {
            economic = 1;
        }

        if (ecologic < -5 && skillID == 1)
        {
            ecologic = 1;
        }
    }

    void SetInitialStats()
    {
        chanceToGetHit = 0;

        int s = Random.Range(0, 3);

        skillID = s;

        switch (s)
        {
            case 0:
                skill = Skill.economy;
                break;
            case 1:
                skill = Skill.ecology;
                break;
            case 2:
                skill = Skill.social;
                break;
        }

        econGrowth = -1;
        ecoGrowth = -1;
        socialGrowth = -1;

        switch (skill)
        {
            case Skill.economy:
                economic = 5;
                social = 0;
                ecologic = 0;
                break;

            case Skill.social:
                social = 5;
                economic = 0;
                ecologic = 0;
                break;

            case Skill.ecology:
                ecologic = 5;
                economic = 0;
                social = 0;
                break;
        }

        actualEco = ecologic;
        actualEcon = economic;
        actualSocial = social;

        max = 10;
        min = -10;
    }

    public void CalculateChanceToGetHit()
    {
        switch (skill)
        {
            case Skill.social:
                chanceToGetHit = ((.4f * economic + .4f * ecologic + .2f * social) / 3);
                break;

            case Skill.economy:
                chanceToGetHit = ((.4f * social + .4f * ecologic + .2f * economic) / 3);
                break;

            case Skill.ecology:
                chanceToGetHit = ((.4f * economic + .4f * social + .2f * ecologic) / 3);
                break;
        }
    }

    void LimitStats()
    {
        if (social < min)
            social = min;
        if (social > max)
            social = max;

        if (economic < min)
            economic = min;
        if (economic > max)
            economic = max;

        if (ecologic < min)
            ecologic = min;
        if (ecologic > max)
            ecologic = max;
    }

    public void TakeDamage(string type, int ammount)
    {
        switch (type)
        {
            case "social":

                if(skillID == 2 )
                {
                    if( ammount > 0)
                    {
                        social += ammount * 2;
                    }
                    else if (ammount < 0)
                    {
                        social += Mathf.RoundToInt(ammount / 2);
                    }
                    
                }
                else
                {
                    social += ammount;
                }

                break;

            case "economic":

                if (skillID == 0)
                {
                    if (ammount > 0)
                    {
                        economic += ammount * 2;
                    }
                    else if (ammount < 0)
                    {
                        economic += Mathf.RoundToInt(ammount / 2);
                    }
                }
                else
                {
                    economic += ammount;
                }

                break;

            case "ecologic":

                if (skillID == 1)
                {
                    if (ammount > 0)
                    {
                        ecologic += ammount * 2;
                    }
                    else if (ammount < 0)
                    {
                        ecologic += Mathf.RoundToInt(ammount / 2);
                    }
                }
                else
                {
                    ecologic += ammount;
                }

                break;
        }
    }

    public void InvertStats()
    {

        social *= -1;
        ecologic *= -1;
        economic *= -1;
    }
}
