using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TilePrefabCreator : MonoBehaviour
{
    [SerializeField] Vector3 _childPosition;
    [SerializeField] List<GameObject> _tilesToCreate;

    [Button]
    void CreateTilePrefabs()
    {
        foreach (var tile in _tilesToCreate)
        {
            //* Root Object
            var newRoot = new GameObject($"GridTile {tile.name}");
            newRoot.transform.parent = transform;

            var newChild = Instantiate(tile, newRoot.transform);
            newChild.AddComponent<BoxCollider>();
            newChild.transform.localPosition = _childPosition;
            newChild.name = tile.name;
        }
    }
}
