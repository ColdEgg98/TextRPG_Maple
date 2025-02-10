using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Maple._04._Manager.Sound
{
    // 나중에 모든 사운드 파일에 대한 WaveOutEvent를 미리 할당해 놓고, AudioFileReader만 매번 새로 할당해서 사용하는 방식으로 수정
    // 지금은 매번 모든 사운드 파일도 할당받아서 사용하는 중.
    internal class SoundChannel
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private string currentFile;
        private bool isLooping;
        private Thread loopThread;

        public void Play(string filePath, bool loop = false)
        {
            Stop(); // 기존 사운드 정지
            isLooping = loop;
            currentFile = filePath;

            outputDevice = new WaveOutEvent();
            audioFile = new AudioFileReader(filePath);
            outputDevice.Init(audioFile);
            outputDevice.Play();

            if (isLooping)
            {
                loopThread = new Thread(LoopSound);
                loopThread.Start();
            }
        }

        private void LoopSound()
        {
            while (isLooping)
            {
                Thread.Sleep(100); // CPU 점유율을 낮추기 위해 약간의 대기
                if (outputDevice.PlaybackState == PlaybackState.Stopped)
                {
                    audioFile.Position = 0; // 처음부터 다시 재생
                    outputDevice.Play();
                }
            }
        }

        public void Stop()
        {
            isLooping = false;
            loopThread?.Join(); // 루프 스레드 종료 대기
            outputDevice?.Stop();
            outputDevice?.Dispose();
            audioFile?.Dispose();
        }

        public void SetVolume(float volume)
        {
            // 볼륨 값 범위 체크 후 설정
            if (volume < 0.0f)
                volume = 0.0f;
            else if (volume > 1.0f)
                volume = 1.0f;

            outputDevice.Volume = volume;  // 볼륨 설정
        }
    }
}
