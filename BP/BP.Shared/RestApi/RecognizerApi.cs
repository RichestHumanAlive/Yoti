﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Uno.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BP.Shared.RestApi
{
	public class RecognizerApi : WebApiBase
	{
		private Dictionary<string, string> defaultHeaders = new Dictionary<string, string> {
				{"accept", "application/json" },
			};
		private readonly string baseUrl = "https://yotiserver.azurewebsites.net/recognition";

		public async Task<Song> UploadSong(SongWavFormat songToUpload)
		{
			var result = await PostAsync(
				baseUrl + "/addnewsong",
				JsonConvert.SerializeObject(songToUpload),
				defaultHeaders);

			this.Log().LogInformation(result);

			if (result != null)
			{
				return JsonConvert.DeserializeObject<Song>(result);
			}

			return null;
		}
	
		public async Task<RecognitionResult> RecognizeSong(SongWavFormat songToRecognize)
		{
			var result = await PostAsync(
				baseUrl + "/recognizesong",
				JsonConvert.SerializeObject(songToRecognize),
				defaultHeaders);

			this.Log().LogInformation(result);

			if (result != null)
			{
				return JsonConvert.DeserializeObject<RecognitionResult>(result);
			}

			return null;
		}

		public async Task<List<Song>> GetSongs()
		{
			var result = await GetAsync(
			   baseUrl + "/getsongs",
			   defaultHeaders);

			this.Log().LogInformation(result);

			if (result != null)
			{
				return JsonConvert.DeserializeObject<List<Song>>(result);
			}

			return new List<Song>();
		}
		
		public async Task<Song> DeleteSong(Song songToDelete)
		{
			var result = await this.DeleteAsync(
				baseUrl + "/deletesong",
				JsonConvert.SerializeObject(songToDelete),
				defaultHeaders);

			if (result != null)
			{
				return JsonConvert.DeserializeObject<Song>(result);
			}

			return null;
		}
		
	
	}
}
