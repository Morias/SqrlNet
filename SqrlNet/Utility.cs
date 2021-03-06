using System;

namespace SqrlNet
{
	/// <summary>
	/// A static class with useful utility methods that are useful to both client
	/// and server applications.  Please be careful not to bloat this class.
	/// </summary>
	internal static class Utility
	{
		#region ISqrlParser implementation

		/// <summary>
		/// Gets the URL without protocol.
		/// </summary>
		/// <returns>
		/// The URL without protocol.
		/// </returns>
		/// <param name='url'>
		/// The URL.
		/// </param>
		public static string GetUrlWithoutProtocol(string url)
		{
			// only use this variable for validity checking, never for any cryptographic features because ToLower() will modify nonces
			var lowerUrl = url.ToLower();

			if(lowerUrl.StartsWith("sqrl://"))
			{
				return url.Substring(7);
			}

			if(lowerUrl.StartsWith("qrl://"))
			{
				return url.Substring(6);
			}

			throw new Exception("SQRL urls must begin with 'sqrl://' or 'qrl://'");
		}

		/// <summary>
		/// Gets the domain from the URL.
		/// </summary>
		/// <returns>
		/// The domain from the URL.
		/// </returns>
		/// <param name='url'>
		/// The URL.
		/// </param>
		public static string GetDomainFromUrl(string url)
		{
			// strip off scheme
			var domain = GetUrlWithoutProtocol(url);

			var pipeIndex = domain.IndexOf('|');

			if(pipeIndex >= 0)
			{
				return domain.Substring(0, pipeIndex);
			}

			var slashIndex = domain.IndexOf('/');

			if(slashIndex < 0)
			{
				throw new Exception("SQRL urls must contain a '/'");
			}

			return domain.Substring(0, slashIndex);
		}

		#endregion
	}
}