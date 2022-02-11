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

    public int min, max;
    public Skill skill;

    public float chanceToGetHit;

    private GameManagerGeneral worldGameManager;

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
    }

    void SetInitialStats()
    {
        chanceToGetHit = 0;

        int s = Random.Range(0, 3);

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
}
