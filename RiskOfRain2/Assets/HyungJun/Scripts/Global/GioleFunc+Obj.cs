using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static partial class GioleFunc
{
    //! 특정 오브젝트의 자식 오브젝트를 서치해서 컴포넌트를 찾아주는 함수
    public static T FindChildComponent<T>(
        this GameObject targetObj_, string objName_) where T : Component
    {
        T searchResultComponent = default;
        GameObject searchResultObj = default;

        searchResultObj = FindChildObj(targetObj_, objName_);
        if (searchResultObj.IsValid())
        {
            searchResultComponent = searchResultObj.GetComponent<T>();
        }

        return searchResultComponent;
    }

    //! 특정 오브젝트의 자식 오브젝트를 서치해서 찾아주는 함수
    public static GameObject FindChildObj(
        this GameObject targetObj_, string objName_)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;
        for (int i = 0; i < targetObj_.transform.childCount; ++i)
        {
            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            if (searchTarget.name.Equals(objName_))
            {
                searchResult = searchTarget;
                return searchResult;
            }
            else
            {
                searchResult = FindChildObj(searchTarget, objName_);
                // 방어로직
                if (searchTarget == null || searchResult == default)
                {
                    /* Pass */
                }
                else
                {
                    return searchResult;
                }
            }
        }
        return searchResult;
    }

    //! 씬의 루트 오브젝트를 서치해서 찾아주는 함수 -> 현재 활성화 씬에서 특정 오브젝트(objName_)를 찾아주는 함수
    public static GameObject GetRootObj(string objName_)
    {
        Scene activeScene_ = GetActiveScene();
        GameObject[] rootObjs_ = activeScene_.GetRootGameObjects();

        GameObject targetObj_ = default;
        foreach (GameObject rootObj in rootObjs_)
        {
            if (rootObj.name.Equals(objName_))
            {
                targetObj_ = rootObj;
                return targetObj_;
            }
            else { continue; }
        }       // loop

        return targetObj_;
    }       // GetRootObj()

    //! 특정 오브젝트의 자식 오브젝트를 모두 리턴하는 함수
    public static List<GameObject> GetChildrenObjs(
        this GameObject targetObj_)
    {
        List<GameObject> objs = new List<GameObject>();
        GameObject searchTarget = default;

        for (int i = 0; i < targetObj_.transform.childCount; i++)
        {
            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            objs.Add(searchTarget);
        }

        if (objs.IsVaild()) { return objs; }
        else { return default(List<GameObject>); }
    }       // GetChildrenObjs()

    //! 현재 활성화 되어 있는 씬을 찾아주는 함수
    public static Scene GetActiveScene()
    {
        Scene activeScene_ = SceneManager.GetActiveScene();
        return activeScene_;
    }

    //! RectTransform 을 반환하는 함수
    public static RectTransform GetRect(this GameObject obj_)
    {
        return obj_.GetComponent<RectTransform>();
    }       // GetRect()


    //! 오브젝트 앵커 포지션을 연산한는 함수
    public static void AddAnchoredPos(this GameObject obj_,
        Vector2 position2D)
    {
        obj_.GetRect().anchoredPosition += position2D;
    }


    //! 컴포넌트 가져오는 함수
    public static SomeType GetComponentMust<SomeType>(this GameObject obj) where SomeType : Component
    {
        SomeType component_ = obj.GetComponent<SomeType>();

        GioleFunc.Assert(component_.IsValid<SomeType>() != false, $"{obj.name}에서 " +
            $"{component_.GetType().Name}을(를) 찾을 수 없습니다.");

        return component_;
    }       // GetComponentMust<>() 


    //! 컴포넌트 가져오는 함수 
    public static SomeType GetComponentMust<SomeType>(this GameObject obj, string ObjName) where SomeType : Component
    {
        SomeType component_ = obj.FindChildObj(ObjName).GetComponent<SomeType>();

        GioleFunc.Assert(component_.IsValid<SomeType>() != false, $"{obj.name}에서 " +
            $"{component_.GetType().Name}을(를) 찾을 수 없습니다.");

        return component_;
    }       // GetComponentMust<>()


    //! 트랜스폼을 사용해서 오브젝트를 움직이는 함수
    public static void Translate(this Transform transform_, Vector2 moveVector)
    {
        transform_.Translate(moveVector.x, moveVector.y, 0f);

    }

    //! RectTransform 에서 sizeDelta를 찾아서 리턴하는 함수
    public static Vector2 GetRectSizeDelta(this GameObject obj_)
    {
        return obj_.GetComponentMust<RectTransform>().sizeDelta;
    }


    #region Object transform control
    //! 오브젝트의 로컬 스케일을 변경하는 함수
    public static void SetLocalScale(this GameObject obj_, Vector3 localScale_)
    {
        obj_.transform.localScale = localScale_;
    }       // SetLocalScale()

    //! 오브젝트의 로컬 포지션을 변경하는 함수
    public static void SetLocalPos(this GameObject obj_,
        float x, float y, float z)
    {
        obj_.transform.localPosition = new Vector3(x, y, z);
    }

    //! 오브젝트의 로컬 포지션을 변경하는 함수
    public static void SetLocalPos(this GameObject obj_,
        Vector3 localPos)
    {
        obj_.transform.localPosition = localPos;
    }

    //! 오브젝트의 로컬 포지션을 연산하는 함수
    public static void AddLocalPos(this GameObject obj_,
        float x, float y, float z)
    {
        obj_.transform.localPosition =
            obj_.transform.localPosition + new Vector3(x, y, z);
    }           // AddLocalPos()
    #endregion

    //! 새로운 오브젝트를 만들어서 컴포넌트를 리턴하는 함수
    public static T CreateObj<T>(string objName) where T : Component
    {
        GameObject newObj = new GameObject(objName);
        return newObj.AddComponent<T>();
    }       // CreateObj()

    //! 오브젝트를 파괴하는 함수
    public static void DestroyObj(this GameObject obj_, float delay = 0.0f)
    {
        Object.Destroy(obj_, delay);
    }       // DestroyObj()

    //! 로컬 포지션을 기준으로 두 타일 오브젝트의 위치를 비교하는 함수
    public static int CompareTileObjToLocalPos2D(
        GameObject firstObj, GameObject secondObj)
    {
        Vector2 fPos = firstObj.transform.localPosition;
        Vector2 sPos = secondObj.transform.localPosition;

        int compareResult = 0;
        if (fPos.y.IsEquals(sPos.y))
        {
            // if: x 포지션이 같으면 같은 타일이므로 0을 리턴
            if (fPos.x.IsEquals(sPos.x)) { compareResult = 0; }
            else
            {
                if (fPos.x < sPos.x) { compareResult = -1; }
                else { compareResult = 1; }
            }
            return compareResult;
        }       // if: y 포지션이 같은 경우

        // y 포지션이 다른 경우 대소비교
        if (fPos.y < sPos.y) { compareResult = -1; }
        else { compareResult = 1; }

        return compareResult;
    }       // CompareTileObjToLocalPos2D()




}