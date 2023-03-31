using UnityEngine;
using System;
public interface ISubject
{
	/// <summary>옵저버 등록 함수</summary>
	public void RegisterObserver(IObserver observer);
	/// <summary>옵저버 해지 함수</summary>
	public void RemoveObserver(IObserver observer);
	/// <summary>옵저버 정보 전달 함수</summary>
	void NotifyObservers(object data);
	void NotifyObservers();
}

public interface IObserver
{
	public void UpdateDate(object data);
	public void UpdateDate();
}