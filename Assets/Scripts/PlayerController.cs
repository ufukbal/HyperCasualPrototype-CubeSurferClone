using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10)]
    private float speed = 1f;
    public static PlayerController Instance { get; private set; }
    private float playerStartHeight;
    private bool _reposition;

    public int score = 0;

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

        #endregion //Singleton

        playerStartHeight = transform.position.y;

    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }
    private void LateUpdate()
    {
        if (!_reposition) return;
        RepositionPlayer();
        CheckColorSet();
        SetUIToggles();

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Collider>().tag == "Cube")
        {
            col.transform.position = new Vector3(transform.position.x, col.transform.position.y, transform.position.z);
            transform.position = transform.position + Vector3.up * 0.04f;
            col.transform.SetParent(transform);

            if (CheckColorSet())
            {
                DestroyColorSet();
                _reposition = true;
            }
            SetUIToggles();

        }
        if (col.GetComponent<Collider>().tag == "LevelCreate")
        {
            int playerZ = (int)transform.position.z;
            int mod = playerZ / 45;
            Vector3 levelCreatePos = new Vector3(0, 0, (mod + 1) * 45);
            GameController.Instance.CreateLevel(levelCreatePos);
        }

    }
    int CountChildObjectsByTag(GameObject parent, string tag)
    {
        int childCount = 0;
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
                childCount++;
        }
        return childCount;
    }
    int CountChildObjectsByTag()
    {
        int childCount = 0;
        foreach (Transform child in gameObject.transform)
        {
            if (child.CompareTag("Cube"))
                childCount++;
        }
        return childCount;
    }

    //green, yellow, red, purple, blue
    [SerializeField]
    private Transform[] _colorList;
    bool CheckColorSet()
    {

        foreach (Transform child in gameObject.transform)
        {
            if (child.name == "cubeGreen(Clone)")
            {
                _colorList[0] = child.transform;
            }
            else if (child.name == "cubeYellow(Clone)")
            {
                _colorList[1] = child.transform;
            }
            else if (child.name == "cubeRed(Clone)")
            {
                _colorList[2] = child.transform;
            }
            else if (child.name == "cubePurple(Clone)")
            {
                _colorList[3] = child.transform;
            }
            else if (child.name == "cubeBlue(Clone)")
            {
                _colorList[4] = child.transform;
            }
        }

        for (int i = 0; i < _colorList.Length; i++)
        {
            if (_colorList[i] == null) return false;
        }
        return true;

    }
    private void DestroyColorSet()
    {
        foreach (Transform child in _colorList)
        {
            Destroy(child.gameObject);
        }
        score++;

    }
    private void RepositionPlayer()
    {
        int childCount = CountChildObjectsByTag();

        transform.position = new Vector3(transform.position.x, (playerStartHeight + 0.04f * childCount), transform.position.z);
        int index = 0;
        foreach (Transform child in gameObject.transform)
        {

            if (child.CompareTag("Cube"))
            {
                child.position = new Vector3(transform.position.x, (transform.position.y - 0.02f - 0.04f * index), transform.position.z);
                index++;
            }
        }
        _reposition = false;

    }

    private void SetUIToggles()
    {
        UIController.Instance.SetToggleStatus(UIController.Instance.toggleList[0], _colorList[0] != null ? true : false);
        UIController.Instance.SetToggleStatus(UIController.Instance.toggleList[1], _colorList[1] != null ? true : false);

        UIController.Instance.SetToggleStatus(UIController.Instance.toggleList[2], _colorList[2] != null ? true : false);

        UIController.Instance.SetToggleStatus(UIController.Instance.toggleList[3], _colorList[3] != null ? true : false);

        UIController.Instance.SetToggleStatus(UIController.Instance.toggleList[4], _colorList[4] != null ? true : false);

        UIController.Instance.scoreText.text = score.ToString();
    }
}

