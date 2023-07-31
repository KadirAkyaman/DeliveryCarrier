using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectOnGrid : MonoBehaviour
{
    public Transform gridCellPrefab;
    public Transform objectPrefab;

    [SerializeField] private int _height;
    [SerializeField] private int _width;

    private Node[,] _nodes;

    [SerializeField] private Transform _gridStartPos;

    void Start()
    {
        CreateGrid();//oyun baþladýðýnda grid oluþtur
        transform.position = _gridStartPos.position;
    }

    void Update()
    {

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