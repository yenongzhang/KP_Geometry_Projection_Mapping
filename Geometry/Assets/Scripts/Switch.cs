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
    protected string formula;
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
        Geometry = GameObject.Find(geometryName);
        labeltext = name[1];
        AddLabel(button, labeltext);
    }

    private void OnMouseEnter()
    {
        //for(int i = 0; i < Geometries.Length; i++)
        //{
        //    Geometries[i].GetComponent<Renderer>().material = material;
        //}
        Geometry.GetComponent<Renderer>().material = material;
        AddFormula();
    }
    private void AddLabel(GameObject parent, string text)
    {
        var label = new GameObject("Label");
        TextMeshPro textRenderer = label.AddComponent<TextMeshPro>();
        textRenderer.text = text;
        textRenderer.color = Color.black;

        label.transform.parent = parent.transform;
        label.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        label.transform.localPosition = new Vector3(0.8f, -0.5f, 0.32f);
        label.transform.localRotation = Quaternion.Euler(90, -90, 0);
    }

    private void AddFormula()
    {
        switch (Geometry.name)
        {
            case "Cube":
                if(labeltext == "volume")
                {
                    formula = "V = s x s x s";
                }
                else if(labeltext == "surface")
                {
                    formula = "SA = 6 x a x a";
                }
                break;
            case "Cuboid":
                if (labeltext == "volume")
                {
                    formula = "V = l x w x h";
                }
                else if (labeltext == "surface")
                {
                    formula = "SA = 2 x l x w + 2 x l x h + 2 x h x w";
                }
                break;
            case "Pyramid":
                break;
            case "Prism":
                break;
        }
        AddLabel(Geometry, formula);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
