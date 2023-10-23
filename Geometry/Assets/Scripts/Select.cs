using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    private Coroutine gridCoroutine;
    private const float WaitTime = 1f;
    private Dictionary<string, Action> geometryActions;
    private (GameObject, Vector2)[] offsetArray;
    protected GameObject[] cal_buttons;
    private Color highlight = Color.cyan;
    public Material[] materials;
    private Vector3 gridSize = new Vector3(0.00014f, 0.00014f, 1e-07f);
    private GameObject textObj;
    private TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        geometryName = this.gameObject.name.Split(' ')[0];
        cal_mode = this.gameObject.name.Split(" ")[1];
        geometry = GameObject.Find(geometryName);
        middle = GameObject.Find("middle");
        textObj = GameObject.Find("Text");
        text = textObj.GetComponent<TextMeshPro>();
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " entered the trigger area " + this.gameObject.name);

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
        middle.transform.rotation = Quaternion.Euler(-90f, 45f, 0f);
        ChangeOffset(middle, new Vector2(0f, 0.6666f));
        textObj.transform.rotation = Quaternion.Euler(90f, 0f, 135f);
        textObj.transform.position = new Vector3(-0.225f, 0.003f, -0.01f);
        
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
                int[] materialIndex = new int[6];
                for (int i = 0; i < materialIndex.Length; i++)
                {
                    materialIndex[i] = 0;
                }
                Vector2Int[] gridCounts = new Vector2Int[6];
                for(int i = 0; i < 6; i++)
                {
                    gridCounts[i] = new Vector2Int(10, 10);
                }
                Vector3[] startPos = {
                    new Vector3(0.00063f, 0.00275f, 7e-06f),
                    new Vector3(0.00204f, 0.00133f, 7e-06f),
                    new Vector3(0.00063f, 0.00133f, 7e-06f),
                    new Vector3(-0.00079f, 0.00133f, 7e-06f),
                    new Vector3(0.00063f, -7.7e-05f, 7e-06f),
                    new Vector3(0.00063f, -0.0015f, 7e-06f)
                };
                gridCoroutine = StartCoroutine(ShowGrids(startPos, gridCounts, materialIndex));
                
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
        middle.transform.rotation = Quaternion.Euler(-90f, 135f, 0f);
        ChangeOffset(middle, new Vector2(0.5f, 0.6666f));
        textObj.transform.rotation = Quaternion.Euler(90f, 0f, 225f);
        textObj.transform.position = new Vector3(0.218f, 0.003f, -0.027f);
        switch (cal_mode)
        {
            case "Area":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (ground, new Vector2(-0.0006f, 0f)),
                    (ground, new Vector2(-0.0006f, 0.5f)),
                    (ground, new Vector2(0.1984f, 0.5f)),
                    (geometry, new Vector2(0f, 0.6666f)),
                    (ground, new Vector2(0.3974f, 0.5f)),
                    (geometry, new Vector2(0.3333f, 0.6666f)),
                    (ground, new Vector2(0.5966f, 0.5f)),
                    (geometry, new Vector2(0.6666f, 0.6666f)),
                    (ground, new Vector2(0.7957f, 0.5f)),
                };
                int[] materialIndex = {0, 2, 1, 0, 1, 2};
                Vector2Int[] gridCounts = {
                    new Vector2Int(11, 9),
                    new Vector2Int(11, 7),
                    new Vector2Int(7, 9),
                    new Vector2Int(11, 9),
                    new Vector2Int(7, 9),
                    new Vector2Int(11, 7),
                };
                Vector3[] startPos = {
                    new Vector3(0.00071f, 0.00219f, 7e-06f),
                    new Vector3(0.00071f, 0.00092f, 7e-06f),
                    new Vector3(0.0017f, -7.4e-05f, 7e-06f),
                    new Vector3(0.00071f, -7.4e-05f, 7e-06f),
                    new Vector3(-0.00085f, -7.4e-05f, 7e-06f),
                    new Vector3(0.00071f, -0.00135f, 7e-06f)
                };
                gridCoroutine = StartCoroutine(ShowGrids(startPos, gridCounts, materialIndex));
                break;
            case "Volume":
                offsetArray = new (GameObject, Vector2)[]
                {
                    (ground, new Vector2(-0.0006f, 0f)),
                    (ground, new Vector2(-0.0006f, 0.25f)),
                    (ground, new Vector2(0.1984f, 0.25f)),
                    (geometry, new Vector2(0f, 0.3333f)),
                    (ground, new Vector2(0.3974f, 0.25f)),
                    (geometry, new Vector2(0.3333f, 0.3333f)),
                    (ground, new Vector2(0.5966f, 0.25f)),
                    (geometry, new Vector2(0.6666f, 0.3333f)),
                    (ground, new Vector2(0.7957f, 0.25f)),
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
        middle.transform.rotation = Quaternion.Euler(-90f, 45f, 0f);
        ChangeOffset(middle, new Vector2(0.25f, 0.6666f));
        textObj.transform.rotation = Quaternion.Euler(90f, 0f, 45f);
        textObj.transform.position = new Vector3(-0.22f, 0.003f, 0.025f);
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
                int[] materialIndex = { 2, 0, 0, 1};
                Vector2Int[] gridCounts = {
                    new Vector2Int(5, 14),
                    new Vector2Int(11, 10),
                    new Vector2Int(10, 10),
                    new Vector2Int(5, 10),
                };
                Vector3[] startPos = {
                    new Vector3(0.0006f, 0.002325f, 7e-06f),
                    new Vector3(0.00218f, 0.00028f, 7e-06f),
                    new Vector3(0.000583f, 0.00028f, 7e-06f),
                    new Vector3(0.000583f, -0.001134f, 7e-06f),
                };
                gridCoroutine = StartCoroutine(ShowGrids(startPos, gridCounts, materialIndex));
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
        middle.transform.rotation = Quaternion.Euler(-90f, -45f, 0f);
        ChangeOffset(middle, new Vector2(0.75f, 0.6666f));
        textObj.transform.rotation = Quaternion.Euler(90f, 0f, 315f);
        textObj.transform.position = new Vector3(0.207f, 0.003f, 0.031f);
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
                int[] materialIndex = { 1, 2, 0, 2 };
                Vector2Int[] gridCounts = {
                    new Vector2Int(9, 7),
                    new Vector2Int(8, 11),
                    new Vector2Int(9, 11),
                    new Vector2Int(8, 11),
                };
                Vector3[] startPos = {
                    new Vector3(0.000565f, 0.0017f, 7e-06f),
                    new Vector3(0.001743f, 0.000706f, 7e-06f),
                    new Vector3(0.000565f, 0.000706f, 7e-06f),
                    new Vector3(-0.000717f, 0.000706f, 7e-06f),
                };
                gridCoroutine = StartCoroutine(ShowGrids(startPos, gridCounts, materialIndex));
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
        DeleteAllGrids();
        if (gridCoroutine != null)
        {
            StopCoroutine(gridCoroutine);
        }
        foreach (GameObject obj in Objects)
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
    IEnumerator ShowGrids(Vector3[] startPos, Vector2Int[] gridCounts, int[] materialIndex)
    {
        Quaternion middleRotation = middle.transform.rotation;
        //middleRotation.x = 45f;
        //Quaternion gridRotation = Quaternion.Euler(middleRotation.eulerAngles.x, 0f, 0f);
        int squareCount = 0;
        for(int i= 0; i<startPos.Length; i++)
        {
            for (int y = 0; y < gridCounts[i].y; y++)
            {
                for (int x = 0; x < gridCounts[i].x; x++)
                {
                    Vector3 gridPosition = new Vector3(startPos[i].x - x * gridSize.x, startPos[i].y - y * gridSize.y, startPos[i].z);
                    //GameObject newGrid = Instantiate(gridPrefab, gridPosition, gridRotation, middle.transform);
                    GameObject newGrid = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    newGrid.transform.parent = middle.transform;
                    newGrid.transform.localPosition = gridPosition;
                    newGrid.transform.localScale = gridSize;
                    newGrid.transform.rotation = middleRotation;
                    newGrid.GetComponent<Renderer>().material = materials[materialIndex[i]];
                    newGrid.SetActive(false);
                    if(i == 0) { 
                        yield return new WaitForSeconds(0.05f);
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.01f);
                    }
                        
                    newGrid.SetActive(true);
                    squareCount++;
                    text.text = "Squares: " + squareCount;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
    void DeleteAllGrids()
    {
        text.text = "";
        GameObject[] grids = new GameObject[middle.transform.childCount];
        for (int i = 0; i < middle.transform.childCount; i++)
        {
            grids[i] = middle.transform.GetChild(i).gameObject;
        }

        // 销毁所有 grids
        foreach (GameObject grid in grids)
        {
            Destroy(grid);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
