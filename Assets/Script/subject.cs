using System.Collections.Generic;
using UnityEngine;

 public class Notifier : MonoBehaviour 
{
        
    private List<IObserver> observers = new List<IObserver>();
    public static Notifier notifier;

    void Awake ()
    {
        notifier = this;
    }

   

    public void AddObserver (IObserver observer)
    {
        observers.Add(observer);

    }

    public void RemoveObserver (IObserver observer)
    {
        observers.Remove(observer);
    }

    protected void NotifyMessage (PlayerActions action)
    {
        observers.ForEach(_observer => {
            _observer.OnNotify(action);
        });
    }



}