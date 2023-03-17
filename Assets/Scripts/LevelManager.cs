using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies.Bosses;
using Pickups;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

[System.Serializable]
public class Round
{
    public List<Wave> waves;

    public Round(List<Wave> list)
    {
        waves = list;
    }

    public void AddWave(int enemy, int number, int curve)
    {
        var wave = new Wave(enemy, number, curve);
        waves.Add(wave);
    }
}
[System.Serializable]
public class Level
{
    public BossEnemy boss;
    public List<Round> rounds;
}
public class LevelManager : MonoBehaviour
{
    public List<Level> levels;
    public int level;
    public List<PlayerShip> players;
    public List<SpriteShapeController> enemyLines;
    public List<EnemyShip> enemyPrefabs;
    public List<Pickup> pickupPrefabs;
    private BossEnemy _boss;
    public int progress;
    private float _levelTimer = 1;
    private bool _bossFight;
    public GameObject audioController;
    
    private void Update() {
        if (_levelTimer > 0) {
            _levelTimer-=Time.deltaTime;
            return;
        }
        if (_bossFight) {
            /*if (level == 0 && !bossChannel.isPlaying) {
                bossChannel.clip = vsBaronMusic;
                bossChannel.Play();
            }
            if (level == 1 && !bossChannel.isPlaying) {
                bossChannel.clip = vsJesterMusic;
                bossChannel.Play();
            }
            if (level == 2 && !bossChannel.isPlaying) {
                bossChannel.clip = vsMonarchMusic;
                bossChannel.Play();
            }
            */
            if (_boss.summon) {
                StartCoroutine(AddRound(_boss.CreateWaves()));
            }
            return;
        }
        /*if (level > 2) {
            done = true;
            next_state = "WIN";
            gameLevel = 0;
            on_boss = false;
            return;
        }*/
        if (progress >= levels[level].rounds.Count) {
            // on_boss = true;
            _boss = Instantiate(levels[level].boss);
            _boss.onDefeat.AddListener(NextLevel);
            // AudioSource.PlayClipAtPoint(warningBossSoundEffect, Vector3.zero);
            _bossFight = true;
            _levelTimer = 10f;
            audioController.GetComponent<MusicController>().PlayWarning();
            return;
        }
        var currentRound = levels[level].rounds[progress];
        StartCoroutine(AddRound(currentRound));
    }

    private void NextLevel()
    {
        level++;
        _bossFight = false;
        progress = 0;
        foreach (var player in players)
        {
            player.LevelScale(level);
        }
    }
    
    private IEnumerator AddRound(Round currentRound)
    {
        _levelTimer = 12f;
        foreach (var wave in currentRound.waves)
        {
            StartCoroutine(AddWave(wave));
        }
        yield return new WaitForSeconds(6f);
        progress += 1;
        if (_bossFight)
        {
            _boss.summon = false;
        }
    }

    private IEnumerator AddWave(Wave wave)
    {
        //yield return new WaitForSeconds(1f/wave.number);
        for (var i=0; i < wave.number; i++)
        {
            yield return new WaitForSeconds(0.5f / wave.number);
            MakeEnemy(wave.enemy, wave.curve, wave.offset, i*0.5f);
            yield return new WaitForSeconds(0.5f / wave.number);
        }
    }

    private void MakeEnemy(int type, int curve, Vector2 offset, float delay)
    {
        var enemy = Instantiate(enemyPrefabs[type]);
        enemy.curve = enemyLines[curve];
        enemy.AddDelay(delay);
        enemy.SetOffset(offset);
        enemy.onDeath.AddListener(SpawnPickup);
    }

    private void SpawnPickup(Vector2 pos)
    {
        if(Random.Range(1, 10)==1)
            Instantiate(pickupPrefabs[Random.Range(0, pickupPrefabs.Count)], pos, quaternion.identity);
    }
}
