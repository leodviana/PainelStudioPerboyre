using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PainelStudioPerboyre.Droid.Platform;
using PainelStudioPerboyre.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileStore))]
namespace PainelStudioPerboyre.Droid.Platform
{
    public class FileStore : IFileStore
    {
        public string GetFilePath()
        {
            return Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads, "ImageName.jpg");
           // return Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "image.png");
        }
        public string GetFilePath(string file)
        {
            return Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads, file);
            // return Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "image.png");
        }
    }
}