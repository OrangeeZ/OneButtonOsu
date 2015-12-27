using UnityEngine;
using System.Collections;

public static class StringExtensions {

	public static string ToBase64( this string self ) {
		
		var plainTextBytes = System.Text.Encoding.UTF8.GetBytes( self );
		
		return System.Convert.ToBase64String( plainTextBytes );
	}
}
