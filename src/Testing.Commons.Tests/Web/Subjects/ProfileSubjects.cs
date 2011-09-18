using System;
using System.ComponentModel;
using System.Web.Profile;

namespace Testing.Commons.Tests.Web.Subjects
{
	public class ProfileSubject
	{
		private readonly ProfileBase _profile;

		public ProfileSubject(string profileName) : this(ProfileBase.Create(profileName)) { }

		public ProfileSubject(ProfileBase profile)
		{
			_profile = profile;
		}

		internal static readonly string A_STRING = "AString";
		public string AString
		{
			get { return GetProperty<string>(A_STRING); }
			set { SetProperty(A_STRING, value); }
		}

		internal static readonly string A_NULLABLE_DATE = "ANullableDate";

		public DateTime? ANullableDate
		{
			get
			{
				return GetProperty<DateTime?>(A_NULLABLE_DATE);
			}
			set
			{
				SetProperty(A_NULLABLE_DATE, value);
			}
		}

		internal static readonly string AN_ENUM = "AnEnum";
		public ACustomEnum AnEnum
		{
			get
			{
				return convert(GetProperty<byte>(AN_ENUM));
			}
			set
			{
				SetProperty(AN_ENUM, convert(value));
			}
		}

		#region enum conversion methods

		private static ACustomEnum convert(byte @enum)
		{
			if (!Enum.IsDefined(typeof(ACustomEnum), @enum)) throw new InvalidEnumArgumentException("enum", @enum, typeof(ACustomEnum));
			return (ACustomEnum)Enum.ToObject(typeof(ACustomEnum), @enum);
		}

		private static byte convert(ACustomEnum @enum)
		{
			if (!Enum.IsDefined(typeof(ACustomEnum), @enum)) throw new InvalidEnumArgumentException("enum", (int)@enum, typeof(ACustomEnum));
			return (byte)@enum;
		}

		#endregion

		internal static readonly string AN_INTERVAL = "AnInterval";
		public TimeSpan AnInterval
		{
			get
			{
				return GetProperty<TimeSpan>(AN_INTERVAL);
			}
			set
			{
				SetProperty(AN_INTERVAL, value);
			}
		}

		internal static readonly string ID = "Id";
		public Guid Id
		{
			get { return GetProperty<Guid>(ID); }
		}

		// ... more properties...

		public virtual bool IsDirty { get { return _profile.IsDirty; } }

		public virtual void Save()
		{
			_profile.Save();
		}

		internal virtual T GetProperty<T>(string propertyName)
		{
			return (T)_profile.GetPropertyValue(propertyName);
		}

		internal virtual void SetProperty<T>(string propertyName, T value)
		{
			_profile.SetPropertyValue(propertyName, value);
		}
	}
}
