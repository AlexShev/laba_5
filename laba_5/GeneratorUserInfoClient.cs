using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace laba_5
{
	internal class GeneratorUserInfoClient : IDisposable
	{
		private readonly HttpClient _httpClient;

		public GeneratorUserInfoClient()
		{
			_httpClient = new HttpClient();
		}

		public void Dispose()
		{
			_httpClient.Dispose();
		}

		private string GeneratePhoneNumber()
		{
			var rand = new Random();
			
			return $"+7({rand.Next(000, 999)})-{rand.Next(000, 999)}-{rand.Next(00, 99)}-{rand.Next(00, 99)}";
		}

		public string[] Generate()
		{
			var responseMessage = _httpClient.GetAsync("https://randomuser.me/api/?inc=gender,name,location,login,dob,nat=gb&noinfo").Result;

			var contentString = responseMessage.Content.ReadAsStringAsync().Result;

			// зато работает)
			var a = JsonSerializer.Deserialize<Root>(contentString).results.First();

			string[] result = new string[7];

			result[0] = a.login.username;

			result[1] = a.login.password;

			result[2] = a.name.last + ' ' + a.name.first;

			result[3] = a.gender;

			result[4] = a.dob.date.ToShortDateString();

			result[5] = a.location.city;

			result[6] = GeneratePhoneNumber();

			Dispose();

			return result;
		}
	}
}
