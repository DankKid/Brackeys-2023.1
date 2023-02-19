using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Vector4 curve;
    [SerializeField] private float spawnTimeConstant;
    [SerializeField] private float spawnTimePerAttacker;

    [SerializeField] private List<Attacker> zombies;
    [SerializeField] private List<Attacker> robots;

    [SerializeField] private TMP_Text waveText;

    public int wave = 0;

    private List<Attacker> attackers = new();
    private float timeToSpawnInThings = 0;
    private float spawningTimeRemainging = 0;
    private int spawnedThisWave = 0;
    private int attackersToSpawnInThisWave = 0;

    private PlayerManager playerManager;

    // https://www.desmos.com/calculator/l39irp6qbf

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        waveText.text = $"Wave: {wave}";

        attackers.RemoveAll(a => a == null);

        if (IsWaveComplete())
        {
            wave++;
            // start new wave
            attackersToSpawnInThisWave = Mathf.RoundToInt(Get(wave, curve));
            timeToSpawnInThings = spawnTimeConstant + (attackersToSpawnInThisWave * spawnTimePerAttacker);
            spawningTimeRemainging = timeToSpawnInThings;
            spawnedThisWave = 0;
        }
        else
        {
            // ongoing wave
            spawningTimeRemainging -= Time.deltaTime;
            float percent = spawningTimeRemainging / timeToSpawnInThings;
            percent = 1 - percent;
            if (percent > 1)
            {
                percent = 1;
            }
            int expectedSpawnedthisWave = Mathf.RoundToInt(percent * attackersToSpawnInThisWave);
            if (spawnedThisWave < expectedSpawnedthisWave)
            {
                spawnedThisWave++;
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        Attacker attacker;
        if (playerManager.currentPhase == 0) // zombies
        {
            attacker = zombies[Random.Range(0, zombies.Count)];
        }
        else // robots
        {
            attacker = robots[Random.Range(0, robots.Count)];
        }

        Attacker spawned = Instantiate(attacker, new Vector3(16, 0, 0), Quaternion.identity);
        spawned.SetPosition(16, Random.Range(1, 6));
        attackers.Add(spawned);
    }

    private bool IsWaveComplete()
    {
        return attackers.Count == 0 && spawningTimeRemainging <= 0;
    }

    public static float Get(float x, Vector4 input)
    {
        float p = -(Mathf.Log10(input.z) / Mathf.Log10(input.y / input.x));
        return (input.x / Mathf.Pow(x, 1 / p)) + input.w;
    }
}
