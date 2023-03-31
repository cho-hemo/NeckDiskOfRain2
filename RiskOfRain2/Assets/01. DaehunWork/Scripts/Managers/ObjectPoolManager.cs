using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RiskOfRain2.Manager
{
    public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
    {
        public const string BULLET = "Bullet";

        #region Inspector
        [Serializable]
        public class PoolingObject
        {
            [Tooltip("추가할 오브젝트에 이름")]
            public string objectName;
            [Tooltip("추가할 오브젝트(프리팹)")]
            public GameObject obj;
            [Tooltip("추가할 오브젝트에 갯수")]
            public int count;
        }

        [Header("오브젝트 풀링 목록")]
        public List<PoolingObject> poolingObjectList = default;
        #endregion

        private Dictionary<string, Stack<GameObject>> _objectPool = default;

        private new void Awake()
        {
            base.Awake();
            ObjectPoolInit();
            Global.AddOnSceneLoaded(OnSceneLoaded);
        }

        ///<summary>씬이 로드가 완료되었을 때 호출될 함수</summary>
        ///<param name = "scene">로드된 씬에 정보를 담고 있는 변수</param>
        ///<param name = "mode">씬 로드 모드 정보를 담고 있는 변수</param>
        public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {

        }

        ///<summary>ObjectPool을 초기화 하는 함수</summary>
        private void ObjectPoolInit()
        {
            _objectPool = new Dictionary<string, Stack<GameObject>>();
            foreach (var iterator in poolingObjectList)
            {
                Stack<GameObject> tempStack = new Stack<GameObject>();
                if (0 < iterator.count)
                {
                    tempStack = new Stack<GameObject>();
                    for (int i = 0; i < iterator.count; i++)
                    {
                        GameObject tempObj = Instantiate(iterator.obj, Vector3.zero, Quaternion.identity, transform);
                        tempObj.name = iterator.obj.name;
                        tempObj.SetActive(false);
                        tempStack.Push(tempObj);
                    }
                    _objectPool.Add(iterator.objectName, tempStack);
                }
            }
        }

        ///<summary>특정 오브젝트를 ObjectPool에서 Pop하는 함수</summary>
        ///<param name = "objectPoolName_">꺼낼 오브젝트의 이름</param>
        public GameObject ObjectPoolPop(string objectPoolName_)
        {
            if (Global.ValidCollectionElement(_objectPool.Keys, objectPoolName_))
            {
                GameObject obj_ = _objectPool[objectPoolName_].Pop();
                return obj_;
            }
            else
            {
                return null;
            }
        }

        ///<summary>특정 오브젝트를 풀에 Push하는 함수</summary>
        ///<param name = "objectPoolName_">넣을 ObjectPool에 이름</param>
        ///<param name = "obj_">ObjectPool에 넣을 오브젝트</param>
        public void ObjectPoolPush(string objectPoolName_, GameObject obj_)
        {
            if (Global.ValidCollectionElement(_objectPool.Keys, objectPoolName_))
            {
                obj_.SetActive(false);
                obj_.transform.localPosition = Vector3.zero;
                obj_.transform.rotation = Quaternion.identity;
                _objectPool[objectPoolName_].Push(obj_);
            }
        }

        ///<summary>특정 오브젝트를 풀에 Push하는 함수</summary>
        ///<param name = "obj_">ObjectPool에 넣을 오브젝트</param>
        public void ObjectPoolPush(GameObject obj_)
        {
            if (Global.ValidCollectionElement(_objectPool.Keys, obj_.name))
            {
                // Debug.Log("ObjectPush");
                obj_.SetActive(false);
                obj_.transform.localPosition = Vector3.zero;
                obj_.transform.rotation = Quaternion.identity;
                _objectPool[obj_.name].Push(obj_);
            }
        }




    }
}