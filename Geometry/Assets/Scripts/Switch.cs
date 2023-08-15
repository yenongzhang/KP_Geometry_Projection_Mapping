using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Switch : MonoBehaviour
{
    protected GameObject Geometry;
    protected Material material;
    protected string labeltext;
    protected GameObject button;
    protected string geometryName;
    protected GameObject formula;
    protected string formulaStringCopy;
    protected string formulaName;
    protected GameObject[] formulas;
    private Coroutine colorChangeCoroutine;
    protected Transform parentTransform;
    List<GameObject> highlights = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //if (Geometries == null)
        //{
        //    Geometries = GameObject.FindGameObjectsWithTag("Geometry");
        //}
        button = this.gameObject;
        material = this.GetComponent<Renderer>().material;
        string[] name = button.name.Split(' ');
        geometryName = name[0];
        labeltext = name[1];
        Geometry = GameObject.Find(geometryName);
    }

    private void OnMouseEnter()
    {
        Debug.Log(geometryName);
        //Geometry.GetComponent<Renderer>().material = material;
        parentTransform = Geometry.transform;
        formulas = GameObject.FindGameObjectsWithTag("Formula");
        foreach (GameObject formula in formulas)
        {
            formula.SetActive(false);
        }
        switch (labeltext)
        {
            case "Volume":
                formulaName = geometryName + " V";
                break;
            case "Area":
                formulaName = geometryName + " SA";
                break;
        }
        formula = FindChildGameObject(parentTransform, formulaName);
        formula.SetActive(true);
        if (colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
        }
        colorChangeCoroutine = StartCoroutine(ChangeColorOverTime());
    }

    IEnumerator ChangeColorOverTime()
    {
        yield return new WaitForSeconds(1f);
        TextMeshPro formulaText = formula.GetComponent<TextMeshPro>();
        string formulaString = formulaText.text;
        formulaStringCopy = formulaString;
        switch (geometryName)
        {
            case "Cube":
                formulaString = formulaString.Replace("s", "<color=red>s</color>");
                break;
            case "Cuboid":
                if(labeltext == "Area")
                {
                    formulaString = formulaString.Replace("2LW", "<color=red>2LW</color>");
                    formulaString = formulaString.Replace("2LH", "<color=green>2LH</color>");
                    formulaString = formulaString.Replace("2WH", "<color=blue>2WH</color>");
                    ShowHighlights("CuboidSurface");
                }
                formulaText.text = formulaString;
                yield return new WaitForSeconds(2f);
                HideHighLights();
                formulaString = formulaStringCopy;
                formulaString = formulaString.Replace("L", "<color=red>L</color>");
                formulaString = formulaString.Replace("W", "<color=blue>W</color>");
                formulaString = formulaString.Replace("H", "<color=green>H</color>");
                
                break;
            case "Pyramid":
                break;
            case "Prism":
                break;
        }
        ShowHighlights("HighlightLine");
        formulaText.text = formulaString;
        yield return new WaitForSeconds(3f);
        formulaText.text = formulaStringCopy;
        HideHighLights();
    }
    private void ShowHighlights(string tag)
    {
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform child = parentTransform.GetChild(i);
            if (child.CompareTag(tag))
            {
                highlights.Add(child.gameObject);
            }
        }
        foreach (GameObject line in highlights)
        {
            line.SetActive(true);
        }
    }
    private void HideHighLights()
    {
        if (highlights.Count > 0)
        {
            foreach (GameObject line in highlights)
            {
                line.SetActive(false);
            }
        }
        highlights.Clear();
    }

    GameObject FindChildGameObject(Transform parent, string name)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.name == name)
            {
                return child.gameObject;
            }
            GameObject foundChild = FindChildGameObject(child, name);
            if (foundChild != null)
            {
                return foundChild;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
