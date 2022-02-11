using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCountryStats : MonoBehaviour
{
    public Country_M selectedCountry;
    public GameObject StatsCanvas;
    public Country_M africa, asia, europe, oceania, southAmerica, northAmerica;

    public TextMeshProUGUI _name, social, economic, ecologic;

    void Start()
    {
        _name.text = "";
        social.text = "";
        economic.text = "";
        ecologic.text = "";
    }
    void Update()
    {
        _name.text = selectedCountry.name;
        social.text = "Social: " + selectedCountry.social.ToString();
        economic.text = "Economic: " + selectedCountry.economic.ToString();
        ecologic.text = "Ecologic: " + selectedCountry.ecologic.ToString();
    }

    public void SelectAfrica()
    {
        selectedCountry = africa;
    }

    public void SelectAsia()
    {
        selectedCountry = asia;
    }

    public void SelectNorthAmerica()
    {
        selectedCountry = northAmerica;
    }

    public void SelectSouthAmerica()
    {
        selectedCountry = southAmerica;
    }

    public void SelectOceania()
    {
        selectedCountry = oceania;
    }

    public void SelectEurope()
    {
        selectedCountry = europe;
    }

    public void Appear()
    {
        StatsCanvas.SetActive(true);
    }

    public void Disappear()
    {
        StatsCanvas.SetActive(false);
    }
}
