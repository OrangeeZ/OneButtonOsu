using UnityEngine;
using System.Collections;

public static class ColorExtensions {
	
	public static string ToHex(this Color32 source){
		var hex = source.r.ToString("X2") + source.g.ToString("X2") + source.b.ToString("X2");
		return hex;
	}
}

public static class ColorHelper {

	public static Color32 FromHex32(string hex){
		hex = hex.Trim('#');
		
		var result = new Color32();

		result.r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		result.g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		result.b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		result.a = 255;
		
		return result;
	}
	
	public static Color FromHex(string hex){
		hex = hex.Trim('#');
		
		var result = new Color();
		
		result.r = (float)byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255;
		result.g = (float)byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255;
		result.b = (float)byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255;
		result.a = 1f;
		
		return result;
	}
}
