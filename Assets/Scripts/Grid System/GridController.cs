using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController Instance;

    public Transform gridCellPrefab;

    public int _height;
    public int _width;
    public int _maxObjectOnCell;

    private Node[,] _nodes;

    [SerializeField] private Transform _gridCenterPos; // Sahnenin ortasýndaki nokta

    public List<GameObject> cells = new List<GameObject>();

    public int emptyGridNumber;
    public bool isCellOccupied;

    [SerializeField] private BoxCollider _boxCollider;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreateGrid();
        ChangeColliderSize();
        transform.position = _gridCenterPos.position; // Grid'i sahnenin ortasýna taþý
        isCellOccupied = false;
        emptyGridNumber = 0;
    }

    public void CreateGrid()
    {
        _nodes = new Node[_width, _height];
        var name = 0;

        Transform parentTransform = GameObject.Find("Grid").transform; // Parent obje

        Vector3 startPos = _gridCenterPos.position - new Vector3((_width - 1) * 0.5f, 0, (_height - 1) * 0.5f + 0.58f);// Grid'in sol alt köþesinin konumu
        startPos.z -= 5f; // Z ekseni deðerini ayarla

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Vector3 worldPos = startPos + new Vector3(i, 0, j);
                Transform obj = Instantiate(gridCellPrefab, worldPos, Quaternion.identity, parentTransform);
                obj.name = "Cell " + name;
                _nodes[i, j] = new Node(true, worldPos, obj);
                cells.Add(obj.gameObject);
                name++;
            }
        }
    }

    private void ChangeColliderSize()
    {
        _boxCollider.center = new Vector3(0, 0, 0);//Merkezi sýfýrlýyoruz 
        _boxCollider.size = new Vector3(_width, 0, _height);
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