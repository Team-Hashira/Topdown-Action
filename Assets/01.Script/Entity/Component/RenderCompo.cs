using System.Collections.Generic;
using UnityEngine;

public class RenderCompo : MonoBehaviour, IEntityInitComponent
{
    private List<SpriteRenderer> _rendererList;

    public float Fancing { get; private set; }
    private Transform _lookTargetTrm;

    public void Initialize(Entity unit)
    {
        GetComponentsInChildren(_rendererList);
    }

    public void SetAutoLookTarget(Transform target)
    {
        _lookTargetTrm = target;
    }

    private void Update()
    {
        if (_lookTargetTrm != null)
        {
            LookDir(Mathf.Sign(_lookTargetTrm.position.x - transform.position.x));
        }
    }

    public void Flip()
    {
        Fancing *= -1;
        transform.eulerAngles = new Vector3(0, Fancing < 0 ? 180 : 0, 0);
    }

    public void LookDir(float xDir)
    {
        if (Mathf.Abs(Fancing - xDir) > 1f)
        {
            Flip();
        }
    }
}
