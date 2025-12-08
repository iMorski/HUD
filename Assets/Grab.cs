using System;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public ClipManager ClipManager;
    public float Distance;

    private void Update()
    {
        for (int i = 0; i < ClipManager.LetterGroup.Count; i++)
        {
            Letter Letter = ClipManager.LetterGroup[i];
            
            if (Vector3.Distance(transform.position, Letter.transform.position) < Distance &&
                !(Letter.State != Letter.StateGroup.Ground)) ClipManager.Fill(Letter);
        }
    }
}
