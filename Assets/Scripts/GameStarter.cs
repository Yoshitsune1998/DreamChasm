using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    public GameObject player;
    public GameObject StartGameMenu;
    public GameObject Controller;
    public GameObject PlayUI;

    private float beforePlay = .5f;
    private List<Cubes> starterCubes;
    private Data data;
    private int nullCount = 0;
    private int fakeCount = 0;
    private bool midIsNull = false;

    void Start () {
        Debug.Log("start game");
        Instantiate(player, player.transform.position, Quaternion.identity);
        data = GetComponent<Data>();
        starterCubes = new List<Cubes>();

        //starter position, size and invisibility
        float[] pos = new float[]
        {
            -0.50f, 1.85f, 4.25f
        };
        float[] size = new float[]
        {
            0.575f, 0.275f, 0.01f
        };
        int[] inv = new int[]
        {
            255,255,185
        };
        //

        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                StartCube();
            }
            else
            {
                RandomThings();
            }
            var cube = starterCubes;
            Cubes temp;
            GameObject line = new GameObject();
            GameVariables.LineCount++;
            Vector2 temp2;
            for (int j = 0; j < starterCubes.Count; j++)
            {
                if (cube[j] == null) continue;
                switch (cube[j].Pos)
                {
                    case "Middle":
                        temp2 = new Vector2(0,pos[i]);
                        break;
                    case "Left":
                        temp2 = new Vector2(-2.25f, pos[i]);
                        break;
                    case "Right":
                        temp2 = new Vector2(2.25f, pos[i]);
                        break;
                    default: return;
                }
                temp = Instantiate(cube[j], temp2, Quaternion.identity);
                if (i == 0)
                {
                    Debug.Log("masuk");
                    Player._player.cubesOn = temp.gameObject;
                }
                temp.transform.SetParent(line.transform);
                
                GameObject x = temp.gameObject;
                x.transform.localScale = new Vector2(size[i],size[i]);
                var y = x.GetComponent<SpriteRenderer>();
                var z = y.color;
                z.a = inv[i];
                y.color = z;
            }
            line.AddComponent<Line>();
            if(GameVariables.stopLining) line.name = "Line " + GameVariables.LineCount;
            ResetValue();
        }
    }

    private void Update()
    {
        if (beforePlay <= 0)
        {
            StartPlay();
        }
        else
        {
            beforePlay -= Time.deltaTime;
        }
    }

    public void BeginGame()
    {
        Time.timeScale = 1f;
        StartGameMenu.SetActive(false);
        Controller.SetActive(true);
        PlayUI.SetActive(true);
        GameVariables.gameNotStart = false;
        Destroy(GetComponent<GameStarter>());
    }

    private void StartPlay()
    {
        Time.timeScale = 0f;
        StartGameMenu.SetActive(true);
        GameVariables.gameNotStart = true;
        GameVariables.stopLining = false;
    }

    void StartCube()
    {
        Cubes temp;
        int index = 0;
        do
        {
            index = Random.Range(0, data.mid.Length);
            if (data.mid[index] == null || !data.mid[index].GetComponent<Cubes>().IsReal) midIsNull = true;
            else midIsNull = false;
            temp = data.mid[index];
        } while (temp==null || !temp.IsReal);
        starterCubes.Add(temp);
    }

    void RandomThings()
    {
        Cubes temp;
        bool x = true;
        int index = 0;
        do
        {
            index = Random.Range(0, data.mid.Length);
            if (data.mid[index] == null || !data.mid[index].GetComponent<Cubes>().IsReal) midIsNull = true;
            else midIsNull = false;
            x = CheckCube(data.mid[index]);
        } while (x);
        temp = data.mid[index];
        starterCubes.Add(temp);
        do
        {
            index = Random.Range(0, data.left.Length);
            if (midIsNull)
            {
                if (data.left[index] == null || !data.left[index].GetComponent<Cubes>().IsReal) x = true;
                else x = CheckCube(data.left[index]);
            }
            else
            {
                x = CheckCube(data.left[index]);
            }
        } while (x);
        temp = data.left[index];
        starterCubes.Add(temp);

        do
        {
            index = Random.Range(0, data.right.Length);
            if (midIsNull)
            {
                if (data.right[index] == null || !data.right[index].GetComponent<Cubes>().IsReal) x = true;
                else x = CheckCube(data.right[index]);
            }
            else
            {
                x = CheckCube(data.right[index]);
            }

        } while (x);
        temp = data.right[index];
        starterCubes.Add(temp);

    }

    bool CheckCube(Cubes cube)
    {
        if (nullCount > 0 && cube == null) return true;
        if (cube == null)
        {
            nullCount++;
            return false;
        }
        if (fakeCount > 0 && !cube.IsReal) return true;
        if (starterCubes.Count != 0)
        {
            foreach (var item in starterCubes)
            {
                if (item == null) continue;
                if (item.Name.Equals(cube.Name)) return true;
            }
        }
        if (!cube.IsReal) fakeCount++;
        return false;
    }

    void ResetValue()
    {
        fakeCount = 0;
        nullCount = 0;
        starterCubes = new List<Cubes>();
    }

}
