using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{

    private GridManager _grid;
    private Tile _current;

    private void Awake()
    {
        _grid = FindObjectOfType<GridManager>();
    }

    private void Start()
    {
        _current = _grid.GetStartingTile();
        OnNewTile();
    }

    private void OnNewTile()
    {
        transform.position = _current.transform.position;
        _current.OnEnter();
    }

    public void MoveTo(Tile target)
    {
        if (!_current.CanMoveTo(target))
            return;

        _current.OnLeave();
        _current = target;

        OnNewTile();
    }

}
