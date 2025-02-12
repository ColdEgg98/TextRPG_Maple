using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._01._GameObject.Monster;
using TextRPG_Maple._04._Manager._04._Log;
using TextRPG_Maple._04._Manager._05._Object;

namespace TextRPG_Maple._04._Manager._06._DB
{
    // 파일로 부터 관련 자료를 읽어오는 매니저
    internal class DBManager
    {
        private static DBManager? instance;
        public static DBManager Instance => instance ??= new DBManager();

        //=======================================================================================================================
        // 오브젝트 리스트를 CSV 파일로 저장, 이때 GameObject를 기준으로 저장함. 만약 다른 타입의 객체라면 다른 방식 사용
        //=======================================================================================================================
        public static void SaveToCSV(string fileName, List<GameObject> objects)
        {
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string fileDirectory = Path.Combine(projectRoot, "..", "..", "..", "Resources", "Csv");

            // 저장 폴더의 유뮤 확인
            if (!Directory.Exists(fileDirectory))
            {
                // 저장 폴더가 없으면 생성
                Directory.CreateDirectory(fileDirectory);
            }

            // Resources의 Csv에 위치한 fileName을 가져옴.
            string filePath = Path.Combine(fileDirectory, fileName);

            using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
            {
                if (objects.Count == 0) return;

                //============================================================================================================================================
                // 1️. 모든 객체의 속성과 Status 속성을 가져오기
                //============================================================================================================================================
                // objects의 첫번째 객체의 Property들을 Stat 정보만 제외하고 가져온다. 이때 상속받은 객체일 경우 하위 객체의 Property까지 들고 온다.
                var gameObjectProps = objects.First().GetType().GetProperties().Where(p => p.Name != "Stat").ToList();

                // Stat class의 Property들을 들고온다.
                var statusProps = typeof(Status).GetProperties().ToList();


                //============================================================================================================================================
                // 2️. CSV 헤더 생성
                //============================================================================================================================================
                // CSV 파일 첫번째 인덱스에 ClassType 인덱스를 만든다. 이후 gameObjectProps 및 statusProps의 정보들을 가져온다.

                writer.WriteLine("ClassType," + string.Join(",", gameObjectProps.Select(p => p.Name)) + "," + string.Join(",", statusProps.Select(p => p.Name)));


                //============================================================================================================================================
                // 3️. 각 객체의 정보를 CSV에 저장
                //============================================================================================================================================
                foreach (var obj in objects)
                {
                    List<string> values = new List<string>
                        {
                            obj.GetType().Name  // 첫 번째 컬럼: 객체 타입
                        };

                    // values에 속성을 추가하되 각 속성의 이름을 문자열로 변환한다. 속성이 없다면 빈 문자열("")을 values에 추가한다.

                    // GameObject 속성 저장
                    values.AddRange(gameObjectProps.Select(p => p.GetValue(obj)?.ToString() ?? ""));

                    // Status 속성 저장
                    values.AddRange(statusProps.Select(p => p.GetValue(obj.Stat)?.ToString() ?? ""));

                    // values 리스트의 항목들을 쉼표로 구분하여 하나의 문자열로 결합하고, 이를 WriteLine을 통해 파일에 한 줄로 저장한다.
                    writer.WriteLine(string.Join(",", values));
                }
            }
        }

        public static List<GameObject> LoadFromCSV(string fileName)
        {
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string fileDirectory = Path.Combine(projectRoot, "..", "..", "..", "Resources", "Csv");

            // 저장 폴더의 유뮤 확인
            if (!Directory.Exists(fileDirectory))
            {
                // 저장 폴더가 없으면 파일이 없는 것.
                return null;
            }

            // Resources의 Csv에 위치한 fileName을 가져옴.
            string filePath = Path.Combine(fileDirectory, fileName);

            List <GameObject> objects = new List<GameObject>();

            using (var reader = new StreamReader(filePath))
            {
                string headerLine = reader.ReadLine();
                if (headerLine == null) return objects;

                string[] headers = headerLine.Split(',');

                while (!reader.EndOfStream)
                {
                    string[] values = reader.ReadLine().Split(',');

                    string classType = values[0]; // 첫 번째 값은 클래스 이름
                    Dictionary<string, string> propertyDict = new Dictionary<string, string>();

                    for (int i = 1; i < headers.Length; i++)
                    {
                        propertyDict[headers[i]] = values[i];
                    }

                    GameObject obj = CreateGameObject(classType, propertyDict);
                    if (obj != null)
                    {
                        objects.Add(obj);
                    }
                }
            }

            return objects;
        }

