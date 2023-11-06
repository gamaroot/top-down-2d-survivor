using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    internal class Pool
    {
        internal int ExpandedPoolSize = 1;

        private readonly GameObject _resourcePrefab;

        private readonly Stack<GameObject> _pool = new();
        private readonly Transform _parentGameObject;

        private readonly Action<GameObject> _onObjectCreated;
        private readonly Action<GameObject> _onObjectActivated;

        internal Pool(Transform parentGameObject, GameObject resourcePrefab,
                      Action<GameObject> onObjectCreated = null,
                      Action<GameObject> onObjectActivated = null)
        {
            var node = new GameObject(resourcePrefab.name);
            node.transform.SetParent(parentGameObject);

            this._parentGameObject = node.transform;
            this._resourcePrefab = resourcePrefab;

            this._onObjectCreated = onObjectCreated;
            this._onObjectActivated = onObjectActivated;
        }

        internal T BorrowMeObjectFromPool<T>()
        {
            if (this._pool.Count == 0)
                this.AddObjectsToPool(ExpandedPoolSize);

            GameObject hereToYou = this._pool.Pop();
            hereToYou.SetActive(true);

            this._onObjectActivated?.Invoke(hereToYou);

            return hereToYou.GetComponent<T>();
        }

        internal Transform Spawn(float autoDisableInSeconds)
        {
            if (this._pool.Count == 0)
                this.AddObjectsToPool(ExpandedPoolSize);

            GameObject hereToYou = this._pool.Pop();
            PoolingObject poolingObject = hereToYou.GetComponent<PoolingObject>();
            poolingObject.ScheduleAutoDisableInSeconds(autoDisableInSeconds);
            hereToYou.SetActive(true);

            this._onObjectActivated?.Invoke(hereToYou);

            return hereToYou.transform;
        }

        internal AudioSource PlayAudioFromPool(float volume)
        {
            if (this._pool.Count == 0)
                this.AddObjectsToPool(ExpandedPoolSize);

            GameObject element = this._pool.Pop();

            AudioSource audioSource = element.GetComponent<AudioSource>();
            audioSource.volume = volume;

            PoolingObject poolingObject = element.GetComponent<PoolingObject>();
            poolingObject.ScheduleAutoDisableInSeconds(audioSource.clip.length);

            element.SetActive(true);

            return audioSource;
        }

        internal void SendBackToThePool(GameObject giveMeBack)
        {
            giveMeBack.transform.SetParent(this._parentGameObject, true);

            this._pool.Push(giveMeBack);
        }

        internal Transform GetParent()
        {
            return this._parentGameObject;
        }

        internal void DisableAll()
        {
            foreach (PoolingObject item in this._parentGameObject.GetComponentsInChildren<PoolingObject>())
                item.Disable();
        }

        private void AddObjectsToPool(int quantity)
        {
            for (int index = 0; index < quantity; index++)
            {
                GameObject newResourceObject = MonoBehaviour.Instantiate(this._resourcePrefab);
                newResourceObject.name += index;
                newResourceObject.transform.SetParent(this._parentGameObject, true);
                newResourceObject.SetActive(false);

                this._onObjectCreated?.Invoke(newResourceObject);

                PoolingObject poolingObject = newResourceObject.AddComponent<PoolingObject>();
                poolingObject.SetResourcePool(this);

                this._pool.Push(newResourceObject);
            }
        }
    }
}