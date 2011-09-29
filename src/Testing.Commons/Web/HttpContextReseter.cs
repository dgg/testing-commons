using System;
using System.Web;

namespace Testing.Commons.Web
{
	/// <summary>
	/// Allows setting the <see cref="HttpContext.Current"/> property to a custom built instance during the scope if the instance.
	/// </summary>
	public class HttpContextReseter : IDisposable
	{
		/// <summary>
		/// Sets the instance built by the <paramref name="factory"/>
		/// </summary>
		/// <param name="factory">Builder of the <see cref="HttpContext"/> instance.</param>
		/// <returns>A scope object that resets the <see cref="HttpContext.Current"/> after is disposed.</returns>
		public static HttpContextReseter Set(HttpContextFactory factory)
		{
			return new HttpContextReseter(factory.Context);
		}
		
		private HttpContextReseter(HttpContext context)
		{
			HttpContext.Current = context;
		}

		/// <summary>
		/// Resets <see cref="HttpContext.Current"/> to <c>null</c>.
		/// </summary>
		~HttpContextReseter()
		{
			Dispose(false);
		}

		#region IDisposable Members

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		///<remarks>Resets <see cref="HttpContext.Current"/> to <c>null</c>.</remarks>
		public void Dispose()
		{
			Dispose(true);
		}

		#endregion

		/// <summary>
		/// Resets <see cref="HttpContext.Current"/> to <c>null</c>.
		/// </summary>
		/// <param name="disposing"><c>true</c> if disposed via the <see cref="IDisposable.Dispose"/> method, <c>false</c> otherwise.</param>
		protected virtual void Dispose(bool disposing)
		{
			// either way (from finalizer or IDisposable.Dispose()) the context needs to be reseted
			HttpContext.Current = null;
		}
	}
}