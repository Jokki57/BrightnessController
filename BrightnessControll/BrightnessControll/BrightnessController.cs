using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace BrightnessControll
{
	public static class BrightnessController
	{
		[DllImport("GDI32.dll")]
		private unsafe static extern Boolean SetDeviceGammaRamp(Int32 hdc, void* ramp);

		private static Boolean initialized = false;
		private static Int32 hdc;

		private static void Initialize()
		{
			if (initialized)
				return;
			hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc().ToInt32();
			initialized = true;
		}

		public static unsafe bool SetBrightness(short brightness)
		{
			Initialize();
			if (brightness > 255)
				brightness = 255;
			else if (brightness < 0)
				brightness = 0;

			short* gArray = stackalloc short[3 * 256];
			short* index = gArray;

			int arrayValue;
			for (int j = 0; j < 3; j++)
			{
				for (int i = 0; i < 256; i++)
				{
					arrayValue = i * (brightness + 128);
					if (arrayValue > 65535)
						arrayValue = 65535;
					*index = (short)arrayValue;
					index++;
				}
			}
			return SetDeviceGammaRamp(hdc, gArray);
		}
	}
}
