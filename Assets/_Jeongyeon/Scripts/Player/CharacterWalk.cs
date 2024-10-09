using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalk : MonoBehaviour
{
    public int walkSoundIndex;

    public void CharacterWalkSound()
    {
        SoundManager.Instance.PlayCharacterAudio(walkSoundIndex);
    }
}
