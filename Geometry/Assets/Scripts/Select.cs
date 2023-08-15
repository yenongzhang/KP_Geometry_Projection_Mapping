using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Select : MonoBehaviour
{
    protected string geometryName;
    protected string cal_mode;
    protected GameObject geometry;
    protected GameObject middle;
    protected GameObject ground;
    protected GameObject[] Objects;
    private Coroutine textureChangeCoroutine;
    private const float WaitTime = 1f;
    private Dictionary<string, Action> geometryActions;
    private (GameObject, Vector2)[] offsetArray;
    protected GameObject[] cal_buttons;
    private Color highlight = Color.cyan;
    // Start is called before the first frame update
    void Start()
    {
        geometryName = this.gameObject.name.Split(' ')[0];
        cal_mode = this.gameObject.name.Split(" ")[1];
        geometry = GameObject.Find(geometryName);
        middle = GameObject.Find("middle");
        string groundName = geometryName + " Ground";
        ground = GameObject.Find(groundName);
        Objects = GameObject.FindGameObjectsWithTag("ObjectsWithTexture");
        cal_buttons = GameObject.FindGameObjectsWithTag("Button");
        geometryActions = new Dictionary<string, Action>
    {
        {"cube", CubeAction},
        {"cuboid", CuboidAction},
        {"pyramid", PyramidAction},
        {"prism", PrismAction}
    };
    }
    private void OnMouseEnter()
    {
        ResetMaterials();
        this.GetComponent<Renderer>().material.color = highlight;
        if (geometryActions.TryGetValue(geometryName, out Action action))
        {
            action.Invoke();
        }
        if (textureChangeCoroutine != null)
        {
            StopCoroutine(textureChangeCoroutine);
        }
        textureChangeCoroutine = StartCoroutine(PerformAction());
    }
    void CubeAction()
    {
        middle.transform.rotation = Quaternion.Euler(-90f, -45f, 0f);
        ChangeOffset(middle, new Vector2(0f, 0.6666f));
        
        switch (cal_mode)
        {
            case "Area":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (ground, new Vector2(0f, 0.25f)),
                    (ground, new Vector2(0f, 0.75f)),
                    (ground, new Vector2(0.3333f, 0.75f)),
                    (geometry, new Vector2(0f, 0.6666f)),
                    (ground, new Vector2(0.6666f, 0.75f)),
                };
                break;
            case "Volume":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (ground, new Vector2(0f, 0.25f)),
                    (ground, new Vector2(0f, 0.5f)),
                    (ground, new Vector2(0.3333f, 0.5f)),
                    (geometry, new Vector2(0f, 0.3333f)),
                    (ground, new Vector2(0.6666f, 0.5f)),
                };
                break;
            case "Formula":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (middle, new Vector2(0f, 0.3333f)),
                    (geometry, new Vector2(0f, 0.3333f)),
                    (geometry, new Vector2(0f, 0.6666f)),
                };
                break;
        }
        
    }
    void CuboidAction()
    {
        middle.transform.rotation = Quaternion.Euler(-90f, 45f, 0f);
        ChangeOffset(middle, new Vector2(0.5f, 0.6666f));
        switch (cal_mode)
        {
            case "Area":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (ground, new Vector2(0f, 0f)),
                    (ground, new Vector2(0f, 0.5f)),
                    (ground, new Vector2(0.2f, 0.5f)),
                    (geometry, new Vector2(0f, 0.6666f)),
                    (ground, new Vector2(0.4f, 0.5f)),
                    (geometry, new Vector2(0.3333f, 0.6666f)),
                    (ground, new Vector2(0.6f, 0.5f)),
                    (geometry, new Vector2(0.6666f, 0.6666f)),
                    (ground, new Vector2(0.8f, 0.5f)),
                };
                break;
            case "Volume":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (ground, new Vector2(0f, 0f)),
                    (ground, new Vector2(0f, 0.25f)),
                    (ground, new Vector2(0.2f, 0.25f)),
                    (geometry, new Vector2(0f, 0.3333f)),
                    (ground, new Vector2(0.4f, 0.25f)),
                    (geometry, new Vector2(0.3333f, 0.3333f)),
                    (ground, new Vector2(0.6f, 0.25f)),
                    (geometry, new Vector2(0.6666f, 0.3333f)),
                    (ground, new Vector2(0.8f, 0.25f)),
                };
                break;
            case "Formula":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (middle, new Vector2(0.5f, 0.3333f)),
                    (geometry, new Vector2(0.6666f, 0.3333f)),
                    (geometry, new Vector2(0.6666f, 0.6666f)),
                };
                break;
        }
        
        
    }
    void PyramidAction()
    {
        middle.transform.rotation = Quaternion.Euler(-90f, -45f, 0f);
        ChangeOffset(middle, new Vector2(0.25f, 0.6666f));
        switch (cal_mode)
        {
            case "Area":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (ground, new Vector2(0f, 0.25f)),
                    (ground, new Vector2(0f, 0.75f)),
                    (ground, new Vector2(0.25f, 0.75f)),
                    (geometry, new Vector2(0f, 0.6666f)),
                    (ground, new Vector2(0.5f, 0.75f)),
                    (geometry, new Vector2(0.5f, 0.6666f)),
                    (ground, new Vector2(0.75f, 0.75f)),
                };
                break;
            case "Volume":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (ground, new Vector2(0f, 0.25f)),
                    (ground, new Vector2(0f, 0.5f)),
                    (ground, new Vector2(0.25f, 0.5f)),
                    (geometry, new Vector2(0f, 0.3333f)),
                    (geometry, new Vector2(0.5f, 0.3333f)),
                    (ground, new Vector2(0.5f, 0.5f)),
                };
                break;
            case "Formula":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (middle, new Vector2(0.25f, 0.3333f)),
                    (geometry, new Vector2(0.5f, 0.3333f)),
                    (geometry, new Vector2(0.5f, 0.6666f)),
                };
                break;
        }
        
    }
    void PrismAction()
    {
        middle.transform.rotation = Quaternion.Euler(-90f, -135f, 0f);
        ChangeOffset(middle, new Vector2(0.75f, 0.6666f));
        switch (cal_mode)
        {
            case "Area":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (ground, new Vector2(0f, 0f)),
                    (ground, new Vector2(0f, 0.5f)),
                    (ground, new Vector2(0.2f, 0.5f)),
                    (geometry, new Vector2(0f, 0.6666f)),
                    (ground, new Vector2(0.4f, 0.5f)),
                    (geometry, new Vector2(0.3333f, 0.6666f)),
                    (ground, new Vector2(0.6f, 0.5f)),
                    (geometry, new Vector2(0.6666f, 0.6666f)),
                    (ground, new Vector2(0.8f, 0.5f)),
                };
                break;
            case "Volume":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (ground, new Vector2(0f, 0f)),
                    (ground, new Vector2(0f, 0.25f)),
                    (ground, new Vector2(0.2f, 0.25f)),
                    (geometry, new Vector2(0f, 0.3333f)),
                    (ground, new Vector2(0.4f, 0.25f)),
                    (geometry, new Vector2(0.3333f, 0.3333f)),
                    (geometry, new Vector2(0.6666f, 0.3333f)),
                    (ground, new Vector2(0.6f, 0.25f)),
                };
                break;
            case "Formula":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (middle, new Vector2(0.75f, 0.3333f)),
                    (geometry, new Vector2(0.6666f, 0.3333f)),
                    (geometry, new Vector2(0.6666f, 0.6666f)),
                };
                ChangeOffset(middle, new Vector2(0.75f, 0.3333f));
                break;
        }
        
    }
    IEnumerator PerformAction()
    {
        
        foreach((GameObject obj, Vector2 offset) in offsetArray)
        {
            if(this.GetComponent<Renderer>().material.color == highlight)
            {
                ChangeOffset(obj, offset);
                yield return new WaitForSeconds(WaitTime);
            }
        }
    }

    void ChangeOffset(GameObject obj, Vector2 v)
    {
        obj.GetComponent<Renderer>().material.mainTextureOffset = v;
    }
    void ResetMaterials()
    {
        foreach(GameObject obj in Objects)
        {
            Debug.Log(obj);
            if(obj.name == "cuboid Ground" || obj.name == "prism Ground")
            {
                ChangeOffset(obj, new Vector2(0f, 0.75f));
            }
            else
            {
                ChangeOffset(obj, new Vector2(0f, 0f));
            }  
        }
        foreach(GameObject button in cal_buttons)
        {
            string mode = button.name.Split(' ')[1];
            switch (mode)
            {
                case "Area":
                    button.GetComponent<Renderer>().material.color = new Color(0.576f, 0.933f, 0.506f);
                    //button.GetComponent<Renderer>().material.color = Color.green;
                    break;
                case "Volume":
                    button.GetComponent<Renderer>().material.color = new Color(0.514f, 0.376f, 0.819f);
                    //button.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case "Formula":
                    button.GetComponent<Renderer>().material.color = new Color(0.945f, 0.439f, 0.745f);
                    //button.GetComponent<Renderer>().material.color = Color.magenta;
                    break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
