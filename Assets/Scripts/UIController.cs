using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public GameObject toggleParent;
    public List<Toggle> toggleList;

    public Text scoreText;

    private void Awake()
    {
        #region Singleton

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        foreach (Transform child in UIController.Instance.toggleParent.transform)
        {
            toggleList.Add(child.gameObject.GetComponent<Toggle>());
        }
        #endregion //Singleton
    }
    public void SetToggleStatus(Toggle toggle, bool isOn)
    {
        toggle.isOn = isOn;
    }


}
