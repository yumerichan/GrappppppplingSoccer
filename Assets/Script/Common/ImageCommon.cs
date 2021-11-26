using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ImageCommon
{
    /// <summary>
    /// “§–¾“x‚Ì•ÏX
    /// </summary>
    /// <param name="image">‘ÎÛ‚Ì‰æ‘œ </param>
    /// <param name="alpha">“§–¾“x</param>
    public static void SetOpacity(this Image image, float alpha)
    {
        var c = image.color;
        image.color = new Color(c.r, c.g, c.b, alpha);
    }
}
