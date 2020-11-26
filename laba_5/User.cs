using System;
using System.Collections.Generic;

namespace laba_5
{
	public class Name
	{
		public string first { get; set; }
		public string last { get; set; }
	}

	public class Location
	{
		public string city { get; set; }
	}

	public class Login
	{
		public string username { get; set; }
		public string password { get; set; }
	}

	public class Dob
	{
		public DateTime date { get; set; }
	}

	public class Result
	{
		public string gender { get; set; }
		public Name name { get; set; }
		public Location location { get; set; }
		public Login login { get; set; }
		public Dob dob { get; set; }
	}

	public class Root
	{
		public List<Result> results { get; set; }
	}
}
