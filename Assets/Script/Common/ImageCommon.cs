using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ImageCommon
{
    /// <summary>
    /// �����x�̕ύX
    /// </summary>
    /// <param name="image">�Ώۂ̉摜 </param>
    /// <param name="alpha">�����x</param>
    public static void SetOpacity(this Image image, float alpha)
    {
        var c = image.color;
        image.color = new Color(c.r, c.g, c.b, alpha);
    }
}
