using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class GraphicExtensions {

    public static void SetColor( this Graphic self, float r = float.NaN, float g = float.NaN, float b = float.NaN, float a = float.NaN ) {

        var color = self.color;

        color.r = r.IsNan() ? color.r : r;
        color.g = g.IsNan() ? color.g : g;
        color.b = b.IsNan() ? color.b : b;
        color.a = a.IsNan() ? color.a : a;

        self.color = color;
    }

}