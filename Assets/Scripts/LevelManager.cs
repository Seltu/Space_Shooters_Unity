using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

[System.Serializable]
public class Round
{
    public List<Wave> waves;
    public void AddWave(int enemy, int number, int curve)
    {
        var wave = new Wave(enemy, number, curve);
        waves.Add(wave);
    }
}
public class LevelManager : MonoBehaviour
{
    public GameObject boss;
    public List<SpriteShapeController> enemyLines;
    public List<EnemyShip> enemyPrefabs;
    public List<Round> rounds;
    private int _progress;
    private float _levelTimer = 1;
    private bool _bossFight;
    
    private void Update() {
        if (_levelTimer > 0) {
            _levelTimer-=Time.deltaTime;
            return;
        }
        if (_bossFight) {
            /*if (gameLevel == 0 && !bossChannel.isPlaying) {
                bossChannel.clip = vsBaronMusic;
                bossChannel.Play();
            }
            if (gameLevel == 1 && !bossChannel.isPlaying) {
                bossChannel.clip = vsJesterMusic;
                bossChannel.Play();
            }
            if (gameLevel == 2 && !bossChannel.isPlaying) {
                bossChannel.clip = vsMonarchMusic;
                bossChannel.Play();
            }
            */
            //if (boss.summon) {
            //    AddWaves(level.boss.CreateWaves());
            //}

            return;
        }

        /*if (gameLevel > 2) {
            done = true;
            next_state = "WIN";
            gameLevel = 0;
            on_boss = false;
            return;
        }*/

        if (_progress >= rounds.Count) {
            // on_boss = true;
            // enemies.Add(level.boss);
            // AudioSource.PlayClipAtPoint(warningBossSoundEffect, Vector3.zero);

            /*if (boss.aimed) {
                aim_enemies.Add(level.boss);
            }*/
            _bossFight = true;
            _levelTimer = 10f;
            return;
        }
        var currentRound = rounds[_progress];
        StartCoroutine(AddRound(currentRound));
    }

    private IEnumerator AddRound(Round currentRound)
    {
        _levelTimer = 12f;
        foreach (var wave in currentRound.waves)
        {
            StartCoroutine(AddWave(wave));
        }
        yield return new WaitForSeconds(6f);
        _progress += 1;
        if (_bossFight)
        {
            //boss.summon = false;
        }
    }

    private IEnumerator AddWave(Wave wave)
    {
        for (var i=0; i < wave.number; i++)
        {
            MakeEnemy(wave.enemy, wave.curve, i*0.5f);
            yield return new WaitForSeconds(1f / wave.number);
        }
    }

    private void MakeEnemy(int type, int curve, float delay)
    {
        var enemy = Instantiate(enemyPrefabs[type]);
        enemy.curve = enemyLines[curve];
        enemy.AddDelay(delay);
    }
}