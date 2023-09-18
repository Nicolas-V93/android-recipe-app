using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Imi.Project.Mobile.Helpers
{
    public class UserSecretsHelper
    {
        private static UserSecretsHelper _instance;
        private JObject _secrets;

        private const string Namespace = "Imi.Project.Mobile";
        private const string FileName = "secrets.json";

        private UserSecretsHelper()
        {
            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(UserSecretsHelper)).Assembly;
                var stream = assembly.GetManifestResourceStream($"{Namespace}.{FileName}");
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    _secrets = JObject.Parse(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to load secrets file");
            }
        }

        public static UserSecretsHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserSecretsHelper();
                }

                return _instance;
            }
        }

        public string this[string name]
        {
            get
            {
                try
                {
                    var path = name.Split(':');

                    JToken node = _secrets[path[0]];
                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Unable to retrieve secret '{name}'");
                    return string.Empty;
                }
            }
        }
    }
}
