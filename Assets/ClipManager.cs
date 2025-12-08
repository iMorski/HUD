using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipManager : MonoBehaviour
{
    public Fire Fire;
    public List<Transform> LetterPointGroup;
    public List<Letter> LetterGroup;
    public Transform Point;
    public float Duration;

    void Start()
    {
        //Release();
    }

    public void Release()
    {
        Letter Letter = null;
        
        for (int i = 0; i < LetterGroup.Count; i++)
        {
            if (!(LetterGroup[i].State != Letter.StateGroup.Clip)) { Letter = LetterGroup[i]; break; }
        }

        if (!Letter) return;

        Letter.State = Letter.StateGroup.Chamber;
        StartCoroutine(Move(Letter));
    }

    public void Fill(Letter Letter)
    {
        Letter.State = Letter.StateGroup.Clip;
        Letter.transform.rotation = new Quaternion();
        
        StartCoroutine(Move(Letter));
    }

    IEnumerator Move(Letter Letter)
    {
        Vector3 From = Letter.transform.position;
        Transform To = new RectTransform();
        
        switch (Letter.State)
        {
            case Letter.StateGroup.Chamber:
                Letter.GetComponent<Oscillation>().On = false;
                To = Point; break;
            case Letter.StateGroup.Clip:
                Letter.GetComponent<Spin>().On = false;
                To = LetterPointGroup[Letter.Index]; break;
        }
        
        float Time = 0;
        
        while (Time < Duration)
        {
            Letter.transform.position = Vector3.Lerp(
                From, To.position, Time / Duration);

            Time = Time + API.DTime();

            yield return null; 
        }
        
        Letter.transform.position = To.position;
        Letter.transform.SetParent(To);
        
        switch (Letter.State)
        {
            case Letter.StateGroup.Chamber:
                Letter.GetComponent<Spin>().On = true;
                Fire.Letter = Letter; break;
            case Letter.StateGroup.Clip:
                Letter.GetComponent<Oscillation>().On = true;
                
                if (!Fire.Letter) Release();
                break;
        }
    }
}
