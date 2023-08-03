using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController Instance;

    public Transform gridCellPrefab;
   // public Transform objectPrefab;

    public int _height;
    public int _width;

    private Node[,] _nodes;

    [SerializeField] private Transform _gridStartPos;

    public List<GameObject> cells = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreateGrid();//oyun ba�lad���nda grid olu�tur
        transform.position = _gridStartPos.position;
    }

    void Update()
    {
        if (PlaceObjectsOnGrid.Instance.isCellOccupied)//E�er grid tamamen doluysa
        {
            Debug.Log("Cells Occupied");
        }
    }


    private void CreateGrid()
    {
        _nodes = new Node[_width, _height];
        var name = 0;

        Transform parentTransform = GameObject.Find("Grid").transform;//Parent obje

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Vector3 worldPos = new Vector3(i, 0, j);
                Transform obj = Instantiate(gridCellPrefab, worldPos, Quaternion.identity, parentTransform);
                obj.name = "Cell " + name;
                _nodes[i, j] = new Node(true, worldPos, obj);
                cells.Add(obj.gameObject);
                name++;
            }
        }
    }
}

public class Node
{
    public bool isPlaceable;
    public Vector3 cellPos;
    public Transform obj;

    public Node(bool isPlaceable, Vector3 cellPos, Transform obj)
    {
        this.isPlaceable = isPlaceable;
        this.cellPos = cellPos;
        this.obj = obj;
    }


}