        private static GameObject CreateGameObject(string classType, Dictionary<string, string> properties)
        {
            // 클래스 타입을 가져와 객체 생성
            // 현재 실행 중인 어셈블리에서 classType을 검색
            Type? type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == classType);

            if (type == null) 
                return null;

            // 기본 생성자가 필요함. 
            GameObject? Object = Activator.CreateInstance(type) as GameObject;

            foreach (var prop in properties)
            {
                PropertyInfo propertyInfo = type.GetProperty(prop.Key);
                if (propertyInfo != null && prop.Key != "Stat")
                {
                    object value = Convert.ChangeType(prop.Value, propertyInfo.PropertyType);
                    propertyInfo.SetValue(Object, value);
                }
            }

            // Status 객체 생성 및 할당
            Status status = new Status();
            Type statusType = typeof(Status);

            foreach (var prop in properties)
            {
                PropertyInfo propertyInfo = statusType.GetProperty(prop.Key);
                if (propertyInfo != null)
                {
                    object value = Convert.ChangeType(prop.Value, propertyInfo.PropertyType);
                    propertyInfo.SetValue(status, value);
                }
            }

            if (Object != null)
                Object.Stat = status;

            return Object;
        }


        //=======================================================================================================================
        // GameObject를 상속받은 객체가 아닌, 일반 객체들을 CSV 파일로 저장
        //=======================================================================================================================
        public static void SaveToCSV<T>(string fileName, List<T> objects)
        {
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string fileDirectory = Path.Combine(projectRoot, "..", "..", "..", "Resources", "Csv");

            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }

            string filePath = Path.Combine(fileDirectory, fileName);

            using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
            {
                if (objects.Count == 0) return;

                var props = typeof(T).GetProperties().ToList();

                writer.WriteLine("DataType," + string.Join(",", props.Select(p => p.Name)));

                foreach (var obj in objects)
                {
                    List<string> values = new List<string>
            {
                obj.GetType().Name
            };

                    values.AddRange(props.Select(p =>
                    {
                        var value = p.GetValue(obj);
                        if (p.PropertyType.IsEnum) // Enum 타입인 경우 int로 변환
                        {
                            return ((int)value).ToString();
                        }
                        return value?.ToString() ?? "";
                    }));

                    writer.WriteLine(string.Join(",", values));
                }
            }
        }

        public static List<T> LoadFromCSV<T>(string fileName) where T : class, new()
        {
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string fileDirectory = Path.Combine(projectRoot, "..", "..", "..", "Resources", "Csv");

            if (!Directory.Exists(fileDirectory))
            {
                return null;
            }

            string filePath = Path.Combine(fileDirectory, fileName);

            List<T> objects = new List<T>();

            using (var reader = new StreamReader(filePath))
            {
                string headerLine = reader.ReadLine();
                if (headerLine == null) return objects;

                string[] headers = headerLine.Split(',');

                while (!reader.EndOfStream)
                {
                    string[] values = reader.ReadLine().Split(',');

                    string DataType = values[0];
                    Dictionary<string, string> propertyDict = new Dictionary<string, string>();

                    for (int i = 1; i < headers.Length; i++)
                    {
                        propertyDict[headers[i]] = values[i];
                    }

                    T obj = CreateObject<T>(DataType, propertyDict);
                    if (obj != null)
                    {
                        objects.Add(obj);
                    }
                }
            }

            return objects;
        }

        private static T CreateObject<T>(string dataType, Dictionary<string, string> properties) where T : class, new()
        {
            Type? type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == dataType);

            if (type == null)
                return null;

            T obj = new T();

            foreach (var prop in properties)
            {
                PropertyInfo propertyInfo = type.GetProperty(prop.Key);
                if (propertyInfo != null)
                {
                    object value;
                    if (propertyInfo.PropertyType.IsEnum) // Enum 타입인 경우
                    {
                        value = Enum.ToObject(propertyInfo.PropertyType, int.Parse(prop.Value)); // int로 변환 후 Enum으로 캐스팅
                    }
                    else
                    {
                        value = Convert.ChangeType(prop.Value, propertyInfo.PropertyType);
                    }
                    propertyInfo.SetValue(obj, value);
                }
            }

            return obj;
        }
    }
}
