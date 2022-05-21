using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
    public enum SpawnState {SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave wave;
    public Text waveText;

    private int nextWave = 0;

    public int NextWave {
        get {return nextWave+1;}
    }
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    public float WaveCountDown {
        get { return waveCountdown;}
    }
    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.WAITING;
    public SpawnState State
    {
        get{return state;}
    }

    void Start()
    {
        waveText.text = "WAVE " + nextWave.ToString();
        if(spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced");
        }
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if(state == SpawnState.WAITING){
            if(!EnemyIsAlive())
            {
                WaveCompleted();
            } else
            {
                return;
            }
        }        
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.SPAWNING;
        waveCountdown = timeBetweenWaves;

        nextWave++;
        waveText.text = "WAVE " + nextWave.ToString();

        wave.count = wave.count + 2;
        StartCoroutine(SpawnWave(wave));
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            foreach(GameObject i in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                EnemyController e = i.GetComponent<EnemyController>();
                if(e.currentHealth > 0.0f)
                {
                    return true;
                }
            }
            return false;
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;
        
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0,spawnPoints.Length)];
        Transform t = Instantiate(_enemy,new  Vector3(Random.Range(_sp.position.x-10,_sp.position.x+10),0,Random.Range(_sp.position.z-10,_sp.position.z+10)), _sp.rotation);
        t.GetComponent<EnemyController>().currentHealth = 100.0f;
        t.GetComponent<EnemyController>().healthBarCanvas.SetActive(true);
        t.GetComponent<EnemyAttack>().setCanAttack(true);
        t.GetComponent<Animator>().SetBool("Walk Forward", true);
        t.GetComponent<BoxCollider>().enabled = true;
        t.GetComponent<EnemyController>().isDead = false;
        t.GetComponent<Animator>().SetBool("Walk Forward", true);
        t.GetComponent<Animator>().enabled = true;
    }
}