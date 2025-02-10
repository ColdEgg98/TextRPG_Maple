using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._04._Manager.Sound;
using System.IO;
using System.Diagnostics;
using TextRPG_Maple._04._Manager._04._Log;
using System.Threading.Channels;

namespace TextRPG_Maple._04._Manager
{
    enum SoundType
    {
        BGM,
        Click,
        END
    }

    internal class SoundManager
    {
        //========================================================================= 
        // 매니저 초기화
        //========================================================================= 
        private static SoundManager? instance;
        public static SoundManager Instance => instance ??= new SoundManager();


        private Dictionary<int, SoundChannel> channels = new Dictionary<int, SoundChannel>();
        private Dictionary<string, string> soundFiles = new Dictionary<string, string>();

        public void LoadSounds(string folderPath = "sound")
        {
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string soundFolderPath = Path.Combine(projectRoot, "..", "..", "..", "Resources", "Sound");

            if (!Directory.Exists(soundFolderPath))
            {
                LogManager.Instance.Log(LogLevel.ERROR, $"폴더 {soundFolderPath}가 존재하지 않습니다.");
                return;
            }

            // 재귀적으로 모든 폴더를 순회(SearchOption.AllDirectories)하면서 사운드 파일 검색
            foreach (var file in Directory.GetFiles(soundFolderPath, "*.*", SearchOption.AllDirectories))
            {
                // 파일 형식이 "*.*" 이므로, 사운드 파일이 아닌 파일까지 검색됨. 그러므로 사운드 파일만 분류
                string extension = Path.GetExtension(file).ToLower();
                if (extension == ".mp3" || extension == ".wav" || extension == ".ogg")
                {
                    string fileNameWithExtension = Path.GetFileNameWithoutExtension(file); // key: "이름"
                    soundFiles[fileNameWithExtension] = file;

                    LogManager.Instance.Log(LogLevel.DEBUG, $"사운드 로드: {fileNameWithExtension}");
                }
            }
        }

        public void PlaySound(SoundType channel, string soundName, bool loop = false)
        {
            if (!soundFiles.ContainsKey(soundName))
            {
                Console.WriteLine($"[오류] {soundName} 파일을 찾을 수 없음!");
                return;
            }

            if (!channels.ContainsKey((int)channel))
                channels[(int)channel] = new SoundChannel();

            channels[(int)channel].Play(soundFiles[soundName], loop);

            LogManager.Instance.Log(LogLevel.DEBUG, $"채널 {channel}: {soundName} 재생 (Loop={loop})");
        }

        public void StopSound(int channel)
        {
            if (channels.ContainsKey(channel))
            {
                channels[channel].Stop();

                LogManager.Instance.Log(LogLevel.DEBUG, $"채널 {channel} 정지");
            }
        }

        public void SetVolume(SoundType channel, float volume)
        {
            if (channels.ContainsKey((int)channel))
            {
                channels[(int)channel].SetVolume(volume);

                LogManager.Instance.Log(LogLevel.DEBUG, $"채널 {channel} 볼륨 \"{volume}\"으로 조정");
            }
        }
    }
}
