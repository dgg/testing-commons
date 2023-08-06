using System.Threading.Tasks;

namespace Testing.Commons.Async
{
	/// <summary>
	/// Creates stubbed values for apis that return asynchronous results in the shape of <see cref="Task{TResult}"/>.
	/// </summary>
	public class PseudoTask
	{
		/// <summary>
		/// Builds an asynchronous result with little overhead.
		/// </summary>
		/// <typeparam name="T">The type of the result produced by the returned <see cref="Task{TResult}"/>. </typeparam>
		/// <param name="result">The result value to bind to the returned task.</param>
		/// <returns>A <see cref="TaskStatus.RanToCompletion"/> transitioned task.</returns>
		public static Task<T> Create<T>(T result)
		{
			var tcs = new TaskCompletionSource<T>();
			tcs.SetResult(result);
			return tcs.Task;
		}
	}
}
