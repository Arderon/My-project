using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LvlUpButtonCreator : MonoBehaviour
{
    public GameObject levelUpMenu;
    public GameObject buttonPrefab;
    public GameObject buttonParent;
    public int choicesNum = 3;
    private List<Component> possibleUpgrades = new List<Component>();

    private void OnEnable()
    {
        GetAllPossibleUpgrades();
        if (possibleUpgrades.Count == 0)
        {
            Time.timeScale = 1;
            levelUpMenu.SetActive(false);
            Debug.Log("Max level reached");
        }

        else{
            for (int i = 0; i < choicesNum; i++)
            {
                if (possibleUpgrades.Count > 0)
                {
                    Upgradable w = ChooseUpgrade();
                    if (w.level == 10)
                    {
                        break;
                    }
                    GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
                    newButton.GetComponentInChildren<TextMeshProUGUI>().text = w.GetUpgradeText();
                    newButton.GetComponent<Button>().onClick.AddListener(delegate { UpgradeButton(w); });
                }

            }
        }
    }

    private void UpgradeButton(Upgradable weapon)
    {
        Time.timeScale = 1;
        levelUpMenu.SetActive(false);
        for (int i = buttonParent.transform.childCount - 1; i >= 0; i--)
        {
            GameObject child = buttonParent.transform.GetChild(i).gameObject;
            DestroyImmediate(child);
        }
        weapon.Upgrade();
    }

    private void GetAllPossibleUpgrades()
    {
        List<Component> components = new List<Component>(GameObject.Find("Player").GetComponents(typeof(Upgradable)));
        List<Component> newList  = new List<Component>();
        for (int i = 0; i < components.Count; i++)
        {
            Upgradable w = (Upgradable)components[i];
            if (w.level == w.maxLevel)
            {
/*                Debug.Log(w.GetType() + " removed");*/
            }
            else
            {
                newList.Add(w);
/*                Debug.Log(w.GetType());*/
            }
        }
        possibleUpgrades = newList;
    }

    private Upgradable ChooseUpgrade()
    {
        Upgradable w = (Upgradable)possibleUpgrades[Random.Range(0, possibleUpgrades.Count)];
        possibleUpgrades.Remove(w);
        return w;
    }
}
