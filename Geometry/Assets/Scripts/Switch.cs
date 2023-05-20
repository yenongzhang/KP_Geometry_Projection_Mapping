using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Switch : MonoBehaviour
{
    protected GameObject[] Geometries;
    protected Material material;
    protected string labeltext;
    protected GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        if (Geometries == null)
        {
            Geometries = GameObject.FindGameObjectsWithTag("Geometry");
        }
        button = this.gameObject;
        material = this.GetComponent<Renderer>().material;
        labeltext = button.name;
        AddLabel();
    }

    private void OnMouseEnter()
    {
        for(int i = 0; i < Geometries.Length; i++)
        {
            Geometries[i].GetComponent<Renderer>().material = material;
        }
    }
    private void AddLabel()
    {
        var label = new GameObject("Label");
        TextMeshPro textRenderer = label.AddComponent<TextMeshPro>();
        textRenderer.text = labeltext;
        textRenderer.color = Color.black;


        label.transform.parent = button.transform;
        label.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        label.transform.localPosition = new Vector3(0.8f, -0.5f, 0.32f);
        label.transform.localRotation = Quaternion.Euler(90, -90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
