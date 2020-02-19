﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fourplaces.DTOs
{
	public class RegisterRequest
	{
		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("first_name")]
		public string FirstName { get; set; }

		[JsonProperty("last_name")]
		public string LastName { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }
	}
}
