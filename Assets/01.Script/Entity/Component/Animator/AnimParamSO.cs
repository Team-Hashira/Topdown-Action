using UnityEngine;

[CreateAssetMenu(fileName = "AnimParamSO", menuName = "SO/Animator/AnimParamSO")]
public class AnimParamSO : ScriptableObject
{
    public string paramName;
    public int paramHash;

    private void OnValidate()
    {
        paramHash = Animator.StringToHash(paramName);
    }
}
