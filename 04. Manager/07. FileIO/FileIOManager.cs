using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TextRPG_Maple._04._Manager._04._Log;
using TextRPG_Maple._04._Manager._06._DB;

namespace TextRPG_Maple._04._Manager._07._FileIO
{
    internal class FileIOManager
    {
        // 싱글턴 화
        private static FileIOManager? _instance;
        public static FileIOManager Instance => _instance ??= new FileIOManager();

        // 디렉토리 경로
        private static string SaveDirectory;

        static FileIOManager()
        {
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            SaveDirectory = Path.Combine(projectRoot, "..", "..", "..", "Resources", "Saves");

            // 저장 폴더의 유뮤 확인
            if (!Directory.Exists(SaveDirectory))
            {
                // 저장 폴더가 없으면 생성
                Directory.CreateDirectory(SaveDirectory);
            }
        }

        // 데이터를 JSON 형식으로 저장
        public static void SaveJson<T>(T data, string fileName)
        {
            try
            {
                string filePath = Path.Combine(SaveDirectory, fileName + ".json");
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);

                LogManager.Instance.Log(LogLevel.ERROR, $"데이터 저장 완료: {filePath}");
            }
            catch (Exception ex)
            {
                LogManager.Instance.Log(LogLevel.ERROR, $"저장 오류: {ex.Message}");
            }
        }

        // JSON 형식 데이터를 불러오기
        public static T? LoadJson<T>(string fileName) where T : class
        {
            try
            {
                string filePath = Path.Combine(SaveDirectory, fileName + ".json");

                if (!File.Exists(filePath))
                {
                    LogManager.Instance.Log(LogLevel.ERROR, $"파일이 존재하지 않음: {filePath}");
                    return null;  // null 반환
                }

                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Log(LogLevel.ERROR, $"불러오기 오류: {ex.Message}");
                return null;
            }
        }

        // 파일 존재 여부 확인 함수 추가
        public static bool JsonFileExists(string fileName)
        {
            string filePath = Path.Combine(SaveDirectory, fileName + ".json");
            return File.Exists(filePath);
        }
    }
}
