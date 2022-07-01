using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace POS.Infrastructure.ResourceLibrary
{
    public class ResourceLocalizer
    {
        private static readonly ResourceLocalizer _selfRef = new ResourceLocalizer();
        private string _fileName = AppDomain.CurrentDomain.BaseDirectory;
        private Dictionary<string, string> _resourceStrings = new Dictionary<string, string>();
        private CultureInfo _currentCulture = Thread.CurrentThread.CurrentCulture;


        public ResourceLocalizer()
        {
            this.LoadResources();
        }
        public void LoadResources()
        {
            string resourceFile = string.Empty;
            resourceFile = Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath, "Resources\\Resources.txt");
            if (File.Exists(resourceFile))
            {
                using (var fileStream = new FileStream(resourceFile, FileMode.Open))
                {
                    using (var reader = new StreamReader(fileStream))
                    {
                        while (!reader.EndOfStream)
                        {
                            var resource = reader.ReadLine();
                            if (!string.IsNullOrEmpty(resource))
                            {
                                var keyAndValue = resource.Split(new char[] { '=' });
                                if (keyAndValue != null && keyAndValue.Length > 0)
                                {
                                    if (_resourceStrings.ContainsKey(keyAndValue[0]))
                                    {
                                        _resourceStrings[keyAndValue[0]] = keyAndValue[1].Trim();
                                    }
                                    else
                                    {
                                        _resourceStrings.Add(keyAndValue[0], keyAndValue[1].Trim());
                                    }
                                }
                            }
                        }
                        reader.Close();
                    }
                    fileStream.Close();
                }
            }
            else
            {
                _resourceStrings.Clear();
            }
        }
        public static ResourceLocalizer Current
        {
            get
            {            
                return _selfRef;
            }
        }
        public void SetCulture(CultureInfo culture)
        {
            if (!string.Equals(_currentCulture, culture))
            {
                _currentCulture = culture;
                LoadResources();
            }
        }
        public string GetResource(string keyName)
        {
            if (string.IsNullOrEmpty(keyName))
            {
                return string.Empty;
            }
            var value = string.Empty;
            if (!_resourceStrings.TryGetValue(keyName, out value))
            {
                //Log message that the key does not exit
            }
            return value;
        }
    }
}
