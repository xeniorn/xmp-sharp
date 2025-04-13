using System.Runtime.InteropServices;

namespace SE.Halligang.CsXmpToolkit.PInvoke
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct XmpDateTime
	{
		public int Year;
		public int Month;
		public int Day;
		public int Hour;
		public int Minute;
		public int Second;
        [MarshalAs(UnmanagedType.U1)] public bool HasDate;
        [MarshalAs(UnmanagedType.U1)] public bool HasTime;
        [MarshalAs(UnmanagedType.U1)] public bool HasTimeZone;
        [MarshalAs(UnmanagedType.I1)] public TimeZoneSign TZSign;
		public int TZHour;
		public int TZMinute;
		public int NanoSecond;
	}
}
