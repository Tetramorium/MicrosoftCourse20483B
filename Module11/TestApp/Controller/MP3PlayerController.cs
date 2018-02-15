using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.Word;

namespace TestApp.Controller
{
    public class MP3PlayerController : IDisposable
    {
        public bool Repeat { get; set; }
        public bool IsPlaying { get; set; }

        private bool _isDisposed;

        public MP3PlayerController(string fileName)
        {
            const string FORMAT = @"open ""{0}"" type mpegvideo alias MediaFile";
            string command = string.Format(FORMAT, fileName);
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        public void Play()
        {
            string command = "play MediaFile";
            if (Repeat) command += " REPEAT";
            mciSendString(command, null, 0, IntPtr.Zero);

            IsPlaying = true;
        }

        public void Stop()
        {
            string command = "stop MediaFile";
            mciSendString(command, null, 0, IntPtr.Zero);

            IsPlaying = false;
        }

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand,
       StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        public void Dispose()
        {
            if (!this._isDisposed)
            {
                string command = "close MediaFile";
                mciSendString(command, null, 0, IntPtr.Zero);
                this._isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
