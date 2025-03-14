using UnityEngine;

public static class Anim
{
    const float c4 = (2 * Mathf.PI) / 3;
    public static float ElasticOut(float original,float value,float t,float bounce = 1)
    {
        float currentC4 = c4 * bounce;

        if (t == 0)
            return original;
        else if (t == 1)
            return value;

        float y = Mathf.Pow(2 , -10 * t) * Mathf.Sin((t * 10 - 0.75f) * currentC4) + 1;
        return original + ((value - original) * y);
    }

    public static float ElasticIn(float original, float value, float t, float bounce = 1)
    {
        float currentC4 = c4 * bounce;

        if (t == 0)
            return original;
        else if (t == 1)
            return value;

        float y = Mathf.Pow(2, 10 * t - 10) * Mathf.Sin((t * 10 - 10.75f) * currentC4);

        return original + ((value - original) * y);
    }

    public static float SineInOut(float original,float value,float t)
    {
        float y = -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
        return original + ((value - original) * y);
    }

    public static float Linear(float original, float value, float t)
    {
        return original + ((value - original) * t);
    }

    /* SOURCE: https://easings.net/ */
}

public enum EasingStyle
{
    ElasticInOut,
    ElasticIn,
    SineInOut,
    Linear
}