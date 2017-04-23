using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ElearningClient.Droid;
using Xamarin.Forms;
using ElearningClient.Interface;
using Android.Media;

[assembly: Dependency(typeof(AudioService))]
namespace ElearningClient.Droid
{
    public class AudioService : IAudio
    {
        MediaPlayer player = new MediaPlayer();
        public AudioService()
        {
        }

        public void PlayAudioFile(string fileName)
        {
            if (player == null)
            {
                player = new MediaPlayer();
            }
            player.Reset();
            player.Prepared += (s, e) =>
            {
                player.Start();
            };
            player.SetDataSource(fileName);
            player.Prepare();
        }
        public void Pause()
        {
            player.Pause();
        }
        public void Resume()
        {
            player.Start();
        }
        public void Stop()
        {
            player.Stop();
        }
    }
}