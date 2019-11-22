﻿using data;
using UnityEngine;
using wallSystem;
using Debug = UnityEngine.Debug;

using DS = data.DataSingleton;
using L = main.Loader;

public class GenerateMazeFromFile : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        var m = L.Get().CurrTrial.Value.Map;
        var y = m.TopLeft[1];

        // Goes through each map and initializes it based on stuff.
        foreach (var row in m.Map)
        {
            var x = m.TopLeft[0];

            foreach (var col in row.ToCharArray())
            {
                if (col == 'w')
                {
                    var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    obj.GetComponent<Renderer>().sharedMaterial.color = Data.GetColour(m.Color);
                    obj.transform.localScale = new Vector3(m.TileWidth, L.Get().CurrTrial.maze.WallHeight, m.TileWidth);
                    obj.transform.position = new Vector3(x, L.Get().CurrTrial.maze.WallHeight * 0.5f, y);
                }
                else if (col == 's')
                {
                    Debug.Log(x + " " + y);
                    GameObject.Find("FirstPerson").GetComponent<PlayerController>().ExternalStart(x, y, true);
                }
                else if (col != '0')
                {
                    var val = col - '0';
                    var item = DS.GetData().Goals[val - 1];
                    var prefab = (GameObject)Resources.Load("3D_Objects/" + item.Type, typeof(GameObject));

                    var obj = Instantiate(prefab);

                    if (!item.Type.Equals("2DImage"))
                    {
                        obj.AddComponent<RotateBlock>();
                        obj.GetComponent<Renderer>().material.color = Data.GetColour(item.Color);
                    }

                    obj.transform.localScale = item.ScaleVector;

                    obj.transform.position = new Vector3(x, 0.5f, y);
                    var sprite = item.Image;

                    var pic = Img2Sprite.LoadNewSprite(DataSingleton.GetData().SpritesPath + sprite);
                    obj.GetComponent<SpriteRenderer>().sprite = pic;
                }

                x += m.TileWidth;
            }

            y -= m.TileWidth;
        }
    }
}
