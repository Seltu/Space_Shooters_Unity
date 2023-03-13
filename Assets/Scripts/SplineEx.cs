using UnityEngine;
using UnityEngine.U2D;
 
public static class SplineEx
{
    public static Vector2 GetPoint(this Spline spline, float progress)
    {
        var length = spline.GetPointCount();
        var i = Mathf.Clamp(Mathf.CeilToInt((length - 1) * progress), 0, length - 1);
     
        var t = progress * (length - 1) % 1f;
        if (i == length - 1 && progress >= 1f)
            t = 1;
 
        var prevIndex = Mathf.Max(i - 1, 0);
     
        var _p0 = new Vector2(spline.GetPosition(prevIndex).x, spline.GetPosition(prevIndex).y);
        var _p1 = new Vector2(spline.GetPosition(i).x, spline.GetPosition(i).y);
        var _rt = _p0 + new Vector2(spline.GetRightTangent(prevIndex).x, spline.GetRightTangent(prevIndex).y);
        var _lt = _p1 + new Vector2(spline.GetLeftTangent(i).x, spline.GetLeftTangent(i).y);
 
        return BezierUtility.BezierPoint(
            new Vector2(_rt.x, _rt.y),
            new Vector2(_p0.x, _p0.y),
            new Vector2(_p1.x, _p1.y),
            new Vector2(_lt.x, _lt.y),
            t
        );
    }
}