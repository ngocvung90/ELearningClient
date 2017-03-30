using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElearningClient.Interface
{
    public interface IAudio
    {
        void PlayAudioFile(string fileName);
        void Pause();
        void Resume();
        void Stop();
    }
}
