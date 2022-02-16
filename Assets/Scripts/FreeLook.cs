using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeLook : MonoBehaviour
{
    public ShowCountryStats stats;
    public Continente selectedStats;
    public float sensitivity, distance;
    public GameObject selected;
    float mouseX, mouseY;
    Ray ray;
    public bool hitCol;
    // Start is called before the first frame update
    void Start()
    {
        sensitivity = 5f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY += Input.GetAxis("Mouse Y") * sensitivity;

        mouseY = Mathf.Clamp(mouseY, -85f, 85f);

        transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);

        RaycastHit hit;

        ray = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.position, transform.forward * distance);
        if (Physics.Raycast(ray, out hit, 5000))
        {
            if (hit.transform.name == "AfricaButton" ||
            hit.transform.name == "EuropeButton" ||
            hit.transform.name == "NorthAmericaButton" ||
            hit.transform.name == "SouthAmericaButton" ||
            hit.transform.name == "AsiaButton" ||
            hit.transform.name == "OceaniaButton" ||
            hit.transform.name == "Accept Button" ||
            hit.transform.name == "Reject Button" ||
            hit.transform.name == "Social Button" ||
            hit.transform.name == "Economic Button" ||
            hit.transform.name == "Ecologic Button")
            {
                hitCol = true;
                Debug.Log(hit.transform.name + "Found!");
                selected = hit.transform.gameObject;

                selectedStats = stats.selectedCountry;

                switch (selected.name)
                {
                    case "AfricaButton":
                        stats.selectedCountry = stats.africa;
                        break;
                    case "EuropeButton":
                        stats.selectedCountry = stats.europe;
                        break;
                    case "NorthAmericaButton":
                        stats.selectedCountry = stats.northAmerica;
                        break;
                    case "SouthAmericaButton":
                        stats.selectedCountry = stats.southAmerica;
                        break;
                    case "AsiaButton":
                        stats.selectedCountry = stats.asia;
                        break;
                    case "OceaniaButton":
                        stats.selectedCountry = stats.oceania;
                        break;
                }
            }
            else
                hitCol = false;
        }

        if (Input.GetMouseButtonDown(0))
            selected.GetComponent<Button>().onClick.Invoke();

        if (hitCol == false)
            stats.gameObject.SetActive(false);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(ray);
    }
}