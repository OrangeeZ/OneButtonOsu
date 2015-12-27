using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Collections;

public class OsuBeatmapParser : MonoBehaviour {

	public TextAsset beatmap = null;

	public bool normalize = true;

	public float timeScale = 1000f;

	public float startingDelay = 5f;

	private IEnumerable<string> GetLines( StreamReader streamReader ) {

		while ( !streamReader.EndOfStream ) {

			yield return streamReader.ReadLine();
		}
	}

	[ContextMenu( "Parse" )]
	public void Parse() {

		using ( var reader = new StreamReader( new MemoryStream( beatmap.bytes ) ) ) {

			var lines = GetLines( reader );

			var trackDatas = lines.SkipWhile( each => !each.Contains( "HitObjects" ) ).Skip( 1 );

			var beats =
				trackDatas.Select( each => each.Split( ',' ) )
					.Select(
						each => new Beatmap.Beat { time = Convert.ToInt32( each[2] ) / timeScale + startingDelay, isLong = each.Any( any => any.Contains( "|" ) ) || each.Length > 5 } )
					.ToArray() as IEnumerable<Beatmap.Beat>;

			if ( normalize ) {
			
				var firstBeat = beats.FirstOrDefault();

				beats = beats.Select( each => new Beatmap.Beat { time = ( each.time - firstBeat.time ) + startingDelay, isLong = each.isLong } );
			}

			var beatmapComponent = gameObject.GetComponent<Beatmap>() ?? gameObject.AddComponent<Beatmap>();

			beatmapComponent.beatmap = beats.ToArray();
		}
	}
}
