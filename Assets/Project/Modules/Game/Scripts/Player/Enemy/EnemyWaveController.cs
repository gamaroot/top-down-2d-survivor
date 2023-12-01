using Database;
using System;
using UnityEngine;
using Utils;

namespace Game
{
    internal class EnemyWaveController : MonoBehaviour
    {
        [SerializeField] private EnemyDatabase _database;
        [SerializeField] private int _enemiesPerWave = 100;

        internal Action<bool, DamagerObjectType> OnKillListener { private get; set; }
        internal Action<EnemyWaveState> StateUpdateListener { private get; set; }

        private EnemyWaveState State
        {
            get => this._state;
            set
            {
                this._state = value;
                this.StateUpdateListener.Invoke(value);
            }
        }
        private EnemyWaveState _state;

        private IBody _playerBody;

        private int _totalEnemyTypes;
        private double _spawnProbabilitiesSum;
        private double[] _spawnProbabilities;

        private int _totalSpawn;
        private float _timeElapsed, _spawnDelay;

        private readonly object _lock = new();

        private void Start()
        {
            this._totalEnemyTypes = this._database.Enemies.Length;
            this._spawnProbabilities = this.CalculateSpawnProbabilities(this._totalEnemyTypes, out this._spawnProbabilitiesSum);
        }

        private void LateUpdate()
        {
            if (this._totalSpawn >= this._enemiesPerWave)
                return;

            if (this._timeElapsed >= this._spawnDelay)
            {
                this._timeElapsed = 0;

                if (this._spawnDelay > 0.35f)
                    this._spawnDelay -= 0.05f;

                this._totalSpawn++;
                this.SpawnEnemy(this.GetEnemyType(), false, this.OnKill);
            }
            else
            {
                this._timeElapsed += Time.deltaTime;
            }
        }

        internal void Setup(IBody playerBody)
        {
            this._playerBody = playerBody;
        }

        internal void StartSpawn()
        {
            this.OnSpawnStart();
            base.enabled = true;
        }

        private void OnKill(bool isBoss, DamagerObjectType killedBy)
        {
            lock (this._lock)
            {
                this.OnKillListener.Invoke(isBoss, killedBy);
                if (isBoss)
                {
                    return;
                }

                EnemyWaveState newState = this.State;
                newState.Progress++;
                newState.IsBossActive = newState.Progress >= newState.Goal;
                this.State = newState;

                if (this.State.IsBossActive)
                {
                    this.SpawnEnemy(this.GetEnemyType(), true, this.OnKill,
                        (health, maxHealth) =>
                        {
                            EnemyWaveState newState = this.State;
                            newState.Progress = health;
                            newState.Goal = maxHealth;
                            newState.IsBossActive = true;
                            this.State = newState;
                        });
                }
            }
        }

        internal void ResetLevel()
        {
            base.enabled = false;
            EnemyPool.DisableAll();
        }

        private void OnSpawnStart()
        {
            this._totalSpawn = 0;
            this._timeElapsed = 0;
            this._spawnDelay = 2f;
            this.State = new EnemyWaveState
            {
                Progress = 0,
                Goal = this._enemiesPerWave,
                Wave = Statistics.Instance.CurrentWave
            };
        }

        private Enemy SpawnEnemy(int enemyType, bool isBoss, Action<bool, DamagerObjectType> onDestroy,
                                 Action<float, float> onHealthUpdated = null)
        {
            Enemy enemy = EnemyPool.Spawn(enemyType, this.GetSpawnPoint(isBoss));
            enemy.transform.localScale = Vector2.one;
            enemy.MaxHealth = this._database.GetHealth(Statistics.Instance.CurrentWave);
            enemy.Health = enemy.MaxHealth;
            enemy.Damage = this._database.GetDamage(Statistics.Instance.CurrentWave);
            enemy.Speed = this._database.GetSpeed(Statistics.Instance.CurrentWave);
            enemy.OnDestroy = (killedBy) => onDestroy.Invoke(isBoss, killedBy);

            if (isBoss)
            {
                enemy.TurnIntoBoss();
                enemy.OnHealthUpdated = onHealthUpdated;
            }

            EnemyMarkPool.Spawn(this._playerBody, enemy);

            return enemy;
        }

        private int PickRandomElement(double[] probabilities, double probabilitiesSum)
        {
            double randomNumber = new System.Random().NextDouble() * probabilitiesSum;

            double cumulativeSum = 0;
            for (int index = 0; index < probabilities.Length; index++)
            {
                cumulativeSum += probabilities[index];
                if (randomNumber < cumulativeSum)
                    return index;
            }
            return 0;
        }

        private double[] CalculateSpawnProbabilities(int totalElements, out double sum)
        {
            double[] probabilities = new double[totalElements];

            sum = 0;
            for (int index = 0; index < totalElements; index++)
            {
                double probability = Math.Pow((totalElements - index + 1) / 100f, Statistics.Instance.CurrentWave);
                probabilities[index] = probability;
                sum += probability;
            }
            return probabilities;
        }

        private Vector2 GetSpawnPoint(bool isBoss)
        {
            CameraBounds bounds = CameraHandler.Instance.GetRelativeCameraBounds(this._playerBody.GetPosition());

            float randomValue = UnityEngine.Random.value;

            if (isBoss)
            {
                if (randomValue > 0.5f)
                    return new Vector2(bounds.Left - 2f, UnityEngine.Random.Range(-2f, 2f));
                else
                    return new Vector2(bounds.Right + 2f, UnityEngine.Random.Range(-2f, 2f));
            }
            else
            {
                if (randomValue > 0.75f)
                    return new Vector2(UnityEngine.Random.Range(bounds.Left - 2f, bounds.Right + 2f), bounds.Top + 2f);

                else if (randomValue > 0.5f)
                    return new Vector2(UnityEngine.Random.Range(bounds.Left - 2f, bounds.Right + 2f), bounds.Bottom - 2f);

                else if (randomValue > 0.25f)
                    return new Vector2(bounds.Left - 2f, UnityEngine.Random.Range(bounds.Bottom - 2f, bounds.Top + 2f));

                else
                    return new Vector2(bounds.Right + 2f, UnityEngine.Random.Range(bounds.Bottom - 2f, bounds.Top + 2f));
            }
        }

        private int GetEnemyType()
        {
            if (Statistics.Instance.CurrentWave < this._totalEnemyTypes)
            {
                return Statistics.Instance.CurrentWave;
            }
            else {
                return this.PickRandomElement(this._spawnProbabilities, this._spawnProbabilitiesSum);
            }
        }
    }
}