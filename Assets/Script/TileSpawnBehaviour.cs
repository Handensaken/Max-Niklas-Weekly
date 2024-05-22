using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileSpawnBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject baseTile;

    [SerializeField]
    private GameObject tileParent;

    [SerializeField]
    private int width;
   
    [SerializeField]
    private int height;
     [SerializeField]
    private List<GameObject> dirtyTiles = new List<GameObject>();


    private GameObject previousTile;

    private List<GameObject> tiles = new List<GameObject>();

     

    private GameObject currentTile;
    // Start is called before the first frame update
    void Start() { }

    private void OnValidate()
    {
        tileParent.transform.position = new Vector3(0, 0, 0);
        foreach (GameObject t in this.tiles)
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                DestroyImmediate(t);
            };
        }
        tiles.Clear();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {

                if(Random.Range(1, 11) == 10 ){
                    currentTile = dirtyTiles[Random.Range(0,dirtyTiles.Count-1)];
                    Debug.Log("made dirty");
                }
                else
                {
                    currentTile = baseTile;
                }
                GameObject newTile = GameObject.Instantiate(currentTile);
                newTile.transform.SetParent(tileParent.transform);
                newTile.transform.position = new Vector3(j * 3f, 0f, i * 3f);
                tiles.Add(newTile);
            }
        }
        float test = (height * 1.5f) - 3f;
        Debug.Log("" + test);
        tileParent.transform.position = new Vector3(-test, 0f, -(width * 1.5f) + 3f);
    }

    // Update is called once per frame
    void Update() { }
}
