using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesSpawner : MonoBehaviour {

    private List<Cubes> savedCubes;
    public GameObject[] points;
    private GameObject line;
    private int fakeCount = 0;
    private int nullCount = 0;
    private float timeBfrSpawn;
    public float limitSpeed = 0.15f;
    public float decreaseSpeed = 0.0015f;
    public float startSpawnTime = 0.5f;
    private Data data;
    private bool midIsNull = false;

    private void Start()
    {
        timeBfrSpawn = startSpawnTime;
        savedCubes = new List<Cubes>();
        data = GetComponent<Data>();
    }

    void FixedUpdate () {

        if (GameVariables.gameNotStart || GameManager.instance.gameOver || GameManager.instance.paused) return;

        if (timeBfrSpawn <= 0)
        {
            var cube = RandomThings();
            Cubes temp;
            line = new GameObject();
            GameVariables.LineCount++;
            for (int i = 0; i < savedCubes.Count; i++)
            {
                if (cube[i] == null) continue;
                temp = Instantiate(cube[i], points[i].transform.position, Quaternion.identity);
                temp.transform.SetParent(line.transform);
            }
            line.AddComponent<Line>();

            ResetValue();
            if (startSpawnTime>limitSpeed) startSpawnTime -= decreaseSpeed;
            timeBfrSpawn = startSpawnTime;
        }
        else
        {
            timeBfrSpawn -= Time.deltaTime;
        }
	}

    List <Cubes> RandomThings()
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
        savedCubes.Add(temp);
        do
        {
            index = Random.Range(0, data.left.Length);
            if(midIsNull)
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
        savedCubes.Add(temp);
        
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
        savedCubes.Add(temp);
        return savedCubes;
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
        if (savedCubes.Count != 0 )
        {
            foreach (var item in savedCubes)
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
        savedCubes = new List<Cubes>();
    }

}
