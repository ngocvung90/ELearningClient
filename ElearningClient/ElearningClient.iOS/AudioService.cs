using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Foundation;
using AVFoundation;
using Xamarin.Forms;
using ElearningClient.Interface;
using ElearningClient.iOS;

[assembly: Dependency(typeof(AudioService))]

namespace ElearningClient.iOS
{
    public class AudioService : IAudio
    {
        AVAudioPlayer _player = null;
        public AudioService()
        {
        }

        public void PlayAudioFile(string fileName)
        {
            string sFilePath = NSBundle.MainBundle.PathForResource
            (Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
            var url = NSUrl.FromString(sFilePath);

            if (_player == null) _player = AVAudioPlayer.FromUrl(url);

            _player.FinishedPlaying += (object sender, AVStatusEventArgs e) => {
                _player = null;
            };
            _player.Play();
        }
        public void Pause()
        {
            _player.Pause();
        }
        public void Resume()
        {
            _player.Play();
        }
        public void Stop()
        {
            _player.Stop();
        }
    }
}
